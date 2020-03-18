using BindOpen.Application.Configuration;
using BindOpen.Data.Common;
using BindOpen.Data.Elements;
using BindOpen.Data.Helpers.Serialization;
using BindOpen.System.Diagnostics;
using NUnit.Framework;
using System.IO;

namespace BindOpen.Tests.Core.Application.Configuration
{
    [TestFixture]
    public class UsableConfigurationTest
    {
        private readonly string _filePath1 = GlobalVariables.WorkingFolder + "UsableConfiguration_Main.xml";
        private readonly string _filePath20 = GlobalVariables.WorkingFolder + "UsableConfiguration_Child1.xml";
        private readonly string _filePath21 = GlobalVariables.WorkingFolder + "UsableConfiguration_Child2.xml";

        private BdoUsableConfiguration _usableConfiguration1 = null;
        private BdoUsableConfiguration _usableConfiguration20 = null;
        private BdoUsableConfiguration _usableConfiguration21 = null;

        [SetUp]
        public void Setup()
        {
            _usableConfiguration1 = new BdoUsableConfiguration(
                _filePath1,
                new[] { Path.GetFileName(_filePath20), Path.GetFileName(_filePath21) },
                new[]
                {
                    ElementFactory.CreateScalar("float1", DataValueType.Number, 10),
                }
            );

            _usableConfiguration20 = new BdoUsableConfiguration(
                _filePath20,
                new[]
                {
                    ElementFactory.CreateScalar("text1", DataValueType.Text, "item1", "item2", "item3"),
                    ElementFactory.CreateScalar("integer1", DataValueType.Integer, 1, 2, 3),
                }
            );

            _usableConfiguration21 = new BdoUsableConfiguration(
                _filePath21,
                new[]
                {
                    ElementFactory.CreateScalar("float2", DataValueType.Number, 1.1, 1.2, 1.3)
                }
            );
        }

        [Test]
        [Order(1)]
        public void TestSaveUsableConfiguration()
        {
            var log = new BdoLog();

            _usableConfiguration1.SaveXml(_filePath1, log);
            _usableConfiguration20.SaveXml(_filePath20, log);
            _usableConfiguration21.SaveXml(_filePath21, log);

            string xml = "";
            if (log.HasErrorsOrExceptions())
            {
                xml = log.ToXml();
            }
            Assert.That(!log.HasErrorsOrExceptions(), "Usable configuration saving failed. Result was '" + xml);
        }

        [Test]
        [Order(2)]
        public void TestLoadUsableConfiguration()
        {
            var log = new BdoLog();

            if (_usableConfiguration1 == null || !File.Exists(_filePath1))
            {
                TestSaveUsableConfiguration();
            }

            _ = ConfigurationFactory.Load<BdoUsableConfiguration>(_filePath1, null, null, log);
            if (log.HasErrorsOrExceptions())
            {
                string xml = log.ToXml();
            }
            //Assert.That(!log.HasErrorsOrExceptions(), "Usable configuration loading failed. Result was '" + xml);

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
