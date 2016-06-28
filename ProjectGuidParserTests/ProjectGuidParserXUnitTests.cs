using System;
using System.Collections.Generic;
using System.IO;
using ProjectGuidParserTests;
using Xunit;
using Xunit.Extensions;

namespace Microsoft.VisualStudio.Web.Tests
{
    public class ProjectGuidParserXUnitTests
    {
        public static IEnumerable<string[]> ValidWebConfigs
        {
            get
            {
                yield return new string[] { Resource1.ValidProjectGuid };
                yield return new string[] { Resource1.ProjectGuidBFormat };
                yield return new string[] { Resource1.ProjectGuidDFormat };
                yield return new string[] { Resource1.ProjectGuidNFormat };
                yield return new string[] { Resource1.ProjectGuidPFormat };
                yield return new string[] { Resource1.ProjectGuidWithNoSpace };
                yield return new string[] { Resource1.MultipleProjectGuid };
                yield return new string[] { Resource1.ValidWebConfigWithOnlyProjectGuid };
            }
        }

        public Stream GenerateStreamFromString(string webConfig)
        {
            MemoryStream stream = new MemoryStream();
            StreamWriter writer = new StreamWriter(stream);
            writer.Write(webConfig);
            writer.Flush();
            stream.Position = 0;
            return stream;
        }

        [Theory, MemberData("ValidWebConfigs")]
        public void Parsing_Valid_WebConfigContent_Returns_ProjectGuid(string webConfigContent)
        {
            //Arrange
            Guid? expectedProjectGuid = Guid.Parse("F535E3E2-737D-422D-A529-D79D43FB4F5E");

            // Act
            Guid? actualProjectGuid  = null;
            using (Stream stream = GenerateStreamFromString(webConfigContent))
            {
                actualProjectGuid = new ProjectGuidParser().GetProjectGuidFromWebConfig(stream);
            }

            //Assert
            Assert.Equal(expectedProjectGuid.Value, actualProjectGuid.Value);
        }

        public static IEnumerable<string[]> InValidWebConfigs
        {
            get
            {
                yield return new string[] { Resource1.NoProjectGuid };
                yield return new string[] { Resource1.InvalidProjectGuid };
                yield return new string[] { Resource1.InvalidWebConfig };
                yield return new string[] { Resource1.LastNodeInvalidComment };
                yield return new string[] { Resource1.EmptyWebConfig };
                yield return new string[] { Resource1.WebConfigWithOnlyDeclaration };
                yield return new string[] { Resource1.WebConfigWithOnlySelfClosingTag };
                yield return new string[] { Resource1.InvalidWebConfigWithOnlyProjectGuid };
                yield return new string[] { Resource1.WebConfigWithProjectGuidRemoved };
            }
        }

        [Theory, MemberData("InValidWebConfigs")]
        public void Parsing_InValid_WebConfigContent_ReturnsNull(string webConfigContent)
        {
            // Act
            Guid? actualProjectGuid = null;
            using (Stream stream = GenerateStreamFromString(webConfigContent))
            {
                actualProjectGuid = new ProjectGuidParser().GetProjectGuidFromWebConfig(stream);
            }

            //Assert
            Assert.Null(actualProjectGuid);
        }
    }
}
