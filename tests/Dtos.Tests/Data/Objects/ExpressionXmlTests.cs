using BindOpen.Data.Objects;
using NUnit.Framework;
using System.IO;

namespace BindOpen.Data;

/// <summary>
/// Contains unit tests for Xml serialization and deserialization of <see cref="IBdoExpression"/>
/// instances via <see cref="ExpressionDto"/> conversion.
/// </summary>
/// <remarks>
/// These tests verify that standard and scoping expressions created by <see cref="BdoExpressionTests"/>
/// and <see cref="BdoScopingExpressionTests"/> can be mapped to DTOs, persisted to Xml files on disk,
/// and reloaded back into domain objects without losing structural or property equality.
/// </remarks>
[TestFixture]
public class ExpressionXmlTests
{
    private readonly string _filePath_Xml = DataTestData.WorkingFolder + "Expression{0}.Xml";
    private BdoExpressionTests _dataTests;
    private BdoScopingExpressionTests _scopingTests;
    private bool _isSaved1 = false;
    private bool _isSaved2 = false;
    private bool _isSaved3 = false;

    /// <summary>
    /// Cleans up previous test output files and initializes the shared <see cref="BdoExpressionTests"/>
    /// and <see cref="BdoScopingExpressionTests"/> helpers before any tests in this fixture are executed.
    /// </summary>
    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        File.Delete(string.Format(_filePath_Xml, 1));
        File.Delete(string.Format(_filePath_Xml, 2));
        File.Delete(string.Format(_filePath_Xml, 3));

        _dataTests = new BdoExpressionTests();
        _dataTests.OneTimeSetUp();

        _scopingTests = new BdoScopingExpressionTests();
        _scopingTests.OneTimeSetUp();
    }

    // Test 1

    /// <summary>
    /// Verifies that the first test <see cref="IBdoExpression"/> instance converts to an
    /// <see cref="ExpressionDto"/> and saves cleanly to a Xml file.
    /// </summary>
    [Test]
    public void SaveXml1Test()
    {
        _dataTests.Create1Test();
        var exp = _dataTests._exp;

        var filePath = string.Format(_filePath_Xml, 1);

        _isSaved1 = exp.ToDto().SaveXml(filePath);
        Assert.That(_isSaved1, Is.True, "Expression saving failed.");
    }

    /// <summary>
    /// Verifies that the first saved Xml file loads back into an <see cref="ExpressionDto"/>,
    /// converts to a POCO via <c>ToPoco()</c>, and matches the original <see cref="IBdoExpression"/>.
    /// </summary>
    [Test]
    public void LoadXml1Test()
    {
        if (!_isSaved1)
        {
            SaveXml1Test();
        }

        var filePath = string.Format(_filePath_Xml, 1);

        var exp = _dataTests._exp;
        var exp_fromDto = XmlHelper.LoadXml<ExpressionDto>(filePath).ToPoco();
        BdoExpressionTests.AssertEquals(exp, exp_fromDto);
    }

    // Test 2

    /// <summary>
    /// Verifies that the second test <see cref="IBdoExpression"/> instance converts to an
    /// <see cref="ExpressionDto"/> and saves cleanly to a Xml file.
    /// </summary>
    [Test]
    public void SaveXml2Test()
    {
        _dataTests.Create2Test();
        var exp = _dataTests._exp;

        var filePath = string.Format(_filePath_Xml, 2);

        _isSaved2 = exp.ToDto().SaveXml(filePath);
        Assert.That(_isSaved2, Is.True, "Expression saving failed.");
    }

    /// <summary>
    /// Verifies that the second saved Xml file loads back into an <see cref="ExpressionDto"/>,
    /// converts to a POCO via <c>ToPoco()</c>, and matches the original <see cref="IBdoExpression"/>.
    /// </summary>
    [Test]
    public void LoadXml2Test()
    {
        if (!_isSaved2)
        {
            SaveXml2Test();
        }

        var filePath = string.Format(_filePath_Xml, 2);

        var exp = _dataTests._exp;
        var exp_fromDto = XmlHelper.LoadXml<ExpressionDto>(filePath).ToPoco();
        BdoExpressionTests.AssertEquals(exp, exp_fromDto);
    }

    // Test 3

    /// <summary>
    /// Verifies that the third scoping test <see cref="IBdoExpression"/> instance converts to an
    /// <see cref="ExpressionDto"/> and saves cleanly to a Xml file.
    /// </summary>
    [Test]
    public void SaveXml3Test()
    {
        _scopingTests.Create3Test();
        var exp = _dataTests._exp;

        var filePath = string.Format(_filePath_Xml, 3);

        _isSaved3 = exp.ToDto().SaveXml(filePath);
        Assert.That(_isSaved3, Is.True, "Expression saving failed.");
    }

    /// <summary>
    /// Verifies that the third saved Xml file loads back into an <see cref="ExpressionDto"/>,
    /// converts to a POCO via <c>ToPoco()</c>, and matches the original <see cref="IBdoExpression"/>.
    /// </summary>
    [Test]
    public void LoadXml3Test()
    {
        if (!_isSaved3)
        {
            SaveXml3Test();
        }

        var filePath = string.Format(_filePath_Xml, 3);

        var exp = _dataTests._exp;
        var exp_fromDto = XmlHelper.LoadXml<ExpressionDto>(filePath).ToPoco();
        BdoExpressionTests.AssertEquals(exp, exp_fromDto);
    }
}