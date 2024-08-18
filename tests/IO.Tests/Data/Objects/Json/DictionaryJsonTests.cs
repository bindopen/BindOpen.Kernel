using BindOpen.Kernel.Tests;
using Bogus;
using NUnit.Framework;
using System.IO;
using System.Linq;

namespace BindOpen.Data.Json;

[TestFixture, Order(210)]
public class DictionaryJsonTests
{
    private readonly string _filePath_json = SystemData.WorkingFolder + "Dictionary.json";
    dynamic _valueSet;
    private ITBdoDictionary<string> _dico = null;

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        File.Delete(_filePath_json);

        var f = new Faker();
        _valueSet = new
        {
            value1 = f.Random.Word(),
            value2 = f.Random.Word()
        };
    }

    public static bool Equals(
        ITBdoDictionary<string> dico1,
        ITBdoDictionary<string> dico2)
    {
        var b = dico1 != null && dico2 != null
            && dico1.Count == dico2.Count
            && dico1.Keys.All(q => dico1[q] == dico2[q]);
        return b;
    }

    private void CreateTest()
    {
        _dico = BdoData.NewDictionary<string>(
            ("value1", _valueSet.value1),
            ("value2", _valueSet.value2));
    }

    [Test, Order(5)]
    public void SaveJsonTest()
    {
        if (_dico == null)
        {
            CreateTest();
        }

        var isSaved = _dico.ToDto().SaveJson(_filePath_json);
        Assert.That(isSaved, "Dictionary saving failed");
    }

    [Test, Order(6)]
    public void LoadJsonTest()
    {
        if (_dico == null || !File.Exists(_filePath_json))
        {
            SaveJsonTest();
        }

        var dico = JsonHelper.LoadJson<StringDictionaryDto>(_filePath_json).ToPoco<string>();
        Assert.That(Equals(dico, _dico), "Error while loading");
    }
}
