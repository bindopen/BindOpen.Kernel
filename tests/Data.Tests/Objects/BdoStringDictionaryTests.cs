using BindOpen.Data.Helpers;
using BindOpen.Data.Objects;
using Bogus;
using FluentAssertions;
using NUnit.Framework;

namespace BindOpen.Data;

/// <summary>
/// Contains unit tests for <see cref="TBdoDictionary{T}"/>, focusing on specialized string key access.
/// </summary>
/// <remarks>
/// These tests verify advanced dictionary features such as wildcard keys (<c>"*"</c>),
/// implicit conversions from strings and tuple arrays, duplicate key handling, and multi-key fallback resolution.
/// </remarks>
[TestFixture]
public class BdoStringDictionaryTests
{
    private dynamic _valueSet;

    /// <summary>
    /// Gets or sets the <see cref="TBdoDictionary{T}"/> string instance currently under test.
    /// </summary>
    public TBdoDictionary<string> _dico = null;

    /// <summary>
    /// Initializes randomized string values for dictionary testing before any tests in this fixture are executed.
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
    /// Validates that the core entries of the specified <see cref="ITBdoDictionary{T}"/> match the expected test dataset.
    /// </summary>
    /// <param name="dico">The string dictionary instance to validate.</param>
    public void Test(ITBdoDictionary<string> dico)
    {
        dico["value1"].Should().Be((string)_valueSet.value1);
        dico["value2"].Should().Be((string)_valueSet.value2);
        dico["value3"].Should().Be((string)_valueSet.value3);
    }

    /// <summary>
    /// Verifies dictionary instantiation using <see cref="BdoData.NewDictionary{T}"/> with tuple arguments,
    /// ensuring duplicate keys are handled gracefully.
    /// </summary>
    [Test]
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
    /// Verifies dictionary creation via implicit conversion from an array of key-value tuples.
    /// </summary>
    [Test]
    public void Create2Test()
    {
        _dico = new[] {
            ("value1", _valueSet.value1 as string),
            ("value2", _valueSet.value2 as string),
            ("value3", _valueSet.value3 as string) };

        Test(_dico);
    }

    /// <summary>
    /// Verifies implicit conversion from a single <see cref="string"/> into a dictionary containing a wildcard (<see cref="StringHelper.__Star"/>) entry.
    /// </summary>
    [Test]
    public void Create3Test()
    {
        _dico = _valueSet.valueStar as string;

        _dico[StringHelper.__Star].Should().Be((string)_valueSet.valueStar);
        _dico.Get().Should().Be((string)_valueSet.valueStar);
    }

    /// <summary>
    /// Verifies reflexive equality evaluation for <see cref="ITBdoDictionary{T}"/> instances.
    /// </summary>
    [Test]
    public void EqualsTest()
    {
        var dico1 = BdoData.NewDictionary(
            ("value1", _valueSet.value1),
            ("value2", _valueSet.value2),
            ("value3", _valueSet.value3));

        dico1.Equals(dico1).Should().BeTrue("A dictionary must be equal to itself.");
    }

    /// <summary>
    /// Verifies multi-key fallback resolution, missing key handling (<see langword="null"/> return),
    /// and wildcard (<see cref="StringHelper.__Star"/>) fallback behavior.
    /// </summary>
    [Test]
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