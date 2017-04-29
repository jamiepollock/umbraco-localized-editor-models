using Moq;
using NUnit.Framework;
using Our.Umbraco.LocalizedEditorModels.Models;
using Our.Umbraco.LocalizedEditorModels.Services;
using System.Collections.Generic;
using System.Globalization;
using Umbraco.Core.Logging;
using Umbraco.Core.Services;

namespace Our.Umbraco.LocalizedEditorModels.Tests.Services
{
    [TestFixture]
    public class LocalizationKeyTextServiceTests
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
            var editorModelLocalizationService = new LocalizationKeyTextService(textService, expectedCulture);
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
            var editorModelLocalizationService = new LocalizationKeyTextService(textService, expectedCulture);
            var localizationKey = new LocalizationKey("property_labels", "bgColor");

            var localizedText = editorModelLocalizationService.LocalizeKey(localizationKey);

            var expectedText = string.Format("[{0}]", localizationKey.KeyAlias);
            Assert.AreEqual(expectedText, localizedText);
        }
        
        [Test]
        public void LocalizeFirstAvailableKey_Returns_First_Found_Value()
        {
            var defaultCulture = CultureInfo.GetCultureInfo("en-US");
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
                                    {"bgColor", "Background Color"},
                                }
                            },
                            {
                                "textPage_property_labels", new Dictionary<string, string>
                                {
                                    {"bgColor", "Text Page Background Color"},
                                }
                            }
                        }
                    }
                }, Mock.Of<ILogger>());
            var editorModelLocalizationService = new LocalizationKeyTextService(textService, expectedCulture);
            var localizationKeys = new LocalizationKey[] {
                new LocalizationKey("textPage_property_labels", "bgColor"),
                new LocalizationKey("property_labels", "bgColor")
            };

            var localizedText = editorModelLocalizationService.LocalizeFirstAvailableKey(localizationKeys);

            Assert.AreEqual("Text Page Background Color", localizedText);
        }

        [Test]
        public void LocalizeFirstAvailableKey_Returns_DefaultValue_If_Not_Found()
        {
            var defaultCulture = CultureInfo.GetCultureInfo("en-US");
            var expectedCulture = CultureInfo.GetCultureInfo("en-GB");

            var textService = new LocalizedTextService(
                new Dictionary<CultureInfo, IDictionary<string, IDictionary<string, string>>>
                {
                    {
                        expectedCulture, new Dictionary<string, IDictionary<string, string>>()
                    }
                }, Mock.Of<ILogger>());
            var editorModelLocalizationService = new LocalizationKeyTextService(textService, expectedCulture);
            var localizationKeys = new LocalizationKey[] {
                new LocalizationKey("textPage_property_labels", "bgColor"),
                new LocalizationKey("property_labels", "bgColor")
            };

            var localizedText = editorModelLocalizationService.LocalizeFirstAvailableKey(localizationKeys);

            Assert.AreEqual(default(string), localizedText);
        }

        [Test]
        public void LocalizeFirstAvailableKey_Returns_FallbackValue_If_Not_Found_And_FallbackValue_Is_Provided()
        {
            var defaultCulture = CultureInfo.GetCultureInfo("en-US");
            var expectedCulture = CultureInfo.GetCultureInfo("en-GB");

            var textService = new LocalizedTextService(
                new Dictionary<CultureInfo, IDictionary<string, IDictionary<string, string>>>
                {
                    {
                        expectedCulture, new Dictionary<string, IDictionary<string, string>>()
                    }
                }, Mock.Of<ILogger>());
            var editorModelLocalizationService = new LocalizationKeyTextService(textService, expectedCulture);
            var localizationKeys = new LocalizationKey[] {
                new LocalizationKey("textPage_property_labels", "bgColor"),
                new LocalizationKey("property_labels", "bgColor")
            };

            var localizedText = editorModelLocalizationService.LocalizeFirstAvailableKey(localizationKeys, "Background Colour");

            Assert.AreEqual("Background Colour", localizedText);
        }
    }
}
