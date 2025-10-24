using Microsoft.Extensions.Options;
using Project.CSS.Revise.Web.Models.Config;

namespace Project.CSS.Revise.Web.Commond
{
    public class SystemConstantCentralize
    {
        private readonly CentralizeApiConfig _config;
        public string CentralizeApiUrl => _config.ApiUrl;
        public string CentralizeAuthorize => _config.Authorize;

        public SystemConstantCentralize(IOptions<CentralizeApiConfig> options)
        {
            _config = options.Value;
        }
    }
}
