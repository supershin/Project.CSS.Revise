using Microsoft.EntityFrameworkCore;
using Project.CSS.Revise.Web.Commond;
using Project.CSS.Revise.Web.Data;
using Project.CSS.Revise.Web.Models;

namespace Project.CSS.Revise.Web.Respositories
{
    public interface ILoginRepo
    {
        UserProfile VerifyLogin(UserProfile model);
    }
    public class LoginRepo : ILoginRepo
    {
        private readonly CSSContext _context;

        public LoginRepo(CSSContext context)
        {
            _context = context;
        }

        public UserProfile VerifyLogin(UserProfile model)
        {
             // STEP 1: ตรวจว่า username (email หรือ userId) มีหรือไม่  
             var userByUsername = _context.tm_Users
            .FirstOrDefault(u => (u.Email == model.Username || u.UserID == model.Username) && u.FlagActive == true);

            if (userByUsername == null)
            {
                return new UserProfile
                {
                    Status = 0,
                    Message = Constants.Message.ERROR.USER_NOT_FOUND 
                };
            }

            // STEP 2: ตรวจ password ที่เข้ารหัสแล้ว
            var encryptedPassword = SecurityManager.EnCryptPassword(model.Password);

            var user = (from u in _context.tm_Users
                        join m in _context.tm_Exts on u.DepartmentID equals m.ID into deptJoin
                        from m in deptJoin.DefaultIfEmpty()
                        join tTH in _context.tm_TitleNames on u.TitleID equals tTH.ID into titleThJoin
                        from tTH in titleThJoin.DefaultIfEmpty()
                        join tEN in _context.tm_TitleNames on u.TitleID_Eng equals tEN.ID into titleEnJoin
                        from tEN in titleEnJoin.DefaultIfEmpty()
                        where (u.Email == model.Username || u.UserID == model.Username)
                              && u.Password == encryptedPassword
                              && u.FlagActive == true
                        select new UserProfile
                        {
                            ID = u.ID,
                            TitleEN = tEN.Name,
                            TitleTH = tTH.Name, 
                            FirstNameTH = u.FirstName,
                            LastNameTH = u.LastName,
                            FirstNameEN = u.FirstName_Eng,
                            LastNameEN = u.LastName_Eng,
                            Email = u.Email,
                            Mobile = u.Mobile,
                            DepartmentID = u.DepartmentID,
                            DepartmentName = m.Name, 
                            RoleID = u.RoleID,
                            FlagActive = u.FlagActive,
                            Status = 1,
                            Message = Constants.Message.SUCCESS.LOGIN_SUCCESSFUL
                        }).FirstOrDefault();


            if (user == null)
            {
                return new UserProfile
                {
                    Status = 0,
                    Message = Constants.Message.ERROR.INVALID_USER_OR_PASSWORD
                };
            }

            return user;
        }
    }
}
