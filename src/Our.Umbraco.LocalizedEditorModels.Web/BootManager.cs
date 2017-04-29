using Our.Umbraco.LocalizedEditorModels.Services;
using System;
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
        protected override void ApplicationStarted(UmbracoApplicationBase umbracoApplication, ApplicationContext applicationContext)
        {
            EditorModelEventManager.SendingContentModel += EditorModelEventManager_SendingContentModel;
            EditorModelEventManager.SendingMediaModel += EditorModelEventManager_SendingMediaModel;
            EditorModelEventManager.SendingMemberModel += EditorModelEventManager_SendingMemberModel;
        }

        private void EditorModelEventManager_SendingMemberModel(HttpActionExecutedContext sender, EditorModelEventArgs<MemberDisplay> e)
        {
            Localize(e.Model);
        }

        private void EditorModelEventManager_SendingContentModel(HttpActionExecutedContext sender, EditorModelEventArgs<ContentItemDisplay> e)
        {
            Localize(e.Model);
        }

        private void EditorModelEventManager_SendingMediaModel(HttpActionExecutedContext sender, EditorModelEventArgs<MediaItemDisplay> e)
        {
            Localize(e.Model);
        }

        private void Localize<T>(ListViewAwareContentItemDisplayBase<ContentPropertyDisplay, T> model) where T : IContentBase
        {
            var textService = ApplicationContext.Current.Services.TextService;
            var keyService = new LocalizationKeyService();
            var keyTextService = new LocalizationKeyTextService(textService, Thread.CurrentThread.CurrentCulture);

            var editorModelService = new EditorModelLocalizationService(keyService, keyTextService);

            editorModelService.LocalizeModel(model);
        }
    }
}
