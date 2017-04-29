using Umbraco.Core.Models;
using Umbraco.Web.Models.ContentEditing;

namespace Our.Umbraco.LocalizedEditorModels.Services
{
    internal class EditorModelLocalizationService
    {
        private readonly LocalizationKeyService _keyService;
        private readonly LocalizationKeyTextService _keyTextService;
        private readonly IPropertyDescriptionFormatService _propertyDescriptionFormatService;

        public EditorModelLocalizationService(LocalizationKeyService keyService, LocalizationKeyTextService keyTextService) : this(keyService, keyTextService, new DefaultPropertyDescriptionFormatService())
        {
        }

        public EditorModelLocalizationService(LocalizationKeyService keyService, LocalizationKeyTextService keyTextService, IPropertyDescriptionFormatService propertyDescriptionFormatService)
        {
            _keyService = keyService;
            _keyTextService = keyTextService;
            _propertyDescriptionFormatService = propertyDescriptionFormatService;
        }

        internal void LocalizeModel<T>(ListViewAwareContentItemDisplayBase<ContentPropertyDisplay, T> model) where T : IContentBase
        {
            foreach (var property in model.Properties)
            {
                if (property.HideLabel == false)
                {
                    var labelKeys = _keyService.GetLocalizationKeysForPropertyLabel(property.Alias, model.ContentTypeAlias);
                    property.Label = _keyTextService.LocalizeFirstAvailableKey(labelKeys, property.Label);

                    var labelDescriptionKeys = _keyService.GetLocalizationKeysForPropertyDescription(property.Alias, model.ContentTypeAlias);
                    var description = _keyTextService.LocalizeFirstAvailableKey(labelDescriptionKeys, property.Description);

                    if (string.IsNullOrWhiteSpace(description) == false)
                    {
                        property.Description = _propertyDescriptionFormatService.Format(description);
                    }
                }
            }

            foreach (var tab in model.Tabs)
            {
                var tabLabelKeys = _keyService.GetLocalizationKeysForTabLabel(tab.Alias, model.ContentTypeAlias);
                tab.Label = _keyTextService.LocalizeFirstAvailableKey(tabLabelKeys, tab.Label);
            }
        }
    }
}
