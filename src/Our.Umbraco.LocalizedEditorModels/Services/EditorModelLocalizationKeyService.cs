using Our.Umbraco.LocalizedEditorModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Our.Umbraco.LocalizedEditorModels.Services
{
    internal class EditorModelLocalizationKeyService
    {
        public IEnumerable<LocalizationKey> GetLocalizationKeysForPropertyLabel(string propertyAlias, string contentTypeAlias)
        {
            var areaAlias = "property_labels";

            return new LocalizationKey[] {
                new LocalizationKey(string.Format("{0}_{1}", contentTypeAlias, areaAlias), propertyAlias),
                new LocalizationKey(areaAlias, propertyAlias)
            };
        }
    }
}
