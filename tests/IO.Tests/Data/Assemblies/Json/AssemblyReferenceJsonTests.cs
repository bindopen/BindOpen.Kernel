using Bogus;
using DeepEqual.Syntax;
using NUnit.Framework;
using System.IO;

namespace BindOpen.Data.Assemblies;

[TestFixture, Order(210)]
public class AssemblyReferenceJsonTests
{
    private readonly string _filePath_json = GlobalTestData.WorkingFolder + "AssemblyReference.json";
    dynamic _valueSet;
    private IBdoAssemblyReference _exp = null;

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        File.Delete(_filePath_json);

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

    [Test, Order(5)]
    public void SaveJsonTest()
    {
        if (_exp == null)
        {
            CreateTest();
        }

        var isSaved = _exp.ToDto().SaveJson(_filePath_json);
        Assert.That(isSaved, "AssemblyReference saving failed");
    }

    [Test, Order(6)]
    public void LoadJsonTest()
    {
        if (_exp == null || !File.Exists(_filePath_json))
        {
            SaveJsonTest();
        }

        var exp = JsonHelper.LoadJson<AssemblyReferenceDto>(_filePath_json).ToPoco();
        Assert.That(Equals(exp, _exp), "Error while loading");
    }
}
