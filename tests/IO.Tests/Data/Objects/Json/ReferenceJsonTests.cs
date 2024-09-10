using BindOpen.Tests;
using Bogus;
using DeepEqual.Syntax;
using NUnit.Framework;
using System.IO;

namespace BindOpen.Data;

[TestFixture, Order(210)]
public class ReferenceJsonTests
{
    private readonly string _filePath_json = GlobalTestData.WorkingFolder + "Reference.json";
    dynamic _valueSet;
    private IBdoReference _ref = null;

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        File.Delete(_filePath_json);

        var f = new Faker();
        _valueSet = new
        {
            Script = "$(var1)",
            Literal = f.Random.Word(),
            ScriptwordName = "func1",
            ReferenceKind = f.PickRandom<BdoReferenceKind>()
        };
    }

    public static bool Equals(
        IBdoReference exp1,
        IBdoReference exp2)
    {
        var b = exp1 != null && exp2 != null
            && exp1.IsDeepEqual(exp2);
        return b;
    }

    [Test, Order(1)]
    public void CreateTest()
    {
        _ref = BdoData.NewReference(BdoReferenceKind.Any)
            .WithExpression(BdoData.NewExpression("text", BdoExpressionKind.Auto))
            .WithMetaData(BdoData.NewMeta("meta1", "test"))
            .WithIdentifier("id");
    }

    [Test, Order(5)]
    public void SaveJsonTest()
    {
        if (_ref == null)
        {
            CreateTest();
        }

        var isSaved = _ref.ToDto().SaveJson(_filePath_json);
        Assert.That(isSaved, "Reference saving failed");
    }

    [Test, Order(6)]
    public void LoadJsonTest()
    {
        if (_ref == null || !File.Exists(_filePath_json))
        {
            SaveJsonTest();
        }

        var exp = JsonHelper.LoadJson<ReferenceDto>(_filePath_json).ToPoco();
        Assert.That(Equals(exp, _ref), "Error while loading");
    }
}
