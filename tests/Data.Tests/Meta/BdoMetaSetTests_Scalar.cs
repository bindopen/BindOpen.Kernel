using BindOpen.Tests;
using FluentAssertions;
using NUnit.Framework;

namespace BindOpen.Data.Meta;

/// <summary>
/// Contains unit tests for scalar metadata elements, focusing on data initialization and type resolution.
/// </summary>
/// <remarks>
/// These tests verify that <see cref="IBdoMetaScalar"/> correctly stores, resolves, and retrieves
/// various <see cref="DataValueTypes"/> (Number, Text, Integer, Binary) using randomized test data.
/// </remarks>
[TestFixture]
public class BdoMetaSetTests_Scalar
{
    private dynamic _testData;

    /// <summary>
    /// Initializes randomized scalar test data before any tests in this fixture are executed.
    /// </summary>
    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        _testData = BdoMetaScalarFaker.Fake();
    }

    /// <summary>
    /// Verifies the creation and type resolution of numeric (<see cref="DataValueTypes.Number"/>) scalar elements
    /// using both explicit type definitions and generic type inference.
    /// </summary>
    [Test]
    public void NewTest_Number()
    {
        double[] items = _testData.arrayNumber;

        // Test 1: Explicit type definition
        var el1 = BdoData.NewScalar("number1", DataValueTypes.Number, items);
        var itemList = el1.GetDataList<double>();

        itemList.Should().Contain(items, "The scalar should correctly store the provided numeric items.");
        el1.DataType.ValueType.Should().Be(DataValueTypes.Number, "The data type should be identified as Number.");

        // Test 2: Generic type inference
        var el2 = BdoData.NewScalar<double>("number1", items);
        itemList = el2.GetDataList<double>();

        itemList.Should().Contain(items, "The generic scalar should correctly store the provided numeric items.");
        el2.DataType.ValueType.Should().Be(DataValueTypes.Number, "The generic data type should be identified as Number.");
    }

    /// <summary>
    /// Verifies the creation and type resolution of text (<see cref="DataValueTypes.Text"/>) scalar elements.
    /// </summary>
    [Test]
    public void NewTest_String()
    {
        string[] items = _testData.arrayString;
        var el = BdoData.NewScalar("text2", items);

        var itemList = el.GetDataList<string>();

        itemList.Should().Contain(items);
        el.DataType.ValueType.Should().Be(DataValueTypes.Text);
    }

    /// <summary>
    /// Verifies the creation and type resolution of integer (<see cref="DataValueTypes.Integer"/>) scalar elements.
    /// </summary>
    [Test]
    public void NewTest_Integer()
    {
        int[] items = _testData.arrayInteger;
        var el = BdoData.NewScalar("integer3", items);

        var itemList = el.GetDataList<int>();

        itemList.Should().Contain(items);
        el.DataType.ValueType.Should().Be(DataValueTypes.Integer);
    }

    /// <summary>
    /// Verifies the creation, sequence integrity, and type resolution of binary (<see cref="DataValueTypes.Binary"/>) scalar elements.
    /// </summary>
    [Test]
    public void NewTest_ArrayByte()
    {
        byte[][] items = _testData.arrayArrayByte;
        var el = BdoData.NewScalar("byteArray4", items);

        var itemList = el.GetDataList<byte[]>();

        itemList[0].Should().Equal(items[0], "The first byte array should match exactly.");
        itemList[1].Should().Equal(items[1], "The second byte array should match exactly.");
        el.DataType.ValueType.Should().Be(DataValueTypes.Binary, "The data type should be identified as Binary.");
    }

    /// <summary>
    /// Verifies that <see cref="object.ToString"/> returns a valid non-null representation for integer and numeric scalar elements.
    /// </summary>
    [Test]
    public void ToStringTest()
    {
        int[] items_integer = _testData.arrayInteger;
        var el = BdoData.NewScalar().WithData(items_integer);
        el.ToString().Should().NotBeNull("ToString should provide a non-null representation of integer scalars.");

        double[] items_number = _testData.arrayNumber;
        el = BdoData.NewScalar().WithData(items_number);
        el.ToString().Should().NotBeNull("ToString should provide a non-null representation of numeric scalars.");
    }
}