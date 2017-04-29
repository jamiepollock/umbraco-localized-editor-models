using Umbraco.Core.Models;
using Umbraco.Web.Models.ContentEditing;

namespace Our.Umbraco.LocalizedEditorModels.Services
{
    internal class EditorModelLocalizationService
    {
        private readonly LocalizationKeyService _keyService;
        private readonly LocalizationKeyTextService _keyTextService;

        public EditorModelLocalizationService(LocalizationKeyService keyService, LocalizationKeyTextService keyTextService)
        {
            _keyService = keyService;
            _keyTextService = keyTextService;
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
                        property.Description = description;
                    }
                }
            }
        }
    }
}
