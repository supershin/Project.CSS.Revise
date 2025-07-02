using Project.CSS.Revise.Web.Models.Pages.Shop_Event;
using Project.CSS.Revise.Web.Respositories;

namespace Project.CSS.Revise.Web.Service
{
    public interface IShopAndEventService
    {
        public CreateEventsTagsResponse CreateEventsAndTags(CreateEvents_Tags model);
    }
    public class ShopAndEventService : IShopAndEventService
    {
        private readonly IShopAndEventRepo _shopAndEventRepo;
        public ShopAndEventService(IShopAndEventRepo ShopAndEventRepo)
        {
            _shopAndEventRepo = ShopAndEventRepo;
        }
        public CreateEventsTagsResponse CreateEventsAndTags(CreateEvents_Tags model)
        {
            return _shopAndEventRepo.CreateEventsAndTags(model);
        }
    }
}
