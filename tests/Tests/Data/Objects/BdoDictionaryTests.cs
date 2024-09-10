using BindOpen.Data.Helpers;
using Bogus;
using NUnit.Framework;

namespace BindOpen.Data;

[TestFixture, Order(210)]
public class BdoDictionaryTests
{
    dynamic _valueSet;
    public TBdoDictionary<string> _dico = null;


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

    public void Test(ITBdoDictionary<string> dico)
    {
        Assert.That(
            dico["value1"] == _valueSet.value1
            && dico["value2"] == _valueSet.value2
            && dico["value3"] == _valueSet.value3, "Bad dictionary creation");
    }

    [Test, Order(1)]
    public void Create1Test()
    {
        _dico = BdoData.NewDictionary<string>(
            ("value1", _valueSet.value1),
            ("value1", _valueSet.value1),
            ("value2", _valueSet.value2),
            ("value3", _valueSet.value3));

        Test(_dico);
    }

    [Test, Order(2)]
    public void Create2Test()
    {
        _dico = new[] {
            ("value1", _valueSet.value1 as string),
            ("value2", _valueSet.value2 as string),
            ("value3", _valueSet.value3 as string) };

        Test(_dico);
    }

    [Test, Order(3)]
    public void Create3Test()
    {
        _dico = _valueSet.valueStar as string;

        Assert.That(
            _dico[StringHelper.__Star] == _valueSet.valueStar as string, "Bad dictionary creation");
        Assert.That(
            _dico.Get() == _valueSet.valueStar as string, "Bad dictionary creation");
    }

    [Test, Order(4)]
    public void EqualsTest()
    {
        var dico1 = BdoData.NewDictionary(
            ("value1", _valueSet.value1),
            ("value2", _valueSet.value2),
            ("value3", _valueSet.value3));

        Assert.That(
            dico1.Equals(dico1), "Bad dictionary equal funtion");
    }

    [Test, Order(5)]
    public void Create5Test()
    {
        _dico = new[] {
            (StringHelper.__Star, _valueSet.valueStar as string),
            ("value1", _valueSet.value1 as string),
            ("value2", _valueSet.value2 as string),
            ("value3", _valueSet.value3 as string) };

        var valueStar = _dico[StringHelper.__Star];
        var value1 = _dico["value1"];
        var value2 = _dico["hgh", "value2"];
        var value3 = _dico["value3"];
        var value4 = _dico["value4"];
        var value5 = _dico["value5", StringHelper.__Star];

        Assert.That(
            valueStar == _valueSet.valueStar as string, "Bad dictionary creation");
        Assert.That(
            value1 == _valueSet.value1 as string, "Bad dictionary creation");
        Assert.That(
            value2 == _valueSet.value2 as string, "Bad dictionary creation");
        Assert.That(
            value3 == _valueSet.value3 as string, "Bad dictionary creation");
        Assert.That(
            value4 == null, "Bad dictionary creation");
        Assert.That(
            value5 == _valueSet.valueStar as string, "Bad dictionary creation");
    }
}
