using Our.Umbraco.LocalizedEditorModels.Web.Configuration;
using System.Configuration;
using System.Web.Http;
using Umbraco.Web.Mvc;
using Umbraco.Web.WebApi;

namespace Our.Umbraco.LocalizedEditorModels.Web.Controllers
{
    [PluginController("LocalizedEditorModels")]
    public class ConfigApiController : UmbracoAuthorizedApiController
    {
        [HttpGet]
        public IConfigurationSettings Get()
        {
            return NameValueCollectionConfigurationSettings.GetConfig(ConfigurationManager.AppSettings);
        }
    }
}
