using BindOpen.Tests;
using NUnit.Framework;
using System;
using System.Linq;

namespace BindOpen.Data.Meta;

[TestFixture, Order(202)]
public class BdoMetaScalarSetTests
{
    private dynamic _testData;
    public IBdoMetaSet _metaSet;

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        _testData = BdoMetaScalarFaker.Fake();
    }

    [Test, Order(1)]
    public void NewTest_WithNull()
    {
        var el1 = BdoData.NewScalar("null1", null);

        Assert.That(
            el1 != null, "Bad scalar el creation");
    }

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

        var itemList1 = _metaSet.GetDataList<double>("number1");
        Assert.That(
            itemList1?.Intersect(arrayNumber).Any() ?? false, "Bad scalar el - Number");

        var itemList2 = _metaSet.GetDataList<string>("text2");
        Assert.That(
            itemList2?.Intersect(arrayString).Any() ?? false, "Bad scalar el - String");

        var itemList3 = _metaSet.GetDataList<int>("integer3");
        Assert.That(
            itemList3?.Intersect(arrayInteger).Any() ?? false, "Bad scalar el - Integer");

        var item4 = _metaSet.GetDataList<byte[]>("byteArray4");
        Assert.That(
            item4[0]?.SequenceEqual(arrayArrayByte[0]) == true
            , "Bad scalar el - Byte array");
    }

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

        elSetB.Add(elBB);
        elSetA.Add(elAB);
        elSetB.Update(elSetA);

        elSetA.Add<IBdoMetaData>(null);
        elSetB
            .Add((IBdoMetaScalar)null)
            .Add(BdoData.NewScalar("name1", typeof(string)))
            .Add(BdoData.NewScalar("name3", typeof(int)))
            .Add(BdoData.NewScalar("name4", typeof(double)))
            .Add(BdoData.NewScalar("name5", DataValueTypes.Text));
        elSetA
            .Add(BdoData.NewScalar("name1", typeof(string)))
            .Add(BdoData.NewObject("name2", null as string))
            .Add(BdoData.NewScalar("name4", DataValueTypes.Text, null))
            .Add(BdoData.NewScalar("name5", null as string));
        elSetB.Update(elSetA);

        _metaSet = elSetB;
    }

    [Test, Order(4)]
    public void ToStringTest()
    {
        string[] arrayString = _testData.arrayString;

        var el = BdoData.NewScalar(DataValueTypes.Text, arrayString[0]);
        var st = el.ToString();
        Assert.That(st == arrayString[0], "Bad scalar el - ToString");

        int[] arrayInteger = _testData.arrayInteger;

        el = BdoData.NewScalar(DataValueTypes.Text, _testData.arrayInteger[0]);
        st = el.ToString();
        Assert.That(st == arrayInteger[0].ToString(), "Bad scalar el - ToString");
    }

    [Test, Order(5)]
    public void AddMetaSetWithKeys()
    {
        var elAA = BdoData.NewScalar("name1", null);
        var elAB = BdoData.NewScalar("name1", "Test1");
        elAA.Update(elAB);

        var elSetA = BdoData.NewSet(("key1", elAA), (null, elAB), ("key2", this));

        Assert.That(elSetA.Count == 3, "Bad scalar el - ToString");

        var el1 = elSetA.Get("key1");
        Assert.That(el1 != null, "Bad scalar el - ToString");
        var el2 = elSetA.Get("name1");
        Assert.That(el2 != null, "Bad scalar el - ToString");
        var el3 = elSetA.GetData("key2");
        Assert.That(el3 == this, "Bad scalar el - ToString");
    }
}
