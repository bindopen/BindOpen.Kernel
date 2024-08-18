using BindOpen.Kernel.Tests;
using Bogus;
using DeepEqual.Syntax;
using NUnit.Framework;
using System.IO;
using System.Linq;

namespace BindOpen.Data.Json;

[TestFixture, Order(210)]
public class FilterJsonTests
{
    private readonly string _filePath_json = SystemData.WorkingFolder + "Filter.json";
    dynamic _valueSet;
    private IBdoMerger _filter = null;

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        File.Delete(_filePath_json);

        var f = new Faker();
        _valueSet = new
        {
            AddedValues = f.Random.WordsArray(5, 10),
            RemovedValues = f.Random.WordsArray(5, 10)
        };
    }

    public static bool Equals(
        IBdoMerger filter1,
        IBdoMerger filter2)
    {
        var b = filter1 != null && filter2 != null
            && filter1.IsDeepEqual(filter2);
        return b;
    }

    [Test, Order(1)]
    public void CreateTest()
    {
        _filter = BdoData.NewMerger(
            (_valueSet.AddedValues as string[]).ToList(),
            (_valueSet.RemovedValues as string[]).ToList());
    }

    [Test, Order(5)]
    public void SaveJsonTest()
    {
        if (_filter == null)
        {
            CreateTest();
        }

        var isSaved = _filter.ToDto().SaveJson(_filePath_json);
        Assert.That(isSaved, "String filter saving failed");
    }

    [Test, Order(6)]
    public void LoadJsonTest()
    {
        if (_filter == null || !File.Exists(_filePath_json))
        {
            SaveJsonTest();
        }

        var filter = JsonHelper.LoadJson<MergerDto>(_filePath_json).ToPoco();
        Assert.That(Equals(filter, _filter), "Error while loading");
    }
}
