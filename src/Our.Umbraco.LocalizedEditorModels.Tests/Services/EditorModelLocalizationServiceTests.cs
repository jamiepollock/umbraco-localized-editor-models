using Moq;
using NUnit.Framework;
using Our.Umbraco.LocalizedEditorModels.Services;
using System.Globalization;
using Umbraco.Core.Services;
using Umbraco.Core.Logging;
using System.Collections.Generic;
using Our.Umbraco.LocalizedEditorModels.Models;

namespace Our.Umbraco.LocalizedEditorModels.Tests.Services
{
    [TestFixture]
    public class EditorModelLocalizationServiceTests
    {
        [Test]
        public void LocalizeKey_Returns_Value_If_Found()
        {
            var expectedCulture = CultureInfo.GetCultureInfo("en-GB");

            var textService = new LocalizedTextService(
                new Dictionary<CultureInfo, IDictionary<string, IDictionary<string, string>>>
                {
                    {
                        expectedCulture, new Dictionary<string, IDictionary<string, string>>
                        {
                            {
                                "property_labels", new Dictionary<string, string>
                                {
                                    {"bgColor", "Background Colour"},
                                }
                            }
                        }
                    }
                }, Mock.Of<ILogger>());
            var editorModelLocalizationService = new EditorModelLocalizationService(textService, expectedCulture);
            var localizationKey = new LocalizationKey("property_labels", "bgColor");

            var localizedText = editorModelLocalizationService.LocalizeKey(localizationKey);

            Assert.AreEqual("Background Colour", localizedText);
        }


        [Test]
        public void LocalizeKey_Returns_KeyAlias_If_Not_Found()
        {
            var defaultCulture = CultureInfo.GetCultureInfo("en-US");
            var expectedCulture = CultureInfo.GetCultureInfo("en-GB");

            var textService = new LocalizedTextService(
                new Dictionary<CultureInfo, IDictionary<string, IDictionary<string, string>>>
                {
                    {
                        defaultCulture, new Dictionary<string, IDictionary<string, string>>
                        {
                            {
                                "property_labels", new Dictionary<string, string>
                                {
                                    {"bgColor", "Background Color"},
                                }
                            }
                        }
                    }
                }, Mock.Of<ILogger>());
            var editorModelLocalizationService = new EditorModelLocalizationService(textService, expectedCulture);
            var localizationKey = new LocalizationKey("property_labels", "bgColor");

            var localizedText = editorModelLocalizationService.LocalizeKey(localizationKey);

            var expectedText = string.Format("[{0}]", localizationKey.KeyAlias);
            Assert.AreEqual(expectedText, localizedText);
        }
    }
}
