﻿using NUnit.Framework;
using System.IO;

namespace BindOpen.Data;

[TestFixture, Order(210)]
public class ExpressionXmlTests
{
    private readonly string _filePath_xml = DataTestData.WorkingFolder + "Expression{0}.xml";
    private BdoExpressionTests _dataTests;
    private BdoScopingExpressionTests _scopingTests;
    private bool _isSaved1 = false;
    private bool _isSaved2 = false;
    private bool _isSaved3 = false;

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        File.Delete(_filePath_xml);

        _dataTests = new BdoExpressionTests();
        _dataTests.OneTimeSetUp();

        _scopingTests = new BdoScopingExpressionTests();
        _scopingTests.OneTimeSetUp();

    }

    // Test 1

    [Test, Order(10)]
    public void SaveXml1Test()
    {
        _dataTests.Create1Test();
        var exp = _dataTests._exp;

        var filePath = string.Format(_filePath_xml, 1);

        _isSaved1 = exp.ToDto().SaveXml(filePath);
        Assert.That(_isSaved1, "Expression saving failed");
    }

    [Test, Order(11)]
    public void LoadXml1Test()
    {
        if (!_isSaved1)
        {
            SaveXml1Test();
        }

        var filePath = string.Format(_filePath_xml, 1);

        var exp = _dataTests._exp;
        var exp_fromDto = XmlHelper.LoadXml<ExpressionDto>(filePath).ToPoco();
        BdoExpressionTests.AssertEquals(exp, exp_fromDto);
    }

    // Test 2

    [Test, Order(20)]
    public void SaveXml2Test()
    {
        _dataTests.Create2Test();
        var exp = _dataTests._exp;

        var filePath = string.Format(_filePath_xml, 2);

        _isSaved2 = exp.ToDto().SaveXml(filePath);
        Assert.That(_isSaved2, "Expression saving failed");
    }

    [Test, Order(21)]
    public void LoadXml2Test()
    {
        if (!_isSaved2)
        {
            SaveXml2Test();
        }

        var filePath = string.Format(_filePath_xml, 2);

        var exp = _dataTests._exp;
        var exp_fromDto = XmlHelper.LoadXml<ExpressionDto>(filePath).ToPoco();
        BdoExpressionTests.AssertEquals(exp, exp_fromDto);
    }

    // Test 3

    [Test, Order(30)]
    public void SaveXml3Test()
    {
        _scopingTests.Create3Test();
        var exp = _dataTests._exp;

        var filePath = string.Format(_filePath_xml, 3);

        _isSaved3 = exp.ToDto().SaveXml(filePath);
        Assert.That(_isSaved3, "Expression saving failed");
    }

    [Test, Order(31)]
    public void LoadXml3Test()
    {
        if (!_isSaved3)
        {
            SaveXml3Test();
        }

        var filePath = string.Format(_filePath_xml, 3);

        var exp = _dataTests._exp;
        var exp_fromDto = XmlHelper.LoadXml<ExpressionDto>(filePath).ToPoco();
        BdoExpressionTests.AssertEquals(exp, exp_fromDto);
    }
}
