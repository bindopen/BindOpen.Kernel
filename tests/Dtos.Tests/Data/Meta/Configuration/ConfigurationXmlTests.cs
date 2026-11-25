using NUnit.Framework;
using System.IO;

namespace BindOpen.Data.Meta;

/// <summary>
/// Contains unit tests for XML serialization and deserialization of <see cref="IBdoConfiguration"/>
/// instances via <see cref="ConfigurationDto"/> conversion.
/// </summary>
/// <remarks>
/// These tests verify that configurations created by <see cref="BdoConfigurationTests"/> can be
/// mapped to DTOs, persisted to XML files on disk, and reloaded back into domain objects without losing
/// structural or property equality.
/// </remarks>
[TestFixture]
public class ConfigurationXmlTests
{
    private readonly string _filePath_xml = DataTestData.WorkingFolder + "Configuration{0}.xml";
    private BdoConfigurationTests _dataTests;
    private bool _isSaved1 = false;
    private bool _isSaved2 = false;

    /// <summary>
    /// Cleans up previous test output files and initializes the shared <see cref="BdoConfigurationTests"/>
    /// helper before any tests in this fixture are executed.
    /// </summary>
    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        File.Delete(string.Format(_filePath_xml, 1));
        File.Delete(string.Format(_filePath_xml, 2));

        _dataTests = new BdoConfigurationTests();
        _dataTests.OneTimeSetUp();
    }

    // Test 1

    /// <summary>
    /// Verifies that the first test <see cref="IBdoConfiguration"/> instance converts to a
    /// <see cref="ConfigurationDto"/> and saves cleanly to an XML file.
    /// </summary>
    [Test]
    public void SaveXml1Test()
    {
        var exp = _dataTests._config10;

        var filePath = string.Format(_filePath_xml, 1);

        _isSaved1 = exp.ToDto().SaveXml(filePath);
        Assert.That(_isSaved1, Is.True, "Configuration saving failed.");
    }

    /// <summary>
    /// Verifies that the first saved XML file loads back into a <see cref="ConfigurationDto"/>,
    /// converts to a POCO via <c>ToPoco()</c>, and matches the original <see cref="IBdoConfiguration"/>.
    /// </summary>
    [Test]
    public void LoadXml1Test()
    {
        if (!_isSaved1)
        {
            SaveXml1Test();
        }

        var filePath = string.Format(_filePath_xml, 1);

        var exp = _dataTests._config10;
        var exp_fromDto = XmlHelper.LoadXml<ConfigurationDto>(filePath).ToPoco();
        BdoConfigurationTests.AssertEquals(exp, exp_fromDto);
    }

    // Test 2

    /// <summary>
    /// Verifies that the second test <see cref="IBdoConfiguration"/> instance converts to a
    /// <see cref="ConfigurationDto"/> and saves cleanly to an XML file.
    /// </summary>
    [Test]
    public void SaveXml2Test()
    {
        var exp = _dataTests._config20;

        var filePath = string.Format(_filePath_xml, 2);

        _isSaved2 = exp.ToDto().SaveXml(filePath);
        Assert.That(_isSaved2, Is.True, "Configuration saving failed.");
    }

    /// <summary>
    /// Verifies that the second saved XML file loads back into a <see cref="ConfigurationDto"/>,
    /// converts to a POCO via <c>ToPoco()</c>, and matches the original <see cref="IBdoConfiguration"/>.
    /// </summary>
    [Test]
    public void LoadXml2Test()
    {
        if (!_isSaved2)
        {
            SaveXml2Test();
        }

        var filePath = string.Format(_filePath_xml, 2);

        var exp = _dataTests._config20;
        var exp_fromDto = XmlHelper.LoadXml<ConfigurationDto>(filePath).ToPoco();
        BdoConfigurationTests.AssertEquals(exp, exp_fromDto);
    }
}