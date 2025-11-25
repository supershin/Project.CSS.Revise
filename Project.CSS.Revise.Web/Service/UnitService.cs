using Project.CSS.Revise.Web.Respositories;

namespace Project.CSS.Revise.Web.Service
{
    public interface IUnitService
    {

    }
    public class UnitService : IUnitService
    {
        private readonly IUnitRepo _UnitRepo;

        public UnitService(IUnitRepo UnitRepo)
        {
            _UnitRepo = UnitRepo;
        }
    }
}
