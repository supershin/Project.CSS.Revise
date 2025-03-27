using Microsoft.AspNetCore.Mvc;
using Project.CSS.Revise.Web.Models;

namespace Project.CSS.Revise.Web.Controllers
{
    public class CSResponseController : Controller
    {
        public IActionResult Index()
        {
            var users = new List<User>
            {
                new User {
                    ID = "295", QCTypeID = "10", UserID = "kittiya.k", TitleID = "3",
                    FirstName = "กิติยา", FirstName_Eng = "Kitiya", TitleID_Eng = "6",
                    LastName = "กัญยะโสภา", LastName_Eng = "Kanyasopa", Password = "a2l0dGl5YS5r",
                    Email = "kittiya.k@assetwise.co.th", Mobile = "0914149511",
                    DepartmentID = "211", DepartmentName = "Human Resources", RoleID = null, FlagAdmin = false, FlagReadonly = false,
                    FlagActive = true, IsQCFinishPlan = null,
                    CreateDate = DateTime.Parse("2022-08-25 14:12:59.243"), CreateBy = "1",
                    UpdateDate = DateTime.Parse("2025-03-06 10:33:23.120"), UpdateBy = "1"
                },
                new User {
                    ID = "296", QCTypeID = "10", UserID = "namfon_sa", TitleID = "3",
                    FirstName = "น้ำฝน", FirstName_Eng = "Namfon", TitleID_Eng = "6",
                    LastName = "แสงนวล", LastName_Eng = "Sangnuan", Password = "bmFtZm9uX3Nh",
                    Email = "namfon.s@assetwise.co.th", Mobile = "0811100350",
                    DepartmentID = "211", DepartmentName = "Human Resources", RoleID = null, FlagAdmin = false, FlagReadonly = false,
                    FlagActive = true, IsQCFinishPlan = null,
                    CreateDate = DateTime.Parse("2022-08-25 14:17:08.927"), CreateBy = "1",
                    UpdateDate = DateTime.Parse("2022-08-25 14:17:08.927"), UpdateBy = "1"
                },
                new User {
                    ID = "303", QCTypeID = "10", UserID = "tanawat.p", TitleID = "1",
                    FirstName = "ธนวัฒน์", FirstName_Eng = "Tanawat", TitleID_Eng = "5",
                    LastName = "พงศ์ชัย", LastName_Eng = "Pongchai", Password = "dGFuYXdhdC5w",
                    Email = "tanawat.p@assetwise.co.th", Mobile = "0832256789",
                    DepartmentID = "211", DepartmentName = "Human Resources", RoleID = null, FlagAdmin = false, FlagReadonly = false,
                    FlagActive = true, IsQCFinishPlan = null,
                    CreateDate = DateTime.Parse("2023-05-12 09:15:00.000"), CreateBy = "1",
                    UpdateDate = DateTime.Parse("2024-01-20 11:05:25.120"), UpdateBy = "1"
                },
                new User {
                    ID = "304", QCTypeID = "10", UserID = "supansa.w", TitleID = "3",
                    FirstName = "สุพรรษา", FirstName_Eng = "Supansa", TitleID_Eng = "6",
                    LastName = "วิเศษ", LastName_Eng = "Vises", Password = "c3VwYW5zYS53",
                    Email = "supansa.w@assetwise.co.th", Mobile = "0873365412",
                    DepartmentID = "213",DepartmentName = "Finance and Accounting", RoleID = null, FlagAdmin = false, FlagReadonly = false,
                    FlagActive = true, IsQCFinishPlan = null,
                    CreateDate = DateTime.Parse("2023-07-01 14:50:30.000"), CreateBy = "1",
                    UpdateDate = DateTime.Parse("2024-02-15 08:30:45.320"), UpdateBy = "1"
                },
                new User {
                    ID = "305", QCTypeID = "10", UserID = "arunrat.k", TitleID = "3",
                    FirstName = "อรุณรัตน์", FirstName_Eng = "Arunrat", TitleID_Eng = "6",
                    LastName = "กาญจนา", LastName_Eng = "Kanjana", Password = "YXJ1bnJhdC5r",
                    Email = "arunrat.k@assetwise.co.th", Mobile = "0925587741",
                    DepartmentID = "214", DepartmentName = "Information Technology" , RoleID = null, FlagAdmin = false, FlagReadonly = true,
                    FlagActive = true, IsQCFinishPlan = null,
                    CreateDate = DateTime.Parse("2023-09-10 12:25:10.000"), CreateBy = "1",
                    UpdateDate = DateTime.Parse("2024-02-18 09:45:55.500"), UpdateBy = "1"
                },
                new User {
                    ID = "306", QCTypeID = "10", UserID = "wirote.s", TitleID = "1",
                    FirstName = "วิโรจน์", FirstName_Eng = "Wirote", TitleID_Eng = "5",
                    LastName = "ศรีสุข", LastName_Eng = "Srisuk", Password = "d2lyb3RlLnM=",
                    Email = "wirote.s@assetwise.co.th", Mobile = "0896632548",
                    DepartmentID = "215", DepartmentName = "Marketing and Communications" , RoleID = null, FlagAdmin = true, FlagReadonly = false,
                    FlagActive = true, IsQCFinishPlan = null,
                    CreateDate = DateTime.Parse("2023-10-20 08:40:00.000"), CreateBy = "1",
                    UpdateDate = DateTime.Parse("2024-03-12 10:20:30.600"), UpdateBy = "1"
                },
                new User {
                    ID = "307", QCTypeID = "10", UserID = "jirawat.k", TitleID = "1",
                    FirstName = "จิรวัฒน์", FirstName_Eng = "Jirawat", TitleID_Eng = "5",
                    LastName = "แก้วใส", LastName_Eng = "Kaewsai", Password = "amlyYXdhdC5r",
                    Email = "jirawat.k@assetwise.co.th", Mobile = "0814523698",
                    DepartmentID = "216", DepartmentName = "Sales and Business Development" , RoleID = null, FlagAdmin = false, FlagReadonly = true,
                    FlagActive = true, IsQCFinishPlan = null,
                    CreateDate = DateTime.Parse("2023-11-05 10:10:10.000"), CreateBy = "1",
                    UpdateDate = DateTime.Parse("2024-03-15 09:15:20.200"), UpdateBy = "1"
                },
                new User {
                    ID = "308", QCTypeID = "10", UserID = "pattrawadee.p", TitleID = "3",
                    FirstName = "ภัทราวดี", FirstName_Eng = "Pattrawadee", TitleID_Eng = "6",
                    LastName = "พูลสุข", LastName_Eng = "Poolsuk", Password = "cGF0dHJhd2FkZWUucA==",
                    Email = "pattrawadee.p@assetwise.co.th", Mobile = "0867754321",
                    DepartmentID = "217", DepartmentName = "Customer Support" , RoleID = null, FlagAdmin = false, FlagReadonly = false,
                    FlagActive = true, IsQCFinishPlan = null,
                    CreateDate = DateTime.Parse("2023-12-01 14:00:00.000"), CreateBy = "1",
                    UpdateDate = DateTime.Parse("2024-03-18 11:30:40.100"), UpdateBy = "1"
                },
                new User {
                    ID = "309", QCTypeID = "10", UserID = "thitipong.t", TitleID = "1",
                    FirstName = "ฐิติพงศ์", FirstName_Eng = "Thitipong", TitleID_Eng = "5",
                    LastName = "ทองมี", LastName_Eng = "Thongmee", Password = "dGhpdGlwb25nLnQ=",
                    Email = "thitipong.t@assetwise.co.th", Mobile = "0845566778",
                    DepartmentID = "218", DepartmentName = "Operations Management" , RoleID = null, FlagAdmin = true, FlagReadonly = false,
                    FlagActive = true, IsQCFinishPlan = null,
                    CreateDate = DateTime.Parse("2024-01-10 09:35:00.000"), CreateBy = "1",
                    UpdateDate = DateTime.Parse("2024-03-20 15:25:30.150"), UpdateBy = "1"
                },
                new User {
                    ID = "310", QCTypeID = "10", UserID = "kanokwan.r", TitleID = "3",
                    FirstName = "กนกวรรณ", FirstName_Eng = "Kanokwan", TitleID_Eng = "6",
                    LastName = "รุ่งเรือง", LastName_Eng = "Rungrueang", Password = "a2Fub2t3YW4ucg==",
                    Email = "kanokwan.r@assetwise.co.th", Mobile = "0851239874",
                    DepartmentID = "219" , DepartmentName = "Logistics and Supply Chain" , RoleID = null, FlagAdmin = false, FlagReadonly = false,
                    FlagActive = true, IsQCFinishPlan = null,
                    CreateDate = DateTime.Parse("2024-02-05 13:20:20.000"), CreateBy = "1",
                    UpdateDate = DateTime.Parse("2024-03-22 10:45:25.550"), UpdateBy = "1"
                },
                new User {
                    ID = "311", QCTypeID = "10", UserID = "supachai.n", TitleID = "1",
                    FirstName = "ศุภชัย", FirstName_Eng = "Supachai", TitleID_Eng = "5",
                    LastName = "นามวงศ์", LastName_Eng = "Namwong", Password = "c3VwYWNoYWkubg==",
                    Email = "supachai.n@assetwise.co.th", Mobile = "0809988776",
                    DepartmentID = "220", DepartmentName = "Administration" , RoleID = null, FlagAdmin = false, FlagReadonly = true,
                    FlagActive = true, IsQCFinishPlan = null,
                    CreateDate = DateTime.Parse("2024-03-01 11:11:11.000"), CreateBy = "1",
                    UpdateDate = DateTime.Parse("2024-03-25 16:40:50.750"), UpdateBy = "1"
                },
                new User {
                    ID = "312", QCTypeID = "10", UserID = "prapatsorn.s", TitleID = "3",
                    FirstName = "ประภัสสร", FirstName_Eng = "Prapatsorn", TitleID_Eng = "6",
                    LastName = "สุขสม", LastName_Eng = "Suksom", Password = "cHJhcGF0c29ybi5z",
                    Email = "prapatsorn.s@assetwise.co.th", Mobile = "0894455667",
                    DepartmentID = "220", DepartmentName = "Administration" , RoleID = null, FlagAdmin = false, FlagReadonly = false,
                    FlagActive = true, IsQCFinishPlan = null,
                    CreateDate = DateTime.Parse("2024-01-22 10:10:10.000"), CreateBy = "1",
                    UpdateDate = DateTime.Parse("2024-03-23 14:55:35.320"), UpdateBy = "1"
                },
                new User {
                    ID = "313", QCTypeID = "10", UserID = "tanapol.l", TitleID = "1",
                    FirstName = "ธนพล", FirstName_Eng = "Tanapol", TitleID_Eng = "5",
                    LastName = "ลิขิตชัย", LastName_Eng = "Likhitchai", Password = "dGFuYXBvbC5s",
                    Email = "tanapol.l@assetwise.co.th", Mobile = "0812233445",
                    DepartmentID = "222" , DepartmentName = "Research and Development" , RoleID = null, FlagAdmin = false, FlagReadonly = true,
                    FlagActive = true, IsQCFinishPlan = null,
                    CreateDate = DateTime.Parse("2024-02-12 08:45:00.000"), CreateBy = "1",
                    UpdateDate = DateTime.Parse("2024-03-24 09:30:40.210"), UpdateBy = "1"
                },
                new User {
                    ID = "314", QCTypeID = "10", UserID = "orathai.m", TitleID = "3",
                    FirstName = "อรทัย", FirstName_Eng = "Orathai", TitleID_Eng = "6",
                    LastName = "มานะชัย", LastName_Eng = "Manachai", Password = "b3JhdGhhaS5t",
                    Email = "orathai.m@assetwise.co.th", Mobile = "0885566778",
                    DepartmentID = "222", DepartmentName = "Research and Development" ,  RoleID = null, FlagAdmin = true, FlagReadonly = false,
                    FlagActive = true, IsQCFinishPlan = null,
                    CreateDate = DateTime.Parse("2024-03-05 15:05:05.000"), CreateBy = "1",
                    UpdateDate = DateTime.Parse("2024-03-26 10:15:20.450"), UpdateBy = "1"
                },
                new User {
                    ID = "315", QCTypeID = "10", UserID = "waraporn.t", TitleID = "3",
                    FirstName = "วราภรณ์", FirstName_Eng = "Waraporn", TitleID_Eng = "6",
                    LastName = "ทองสุข", LastName_Eng = "Thongsuk", Password = "d2FyYXBvcm4udA==",
                    Email = "waraporn.t@assetwise.co.th", Mobile = "0823344556",
                    DepartmentID = "222", DepartmentName = "Research and Development" ,  RoleID = null, FlagAdmin = false, FlagReadonly = false,
                    FlagActive = true, IsQCFinishPlan = null,
                    CreateDate = DateTime.Parse("2024-03-08 10:20:15.000"), CreateBy = "1",
                    UpdateDate = DateTime.Parse("2024-03-27 09:25:35.150"), UpdateBy = "1"
                },
                new User {
                    ID = "316", QCTypeID = "10", UserID = "sompong.k", TitleID = "1",
                    FirstName = "สมพงษ์", FirstName_Eng = "Sompong", TitleID_Eng = "5",
                    LastName = "ขวัญดี", LastName_Eng = "Kwandee", Password = "c29tcG9uZy5r",
                    Email = "sompong.k@assetwise.co.th", Mobile = "0842233445",
                    DepartmentID = "222", DepartmentName = "Research and Development" ,  RoleID = null, FlagAdmin = true, FlagReadonly = false,
                    FlagActive = true, IsQCFinishPlan = null,
                    CreateDate = DateTime.Parse("2024-03-10 14:40:00.000"), CreateBy = "1",
                    UpdateDate = DateTime.Parse("2024-03-27 11:15:45.520"), UpdateBy = "1"
                },
                new User {
                    ID = "317", QCTypeID = "10", UserID = "jiraporn.p", TitleID = "3",
                    FirstName = "จิราภรณ์", FirstName_Eng = "Jiraporn", TitleID_Eng = "6",
                    LastName = "พรชัย", LastName_Eng = "Pornchai", Password = "amlyYXBvcm4ucA==",
                    Email = "jiraporn.p@assetwise.co.th", Mobile = "0856677889",
                    DepartmentID = "226", DepartmentName = "Quality Assurance" ,  RoleID = null, FlagAdmin = false, FlagReadonly = true,
                    FlagActive = true, IsQCFinishPlan = null,
                    CreateDate = DateTime.Parse("2024-03-12 09:55:30.000"), CreateBy = "1",
                    UpdateDate = DateTime.Parse("2024-03-27 12:40:25.680"), UpdateBy = "1"
                },
                new User {
                    ID = "318", QCTypeID = "10", UserID = "siriporn.n", TitleID = "3",
                    FirstName = "ศิริพร", FirstName_Eng = "Siriporn", TitleID_Eng = "6",
                    LastName = "เนตรนภา", LastName_Eng = "Netnapa", Password = "c2lyaXBvcm4ubiA=",
                    Email = "siriporn.n@assetwise.co.th", Mobile = "0861122334",
                    DepartmentID = "227" , DepartmentName = "Legal and Compliance" ,  RoleID = null, FlagAdmin = false, FlagReadonly = false,
                    FlagActive = true, IsQCFinishPlan = null,
                    CreateDate = DateTime.Parse("2024-03-14 13:30:00.000"), CreateBy = "1",
                    UpdateDate = DateTime.Parse("2024-03-27 14:20:15.100"), UpdateBy = "1"
                },
                new User {
                    ID = "319", QCTypeID = "10", UserID = "chatchai.r", TitleID = "1",
                    FirstName = "ฉัตรชัย", FirstName_Eng = "Chatchai", TitleID_Eng = "5",
                    LastName = "รัตนชัย", LastName_Eng = "Rattanachai", Password = "Y2hhdGNoYWkuciA=",
                    Email = "chatchai.r@assetwise.co.th", Mobile = "0898877665",
                    DepartmentID = "227", DepartmentName = "Legal and Compliance" ,RoleID = null, FlagAdmin = true, FlagReadonly = false,
                    FlagActive = true, IsQCFinishPlan = null,
                    CreateDate = DateTime.Parse("2024-03-16 11:45:20.000"), CreateBy = "1",
                    UpdateDate = DateTime.Parse("2024-03-27 15:10:25.250"), UpdateBy = "1"
                },
                new User {
                    ID = "320", QCTypeID = "10", UserID = "anchalee.w", TitleID = "3",
                    FirstName = "อัญชลี", FirstName_Eng = "Anchalee", TitleID_Eng = "6",
                    LastName = "วรรณดี", LastName_Eng = "Wanadee", Password = "YW5jaGFsZWUudyA=",
                    Email = "anchalee.w@assetwise.co.th", Mobile = "0881122556",
                    DepartmentID = "229", DepartmentName = "Facility Management" , RoleID = null, FlagAdmin = false, FlagReadonly = true,
                    FlagActive = true, IsQCFinishPlan = null,
                    CreateDate = DateTime.Parse("2024-03-18 10:25:50.000"), CreateBy = "1",
                    UpdateDate = DateTime.Parse("2024-03-27 16:05:30.750"), UpdateBy = "1"
                },
                new User {
                    ID = "321", QCTypeID = "10", UserID = "surachai.k", TitleID = "1",
                    FirstName = "สุรชัย", FirstName_Eng = "Surachai", TitleID_Eng = "5",
                    LastName = "คำภีร์", LastName_Eng = "Kamphee", Password = "c3VyYWNoYWkuaw==",
                    Email = "surachai.k@assetwise.co.th", Mobile = "0836677880",
                    DepartmentID = "229", DepartmentName = "Facility Management" , RoleID = null, FlagAdmin = false, FlagReadonly = false,
                    FlagActive = true, IsQCFinishPlan = null,
                    CreateDate = DateTime.Parse("2024-03-20 09:10:15.000"), CreateBy = "1",
                    UpdateDate = DateTime.Parse("2024-03-27 16:55:45.650"), UpdateBy = "1"
                }
            };

            return View(users);
        }
    }
}
