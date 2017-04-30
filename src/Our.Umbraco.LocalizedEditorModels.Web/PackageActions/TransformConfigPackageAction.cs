using Microsoft.Web.XmlTransform;
using System;
using System.Web;
using System.Xml;
using umbraco.cms.businesslogic.packager.standardPackageActions;
using umbraco.interfaces;
using Umbraco.Core.Logging;

namespace Our.Umbraco.LocalizedEditorModels.Web.PackageActions
{
    /// <summary>
    /// Package Action which supports the use of .xdt files during Umbraco Package installation (not required when installed by NuGet.)
    ///
    /// Modified version of Tim Geyssens' https://github.com/TimGeyssens/UmbracoPageNotFoundManager/blob/e4861c2a298c977842c73dd1683a297f4d7b3198/PageNotFoundManager/Umbraco/Installer/PackageActions.cs
    /// </summary>
    public class TransformConfigPackageAction : IPackageAction
    {
        public string Alias()
        {
            return "LEM.TransformConfig";
        }

        private bool Transform(string packageName, XmlNode xmlData, bool uninstall = false)
        {
            try
            {
                var configFileAttributeValue = xmlData.Attributes.GetNamedItem("file").Value;
                var configFileAbsoluteVirtualPath = VirtualPathUtility.ToAbsolute(configFileAttributeValue);

                var fileEnd = "install.xdt";
                if (uninstall)
                {
                    fileEnd = string.Format("un{0}", fileEnd);
                }

                var xdtfileAttributeValue = string.Format("{0}.{1}", xmlData.Attributes.GetNamedItem("xdtfile").Value, fileEnd);
                var xdtFileAbsoluteVirtualPath = VirtualPathUtility.ToAbsolute(xdtfileAttributeValue);

                using (var xmlDoc = new XmlTransformableDocument())
                {
                    var xmlMappedFilePath = HttpContext.Current.Server.MapPath(configFileAbsoluteVirtualPath);
                    var xdtMappedFilePath = HttpContext.Current.Server.MapPath(xdtFileAbsoluteVirtualPath);
                    xmlDoc.PreserveWhitespace = true;
                    xmlDoc.Load(xmlMappedFilePath);

                    using (var xmlTrans = new XmlTransformation(xdtMappedFilePath))
                    {
                        LogHelper.Info<TransformConfigPackageAction>("Applying XDT File ({0}) to XML File ({1}).", () => new[] { xdtMappedFilePath, xmlMappedFilePath });

                        if (xmlTrans.Apply(xmlDoc))
                        {
                            xmlDoc.Save(xmlMappedFilePath);
                            LogHelper.Info<TransformConfigPackageAction>("Successfully Applied XDT File ({0}) to XML File ({1}).", () => new[] { xdtMappedFilePath, xmlMappedFilePath });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error<TransformConfigPackageAction>("Unable to perform package action.", ex);
                return false;
            }

            return true;
        }

        public bool Execute(string packageName, XmlNode xmlData)
        {
            return Transform(packageName, xmlData);
        }

        public XmlNode SampleXml()
        {
            var str = "<Action runat=\"install\" undo=\"true\" alias=\"LEM.TransformConfig\" file=\"~/web.config\" xdtfile=\"~/app_plugins/demo/web.config\"></Action>";
            return helper.parseStringToXmlNode(str);
        }

        public bool Undo(string packageName, XmlNode xmlData)
        {
            return Transform(packageName, xmlData, true);
        }
    }
}
