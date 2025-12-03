namespace Project.CSS.Revise.Web.Models.Pages.QueueBankCounterView
{
    public class UpdateUnitRegisterModel
    {
        public class Entity
        {
            public string ProjectID { get; set; } = string.Empty;
            public string UnitID { get; set; } = string.Empty;
            public int Counter { get; set; } = 0;
        }

        public class Message
        {
            public bool Issucces { get; set; } = false;
            public string TextResult { get; set; } = string.Empty;
        }
    }
}
