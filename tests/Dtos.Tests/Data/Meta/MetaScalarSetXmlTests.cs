using BindOpen.Data.Meta;
using BindOpen.Data.Objects;
using BindOpen.Tests;
using Bogus;
using DeepEqual.Syntax;
using NUnit.Framework;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BindOpen.Data.Meta;

/// <summary>
/// Contains unit tests for XML serialization and deserialization of <see cref="IBdoMetaSet"/>
/// collections populated with scalar metadata entries via <see cref="MetaSetDto"/> conversion.
/// </summary>
/// <remarks>
/// These tests verify that scalar metadata entries—including arrays of numbers, strings, integers,
/// binary buffers, and scalars with expression references—preserve deep structural equality when
/// persisted to an XML file and reloaded back into domain objects.
/// </remarks>
[TestFixture]
public class MetaScalarSetXmlTests
{
    private readonly string _filePath_xml = DataTestData.WorkingFolder + "MetaScalarSet.xml";

    private dynamic _testData;

    private IBdoMetaSet _metaSet = null;

    /// <summary>
    /// Cleans up any leftover XML output file and generates randomized scalar array datasets
    /// using <see cref="Faker"/> before any tests in this fixture are executed.
    /// </summary>
    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        File.Delete(_filePath_xml);

        var f = new Faker();
        _testData = new
        {
            arrayNumber1 = Enumerable.Range(0, 10).Select(p => f.Random.Double()).ToArray(),
            arrayString2 = Enumerable.Range(0, 10).Select(p => f.Random.Word()).ToArray(),
            arrayInteger3 = Enumerable.Range(0, 10).Select(p => f.Random.Int()).ToArray(),
            arrayArrayByte4 = Enumerable.Range(1, 2).Select(p => f.Random.Bytes(5000)).ToArray()
        };
    }

    /// <summary>
    /// Validates that both <see cref="IBdoMetaSet"/> instances are non-null and deeply equal,
    /// ignoring circular <see cref="IBdoMetaData.Parent"/> references and default interface members.
    /// </summary>
    /// <param name="list1">The first metadata set to compare.</param>
    /// <param name="list2">The second metadata set to compare.</param>
    public static void Test(
        IBdoMetaSet list1,
        IBdoMetaSet list2)
    {
        Assert.Multiple(() =>
        {
            Assert.That(list1, Is.Not.Null, "The first metadata set cannot be null.");
            Assert.That(list2, Is.Not.Null, "The second metadata set cannot be null.");
        });

        list1.WithDeepEqual(list2)
            .SkipDefault<IBdoMetaData>()
            .IgnoreProperty<IBdoMetaData>(x => x.Parent)
            .Assert();
    }

    /// <summary>
    /// Verifies that an <see cref="IBdoMetaSet"/> can be constructed from heterogeneous
    /// <see cref="IBdoMetaScalar"/> elements covering numeric, text, integer, binary, and
    /// expression-referenced values.
    /// </summary>
    [Test]
    public void CreateTest()
    {
        var metas = new List<IBdoMetaData>
        {
            BdoData.NewScalar("float1", DataValueTypes.Number, _testData.arrayNumber1 as double[]),
            BdoData.NewScalar("text2", DataValueTypes.Text, _testData.arrayString2 as string[]),
            BdoData.NewScalar("integer3", DataValueTypes.Integer, _testData.arrayInteger3 as int[]),
            BdoData.NewScalar("byteArray4", DataValueTypes.Binary, _testData.arrayArrayByte4 as byte[][]),
            BdoData.NewScalar("float2", DataValueTypes.Number, (_testData.arrayNumber1 as double[])[0]),
            BdoData.NewScalar("float2", DataValueTypes.Number, (_testData.arrayNumber1 as double[])[1])
                .WithReference(BdoData.NewExp("$sampleExp()", BdoExpressionKind.Auto))
        };

        _metaSet = BdoData.NewSet(metas.ToArray());

        Assert.That(_metaSet, Is.Not.Null, "The scalar metadata set failed to initialize.");
        Assert.That(_metaSet.Count, Is.EqualTo(metas.Count - 1), "Scalar metadata set item count mismatch.");
    }

    /// <summary>
    /// Verifies that the populated scalar <see cref="IBdoMetaSet"/> converts to a <see cref="MetaSetDto"/>
    /// and saves cleanly to an XML file on disk.
    /// </summary>
    [Test]
    public void SaveXmlTest()
    {
        if (_metaSet == null)
        {
            CreateTest();
        }

        var isSaved = _metaSet.ToDto().SaveXml(_filePath_xml);
        Assert.That(isSaved, Is.True, "Meta list saving failed.");
    }

    /// <summary>
    /// Verifies that the saved XML file loads back into a <see cref="MetaSetDto"/>, converts to a POCO
    /// via <c>ToPoco()</c>, and matches the original <see cref="IBdoMetaSet"/>.
    /// </summary>
    [Test]
    public void LoadXmlTest()
    {
        if (_metaSet == null || !File.Exists(_filePath_xml))
        {
            SaveXmlTest();
        }

        var metaSet = XmlHelper.LoadXml<MetaSetDto>(_filePath_xml).ToPoco();
        Test(metaSet, _metaSet);
    }
}