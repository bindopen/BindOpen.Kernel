using NUnit.Framework;
using System.IO;

namespace BindOpen.Data.Meta;

[TestFixture, Order(210)]
public class ConfigurationXmlTests
{
    private readonly string _filePath_json = DataTestData.WorkingFolder + "Configuration{0}.json";
    private BdoConfigurationTests _dataTests;
    private bool _isSaved1 = false;
    private bool _isSaved2 = false;

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        File.Delete(_filePath_json);

        _dataTests = new BdoConfigurationTests();
        _dataTests.OneTimeSetUp();
    }

    // Test 1

    [Test, Order(10)]
    public void SaveXml1Test()
    {
        var exp = _dataTests._config10;

        var filePath = string.Format(_filePath_json, 1);

        _isSaved1 = exp.ToDto().SaveXml(filePath);
        Assert.That(_isSaved1, "Configuration saving failed");
    }

    [Test, Order(11)]
    public void LoadXml1Test()
    {
        if (!_isSaved1)
        {
            SaveXml1Test();
        }

        var filePath = string.Format(_filePath_json, 1);

        var exp = _dataTests._config10;
        var exp_fromDto = XmlHelper.LoadXml<ConfigurationDto>(filePath).ToPoco();
        BdoConfigurationTests.AssertEquals(exp, exp_fromDto);
    }

    // Test 2

    [Test, Order(20)]
    public void SaveXml2Test()
    {
        var exp = _dataTests._config20;

        var filePath = string.Format(_filePath_json, 2);

        _isSaved2 = exp.ToDto().SaveXml(filePath);
        Assert.That(_isSaved2, "Configuration saving failed");
    }

    [Test, Order(21)]
    public void LoadXml2Test()
    {
        if (!_isSaved2)
        {
            SaveXml2Test();
        }

        var filePath = string.Format(_filePath_json, 2);

        var exp = _dataTests._config20;
        var exp_fromDto = XmlHelper.LoadXml<ConfigurationDto>(filePath).ToPoco();
        BdoConfigurationTests.AssertEquals(exp, exp_fromDto);
    }
}
