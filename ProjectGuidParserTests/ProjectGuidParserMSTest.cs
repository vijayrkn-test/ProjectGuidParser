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
        public void VerifyProjectGuid_FromWebConfigFile_utf8()
        {
            Guid? expectedProjectGuid = Guid.Parse("F535E3E2-737D-422D-A529-D79D43FB4F5E");

            // Arrange
            string webConfigPath = Path.Combine(TestContext.DeploymentDirectory, "web.config");
            Assert.IsTrue(File.Exists(webConfigPath));

            //Act
            Guid? actualProjectGuid = null;
            using (Stream fs = File.OpenRead(webConfigPath))
            {
                actualProjectGuid = new ProjectGuidParser().GetProjectGuidFromWebConfig(fs);
            }

            // Assert
            Assert.AreEqual(expectedProjectGuid.Value, actualProjectGuid.Value);
        }

        [TestMethod]
        [DeploymentItem(@"..\..\SampleWebConfigs\utf16bebom\web.config")]
        public void VerifyProjectGuid_FromWebConfigFile_utf16bebom()
        {
            Guid? expectedProjectGuid = Guid.Parse("F535E3E2-737D-422D-A529-D79D43FB4F5E");

            // Arrange
            string webConfigPath = Path.Combine(TestContext.DeploymentDirectory, "web.config");
            Assert.IsTrue(File.Exists(webConfigPath));

            //Act
            Guid? actualProjectGuid = null;
            using (Stream fs = File.OpenRead(webConfigPath))
            {
                actualProjectGuid = new ProjectGuidParser().GetProjectGuidFromWebConfig(fs);
            }

            // Assert
            Assert.AreEqual(expectedProjectGuid.Value, actualProjectGuid.Value);
        }

        [TestMethod]
        [DeploymentItem(@"..\..\SampleWebConfigs\encrypted\web.config")]
        public void VerifyProjectGuid_FromWebConfigFile_encrypted()
        {
            Guid? expectedProjectGuid = Guid.Parse("F535E3E2-737D-422D-A529-D79D43FB4F5E");

            // Arrange
            string webConfigPath = Path.Combine(TestContext.DeploymentDirectory, "web.config");
            Assert.IsTrue(File.Exists(webConfigPath));

            //Act
            Guid? actualProjectGuid = null;
            using (Stream fs = File.OpenRead(webConfigPath))
            {
                actualProjectGuid = new ProjectGuidParser().GetProjectGuidFromWebConfig(fs);
            }

            // Assert
            Assert.AreEqual(expectedProjectGuid.Value, actualProjectGuid.Value);
        }

        [TestMethod]
        [DeploymentItem(@"..\..\SampleWebConfigs\utf16lebom\web.config")]
        public void VerifyProjectGuid_FromWebConfigFile_utf16lebom()
        {
            Guid? expectedProjectGuid = Guid.Parse("F535E3E2-737D-422D-A529-D79D43FB4F5E");

            // Arrange
            string webConfigPath = Path.Combine(TestContext.DeploymentDirectory, "web.config");
            Assert.IsTrue(File.Exists(webConfigPath));

            //Act
            Guid? actualProjectGuid = null;
            using (Stream fs = File.OpenRead(webConfigPath))
            {
                actualProjectGuid = new ProjectGuidParser().GetProjectGuidFromWebConfig(fs);
            }

            // Assert
            Assert.AreEqual(expectedProjectGuid.Value, actualProjectGuid.Value);
        }

        [TestMethod]
        [DeploymentItem(@"..\..\SampleWebConfigs\invalid.config")]
        public void InValidConfigFile_Returns_Null()
        {
            // Arrange
            string webConfigPath = Path.Combine(TestContext.DeploymentDirectory, "invalid.config");
            Assert.IsTrue(File.Exists(webConfigPath));

            Guid? actualProjectGuid = null;
            using (Stream fs = File.OpenRead(webConfigPath))
            {
                actualProjectGuid = new ProjectGuidParser().GetProjectGuidFromWebConfig(fs);
            }

            // Assert
            Assert.IsNull(actualProjectGuid);
        }
    }
}
