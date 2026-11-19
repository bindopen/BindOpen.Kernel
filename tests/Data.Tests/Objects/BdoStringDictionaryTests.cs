using BindOpen.Data.Helpers;
using BindOpen.Data.Objects.Interfaces;
using Bogus;
using FluentAssertions;
using NUnit.Framework;

namespace BindOpen.Data;

/// <summary>
/// Contains unit tests for <see cref="TBdoDictionary{T}"/>, focusing on specialized string key access.
/// </summary>
/// <remarks>
/// These tests verify advanced dictionary features such as wildcard keys ("*"), 
/// implicit conversions, and fallback value resolution.
/// </remarks>
[TestFixture, Order(210)]
public class BdoStringDictionaryTests
{
    private dynamic _valueSet;

    /// <summary>
    /// The string dictionary instance currently being tested.
    /// </summary>
    public TBdoDictionary<string> _dico = null;

    /// <summary>
    /// Initializes randomized string values for dictionary testing.
    /// </summary>
    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        var f = new Faker();
        _valueSet = new
        {
            valueStar = f.Random.Word(),
            value1 = f.Random.Word(),
            value2 = f.Random.Word(),
            value3 = f.Random.Word()
        };
    }

    /// <summary>
    /// Validates the core content of the dictionary against the expected value set.
    /// </summary>
    /// <param name="dico">The dictionary to validate.</param>
    public void Test(ITBdoDictionary<string> dico)
    {
        dico["value1"].Should().Be((string)_valueSet.value1);
        dico["value2"].Should().Be((string)_valueSet.value2);
        dico["value3"].Should().Be((string)_valueSet.value3);
    }

    /// <summary>
    /// Tests dictionary creation using the factory method with tuple arrays.
    /// </summary>
    [Test, Order(1)]
    public void Create1Test()
    {
        // Tests duplicate key handling and standard initialization
        _dico = BdoData.NewDictionary<string>(
            ("value1", _valueSet.value1),
            ("value1", _valueSet.value1),
            ("value2", _valueSet.value2),
            ("value3", _valueSet.value3));

        Test(_dico);
    }

    /// <summary>
    /// Tests dictionary creation via implicit conversion from a tuple array.
    /// </summary>
    [Test, Order(2)]
    public void Create2Test()
    {
        _dico = new[] {
            ("value1", _valueSet.value1 as string),
            ("value2", _valueSet.value2 as string),
            ("value3", _valueSet.value3 as string) };

        Test(_dico);
    }

    /// <summary>
    /// Tests implicit conversion from a single string to a wildcard dictionary entry.
    /// </summary>
    [Test, Order(3)]
    public void Create3Test()
    {
        _dico = _valueSet.valueStar as string;

        _dico[StringHelper.__Star].Should().Be((string)_valueSet.valueStar);
        _dico.Get().Should().Be((string)_valueSet.valueStar);
    }

    /// <summary>
    /// Verifies the custom equality logic for dictionaries.
    /// </summary>
    [Test, Order(4)]
    public void EqualsTest()
    {
        var dico1 = BdoData.NewDictionary(
            ("value1", _valueSet.value1),
            ("value2", _valueSet.value2),
            ("value3", _valueSet.value3));

        dico1.Equals(dico1).Should().BeTrue("A dictionary must be equal to itself.");
    }

    /// <summary>
    /// Tests advanced fallback resolution using wildcards and multiple keys.
    /// </summary>
    [Test, Order(5)]
    public void Create5Test()
    {
        _dico = new[] {
            (StringHelper.__Star, _valueSet.valueStar as string),
            ("value1", _valueSet.value1 as string),
            ("value2", _valueSet.value2 as string),
            ("value3", _valueSet.value3 as string) };

        // Test wildcard access
        _dico[StringHelper.__Star].Should().Be((string)_valueSet.valueStar);

        // Test direct access
        _dico["value1"].Should().Be((string)_valueSet.value1);

        // Test fallback: find 'value2' even if a prefix/context is provided
        _dico["hgh", "value2"].Should().Be((string)_valueSet.value2);

        // Test missing key: should be null
        _dico["value4"].Should().BeNull();

        // Test wildcard fallback: if 'value5' is missing, use the star value
        _dico["value5", StringHelper.__Star].Should().Be((string)_valueSet.valueStar);
    }
}