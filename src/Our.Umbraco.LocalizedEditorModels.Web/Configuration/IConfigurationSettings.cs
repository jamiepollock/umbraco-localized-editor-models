namespace Our.Umbraco.LocalizedEditorModels.Web.Configuration
{
    public interface IConfigurationSettings
    {
        bool IsEnabled { get; }
        PropertyDescriptionFormats PropertyDescriptionFormat { get; }
    }
}
