using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.CSS.Revise.Web.Models.Master;
using Project.CSS.Revise.Web.Models.Pages.UserBank;
using Project.CSS.Revise.Web.Service;

namespace Project.CSS.Revise.Web.Controllers
{
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    public class UserBankController : BaseController
    {
        private readonly IMasterService _masterService;
        private readonly IUserBankService _userBankService;
        public UserBankController(IHttpContextAccessor httpContextAccessor, IMasterService masterService, IUserBankService userBankService) : base(httpContextAccessor)
        {
            _masterService = masterService;
            _userBankService = userBankService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public JsonResult Page_Load()
        {

            var filter = new GetDDLModel
            {
                Act = "listAllBank"
            };
            var DatalistBank = _masterService.GetlisDDl(filter);
            var DatalistCountUserByBankk = _userBankService.GetListCountUserByBank();
            var DatalistUserBank = _userBankService.GetListUserBank(new GetlistUserBank.FilterData { L_BankIDs = "", L_Name = "" });

            return Json(new { success = true, listBank = DatalistBank, listCountUserByBankk = DatalistCountUserByBankk, listUserBank  = DatalistUserBank });
        }

        [HttpGet]
        public async Task<IActionResult> GetUserBankById(int id)
        {
            var data = await _userBankService.GetUserBankByIdAsync(id);
            return Json(new { success = data != null, data });
        }

        public JsonResult GetListProject()
        {
            var filter = new GetDDLModel
            {
                Act = "listDDlAllProject"
            };
            var result = _masterService.GetlisDDl(filter);
            return Json(result);
        }

        public JsonResult GetListBank()
        {
            var filter = new GetDDLModel
            {
                Act = "listAllBank"
            };
            var result = _masterService.GetlisDDl(filter);
            return Json(result);
        }

        [HttpGet]
        public JsonResult GetlistUserBankInTeam(int ParentBankID)
        {
            var filter = new GetlistUserBankInTeam
            {
                ParentBankID = ParentBankID
            };
            var data = _userBankService.GetlistUserBankInTeam(filter);
            return Json(new { success = data != null, data });
        }

        public JsonResult GetlistUserBankInTeamForAdd(int BankID , int LeadteamID)
        {
            var filter = new GetDDLModel
            {
                 Act = "listUserBankInTeamForAdd"
                ,ID = BankID
                ,ID2 = LeadteamID
            };
            var result = _masterService.GetlisDDl(filter);
            return Json(result);
        }

        [HttpPost]
        public async Task<IActionResult> InsertUserBank([FromBody] UserBankEditModel model)
        {
            if (model == null) return BadRequest("No payload");

            // บังคับค่าคงที่ฝั่งเซิร์ฟเวอร์
            model.UserTypeID = 74; // หรือ Constants.Ext.UserBank
            model.FlagActive = model.FlagActive ?? true;
            model.CreateDate = DateTime.Now;
            model.CreateBy = User?.Identity?.Name ?? "system";  // ปรับตามระบบ login ของพ่อใหญ่

            var newId = await _userBankService.InsertUserBankAsync(model);
            return Json(new { success = newId > 0, id = newId });
        }

    }
}
