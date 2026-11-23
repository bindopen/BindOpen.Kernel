using NUnit.Framework;
using System.IO;

namespace BindOpen.Data.Meta;

/// <summary>
/// Contains unit tests for JSON serialization and deserialization of <see cref="IBdoConfiguration"/>
/// instances via <see cref="ConfigurationDto"/> conversion.
/// </summary>
/// <remarks>
/// These tests verify that configurations created by <see cref="BdoConfigurationTests"/> can be
/// mapped to DTOs, persisted to JSON files on disk, and reloaded back into domain objects without losing
/// structural or property equality.
/// </remarks>
[TestFixture]
public class ConfigurationJsonTests
{
    private readonly string _filePath_json = DataTestData.WorkingFolder + "Configuration{0}.json";
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
        File.Delete(string.Format(_filePath_json, 1));
        File.Delete(string.Format(_filePath_json, 2));

        _dataTests = new BdoConfigurationTests();
        _dataTests.OneTimeSetUp();
    }

    // Test 1

    /// <summary>
    /// Verifies that the first test <see cref="IBdoConfiguration"/> instance converts to a
    /// <see cref="ConfigurationDto"/> and saves cleanly to a JSON file.
    /// </summary>
    [Test]
    public void SaveJson1Test()
    {
        var config = _dataTests._config10;

        var filePath = string.Format(_filePath_json, 1);

        _isSaved1 = config.ToDto().SaveJson(filePath);
        Assert.That(_isSaved1, Is.True, "Configuration saving failed.");
    }

    /// <summary>
    /// Verifies that the first saved JSON file loads back into a <see cref="ConfigurationDto"/>,
    /// converts to a POCO via <c>ToPoco()</c>, and matches the original <see cref="IBdoConfiguration"/>.
    /// </summary>
    [Test]
    public void LoadJson1Test()
    {
        if (!_isSaved1)
        {
            SaveJson1Test();
        }

        var filePath = string.Format(_filePath_json, 1);

        var config = _dataTests._config10;
        var config_fromDto = JsonHelper.LoadJson<ConfigurationDto>(filePath).ToPoco();
        BdoConfigurationTests.AssertEquals(config, config_fromDto);
    }

    // Test 2

    /// <summary>
    /// Verifies that the second test <see cref="IBdoConfiguration"/> instance converts to a
    /// <see cref="ConfigurationDto"/> and saves cleanly to a JSON file.
    /// </summary>
    [Test]
    public void SaveJson2Test()
    {
        var config = _dataTests._config20;

        var filePath = string.Format(_filePath_json, 2);

        _isSaved2 = config.ToDto().SaveJson(filePath);
        Assert.That(_isSaved2, Is.True, "Configuration saving failed.");
    }

    /// <summary>
    /// Verifies that the second saved JSON file loads back into a <see cref="ConfigurationDto"/>,
    /// converts to a POCO via <c>ToPoco()</c>, and matches the original <see cref="IBdoConfiguration"/>.
    /// </summary>
    [Test]
    public void LoadJson2Test()
    {
        if (!_isSaved2)
        {
            SaveJson2Test();
        }

        var filePath = string.Format(_filePath_json, 2);

        var config = _dataTests._config20;
        var config_fromDto = JsonHelper.LoadJson<ConfigurationDto>(filePath).ToPoco();
        BdoConfigurationTests.AssertEquals(config, config_fromDto);
    }
}