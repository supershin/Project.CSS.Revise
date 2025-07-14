using Project.CSS.Revise.Web.Models.Master;
using Project.CSS.Revise.Web.Models.Pages.Shop_Event;
using Project.CSS.Revise.Web.Respositories;

namespace Project.CSS.Revise.Web.Service
{
    public interface IShopAndEventService
    {
        public CreateEventsTagsResponse CreateEventsAndTags(CreateEvents_Tags model);
        public CreateEventsShopsResponse CreateEventsAndShops(CreateEvent_Shops model);
        public GetDataCreateEvent_Shops GetDataCreateEventsAndShops(GetDataCreateEvent_Shops filter);
        public List<GetListShopAndEventCalendar.ListData> GetListShopAndEventSCalendar(GetListShopAndEventCalendar.FilterData filter);
        public GetDataEditEvents.EditEventInProjectModel GetDataEditEventInProject(GetDataEditEvents.GetEditEventInProjectFilterModel filter);
        public List<GetDataEditEvents.ListEditShopsModel> GetDataTrEventsAndShopsinProject(int enventID, string projectID, string eventDate);
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
        public CreateEventsShopsResponse CreateEventsAndShops(CreateEvent_Shops model)
        {
            return _shopAndEventRepo.CreateEventsAndShops(model);
        }

        public GetDataCreateEvent_Shops GetDataCreateEventsAndShops(GetDataCreateEvent_Shops filter)
        {
            return _shopAndEventRepo.GetDataCreateEventsAndShops(filter);
        }

        public List<GetListShopAndEventCalendar.ListData> GetListShopAndEventSCalendar(GetListShopAndEventCalendar.FilterData filter)
        {
            List<GetListShopAndEventCalendar.ListData> resp = _shopAndEventRepo.GetListShopAndEventSCalendar(filter);
            return resp;
        }

        public GetDataEditEvents.EditEventInProjectModel GetDataEditEventInProject(GetDataEditEvents.GetEditEventInProjectFilterModel filter)
        {
            return _shopAndEventRepo.GetDataEditEventInProject(filter);
        }

        public List<GetDataEditEvents.ListEditShopsModel> GetDataTrEventsAndShopsinProject(int enventID, string projectID, string eventDate)
        {
            List<GetDataEditEvents.ListEditShopsModel> resp = _shopAndEventRepo.GetDataTrEventsAndShopsinProject(enventID, projectID, eventDate);
            return resp;
        }
    }
}
