using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace Microsoft.VisualStudio.Web
{
    public class ProjectGuidParser
    {
        public Guid? GetProjectGuidFromWebConfig(Stream webConfig)
        {
            int DefaultBufferSize = 1024;
            string ProjectGuidPrefix = "ProjectGuid:";
            try
            {
                using (TextReader documentReader = new StreamReader(webConfig, Encoding.UTF8, true, DefaultBufferSize, false))
                {
                    XDocument document = null;
                    document = XDocument.Load(documentReader);
                    if (document != null)
                    {
                        XNode lastNode = document.LastNode;
                        while (lastNode != null && lastNode.NodeType != XmlNodeType.EndElement && lastNode.NodeType != XmlNodeType.Element && lastNode.NodeType != XmlNodeType.XmlDeclaration)
                        {
                            if (lastNode.NodeType == XmlNodeType.Comment)
                            {
                                XComment projectGuidComment = (XComment)lastNode;
                                string projectGuidValue = projectGuidComment.Value;
                                if (projectGuidValue != null)
                                {
                                    bool isProjectGuidPrefixPresent = projectGuidValue.Trim().StartsWith(ProjectGuidPrefix, StringComparison.OrdinalIgnoreCase);
                                    // if we find the ProjectGuid prefix, we always exit even if the value returned is not a valid Guid.
                                    if (isProjectGuidPrefixPresent)
                                    {
                                        projectGuidValue = projectGuidValue.Replace(ProjectGuidPrefix, string.Empty).Trim(' ');
                                        Guid projectGuid;
                                        if (Guid.TryParse(projectGuidValue, out projectGuid))
                                        {
                                            return projectGuid;
                                        }

                                        return null;
                                    }
                                }
                            }

                            lastNode = lastNode.PreviousNode;
                        }
                    }
                }
            }
            catch
            {
            }

            return null;
        }
    }
}
