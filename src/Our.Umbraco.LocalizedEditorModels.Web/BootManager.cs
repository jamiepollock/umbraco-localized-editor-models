using Our.Umbraco.LocalizedEditorModels.Services;
using Our.Umbraco.LocalizedEditorModels.Web.Configuration;
using System;
using System.Configuration;
using System.Globalization;
using System.Threading;
using System.Web.Http.Filters;
using Umbraco.Core;
using Umbraco.Core.Models;
using Umbraco.Web.Editors;
using Umbraco.Web.Models.ContentEditing;

namespace Our.Umbraco.LocalizedEditorModels.Web
{
    public class BootManager : ApplicationEventHandler
    {
        private IConfigurationSettings Config = NameValueCollectionConfigurationSettings.GetConfig(ConfigurationManager.AppSettings);


        protected override void ApplicationStarted(UmbracoApplicationBase umbracoApplication, ApplicationContext applicationContext)
        {
            if (Config.IsEnabled)
            {
                EditorModelEventManager.SendingContentModel += EditorModelEventManager_SendingContentModel;
                EditorModelEventManager.SendingMediaModel += EditorModelEventManager_SendingMediaModel;
                EditorModelEventManager.SendingMemberModel += EditorModelEventManager_SendingMemberModel;
            }
        }

        private void EditorModelEventManager_SendingMemberModel(HttpActionExecutedContext sender, EditorModelEventArgs<MemberDisplay> e)
        {
            Localize(e.Model, Config.PropertyDescriptionFormat);
        }

        private void EditorModelEventManager_SendingContentModel(HttpActionExecutedContext sender, EditorModelEventArgs<ContentItemDisplay> e)
        {
            Localize(e.Model, Config.PropertyDescriptionFormat);
        }

        private void EditorModelEventManager_SendingMediaModel(HttpActionExecutedContext sender, EditorModelEventArgs<MediaItemDisplay> e)
        {
            Localize(e.Model, Config.PropertyDescriptionFormat);
        }

        private void Localize<T>(ListViewAwareContentItemDisplayBase<ContentPropertyDisplay, T> model, PropertyDescriptionFormats propertyDscriptionFormat) where T : IContentBase
        {
            var textService = ApplicationContext.Current.Services.TextService;
            var keyService = new LocalizationKeyService();
            var keyTextService = new LocalizationKeyTextService(textService, Thread.CurrentThread.CurrentCulture);
            var propertyDescriptionFormatService = GetPropertyDescriptionFormatService(propertyDscriptionFormat);

            var editorModelService = new EditorModelLocalizationService(keyService, keyTextService, propertyDescriptionFormatService);

            editorModelService.LocalizeModel(model);
        }

        private IPropertyDescriptionFormatService GetPropertyDescriptionFormatService(PropertyDescriptionFormats propertyDscriptionFormat)
        {
            switch(propertyDscriptionFormat)
            {
                case PropertyDescriptionFormats.Markdown:
                    return new MarkdownPropertyDescriptionFormatService();
                default:
                    return new DefaultPropertyDescriptionFormatService();
            }
        }
    }
}
