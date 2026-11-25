using NUnit.Framework;
using System.IO;

namespace BindOpen.Data;

/// <summary>
/// Contains unit tests for XML serialization and deserialization of <see cref="IBdoReference"/>
/// instances via <see cref="ReferenceDto"/> conversion.
/// </summary>
/// <remarks>
/// These tests verify that standard and scoping references created by <see cref="BdoReferenceTests"/>
/// and <see cref="BdoScopingReferenceTests"/> can be mapped to DTOs, persisted to XML files on disk,
/// and reloaded back into domain objects without losing structural or property equality.
/// </remarks>
[TestFixture]
public class ReferenceXmlTests
{
    private readonly string _filePath_xml = DataTestData.WorkingFolder + "Reference{0}.xml";
    private BdoReferenceTests _dataTests;
    private BdoScopingReferenceTests _scopingTests;
    private bool _isSaved1 = false;
    private bool _isSaved2 = false;
    private bool _isSaved3 = false;

    /// <summary>
    /// Cleans up previous test output files and initializes the shared <see cref="BdoReferenceTests"/>
    /// and <see cref="BdoScopingReferenceTests"/> helpers before any tests in this fixture are executed.
    /// </summary>
    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        File.Delete(string.Format(_filePath_xml, 1));
        File.Delete(string.Format(_filePath_xml, 2));
        File.Delete(string.Format(_filePath_xml, 3));

        _dataTests = new BdoReferenceTests();
        _dataTests.OneTimeSetUp();

        _scopingTests = new BdoScopingReferenceTests();
        _scopingTests.OneTimeSetUp();
    }

    // Test 1

    /// <summary>
    /// Verifies that the first test <see cref="IBdoReference"/> instance converts to a
    /// <see cref="ReferenceDto"/> and saves cleanly to an XML file.
    /// </summary>
    [Test]
    public void SaveXml1Test()
    {
        _dataTests.Create1Test();
        var reference = _dataTests._reference;

        var filePath = string.Format(_filePath_xml, 1);

        _isSaved1 = reference.ToDto().SaveXml(filePath);
        Assert.That(_isSaved1, Is.True, "Reference saving failed.");
    }

    /// <summary>
    /// Verifies that the first saved XML file loads back into a <see cref="ReferenceDto"/>,
    /// converts to a POCO via <c>ToPoco()</c>, and matches the original <see cref="IBdoReference"/>.
    /// </summary>
    [Test]
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

    /// <summary>
    /// Verifies that the second test <see cref="IBdoReference"/> instance converts to a
    /// <see cref="ReferenceDto"/> and saves cleanly to an XML file.
    /// </summary>
    [Test]
    public void SaveXml2Test()
    {
        _dataTests.Create2Test();
        var reference = _dataTests._reference;

        var filePath = string.Format(_filePath_xml, 2);

        _isSaved2 = reference.ToDto().SaveXml(filePath);
        Assert.That(_isSaved2, Is.True, "Reference saving failed.");
    }

    /// <summary>
    /// Verifies that the second saved XML file loads back into a <see cref="ReferenceDto"/>,
    /// converts to a POCO via <c>ToPoco()</c>, and matches the original <see cref="IBdoReference"/>.
    /// </summary>
    [Test]
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

    /// <summary>
    /// Verifies that the third scoping test <see cref="IBdoReference"/> instance converts to a
    /// <see cref="ReferenceDto"/> and saves cleanly to an XML file.
    /// </summary>
    [Test]
    public void SaveXml3Test()
    {
        _scopingTests.Create3Test();
        var reference = _dataTests._reference;

        var filePath = string.Format(_filePath_xml, 3);

        _isSaved3 = reference.ToDto().SaveXml(filePath);
        Assert.That(_isSaved3, Is.True, "Reference saving failed.");
    }

    /// <summary>
    /// Verifies that the third saved XML file loads back into a <see cref="ReferenceDto"/>,
    /// converts to a POCO via <c>ToPoco()</c>, and matches the original <see cref="IBdoReference"/>.
    /// </summary>
    [Test]
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