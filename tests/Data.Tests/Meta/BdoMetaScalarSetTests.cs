using BindOpen.Tests;
using FluentAssertions;
using NUnit.Framework;

namespace BindOpen.Data.Meta;

/// <summary>
/// Contains unit tests for sets of scalar metadata, focusing on collection management and data updates.
/// </summary>
/// <remarks>
/// These tests ensure that <see cref="IBdoMetaSet"/> correctly handles multiple <see cref="IBdoMetaScalar"/> elements,
/// supports complex update/repair scenarios, and allows access via both names and custom keys.
/// </remarks>
[TestFixture, Order(202)]
public class BdoMetaScalarSetTests
{
    private dynamic _testData;

    /// <summary>
    /// The global meta set used across tests to verify state persistence.
    /// </summary>
    public IBdoMetaSet _metaSet;

    /// <summary>
    /// Initializes randomized test data.
    /// </summary>
    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        _testData = BdoMetaScalarFaker.Fake();
    }

    /// <summary>
    /// Verifies that a scalar can be created even with null data.
    /// </summary>
    [Test, Order(1)]
    public void NewTest_WithNull()
    {
        var el1 = BdoData.NewScalar("null1", null);
        el1.Should().NotBeNull("A scalar element should be instantiable even with a null value.");
    }

    /// <summary>
    /// Tests the population of a meta set with various scalar types (Number, String, Integer, Binary).
    /// </summary>
    [Test, Order(2)]
    public void NewTest()
    {
        double[] arrayNumber = _testData.arrayNumber;
        string[] arrayString = _testData.arrayString;
        int[] arrayInteger = _testData.arrayInteger;
        byte[][] arrayArrayByte = _testData.arrayArrayByte;

        var el1 = BdoData.NewScalar("number1", arrayNumber);
        var el2 = BdoData.NewScalar("text2", arrayString);
        var el3 = BdoData.NewScalar("integer3", arrayInteger);
        var el4 = BdoData.NewScalar("byteArray4", arrayArrayByte);

        _metaSet = BdoData.NewSet(el1, el2, el3, el4);

        // Validation of data retrieval by key
        _metaSet.GetDataList<double>("number1").Should().Contain(arrayNumber);
        _metaSet.GetDataList<string>("text2").Should().Contain(arrayString);
        _metaSet.GetDataList<int>("integer3").Should().Contain(arrayInteger);

        var item4 = _metaSet.GetDataList<byte[]>("byteArray4");
        item4[0].Should().Equal(arrayArrayByte[0], "Byte arrays must maintain sequence integrity within a set.");
    }

    /// <summary>
    /// Tests complex update and repair scenarios between meta sets, including null handling and type switching.
    /// </summary>
    [Test, Order(3)]
    public void UpdateCheckRepairTest()
    {
        var elAA = BdoData.NewScalar<int?>("name1", null);
        var elAB = BdoData.NewScalar("name1", "Test1");
        elAA.Update(elAB);

        var elSetA = BdoData.NewSet(elAA, elAB);

        var elBA = BdoData.NewScalar("name1", "Test1");
        var elBB = BdoData.NewScalar<int?>("name1", null);
        elBA.Update(elBB);

        var elSetB = BdoData.NewSet(elBA, elBB);

        TBdoSetExtensions.AddItems(elSetB, elBB);
        TBdoSetExtensions.AddItems(elSetA, elAB);
        elSetB.Update(elSetA);

        // Advanced repair testing: adding nulls and type-based placeholders
        TBdoSetExtensions.AddItems(elSetA, (IBdoMetaData[])null);
        TBdoSetExtensions.AddItems(elSetB
, (IBdoMetaScalar)null)
            .AddItems(BdoData.NewScalar("name1", typeof(string)))
            .AddItems(BdoData.NewScalar("name3", typeof(int)))
            .AddItems(BdoData.NewScalar("name4", typeof(double)))
            .AddItems(BdoData.NewScalar("name5", DataValueTypes.Text));

        TBdoSetExtensions.AddItems(elSetA
, BdoData.NewScalar("name1", typeof(string)))
            .AddItems((IBdoMetaData)BdoData.NewObject("name2", null as string))
            .AddItems(BdoData.NewScalar("name4", DataValueTypes.Text, null))
            .AddItems(BdoData.NewScalar("name5", null as string));

        elSetB.Update(elSetA);

        _metaSet = elSetB;
        _metaSet.Should().NotBeNull("The meta set should remain valid after multiple update/repair operations.");
    }

    /// <summary>
    /// Validates the string conversion of scalars within a set for different data types.
    /// </summary>
    [Test, Order(4)]
    public void ToStringTest()
    {
        string[] arrayString = _testData.arrayString;
        var el = BdoData.NewScalar(DataValueTypes.Text, arrayString[0]);
        el.ToString().Should().Be(arrayString[0]);

        int[] arrayInteger = _testData.arrayInteger;
        el = BdoData.NewScalar(DataValueTypes.Text, arrayInteger[0]);
        el.ToString().Should().Be(arrayInteger[0].ToString());
    }

    /// <summary>
    /// Tests the ability to add elements to a set using explicit keys and retrieving them.
    /// </summary>
    [Test, Order(5)]
    public void AddMetaSetWithKeys()
    {
        var elAA = BdoData.NewScalar("name1", null);
        var elAB = BdoData.NewScalar("name1", "Test1");
        elAA.Update(elAB);

        // Mix of keys, nulls, and direct object references
        var elSetA = BdoData.NewSet(("key1", elAA), (null, elAB), ("key2", this));

        elSetA.Count.Should().Be(3);
        elSetA.Get("key1").Should().NotBeNull("Retrieval by custom key 'key1' should work.");
        elSetA.Get("name1").Should().NotBeNull("Retrieval by element name 'name1' should work.");
        elSetA.GetData("key2").Should().Be(this, "Retrieval of raw data via key should return the original object.");
    }
}