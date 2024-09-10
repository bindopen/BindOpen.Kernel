using Bogus;
using DeepEqual.Syntax;
using NUnit.Framework;
using System.IO;

namespace BindOpen.Data.Assemblies;

[TestFixture, Order(210)]
public class AssemblyReferenceXmlTests
{
    private readonly string _filePath_xml = GlobalTestData.WorkingFolder + "AssemblyReference.xml";
    dynamic _valueSet;
    private IBdoAssemblyReference _exp = null;

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        File.Delete(_filePath_xml);

        var f = new Faker();
        _valueSet = new
        {
            Literal = f.Random.Word(),
            ScriptwordName = "func1",
            AssemblyReferenceKind = f.PickRandom(
                BdoAssemblyReferenceKind.Literal,
                BdoAssemblyReferenceKind.Script,
                BdoAssemblyReferenceKind.Auto)
        };
    }

    public static bool Equals(
        IBdoAssemblyReference exp1,
        IBdoAssemblyReference exp2)
    {
        var b = exp1 != null && exp2 != null
            && exp1.IsDeepEqual(exp2);
        return b;
    }

    [Test, Order(1)]
    public void CreateTest()
    {
        _exp = BdoData.NewAssemblyReference(
            _valueSet.Literal as string,
            _valueSet.AssemblyReferenceKind as BdoAssemblyReferenceKind? ?? BdoAssemblyReferenceKind.Auto);
    }

    [Test, Order(3)]
    public void SaveXmlTest()
    {
        if (_exp == null)
        {
            CreateTest();
        }

        var isSaved = _exp.ToDto().SaveXml(_filePath_xml);
        Assert.That(isSaved, "AssemblyReference saving failed");
    }

    [Test, Order(4)]
    public void LoadXmlTest()
    {
        if (_exp == null || !File.Exists(_filePath_xml))
        {
            SaveXmlTest();
        }

        var exp = XmlHelper.LoadXml<AssemblyReferenceDto>(_filePath_xml).ToPoco();
        Assert.That(Equals(exp, _exp), "Error while loading");
    }
}
