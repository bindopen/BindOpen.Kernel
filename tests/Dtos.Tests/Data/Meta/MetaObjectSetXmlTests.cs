using BindOpen.Data.Meta.Reflection;
using BindOpen.Tests;
using DeepEqual.Syntax;
using NUnit.Framework;
using System.IO;

namespace BindOpen.Data.Meta;

/// <summary>
/// Contains unit tests for XML serialization and deserialization of <see cref="BdoMetaSet"/>
/// collections containing complex CLR objects via <see cref="MetaSetDto"/> conversion.
/// </summary>
/// <remarks>
/// These tests verify that metadata sets populated with faked object instances preserve deep object
/// equality when converted to DTOs, saved to an XML file, and reloaded back into a domain <see cref="IBdoMetaSet"/>.
/// </remarks>
[TestFixture]
public class MetaObjectSetXmlTests
{
    private readonly string _filePath_xml = DataTestData.WorkingFolder + "MetaObjectSet.xml";

    private BdoMetaSet _metaSet;

    private object _obj1 = null;
    private object _obj2 = null;
    private object _obj3 = null;

    /// <summary>
    /// Cleans up any leftover XML output file and generates three faked object instances
    /// using <see cref="ClassObjectFaker"/> before any tests in this fixture are executed.
    /// </summary>
    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        File.Delete(_filePath_xml);

        _obj1 = ClassObjectFaker.Fake();
        _obj2 = ClassObjectFaker.Fake();
        _obj3 = ClassObjectFaker.Fake();
    }

    /// <summary>
    /// Validates that the specified <see cref="IBdoMetaSet"/> contains all three expected object entries
    /// and that each retrieved value is deeply equal to its original faked instance.
    /// </summary>
    /// <param name="metaSet">The metadata set to validate.</param>
    private void Test(IBdoMetaSet metaSet)
    {
        var obj1 = metaSet.GetData("object1");
        var obj2 = metaSet.GetData("object2");
        var obj3 = metaSet["object3"].GetData();

        Assert.Multiple(() =>
        {
            Assert.That(obj1.IsDeepEqual(_obj1), Is.True, "The 'object1' entry does not match the original faked object.");
            Assert.That(obj2.IsDeepEqual(_obj2), Is.True, "The 'object2' entry does not match the original faked object.");
            Assert.That(obj3.IsDeepEqual(_obj3), Is.True, "The 'object3' entry does not match the original faked object.");
            Assert.That(metaSet?.Count, Is.EqualTo(3), "Metadata set item count mismatch.");
        });
    }

    /// <summary>
    /// Verifies that a <see cref="BdoMetaSet"/> created from three named <see cref="IBdoMetaObject"/>
    /// instances accurately stores and exposes each underlying CLR object.
    /// </summary>
    [Test]
    public void CreateTest()
    {
        var meta1 = BdoData.NewObject("object1", _obj1);
        var meta2 = BdoData.NewObject("object2", _obj2);
        var meta3 = BdoData.NewObject("object3", _obj3);

        _metaSet = BdoData.NewSet(meta1, meta2, meta3);

        Test(_metaSet);
    }

    /// <summary>
    /// Verifies that the populated <see cref="BdoMetaSet"/> converts to a <see cref="MetaSetDto"/>
    /// and saves cleanly to an XML file on disk.
    /// </summary>
    [Test]
    public void SaveXmlTest()
    {
        if (_metaSet == null)
        {
            CreateTest();
        }

        _metaSet.UpdateTree();

        var isSaved = _metaSet.ToDto().SaveXml(_filePath_xml);
        Assert.That(isSaved, Is.True, "Meta list saving failed.");
    }

    /// <summary>
    /// Verifies that the saved XML file loads back into a <see cref="MetaSetDto"/>, converts to a POCO
    /// via <c>ToPoco()</c>, and retains all expected object entries and values.
    /// </summary>
    [Test]
    public void LoadXmlTest()
    {
        if (_metaSet == null || !File.Exists(_filePath_xml))
        {
            SaveXmlTest();
        }

        var metaSet = XmlHelper.LoadXml<MetaSetDto>(_filePath_xml).ToPoco();

        Test(metaSet);
    }
}