namespace Project.CSS.Revise.Web.Library.Utility
{
    public class ConfigurationManager
    {
        public static IConfiguration AppSetting { get; }

        static ConfigurationManager()
        {

            string env = "Development";

            AppSetting = new ConfigurationBuilder()
              .SetBasePath(Directory.GetCurrentDirectory())
              .AddJsonFile("appsettings." + env + ".json")
              .Build();
        }
    }
}
