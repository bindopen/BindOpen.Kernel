using NUnit.Framework;
using System.IO;

namespace BindOpen.Data;

[TestFixture, Order(210)]
public class MergerJsonTests
{
    private readonly string _filePath_json = GlobalTestData.WorkingFolder + "Merger{0}.json";
    private BdoMergerTests _dataTests;
    private bool _isSaved1 = false;
    private bool _isSaved2 = false;
    private bool _isSaved3 = false;

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        File.Delete(_filePath_json);

        _dataTests = new BdoMergerTests();
        _dataTests.OneTimeSetUp();
    }

    // Test 1

    [Test, Order(10)]
    public void SaveJson1Test()
    {
        _dataTests.Create1Test();
        var exp = _dataTests._merger;

        var filePath = string.Format(_filePath_json, 1);

        _isSaved1 = exp.ToDto().SaveJson(filePath);
        Assert.That(_isSaved1, "Merger saving failed");
    }

    [Test, Order(11)]
    public void LoadJson1Test()
    {
        if (!_isSaved1)
        {
            SaveJson1Test();
        }

        var filePath = string.Format(_filePath_json, 1);

        var exp = _dataTests._merger;
        var exp_fromDto = JsonHelper.LoadJson<MergerDto>(filePath).ToPoco();
        BdoMergerTests.AssertEquals(exp, exp_fromDto);
    }

    // Test 2

    [Test, Order(20)]
    public void SaveJson2Test()
    {
        _dataTests.Create2Test();
        var exp = _dataTests._merger;

        var filePath = string.Format(_filePath_json, 2);

        _isSaved2 = exp.ToDto().SaveJson(filePath);
        Assert.That(_isSaved2, "Merger saving failed");
    }

    [Test, Order(21)]
    public void LoadJson2Test()
    {
        if (!_isSaved2)
        {
            SaveJson2Test();
        }

        var filePath = string.Format(_filePath_json, 2);

        var exp = _dataTests._merger;
        var exp_fromDto = JsonHelper.LoadJson<MergerDto>(filePath).ToPoco();
        BdoMergerTests.AssertEquals(exp, exp_fromDto);
    }

    // Test 3

    [Test, Order(30)]
    public void SaveJson3Test()
    {
        _dataTests.Create3Test();
        var exp = _dataTests._merger;

        var filePath = string.Format(_filePath_json, 3);

        _isSaved3 = exp.ToDto().SaveJson(filePath);
        Assert.That(_isSaved3, "Merger saving failed");
    }

    [Test, Order(31)]
    public void LoadJson3Test()
    {
        if (!_isSaved3)
        {
            SaveJson3Test();
        }

        var filePath = string.Format(_filePath_json, 3);

        var exp = _dataTests._merger;
        var exp_fromDto = JsonHelper.LoadJson<MergerDto>(filePath).ToPoco();
        BdoMergerTests.AssertEquals(exp, exp_fromDto);
    }
}
