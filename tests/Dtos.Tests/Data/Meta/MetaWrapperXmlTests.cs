using BindOpen.Scoping;
using BindOpen.Tests;
using NUnit.Framework;
using System.IO;

namespace BindOpen.Data.Meta;

/// <summary>
/// Contains unit tests for XML serialization and deserialization of <see cref="MetaWrapperFake"/>
/// instances via <see cref="ConfigurationDto"/> conversion and scope-based hydration.
/// </summary>
[TestFixture]
public class MetaWrapperXmlTests
{
    private readonly string _filePath_xml = DataTestData.WorkingFolder + "MetaWrapper.xml";

    private MetaWrapperFake _wrapper;

    /// <summary>
    /// Cleans up any leftover XML output file before any tests in this fixture are executed.
    /// </summary>
    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        File.Delete(_filePath_xml);
    }

    /// <summary>
    /// Validates that the specified <see cref="MetaWrapperFake"/> instance is non-null and
    /// preserves its expected enumeration and nested entity property values.
    /// </summary>
    /// <param name="wrapper">The metadata wrapper instance to validate.</param>
    private void Test(MetaWrapperFake wrapper)
    {
        Assert.That(wrapper, Is.Not.Null, "The metadata wrapper instance cannot be null.");

        Assert.Multiple(() =>
        {
            Assert.That(wrapper.SubEnumValue, Is.EqualTo(AccessibilityLevels.Public), "Wrapper SubEnumValue mismatch.");
            Assert.That(wrapper.EntityFake?.StringValue, Is.EqualTo("_string"), "Nested EntityFake StringValue mismatch.");
        });
    }

    /// <summary>
    /// Verifies that a <see cref="MetaWrapperFake"/> instance initializes properly with a populated
    /// <see cref="WrapperClassFake"/> nested entity.
    /// </summary>
    [Test]
    public void CreateTest()
    {
        _wrapper = new MetaWrapperFake()
        {
            EntityFake = new WrapperClassFake()
            {
                EnumValue = AccessibilityLevels.Public,
                StringValue = "_string"
            }
        };

        Test(_wrapper);
    }

    /// <summary>
    /// Verifies that calling <c>UpdateDetail()</c> on <see cref="MetaWrapperFake"/> produces a
    /// detail configuration that converts to a <see cref="ConfigurationDto"/> and saves cleanly to XML.
    /// </summary>
    [Test]
    public void SaveXmlTest()
    {
        if (_wrapper == null)
        {
            CreateTest();
        }

        _wrapper.UpdateDetail();
        var isSaved = _wrapper.Detail.ToDto().SaveXml(_filePath_xml);
        Assert.That(isSaved, Is.True, "Meta wrapper detail saving failed.");
    }

    /// <summary>
    /// Verifies that the saved XML file loads back into a <see cref="ConfigurationDto"/>, converts to a
    /// configuration POCO, and hydrates a matching <see cref="MetaWrapperFake"/>.
    /// </summary>
    [Test]
    public void LoadXmlTest()
    {
        if (_wrapper == null || !File.Exists(_filePath_xml))
        {
            SaveXmlTest();
        }

        var config = XmlHelper.LoadXml<ConfigurationDto>(_filePath_xml).ToPoco();
        var wrapper = ScopingTestData.Scope.NewMetaWrapper<MetaWrapperFake>(config);
        Test(wrapper);
    }
}