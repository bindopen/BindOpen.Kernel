using BindOpen.Tests;
using DeepEqual.Syntax;
using NUnit.Framework;
using System.IO;

namespace BindOpen.Data.Meta;

[TestFixture, Order(201)]
public class SpecXmlTests
{
    private readonly string _filePath_xml = GlobalTestData.WorkingFolder + "Spec.xml";

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

    [Test, Order(5)]
    public void SaveXmlTest()
    {
        var isSaved = _spec.ToDto().SaveXml(_filePath_xml);
        Assert.That(isSaved, "Meta list saving failed. ");
    }

    [Test, Order(6)]
    public void LoadXmlTest()
    {
        if (_spec == null || !File.Exists(_filePath_xml))
        {
            SaveXmlTest();
        }

        var spec = XmlHelper.LoadXml<SpecDto>(_filePath_xml).ToPoco();
        Equals(spec, _spec);
    }
}
