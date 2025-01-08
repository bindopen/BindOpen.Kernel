using BindOpen.Tests;
using NUnit.Framework;
using System.IO;

namespace BindOpen.Data.Meta;

[TestFixture, Order(201)]
public class MetaWrapperJsonTests
{
    private readonly string _filePath_json = DataTestData.WorkingFolder + "MetaWrapper.json";

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
    public void SaveJsonTest()
    {
        if (_wrapper == null)
        {
            CreateTest();
        }

        _wrapper.UpdateDetail();
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
        var wrapper = ScopingTestData.Scope.NewMetaWrapper<MetaWrapperFake>(config);
        Test(wrapper);
    }
}
