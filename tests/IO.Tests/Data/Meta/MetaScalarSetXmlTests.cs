﻿using BindOpen.Data.Meta;
using BindOpen.Tests;
using Bogus;
using DeepEqual.Syntax;
using NUnit.Framework;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BindOpen.Data.Meta;

[TestFixture, Order(202)]
public class MetaScalarSetXmlTests
{
    private readonly string _filePath_xml = DataTestData.WorkingFolder + "MetaScalarSet.xml";

    private dynamic _testData;

    private IBdoMetaNode _metaSet = null;

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        var f = new Faker();
        _testData = new
        {
            arrayNumber1 = Enumerable.Range(0, 10).Select(p => f.Random.Double()).ToArray(),
            arrayString2 = Enumerable.Range(0, 10).Select(p => f.Random.Word()).ToArray(),
            arrayInteger3 = Enumerable.Range(0, 10).Select(p => f.Random.Int()).ToArray(),
            arrayArrayByte4 = Enumerable.Range(1, 2).Select(p => f.Random.Bytes(5000)).ToArray()
        };
    }

    public static void Test(
        IBdoMetaNode list1,
        IBdoMetaNode list2)
    {
        Assert.That(list1 != null && list2 != null, "Meta list saving failed. ");

        list1.WithDeepEqual(list2)
            .SkipDefault<IBdoMetaData>()
            .IgnoreProperty<IBdoMetaData>(x => x.Parent)
            .Assert();
    }

    [Test, Order(2)]
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
                //.WithSchemas(BdoMeta.NewSchema(), BdoMeta.NewSchema("spec1"))
        };

        _metaSet = BdoData.NewNode(metas.ToArray());
    }

    [Test, Order(5)]
    public void SaveXmlTest()
    {
        if (_metaSet == null)
        {
            CreateTest();
        }

        var isSaved = _metaSet.ToDto().SaveXml(_filePath_xml);
        Assert.That(isSaved, "Meta list saving failed. ");
    }

    [Test, Order(6)]
    public void LoadXmlTest()
    {
        if (_metaSet == null || !File.Exists(_filePath_xml))
        {
            SaveXmlTest();
        }

        var metaSet = XmlHelper.LoadXml<MetaNodeDto>(_filePath_xml).ToPoco();
        Test(metaSet, _metaSet);
    }
}
