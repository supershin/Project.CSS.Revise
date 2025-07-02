namespace Project.CSS.Revise.Web.Models.Master
{
    public class GetDDLModel
    {
        public string? Act { get; set; }
        public int? ID { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Code { get; set; }
        public string? Type { get; set; }
        public int? ValueInt { get; set; }
        public string? ValueString { get; set; }
        public string? Text { get; set; }
        public bool IsActive { get; set; } = true;
        public bool IsDefault { get; set; } = false;
    }
}
