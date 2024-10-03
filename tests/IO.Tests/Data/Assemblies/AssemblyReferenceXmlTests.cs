using NUnit.Framework;
using System.IO;

namespace BindOpen.Data.Assemblies;

[TestFixture, Order(210)]
public class AssemblyReferenceXmlTests
{
    private readonly string _filePath_xml = GlobalTestData.WorkingFolder + "AssemblyReference{0}.xml";
    private BdoAssemblyReferenceTests _dataTests;
    private bool _isSaved1 = false;
    private bool _isSaved2 = false;

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        File.Delete(_filePath_xml);

        _dataTests = new BdoAssemblyReferenceTests();
        _dataTests.OneTimeSetUp();
    }

    // Test 1

    [Test, Order(10)]
    public void SaveXml1Test()
    {
        _dataTests.Create1Test();
        var exp = _dataTests._reference1;

        var filePath = string.Format(_filePath_xml, 1);

        _isSaved1 = exp.ToDto().SaveXml(filePath);
        Assert.That(_isSaved1, "AssemblyReference saving failed");
    }

    [Test, Order(11)]
    public void LoadXml1Test()
    {
        if (!_isSaved1)
        {
            SaveXml1Test();
        }

        var filePath = string.Format(_filePath_xml, 1);

        var exp = _dataTests._reference1;
        var exp_fromDto = XmlHelper.LoadXml<AssemblyReferenceDto>(filePath).ToPoco();
        BdoAssemblyReferenceTests.AssertEquals(exp, exp_fromDto);
    }

    // Test 2

    [Test, Order(20)]
    public void SaveXml2Test()
    {
        _dataTests.Create2Test();
        var exp = _dataTests._reference2;

        var filePath = string.Format(_filePath_xml, 2);

        _isSaved2 = exp.ToDto().SaveXml(filePath);
        Assert.That(_isSaved2, "AssemblyReference saving failed");
    }

    [Test, Order(21)]
    public void LoadXml2Test()
    {
        if (!_isSaved2)
        {
            SaveXml2Test();
        }

        var filePath = string.Format(_filePath_xml, 2);

        var exp = _dataTests._reference2;
        var exp_fromDto = XmlHelper.LoadXml<AssemblyReferenceDto>(filePath).ToPoco();
        BdoAssemblyReferenceTests.AssertEquals(exp, exp_fromDto);
    }
}
