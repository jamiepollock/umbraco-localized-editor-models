using Our.Umbraco.LocalizedEditorModels.Models;
using System.Collections.Generic;

namespace Our.Umbraco.LocalizedEditorModels.Services
{
    internal class LocalizationKeyService
    {
        const string _propertyLabelsAreaAlias = "property_labels";
        const string _propertyDescriptionsAreaAlias = "property_descriptions";
        const string _tabLabelsAreaAlias = "tab_labels";

        public IEnumerable<LocalizationKey> GetLocalizationKeysForPropertyLabel(string propertyAlias, string contentTypeAlias)
        {
            return GetLocalizationKeys(propertyAlias, contentTypeAlias, _propertyLabelsAreaAlias);
        }

        public IEnumerable<LocalizationKey> GetLocalizationKeysForPropertyDescription(string propertyAlias, string contentTypeAlias)
        {
            return GetLocalizationKeys(propertyAlias, contentTypeAlias, _propertyDescriptionsAreaAlias);
        }

        public IEnumerable<LocalizationKey> GetLocalizationKeysForTabLabel(string tabAlias, string contentTypeAlias)
        {
            return GetLocalizationKeys(tabAlias, contentTypeAlias, _tabLabelsAreaAlias);
        }

        private IEnumerable<LocalizationKey> GetLocalizationKeys(string entityAlias, string contentTypeAlias, string areaAlias)
        {
            var contentTypeAliasSpecificAreaAlias = string.Format("{0}_{1}", contentTypeAlias, areaAlias);

            return new LocalizationKey[] {
                new LocalizationKey(contentTypeAliasSpecificAreaAlias, entityAlias),
                new LocalizationKey(areaAlias, entityAlias)
            };
        }
    }
}
