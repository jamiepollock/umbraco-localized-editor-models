using System.Globalization;
using Our.Umbraco.LocalizedEditorModels.Models;
using Umbraco.Core.Services;

namespace Our.Umbraco.LocalizedEditorModels.Services
{
    internal class EditorModelLocalizationService
    {
        private readonly ILocalizedTextService _localizedTextService;
        private readonly CultureInfo _backofficeUserCultureInfo;
        public EditorModelLocalizationService(ILocalizedTextService localizedTextService, CultureInfo backofficeUserCultureInfo)
        {
            _localizedTextService = localizedTextService;
            _backofficeUserCultureInfo = backofficeUserCultureInfo;
        }

        internal string LocalizeKey(LocalizationKey localizationKey)
        {
            return _localizedTextService.Localize(localizationKey.ToUmbracoLocalizationKey(), _backofficeUserCultureInfo);
        }
    }
}
