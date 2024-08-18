using BindOpen.Kernel.Tests;
using Bogus;
using DeepEqual.Syntax;
using NUnit.Framework;
using System.IO;
using System.Linq;

namespace BindOpen.Data.Xml;

[TestFixture, Order(210)]
public class FilterXmlTests
{
    private readonly string _filePath_xml = SystemData.WorkingFolder + "Filter.xml";
    dynamic _valueSet;
    private IBdoMerger _filter = null;

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        File.Delete(_filePath_xml);

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

    [Test, Order(3)]
    public void SaveXmlTest()
    {
        if (_filter == null)
        {
            CreateTest();
        }

        var isSaved = _filter.ToDto().SaveXml(_filePath_xml);
        Assert.That(isSaved, "String filter saving failed");
    }

    [Test, Order(4)]
    public void LoadXmlTest()
    {
        if (_filter == null || !File.Exists(_filePath_xml))
        {
            SaveXmlTest();
        }

        var filter = XmlHelper.LoadXml<MergerDto>(_filePath_xml).ToPoco();
        Assert.That(Equals(filter, _filter), "Error while loading");
    }
}
