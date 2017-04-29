using System;
using System.Collections.Specialized;

namespace Our.Umbraco.LocalizedEditorModels.Web.Configuration
{
    public class NameValueCollectionConfigurationSettings : IConfigurationSettings
    {
        public static NameValueCollectionConfigurationSettings GetConfig(NameValueCollection nameValueCollection)
        {
            if (nameValueCollection.Count == 0)
            {
                return new NameValueCollectionConfigurationSettings();
            }

            var enabledSetting = nameValueCollection["Our.Umbraco.LocalizedEditorModels:Enabled"];
            var propertyDescriptionFormatSetting = nameValueCollection["Our.Umbraco.LocalizedEditorModels:PropertyDescriptionFormat"];

            var isEnabled = (string.IsNullOrWhiteSpace(enabledSetting) == false && string.Equals(enabledSetting, bool.TrueString, StringComparison.OrdinalIgnoreCase));
            
            var propertyDescriptionFormat = PropertyDescriptionFormats.Default;
            var hasPropertyFormatSetting = (string.IsNullOrWhiteSpace(propertyDescriptionFormatSetting) == false && Enum.TryParse(propertyDescriptionFormatSetting, true, out propertyDescriptionFormat));


            return new NameValueCollectionConfigurationSettings()
            {
                IsEnabled = isEnabled,
                PropertyDescriptionFormat = propertyDescriptionFormat
            };
        }


        public bool IsEnabled { get; private set; }
        public PropertyDescriptionFormats PropertyDescriptionFormat { get; private set; }
    }
}
