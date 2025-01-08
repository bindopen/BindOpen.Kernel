using BindOpen.Tests;
using NUnit.Framework;
using System.IO;

namespace BindOpen.Data.Meta;

[TestFixture, Order(201)]
public class MetaWrapperXmlTests
{
    private readonly string _filePath_xml = DataTestData.WorkingFolder + "MetaWrapper.xml";

    private MetaWrapperFake _wrapper;

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
    }

    private void Test(MetaWrapperFake wrapper)
    {
        Assert.That(wrapper?.SubEnumValue == AccessibilityLevels.Public, "Bad obj element set - Count");
        Assert.That(wrapper?.EntityFake?.StringValue == "_string", "Bad obj element set - Count");
    }

    [Test, Order(1)]
    public void CreateTest()
    {
        _wrapper = new MetaWrapperFake()
        {
            EntityFake = new WrapperClassFake()
            {
                EnumValue = AccessibilityLevels.Public,
                StringValue = "_string"
            }
        };
    }

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
        var wrapper = ScopingTestData.Scope.NewMetaWrapper<MetaWrapperFake>(config);
        Test(wrapper);
    }
}
