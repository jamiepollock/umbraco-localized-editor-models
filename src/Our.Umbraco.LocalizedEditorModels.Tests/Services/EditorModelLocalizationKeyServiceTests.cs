using NUnit.Framework;
using Our.Umbraco.LocalizedEditorModels.Services;
using System.Linq;

namespace Our.Umbraco.LocalizedEditorModels.Tests
{
    [TestFixture]
    public class EditorModelLocalizationKeyServiceTests
    {
        [Test]
        public void Get_Property_Label_Keys()
        {
            var service = new EditorModelLocalizationKeyService();

            var keys = service.GetLocalizationKeysForPropertyLabel("pageTitle", "homePage");
            
            Assert.IsTrue(keys.Any());
            Assert.AreEqual("homePage_property_labels/pageTitle", keys.ElementAt(0).ToUmbracoLocalizationKey());
            Assert.AreEqual("property_labels/pageTitle", keys.ElementAt(1).ToUmbracoLocalizationKey());
        }

        [Test]
        public void Get_Property_Description_Keys()
        {
            var service = new EditorModelLocalizationKeyService();

            var keys = service.GetLocalizationKeysForPropertyDescription("pageTitle", "homePage");

            Assert.IsTrue(keys.Any());
            Assert.AreEqual("homePage_property_descriptions/pageTitle", keys.ElementAt(0).ToUmbracoLocalizationKey());
            Assert.AreEqual("property_descriptions/pageTitle", keys.ElementAt(1).ToUmbracoLocalizationKey());
        }
    }
}
