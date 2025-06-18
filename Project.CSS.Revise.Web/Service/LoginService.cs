using Project.CSS.Revise.Web.Models;
using Project.CSS.Revise.Web.Respositories;

namespace Project.CSS.Revise.Web.Service
{
    public interface ILoginService
    {
        UserProfile VerifyLogin(UserProfile model);
    }
    public class LoginService : ILoginService
    {
        private readonly ILoginRepo _loginRepo;

        public LoginService(ILoginRepo loginRepo)
        {
            _loginRepo = loginRepo;
        }

        public UserProfile VerifyLogin(UserProfile model)
        {
            UserProfile resp = _loginRepo.VerifyLogin(model);
            return resp;
        }
    }
}
