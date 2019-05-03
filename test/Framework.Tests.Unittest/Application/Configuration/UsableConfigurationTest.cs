using System.IO;
using BindOpen.Framework.Core.Application.Configuration;
using BindOpen.Framework.Core.Data.Common;
using BindOpen.Framework.Core.Data.Elements.Factories;
using BindOpen.Framework.Core.Data.Helpers.Serialization;
using BindOpen.Framework.Core.System.Diagnostics;
using NUnit.Framework;

namespace BindOpen.Framework.UnitTest.Application.Configuration
{
    [TestFixture]
    public class UsableConfigurationTest
    {
        private readonly string _filePath1 = SetupVariables.WorkingFolder + "UsableConfiguration_Main.xml";
        private readonly string _filePath20 = SetupVariables.WorkingFolder + "UsableConfiguration_Child1.xml";
        private readonly string _filePath21 = SetupVariables.WorkingFolder + "UsableConfiguration_Child2.xml";

        private UsableConfiguration _usableConfiguration1 = null;
        private UsableConfiguration _usableConfiguration20 = null;
        private UsableConfiguration _usableConfiguration21 = null;

        [SetUp]
        public void Setup()
        {
            _usableConfiguration1 = new UsableConfiguration(
                _filePath1,
                new [] { Path.GetFileName(_filePath20), Path.GetFileName(_filePath21) },
                new []
                {
                    ElementFactory.CreateScalar("float1", DataValueType.Number, 10),
                }
            );

            _usableConfiguration20 = new UsableConfiguration(
                _filePath20,
                new[]
                {
                    ElementFactory.CreateScalar("text1", DataValueType.Text, "item1", "item2", "item3"),
                    ElementFactory.CreateScalar("integer1", DataValueType.Integer, 1, 2, 3),
                }
            );

            _usableConfiguration21 = new UsableConfiguration(
                _filePath21,
                new[]
                {
                    ElementFactory.CreateScalar("float2", DataValueType.Number, 1.1, 1.2, 1.3)
                }
            );
        }

        [Test]
        public void TestSaveUsableConfiguration()
        {
            ILog log = new Log();

            _usableConfiguration1.SaveXml(_filePath1, log);
            _usableConfiguration20.SaveXml(_filePath20, log);
            _usableConfiguration21.SaveXml(_filePath21, log);

            Assert.That(!log.HasErrorsOrExceptions(), "Usable configuration saving failed. Result was '" + log.ToXml());
        }

        [Test]
        public void TestLoadUsableConfiguration()
        {
            ILog log = new Log();

            if (_usableConfiguration1 == null || !File.Exists(_filePath1))
                TestSaveUsableConfiguration();

            var configuration = ConfigurationLoader.Load<UsableConfiguration>(_filePath1, null, null, log);

            Assert.That(!log.HasErrorsOrExceptions(), "Usable configuration loading failed. Result was '" + log.ToXml());

            //Assert.That(
            //    ((string)configuration["text1"]?[0] == "item1")
            //    && ((string)configuration["text1"]?[1] == "item2")
            //    && ((string)configuration["text1"]?[2] == "item3"), "Bad usable configuration loading");
            //Assert.That(
            //    ((int)configuration["integer1"]?[0] == 1)
            //    && ((int)configuration["integer1"]?[1] == 2)
            //    && ((int)configuration["integer1"]?[2] == 3), "Bad usable configuration loading");
            //Assert.That(
            //    ((double)configuration["float2"]?[0] == 1.1)
            //    && ((double)configuration["float2"]?[1] == 1.2)
            //    && ((double)configuration["float2"]?[2] == 1.3), "Bad usable configuration loading");

            //Assert.That(
            //    configuration.Count == 4, "Bad usable configuration loading");
        }
    }
}
