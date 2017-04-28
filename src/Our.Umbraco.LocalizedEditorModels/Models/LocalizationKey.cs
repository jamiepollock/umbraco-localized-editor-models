namespace Our.Umbraco.LocalizedEditorModels.Models
{
    internal class LocalizationKey
    {
        public LocalizationKey(string areaAlias, string keyAlias)
        {
            AreaAlias = areaAlias;
            KeyAlias = keyAlias;
        }

        public string AreaAlias { get; private set; }
        public string KeyAlias { get; private set; }

        public string ToUmbracoLocalizationKey()
        {
            return string.Format("{0}/{1}", AreaAlias, KeyAlias);
        }
    }
}
