using Project.CSS.Revise.Web.Data;
using Project.CSS.Revise.Web.Commond;

namespace Project.CSS.Revise.Web.Respositories
{
    public interface IUnitRepo
    {
        public string GetRedirectHousingLoan(string ContractNumber, string UserID, string Password);
    }
    public class UnitRepo : IUnitRepo
    {

        private readonly CSSContext _context;

        public UnitRepo(CSSContext context)
        {
            _context = context;
        }

        private string EnCryptPassword(string password)
        {
            byte[] toEncodeAsBytes = System.Text.ASCIIEncoding.ASCII.GetBytes(password);
            string encryptPassword = System.Convert.ToBase64String(toEncodeAsBytes);
            return encryptPassword;
        }

        public string GetRedirectHousingLoan(string ContractNumber , string UserID , string Password)
        {
            if (!string.IsNullOrEmpty(ContractNumber))
            {
                string Authorize = string.Format("{0}:{1}:{2}", UserID, Password, ContractNumber);
                Authorize = EnCryptPassword(Authorize);
                var RedirectHousingLoan = string.Format("{0}{1}", Constants.ThirdPartyApis.HousingLoanRedirect, Authorize);
                return RedirectHousingLoan;
            }
            return null;
        }

    }
}
