namespace Project.CSS.Revise.Web.Models.Pages.AppointmentLimit
{
    public class AppointmentLimitModel
    {
        // ✅ ใช้สำหรับรายงานแบบ Pivot (แนวนอน): หนึ่งแถวต่อ Day
        public class ProjectAppointLimitPivotRow
        {
            public string ProjectID { get; set; } = "";
            public int DayID { get; set; }
            public string DaysName { get; set; } = "";

            // ใช้ TimeID เป็นคอลัมน์แนวนอน (int ทั้งหมด)
            public int T222_0900 { get; set; }
            public int T223_1000 { get; set; }
            public int T224_1100 { get; set; }
            public int T225_1200 { get; set; }
            public int T226_1300 { get; set; }
            public int T227_1400 { get; set; }
            public int T228_1500 { get; set; }
            public int T324_1600 { get; set; }
            public int T325_1700 { get; set; }
        }

        public class ProjectAppointLimitIUD
        {
            public string ProjectID { get; set; } = "";
            public int DayID { get; set; }
            public string DaysName { get; set; } = "";
            public int TimeID { get; set; }
            public string TimesName { get; set; } = "";
            public int UnitLimitValue { get; set; }
            public bool FlagActive { get; set; }
            public int UserID { get; set; }

            public bool Issuccess { get; set; } = false;
            public string Message { get; set; } = "";
        }

    }
}
