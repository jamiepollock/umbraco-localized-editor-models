using Our.Umbraco.LocalizedEditorModels.Models;
using System.Collections.Generic;
using System.Globalization;
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

        internal string LocalizeFirstAvailableKey(IEnumerable<LocalizationKey> localizationKeys)
        {
            foreach (var key in localizationKeys)
            {
                var defaultLocalizedValue = string.Format("[{0}]", key.KeyAlias);
                var localizedValue = LocalizeKey(key);

                if (string.IsNullOrWhiteSpace(localizedValue) == false && string.Equals(localizedValue, defaultLocalizedValue) == false)
                {
                    return localizedValue;
                }
            }

            return default(string);
        }
    }
}
