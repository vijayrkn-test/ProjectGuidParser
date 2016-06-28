using System;
using System.Xml.Linq;

namespace Microsoft.VisualStudio.Web
{
    public class ProjectGuidParser
    {
        public string GetProjectGuidFromWebConfig(string webConfig)
        {
            string projectGuid = null;
            XDocument document = GetXDocumentFromWebConfig(webConfig);
            if (document != null)
            {
                projectGuid = GetGuidFromXDocument(document);
            }

            return projectGuid;
        }

        private XDocument GetXDocumentFromWebConfig(string webconfig)
        {
            XDocument document = null;
            try
            {
                // If the path to the web.config is passed.
                document = XDocument.Load(webconfig);
            }
            catch
            {
                try
                {
                    // If the content of the web.config is passed instead of the path.
                    document = XDocument.Parse(webconfig);
                }
                catch
                {
                    return null;
                }
            }

            return document;
        }

        private string GetGuidFromXDocument(XDocument document)
        {
            XNode lastNode = null;
            if (document != null)
            {
                lastNode = document.LastNode;
            }

            // if the last node is of type comment get the comment.
            if (lastNode != null && lastNode.NodeType == System.Xml.XmlNodeType.Comment)
            {
                XComment projectGuidComment = lastNode as XComment;
                if (projectGuidComment != null)
                {
                    string projectGuidValue = projectGuidComment.Value;
                    if (projectGuidValue != null)
                    {
                        projectGuidValue = projectGuidValue.Replace("ProjectGuid:", string.Empty).Trim(' ');
                        Guid projectGuid;
                        bool success = Guid.TryParse(projectGuidValue, out projectGuid);
                        if (success)
                        {
                            return string.Format("{0}", projectGuid);
                        }
                    }
                }
            }

            return null;
        }
    }
}
