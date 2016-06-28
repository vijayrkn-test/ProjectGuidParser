using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.VisualStudio.Web;

namespace ProjectGuidParserTests
{
    [TestClass]
    public class ProjectGuidParserMSTest
    {
        public TestContext TestContext { get; set; }

        [TestMethod]
        [DeploymentItem(@"..\..\SampleWebConfigs\web.config")]
        public void VerifyProjectGuid_FromWebConfigFile()
        {
            string expectedProjectGuid = "F535E3E2-737D-422D-A529-D79D43FB4F5E";

            // Arrange
            string webConfigPath = Path.Combine(TestContext.DeploymentDirectory, "web.config");
            Assert.IsTrue(File.Exists(webConfigPath));

            //Act
            string actualProjectGuid = new ProjectGuidParser().GetProjectGuidFromWebConfig(webConfigPath);

            // Assert
            Assert.AreEqual(expectedProjectGuid, actualProjectGuid, ignoreCase: true);
        }

        [TestMethod]
        [DeploymentItem(@"..\..\SampleWebConfigs\invalid.config")]
        public void InValidConfigFile_Returns_Null()
        {
            // Arrange
            string webConfigPath = Path.Combine(TestContext.DeploymentDirectory, "invalid.config");
            Assert.IsTrue(File.Exists(webConfigPath));

            //Act
            string actualProjectGuid = new ProjectGuidParser().GetProjectGuidFromWebConfig(webConfigPath);

            // Assert
            Assert.IsNull(actualProjectGuid);
        }

    }
}
