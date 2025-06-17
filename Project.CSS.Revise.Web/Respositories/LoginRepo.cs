using Microsoft.EntityFrameworkCore;
using Project.CSS.Revise.Web.Common;
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
        private readonly DbContext _context;

        public LoginRepo(DbContext context) // inject DbContext จากภายนอก  
        {
            _context = context;
        }

        public UserProfile VerifyLogin(UserProfile model)
        {
            // Fix: Ensure the DbContext has a DbSet<TmUser> property defined for querying TmUsers.  
            var tmUsers = _context.Set<TmUser>();

            // STEP 1: ตรวจว่า username (email หรือ userId) มีหรือไม่  
            var userByUsername = tmUsers.FirstOrDefault(u => (u.Email == model.Username || u.UserId == model.Username) && u.FlagActive == true);

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

            var user = tmUsers
                .Where(u => (u.Email == model.Username || u.UserId == model.Username)
                         && u.Password == encryptedPassword
                         && u.FlagActive == true)
                .Select(u => new UserProfile
                {
                    ID = u.Id,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Email = u.Email,
                    Mobile = u.Mobile,
                    DepartmentID = u.DepartmentId,
                    RoleID = u.RoleId,
                    FlagActive = u.FlagActive,
                    Status = 1,
                    Message = Constants.Message.SUCCESS.LOGIN_SUCCESSFUL
                })
                .FirstOrDefault();

            if (user == null)
            {
                return new UserProfile
                {
                    Status = 0,
                    Message = Constants.Message.ERROR.INVALID_EMAIL_OR_PASSWORD
                };
            }

            return user;
        }
    }
}
