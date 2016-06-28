using System;
using System.Collections.Generic;
using ProjectGuidParserTests;
using Xunit;
using Xunit.Extensions;

namespace Microsoft.VisualStudio.Web.Tests
{
    public class ProjectGuidParserXUnitTests
    {
        [Theory]
        [InlineData(@"c:\PathToInvalidPath\web.config")]
        [InlineData(@"c:\PathToInvalidFolder\")]
        [InlineData(@"c:\PathTo Invalid Folder\")]
        public void Parsing_Invalid_WebConfigPath_ReturnsNull(string webConfigPath)
        {
            //Act
            string actualGuid = new ProjectGuidParser().GetProjectGuidFromWebConfig(webConfigPath);

            Assert.Null(actualGuid);
        }

        public static IEnumerable<string[]> ValidWebConfigs
        {
            get
            {
                yield return new string[] { Resource1.ValidProjectGuid };
                yield return new string[] { Resource1.ProjectGuidBFormat };
                yield return new string[] { Resource1.ProjectGuidDFormat };
                yield return new string[] { Resource1.ProjectGuidNFormat };
                yield return new string[] { Resource1.ProjectGuidPFormat };
                yield return new string[] { Resource1.ProjectGuidWithNoSpace};
                yield return new string[] { Resource1.MultipleProjectGuid };
            }
        }

        [Theory, MemberData("ValidWebConfigs")]
        public void Parsing_Valid_WebConfigContent_Returns_ProjectGuid(string webConfigContent)
        {
            //Arrange
            string expectedProjectGuid = "F535E3E2-737D-422D-A529-D79D43FB4F5E";

            // Act
            string projectGuid = new ProjectGuidParser().GetProjectGuidFromWebConfig(webConfigContent);

            //Assert
            Assert.Equal(expectedProjectGuid, projectGuid, StringComparer.OrdinalIgnoreCase);
        }

        public static IEnumerable<string[]> InValidWebConfigs
        {
            get
            {
                yield return new string[] { Resource1.NoProjectGuid };
                yield return new string[] { Resource1.InvalidProjectGuid };
                yield return new string[] { Resource1.InvalidWebConfig };
                yield return new string[] { Resource1.LastNodeInvalidComment };
            }
        }

        [Theory, MemberData("InValidWebConfigs")]
        public void Parsing_InValid_WebConfigContent_ReturnsNull(string webConfigContent)
        {
            // Act
            string projectGuid = new ProjectGuidParser().GetProjectGuidFromWebConfig(webConfigContent);

            //Assert
            Assert.Null(projectGuid);
        }
    }
}
