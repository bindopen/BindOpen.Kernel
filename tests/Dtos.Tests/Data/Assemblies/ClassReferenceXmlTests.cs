using NUnit.Framework;
using System.IO;

namespace BindOpen.Data.Assemblies;

/// <summary>
/// Contains unit tests for XML serialization and deserialization of <see cref="IBdoClassReference"/>
/// instances via <see cref="ClassReferenceDto"/> conversion.
/// </summary>
/// <remarks>
/// These tests verify that class references created by <see cref="BdoClassReferenceTests"/> can be
/// mapped to DTOs, persisted to XML files on disk, and reloaded back into domain objects without losing
/// structural or property equality.
/// </remarks>
[TestFixture]
public class ClassReferenceXmlTests
{
    private readonly string _filePath_xml = DataTestData.WorkingFolder + "ClassReference{0}.xml";
    private BdoClassReferenceTests _dataTests;
    private bool _isSaved1 = false;
    private bool _isSaved2 = false;

    /// <summary>
    /// Cleans up previous test output files and initializes the shared <see cref="BdoClassReferenceTests"/>
    /// helper before any tests in this fixture are executed.
    /// </summary>
    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        File.Delete(string.Format(_filePath_xml, 1));
        File.Delete(string.Format(_filePath_xml, 2));

        _dataTests = new BdoClassReferenceTests();
        _dataTests.OneTimeSetUp();
    }

    // Test 1

    /// <summary>
    /// Verifies that the first test <see cref="IBdoClassReference"/> instance converts to a
    /// <see cref="ClassReferenceDto"/> and saves cleanly to an XML file.
    /// </summary>
    [Test]
    public void SaveXml1Test()
    {
        _dataTests.Create1Test();
        var exp = _dataTests._classRef1;

        var filePath = string.Format(_filePath_xml, 1);

        _isSaved1 = exp.ToDto().SaveXml(filePath);
        Assert.That(_isSaved1, Is.True, "ClassReference saving failed.");
    }

    /// <summary>
    /// Verifies that the first saved XML file loads back into a <see cref="ClassReferenceDto"/>,
    /// converts to a POCO via <c>ToPoco()</c>, and matches the original <see cref="IBdoClassReference"/>.
    /// </summary>
    [Test]
    public void LoadXml1Test()
    {
        if (!_isSaved1)
        {
            SaveXml1Test();
        }

        var filePath = string.Format(_filePath_xml, 1);

        var exp = _dataTests._classRef1;
        var exp_fromDto = XmlHelper.LoadXml<ClassReferenceDto>(filePath).ToPoco();
        BdoClassReferenceTests.AssertEquals(exp, exp_fromDto);
    }

    // Test 2

    /// <summary>
    /// Verifies that the second test <see cref="IBdoClassReference"/> instance converts to a
    /// <see cref="ClassReferenceDto"/> and saves cleanly to an XML file.
    /// </summary>
    [Test]
    public void SaveXml2Test()
    {
        _dataTests.Create2Test();
        var exp = _dataTests._classRef2;

        var filePath = string.Format(_filePath_xml, 2);

        _isSaved2 = exp.ToDto().SaveXml(filePath);
        Assert.That(_isSaved2, Is.True, "ClassReference saving failed.");
    }

    /// <summary>
    /// Verifies that the second saved XML file loads back into a <see cref="ClassReferenceDto"/>,
    /// converts to a POCO via <c>ToPoco()</c>, and matches the original <see cref="IBdoClassReference"/>.
    /// </summary>
    [Test]
    public void LoadXml2Test()
    {
        if (!_isSaved2)
        {
            SaveXml2Test();
        }

        var filePath = string.Format(_filePath_xml, 2);

        var exp = _dataTests._classRef2;
        var exp_fromDto = XmlHelper.LoadXml<ClassReferenceDto>(filePath).ToPoco();
        BdoClassReferenceTests.AssertEquals(exp, exp_fromDto);
    }
}