using Moq;
using NUnit.Framework;
using Our.Umbraco.LocalizedEditorModels.Services;
using System.Collections.Generic;
using System.Globalization;
using Umbraco.Core.Logging;
using Umbraco.Core.Services;
using Umbraco.Web.Models.ContentEditing;
using System.Linq;

namespace Our.Umbraco.LocalizedEditorModels.Tests.Services
{
    [TestFixture]
    public class EditorModelLocalizationServiceTests
    {
        private LocalizationKeyTextService _localizationKeyTextService;
        private LocalizationKeyService _localizationKeyService;
        private EditorModelLocalizationService _editorModelLocalizationService;

        [OneTimeSetUp]
        public void Init()
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
                                    {"bgColor", "Background Colour"},
                                    {"favoriteNewsItems", "Favourite News Items"},
                                }
                            },
                            {
                                "property_descriptions", new Dictionary<string, string>
                                {
                                    {"bgColor", "Sets the background colour for the page"}
                                }
                            },
                            {
                                "textPage_property_labels", new Dictionary<string, string>
                                {
                                    {"bgColor", "Text Page Background Colour"},
                                }
                            }
                        }
                    }
                }, Mock.Of<ILogger>());

            _localizationKeyTextService = new LocalizationKeyTextService(textService, expectedCulture);
            _localizationKeyService = new LocalizationKeyService();

            _editorModelLocalizationService = new EditorModelLocalizationService(_localizationKeyService, _localizationKeyTextService);
        }


        [Test]
        public void LocalizeModel_Localizes_Property_Labels()
        {
            var props = new List<ContentPropertyDisplay>()
            {
                new ContentPropertyDisplay
                {
                    Alias = "bgColor",
                    Label = "Background Color",
                    HideLabel = false
                },
                new ContentPropertyDisplay
                {
                    Alias = "favoriteNewsItems",
                    Label = "Favorite New Items",
                    HideLabel = false
                }
            };
            var tabs = new List<Tab<ContentPropertyDisplay>>
            {
                new Tab<ContentPropertyDisplay>()
                {
                    Alias = "Tab",
                    Label = "Tab",
                    Properties = props
                }
            };

            var model = new ContentItemDisplay()
            {
                ContentTypeAlias = "textPage",
                Tabs = tabs
            };

            _editorModelLocalizationService.LocalizeModel(model);

            Assert.AreEqual("Text Page Background Colour", model.Properties.FirstOrDefault(x => x.Alias == "bgColor").Label);
            Assert.AreEqual("Favourite News Items", model.Properties.FirstOrDefault(x => x.Alias == "favoriteNewsItems").Label);
        }

        [Test]
        public void LocalizeModel_Ignores_Property_Which_Have_Hidden_Labels()
        {
            var props = new List<ContentPropertyDisplay>()
            {
                new ContentPropertyDisplay
                {
                    Alias = "bgColor",
                    Label = "Background Color",
                    HideLabel = false
                },
                new ContentPropertyDisplay
                {
                    Alias = "favoriteNewsItems",
                    Label = "Favorite New Items",
                    HideLabel = true
                }
            };
            var tabs = new List<Tab<ContentPropertyDisplay>>
            {
                new Tab<ContentPropertyDisplay>()
                {
                    Alias = "Tab",
                    Label = "Tab",
                    Properties = props
                }
            };

            var model = new ContentItemDisplay()
            {
                ContentTypeAlias = "textPage",
                Tabs = tabs
            };

            _editorModelLocalizationService.LocalizeModel(model);

            Assert.AreEqual("Text Page Background Colour", model.Properties.FirstOrDefault(x => x.Alias == "bgColor").Label);
            Assert.AreEqual("Favorite New Items", model.Properties.FirstOrDefault(x => x.Alias == "favoriteNewsItems").Label);
        }

        [Test]
        public void LocalizeModel_Property_Which_Were_Null_Before_Still_Return_Null_If_Localization_Found()
        {
            var props = new List<ContentPropertyDisplay>()
            {
                new ContentPropertyDisplay
                {
                    Alias = "emptyDescriptionProperty",
                    Label = "No Description Property",
                    HideLabel = false
                }
            };
            var tabs = new List<Tab<ContentPropertyDisplay>>
            {
                new Tab<ContentPropertyDisplay>()
                {
                    Alias = "Tab",
                    Label = "Tab",
                    Properties = props
                }
            };

            var model = new ContentItemDisplay()
            {
                ContentTypeAlias = "textPage",
                Tabs = tabs
            };

            _editorModelLocalizationService.LocalizeModel(model);

            Assert.IsNull(model.Properties.FirstOrDefault(x => x.Alias == "emptyDescriptionProperty").Description);
        }


        [Test]
        public void LocalizeModel_Localizes_Property_Descriptions()
        {
            var props = new List<ContentPropertyDisplay>()
            {
                new ContentPropertyDisplay
                {
                    Alias = "bgColor",
                    Label = "Background Color",
                    Description = "Sets the background color for the page",
                    HideLabel = false
                }
            };
            var tabs = new List<Tab<ContentPropertyDisplay>>
            {
                new Tab<ContentPropertyDisplay>()
                {
                    Alias = "Tab",
                    Label = "Tab",
                    Properties = props
                }
            };

            var model = new ContentItemDisplay()
            {
                ContentTypeAlias = "textPage",
                Tabs = tabs
            };

            _editorModelLocalizationService.LocalizeModel(model);

            var firstProperty = model.Properties.FirstOrDefault();
            Assert.IsNotNull(firstProperty.Description);
            Assert.AreEqual("Sets the background colour for the page", firstProperty.Description);
        }
    }
}
