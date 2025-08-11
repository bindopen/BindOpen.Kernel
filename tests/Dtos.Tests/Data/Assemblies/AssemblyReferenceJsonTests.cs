using NUnit.Framework;
using System.IO;

namespace BindOpen.Data.Assemblies;

[TestFixture, Order(210)]
public class AssemblyReferenceJsonTests
{
    private readonly string _filePath_json = DataTestData.WorkingFolder + "AssemblyReference{0}.json";
    private BdoAssemblyReferenceTests _dataTests;
    private bool _isSaved1 = false;
    private bool _isSaved2 = false;

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        File.Delete(_filePath_json);

        _dataTests = new BdoAssemblyReferenceTests();
        _dataTests.OneTimeSetUp();
    }

    // Test 1

    [Test, Order(10)]
    public void SaveJson1Test()
    {
        _dataTests.Create1Test();
        var exp = _dataTests._reference1;

        var filePath = string.Format(_filePath_json, 1);

        _isSaved1 = exp.ToDto().SaveJson(filePath);
        Assert.That(_isSaved1, "AssemblyReference saving failed");
    }

    [Test, Order(11)]
    public void LoadJson1Test()
    {
        if (!_isSaved1)
        {
            SaveJson1Test();
        }

        var filePath = string.Format(_filePath_json, 1);

        var exp = _dataTests._reference1;
        var exp_fromDto = JsonHelper.LoadJson<AssemblyReferenceDto>(filePath).ToPoco();
        BdoAssemblyReferenceTests.AssertEquals(exp, exp_fromDto);
    }

    // Test 2

    [Test, Order(20)]
    public void SaveJson2Test()
    {
        _dataTests.Create2Test();
        var exp = _dataTests._reference2;

        var filePath = string.Format(_filePath_json, 2);

        _isSaved2 = exp.ToDto().SaveJson(filePath);
        Assert.That(_isSaved2, "AssemblyReference saving failed");
    }

    [Test, Order(21)]
    public void LoadJson2Test()
    {
        if (!_isSaved2)
        {
            SaveJson2Test();
        }

        var filePath = string.Format(_filePath_json, 2);

        var exp = _dataTests._reference2;
        var exp_fromDto = JsonHelper.LoadJson<AssemblyReferenceDto>(filePath).ToPoco();
        BdoAssemblyReferenceTests.AssertEquals(exp, exp_fromDto);
    }
}
