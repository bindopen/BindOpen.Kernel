using BindOpen.Data.Helpers;
using BindOpen.Data.Meta;
using Bogus;
using NUnit.Framework;

namespace BindOpen.Data;

/// <summary>
/// Contains unit tests for string tokenization and metadata extraction.
/// </summary>
/// <remarks>
/// These tests verify the framework's ability to parse structured strings using token patterns,
/// handle quoted text delimiters, map extracted tokens to variables, and format strings back
/// from <see cref="IBdoMetaSet"/> instances.
/// </remarks>
[TestFixture]
public class BdoStringTokensTests
{
    private dynamic _valueSet;

    /// <summary>
    /// Initializes randomized string values and token keys before any tests in this fixture are executed.
    /// </summary>
    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        var f = new Faker();
        _valueSet = new
        {
            preffix = f.Random.Word(),
            value1 = f.Random.Word(),
            middle = f.Random.Word(),
            value2 = f.Random.Word(),
            suffix = f.Random.Word(),
            name_value1 = "value1",
            name_value2 = "value2"
        };
    }

    /// <summary>
    /// Validates that the extracted <see cref="IBdoMetaSet"/> contains the expected token values.
    /// </summary>
    /// <param name="set">The metadata set resulting from token extraction.</param>
    private void Test(IBdoMetaSet set)
    {
        Assert.Multiple(() =>
        {
            Assert.That(set.GetData<string>(_valueSet.name_value1), Is.EqualTo(_valueSet.value1 as string), "Token 1 mismatch.");
            Assert.That(set.GetData<string>(_valueSet.name_value2), Is.EqualTo(_valueSet.value2 as string), "Token 2 mismatch.");
        });
    }

    /// <summary>
    /// Verifies token extraction from a delimited string bounded by both a prefix and a suffix.
    /// </summary>
    [Test]
    public void Create1Test()
    {
        // Pattern: prefix{{value1}}middle{{value2}}suffix
        string st = _valueSet.preffix + _valueSet.value1 + _valueSet.middle + _valueSet.value2 + _valueSet.suffix;
        string pattern = _valueSet.preffix + "{{" + _valueSet.name_value1 + "}}" + _valueSet.middle + "{{" + _valueSet.name_value2 + "}}" + _valueSet.suffix;

        var set = st.ExtractTokenMetas(pattern);
        Test(set);
    }

    /// <summary>
    /// Verifies token extraction from a string starting directly with a token placeholder (no leading prefix).
    /// </summary>
    [Test]
    public void Create2Test()
    {
        // Pattern without prefix: {{value1}}middle{{value2}}suffix
        string st = _valueSet.value1 + _valueSet.middle + _valueSet.value2 + _valueSet.suffix;
        string pattern = "{{" + _valueSet.name_value1 + "}}" + _valueSet.middle + "{{" + _valueSet.name_value2 + "}}" + _valueSet.suffix;

        var set = st.ExtractTokenMetas(pattern);
        Test(set);
    }

    /// <summary>
    /// Verifies token extraction when token values are enclosed within double-quote delimiters.
    /// </summary>
    [Test]
    public void Create5Test()
    {
        // Test with quoted strings: "value1":value2
        string st = (_valueSet.value1 as string).ToQuoted('"') + ":" + _valueSet.value2;
        string pattern = "{{" + _valueSet.name_value1 + "}}" + ":" + "{{" + _valueSet.name_value2 + "}}";

        var set = st.ExtractTokenMetas(pattern, '"');
        Test(set);
    }

    /// <summary>
    /// Verifies structured token extraction using the predefined <see cref="LabelFormats.NameSpaceValue"/> format
    /// with escaped double quotes inside the name token.
    /// </summary>
    [Test]
    public void CreateTest_NameSpaceValue()
    {
        // Test structured parsing using LabelFormats (standard Namespace Value format)
        var name = @"toto ""max";
        string st = name.ToQuoted('"') + " " + _valueSet.value2;
        string pattern = LabelFormats.NameSpaceValue.GetScript();

        var set = st.ExtractTokenMetas(pattern, '"');

        Assert.Multiple(() =>
        {
            Assert.That(set.GetData<string>(BdoMetaDataProperties.Name), Is.EqualTo(name));
            Assert.That(set.GetData<string>(BdoMetaDataProperties.Value), Is.EqualTo(_valueSet.value2 as string));
        });
    }

    /// <summary>
    /// Verifies that <c>Map</c> binds extracted token metadata entries directly to local variables.
    /// </summary>
    [Test]
    public void ExtractTokenMetasTest_Map()
    {
        // Test the Map function to bind extracted tokens to local variables
        var name = @"toto ""max";
        string st = name.ToQuoted('"') + " " + _valueSet.value2;
        string pattern = LabelFormats.NameSpaceValue.GetScript();

        string st_name = null;
        string st_value = null;

        var set = st.ExtractTokenMetas(pattern, '"');
        set?.Map(
            (BdoMetaDataProperties.Name, q => { st_name = q.GetData<string>(); }
        ),
            (BdoMetaDataProperties.Value, q => { st_value = q.GetData<string>(); }
        )
        );

        Assert.That(st_name == name && st_value == _valueSet.value2, Is.True, "Mapping from extracted tokens failed.");
    }

    /// <summary>
    /// Verifies string interpolation from a token pattern and an <see cref="IBdoMetaSet"/> using <c>FormatFromTokens</c>.
    /// </summary>
    [Test]
    public void FormatFromTokensTest()
    {
        // Reverse operation: building a string from a pattern and a metadata set
        string pattern = LabelFormats.NameSpaceValue.GetScript();

        var st = pattern.FormatFromTokens(
            BdoData.NewSet(
                (BdoMetaDataProperties.Name, "_name"),
                (BdoMetaDataProperties.Value, "_value")));

        Assert.That(st, Is.EqualTo("_name _value"), "Formatting string from tokens failed.");
    }
}