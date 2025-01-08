using BindOpen.Data.Helpers;
using BindOpen.Data.Meta;
using Bogus;
using NUnit.Framework;
using System.Linq;

namespace BindOpen.Data;

[TestFixture, Order(210)]
public class BdoStringTokensTests
{
    dynamic _valueSet;
    public TBdoDictionary<string> _dico = null;


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

    public void Test(IBdoMetaSet set)
    {
        Assert.That(
            set.GetData<string>(_valueSet.name_value1) == _valueSet.value1 as string
            && set.GetData<string>(_valueSet.name_value2) == _valueSet.value2 as string, "Bad string parsing");
    }

    [Test, Order(1)]
    public void Create1Test()
    {
        string st = _valueSet.preffix + _valueSet.value1 + _valueSet.middle + _valueSet.value2 + _valueSet.suffix;
        string pattern = _valueSet.preffix + "{{" + _valueSet.name_value1 + "}}" + _valueSet.middle + "{{" + _valueSet.name_value2 + "}}" + _valueSet.suffix;

        var set = st.ExtractTokenMetas(pattern);

        Test(set);
    }

    [Test, Order(2)]
    public void Create2Test()
    {
        string st = _valueSet.value1 + _valueSet.middle + _valueSet.value2 + _valueSet.suffix;
        string pattern = "{{" + _valueSet.name_value1 + "}}" + _valueSet.middle + "{{" + _valueSet.name_value2 + "}}" + _valueSet.suffix;

        var set = st.ExtractTokenMetas(pattern);

        Test(set);
    }

    [Test, Order(3)]
    public void Create3Test()
    {
        string st = _valueSet.preffix + _valueSet.value1 + _valueSet.middle + _valueSet.value2;
        string pattern = _valueSet.preffix + "{{" + _valueSet.name_value1 + "}}" + _valueSet.middle + "{{" + _valueSet.name_value2 + "}}";

        var set = st.ExtractTokenMetas(pattern);

        Test(set);
    }

    [Test, Order(4)]
    public void Create4Test()
    {
        string st = _valueSet.value1 + _valueSet.middle + _valueSet.value2;
        string pattern = "{{" + _valueSet.name_value1 + "}}" + _valueSet.middle + "{{" + _valueSet.name_value2 + "}}";

        var set = st.ExtractTokenMetas(pattern, '"');

        Test(set);
    }

    [Test, Order(5)]
    public void Create5Test()
    {
        string st = (_valueSet.value1 as string).ToQuoted('"') + ":" + _valueSet.value2;
        string pattern = "{{" + _valueSet.name_value1 + "}}" + ":" + "{{" + _valueSet.name_value2 + "}}";

        var set = st.ExtractTokenMetas(pattern, '"');

        Test(set);
    }

    [Test, Order(6)]
    public void CreateTest_NameSpaceValue()
    {
        var name = @"toto ""max";
        string st = name.ToQuoted('"') + " " + _valueSet.value2;
        string pattern = LabelFormats.NameSpaceValue.GetScript();

        var set = st.ExtractTokenMetas(pattern, '"');

        Assert.That(
            set.GetData<string>(BdoMetaDataProperties.Name) == name
            && set.GetData<string>(BdoMetaDataProperties.Value) == _valueSet.value2 as string, "Bad string parsing");
    }

    [Test, Order(6)]
    public void CreateTest_NameSpaceValue2()
    {
        var name = "toto";
        string st = name;
        string pattern = LabelFormats.NameSpaceValue.GetScript();

        var set = st.ExtractTokenMetas(pattern, '"');

        Assert.That(set == null, "Bad string parsing");
    }

    [Test, Order(6)]
    public void CreateTest_PatternNull()
    {
        string st = null;
        string pattern = null;

        var set = st.ExtractTokenMetas(pattern, '"');

        Assert.That(set == null, "Bad string parsing");
    }

    [Test, Order(7)]
    public void ExtractTokenMetasTest()
    {
        var name = @"toto ""max";
        string st = name.ToQuoted('"') + " " + _valueSet.value2;
        string pattern = LabelFormats.NameSpaceValue.GetScript();

        string st_name = null;
        string st_value = null;

        var set = st.ExtractTokenMetas(pattern, '"');
        set.Map(
            (BdoMetaDataProperties.Name, q => { st_name = q.GetData<string>(); }
        ),
            (BdoMetaDataProperties.Value, q => { st_value = q.GetData<string>(); }
        ));

        Assert.That(st_name == name as string && st_value == _valueSet.value2, "Bad string parsing");
    }

    [Test, Order(7)]
    public void ExtractTokensTest()
    {
        string pattern = LabelFormats.NameSpaceValue.GetScript();

        var list = pattern.ExtractTokens()?.ToList();

        Assert.That(list.Count == 2
            && list[0] == BdoMetaDataProperties.Name
            && list[1] == BdoMetaDataProperties.Value, "Bad string parsing");
    }

    [Test, Order(8)]
    public void FormatFromTokensTest()
    {
        string pattern = LabelFormats.NameSpaceValue.GetScript();

        var st = pattern.FormatFromTokens(
            BdoData.NewSet(
                (BdoMetaDataProperties.Name, "_name"),
                (BdoMetaDataProperties.Value, "_value")));

        Assert.That(st == "_name _value", "Bad string parsing");
    }

    [Test, Order(8)]
    public void FormatFromTokensWithNullTokenTest()
    {
        string pattern = LabelFormats.NameSpaceValue.GetScript();

        var st = pattern.FormatFromTokens(
            BdoData.NewSet(
                (BdoMetaDataProperties.Name, "_name"),
                (BdoMetaDataProperties.Value, null)));

        Assert.That(st == "_name ", "Bad string parsing");
    }
}