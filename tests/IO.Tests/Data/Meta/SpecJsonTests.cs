using BindOpen.Tests;
using DeepEqual.Syntax;
using NUnit.Framework;
using System.IO;

namespace BindOpen.Data.Meta;

[TestFixture, Order(201)]
public class SpecJsonTests
{
    private readonly string _filePath_json = GlobalTestData.WorkingFolder + "Spec.json";

    private IBdoSpec _spec;

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        _spec = BdoSpecFaker.CreateSpecWithReference();
    }

    public static bool Equals(
        IBdoSpec spec1,
        IBdoSpec spec2)
    {
        var b = spec1 != null && spec2 != null
            && spec1.IsDeepEqual(spec2);
        return b;
    }

    [Test, Order(7)]
    public void SaveJsonTest()
    {
        var isSaved = _spec.ToDto().SaveJson(_filePath_json);
        Assert.That(isSaved, "Meta list saving failed. ");
    }

    [Test, Order(8)]
    public void LoadJsonTest()
    {
        if (_spec == null || !File.Exists(_filePath_json))
        {
            SaveJsonTest();
        }

        var spec = JsonHelper.LoadJson<SpecDto>(_filePath_json).ToPoco();
        Equals(spec, _spec);
    }
}
