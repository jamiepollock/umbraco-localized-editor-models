using Our.Umbraco.LocalizedEditorModels.Models;
using System.Collections.Generic;
using System;

namespace Our.Umbraco.LocalizedEditorModels.Services
{
    internal class EditorModelLocalizationKeyService
    {
        public IEnumerable<LocalizationKey> GetLocalizationKeysForPropertyLabel(string propertyAlias, string contentTypeAlias)
        {
            var areaAlias = "property_labels";

            return GetLocalizationKeys(propertyAlias, contentTypeAlias, areaAlias);
        }

        public IEnumerable<LocalizationKey> GetLocalizationKeysForPropertyDescription(string propertyAlias, string contentTypeAlias)
        {
            var areaAlias = "property_descriptions";

            return GetLocalizationKeys(propertyAlias, contentTypeAlias, areaAlias);
        }

        public IEnumerable<LocalizationKey> GetLocalizationKeysForTabLabel(string tabAlias, string contentTypeAlias)
        {
            var areaAlias = "tab_labels";

            return GetLocalizationKeys(tabAlias, contentTypeAlias, areaAlias);
        }

        private IEnumerable<LocalizationKey> GetLocalizationKeys(string entityAlias, string contentTypeAlias, string areaAlias)
        {
            return new LocalizationKey[] {
                new LocalizationKey(string.Format("{0}_{1}", contentTypeAlias, areaAlias), entityAlias),
                new LocalizationKey(areaAlias, entityAlias)
            };
        }
    }
}
