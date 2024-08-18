using BindOpen.Kernel.Tests;
using Bogus;
using DeepEqual.Syntax;
using NUnit.Framework;
using System.IO;

namespace BindOpen.Data.Json;

[TestFixture, Order(210)]
public class ExpressionJsonTests
{
    private readonly string _filePath_json = SystemData.WorkingFolder + "Expression.json";
    dynamic _valueSet;
    private IBdoExpression _exp = null;

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        File.Delete(_filePath_json);

        var f = new Faker();
        _valueSet = new
        {
            Literal = f.Random.Word(),
            ScriptwordName = "func1",
            ExpressionKind = f.PickRandom(
                BdoExpressionKind.Literal,
                BdoExpressionKind.Script,
                BdoExpressionKind.Auto)
        };
    }

    public static bool Equals(
        IBdoExpression exp1,
        IBdoExpression exp2)
    {
        var b = exp1 != null && exp2 != null
            && exp1.IsDeepEqual(exp2);
        return b;
    }

    [Test, Order(1)]
    public void CreateTest()
    {
        _exp = BdoData.NewExpression(
            _valueSet.Literal as string,
            _valueSet.ExpressionKind as BdoExpressionKind? ?? BdoExpressionKind.Auto);
    }

    [Test, Order(5)]
    public void SaveJsonTest()
    {
        if (_exp == null)
        {
            CreateTest();
        }

        var isSaved = _exp.ToDto().SaveJson(_filePath_json);
        Assert.That(isSaved, "Expression saving failed");
    }

    [Test, Order(6)]
    public void LoadJsonTest()
    {
        if (_exp == null || !File.Exists(_filePath_json))
        {
            SaveJsonTest();
        }

        var exp = JsonHelper.LoadJson<ExpressionDto>(_filePath_json).ToPoco();
        Assert.That(Equals(exp, _exp), "Error while loading");
    }
}
