using NUnit.Framework;
using System.IO;

namespace BindOpen.Data;

[TestFixture, Order(210)]
public class ReferenceXmlTests
{
    private readonly string _filePath_xml = GlobalTestData.WorkingFolder + "Reference{0}.xml";
    private BdoReferenceTests _dataTests;
    private bool _isSaved1 = false;
    private bool _isSaved2 = false;
    private bool _isSaved3 = false;

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        File.Delete(_filePath_xml);

        _dataTests = new BdoReferenceTests();
        _dataTests.OneTimeSetUp();
    }

    // Test 1

    [Test, Order(10)]
    public void SaveXml1Test()
    {
        _dataTests.Create1Test();
        var reference = _dataTests._reference;

        var filePath = string.Format(_filePath_xml, 1);

        _isSaved1 = reference.ToDto().SaveXml(filePath);
        Assert.That(_isSaved1, "Reference saving failed");
    }

    [Test, Order(11)]
    public void LoadXml1Test()
    {
        if (!_isSaved1)
        {
            SaveXml1Test();
        }

        var filePath = string.Format(_filePath_xml, 1);

        var reference = _dataTests._reference;
        var reference_fromDto = XmlHelper.LoadXml<ReferenceDto>(filePath).ToPoco();
        BdoReferenceTests.AssertEquals(reference, reference_fromDto);
    }

    // Test 2

    [Test, Order(20)]
    public void SaveXml2Test()
    {
        _dataTests.Create2Test();
        var reference = _dataTests._reference;

        var filePath = string.Format(_filePath_xml, 2);

        _isSaved2 = reference.ToDto().SaveXml(filePath);
        Assert.That(_isSaved2, "Reference saving failed");
    }

    [Test, Order(21)]
    public void LoadXml2Test()
    {
        if (!_isSaved2)
        {
            SaveXml2Test();
        }

        var filePath = string.Format(_filePath_xml, 2);

        var reference = _dataTests._reference;
        var reference_fromDto = XmlHelper.LoadXml<ReferenceDto>(filePath).ToPoco();
        BdoReferenceTests.AssertEquals(reference, reference_fromDto);
    }

    // Test 3

    [Test, Order(30)]
    public void SaveXml3Test()
    {
        _dataTests.Create3Test();
        var reference = _dataTests._reference;

        var filePath = string.Format(_filePath_xml, 3);

        _isSaved3 = reference.ToDto().SaveXml(filePath);
        Assert.That(_isSaved3, "Reference saving failed");
    }

    [Test, Order(31)]
    public void LoadXml3Test()
    {
        if (!_isSaved3)
        {
            SaveXml3Test();
        }

        var filePath = string.Format(_filePath_xml, 3);

        var reference = _dataTests._reference;
        var reference_fromDto = XmlHelper.LoadXml<ReferenceDto>(filePath).ToPoco();
        BdoReferenceTests.AssertEquals(reference, reference_fromDto);
    }
}
