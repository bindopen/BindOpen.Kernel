using BindOpen.Kernel.IO.Dtos;
using BindOpen.Kernel.Tests;
using NUnit.Framework;
using System.IO;

namespace BindOpen.Kernel.Data.Meta
{
    [TestFixture, Order(201)]
    public class MetaWrapperIOTests
    {
        private readonly string _filePath_xml = SystemData.WorkingFolder + "MetaWrapperIO.xml";
        private readonly string _filePath_json = SystemData.WorkingFolder + "MetaWrapperIO.json";

        private MetaWrapperFake _wrapper;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
        }

        private void Test(MetaWrapperFake wrapper)
        {
            Assert.That(wrapper?.SubEnumValue == ActionPriorities.Medium, "Bad obj element set - Count");
            Assert.That(wrapper?.EntityFake?.StringValue == "_string", "Bad obj element set - Count");
        }

        [Test, Order(1)]
        public void CreateTest()
        {
            _wrapper = new MetaWrapperFake()
            {
                EntityFake = new WrapperClassFake()
                {
                    EnumValue = ActionPriorities.Medium,
                    StringValue = "_string"
                }
            };
        }

        // Xml

        [Test, Order(5)]
        public void SaveXmlTest()
        {
            if (_wrapper == null)
            {
                CreateTest();
            }

            _wrapper.UpdateDetail();
            var isSaved = _wrapper.Detail.ToDto().SaveXml(_filePath_xml);
            Assert.That(isSaved, "Meta list saving failed. ");
        }

        [Test, Order(6)]
        public void LoadXmlTest()
        {
            if (_wrapper == null || !File.Exists(_filePath_xml))
            {
                SaveXmlTest();
            }

            var config = XmlHelper.LoadXml<ConfigurationDto>(_filePath_xml).ToPoco();
            var wrapper = SystemData.Scope.NewMetaWrapper<MetaWrapperFake>(config);
            Test(wrapper);
        }

        // Json

        [Test, Order(5)]
        public void SaveJsonTest()
        {
            if (_wrapper == null)
            {
                CreateTest();
            }

            var isSaved = _wrapper.Detail.ToDto().SaveJson(_filePath_json);
            Assert.That(isSaved, "Meta list saving failed. ");
        }

        [Test, Order(6)]
        public void LoadJsonTest()
        {
            if (_wrapper == null || !File.Exists(_filePath_json))
            {
                SaveJsonTest();
            }

            var config = JsonHelper.LoadJson<ConfigurationDto>(_filePath_json).ToPoco();
            var wrapper = SystemData.Scope.NewMetaWrapper<MetaWrapperFake>(config);
            Test(wrapper);
        }
    }
}
