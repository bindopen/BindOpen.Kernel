﻿using BindOpen.Tests;
using DeepEqual.Syntax;
using NUnit.Framework;
using System.IO;

namespace BindOpen.Data.Meta;

[TestFixture, Order(201)]
public class MetaObjectSetXmlTests
{
    private readonly string _filePath_xml = DataTestData.WorkingFolder + "MetaObjectSet.xml";

    private BdoMetaNode _metaSet;

    private object _obj1 = null;
    private object _obj2 = null;
    private object _obj3 = null;

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        _obj1 = ClassObjectFaker.Fake();
        _obj2 = ClassObjectFaker.Fake();
        _obj3 = ClassObjectFaker.Fake();
    }

    private void Test(IBdoMetaNode metaSet)
    {
        var obj1 = metaSet.GetData("object1");
        var obj2 = metaSet.GetData("object2");
        var obj3 = metaSet["object3"].GetData();

        Assert.That(obj1.IsDeepEqual(_obj1) == true, "Bad obj element set - Count");
        Assert.That(obj2.IsDeepEqual(_obj2) == true, "Bad obj element set - Count");
        Assert.That(obj3.IsDeepEqual(_obj3) == true, "Bad obj element set - Count");
        Assert.That(metaSet?.Count == 3, "Bad object element set - Count");
    }

    [Test, Order(1)]
    public void CreateTest()
    {
        var meta1 = BdoData.NewObject("object1", _obj1);
        var meta2 = BdoData.NewObject("object2", _obj2);
        var meta3 = BdoData.NewObject("object3", _obj3);

        _metaSet = BdoData.NewNode(meta1, meta2, meta3);

        Test(_metaSet);
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
        Equals(metaSet, _metaSet);
    }
}
