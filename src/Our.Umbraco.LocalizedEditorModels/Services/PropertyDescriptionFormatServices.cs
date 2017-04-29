using MarkdownSharp;

namespace Our.Umbraco.LocalizedEditorModels.Services
{
    public interface IPropertyDescriptionFormatService
    {
        string Format(string propertyDescription);
    }

    public class DefaultPropertyDescriptionFormatService : IPropertyDescriptionFormatService
    {
        public string Format(string propertyDescription)
        {
            return propertyDescription;
        }
    }

    public class MarkdownPropertyDescriptionFormatService : IPropertyDescriptionFormatService
    {
        private readonly Markdown _markdown;

        public MarkdownPropertyDescriptionFormatService() : 
            this(new MarkdownOptions() {
                DisableHeaders = true,
                DisableImages = true
            })
        {
        }

        public MarkdownPropertyDescriptionFormatService(MarkdownOptions options)
        {
            _markdown = new Markdown(options);
        }

        public string Format(string propertyDescription)
        {
            return _markdown.Transform(propertyDescription)
                .Replace("<a href=", "<a target=\"_blank\" href=")
                .Replace("  _", "<br /><br />").Replace("<br /><br /></p>\n\n<p>", "</p><p>");
        }
    }
}
