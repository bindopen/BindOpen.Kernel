using BindOpen.Data.Objects;
using BindOpen.Scoping.Script;
using BindOpen.Tests;
using NUnit.Framework;

namespace BindOpen.Data;

/// <summary>
/// Contains unit tests for script-based <see cref="IBdoExpression"/> instances within the BindOpen scoping engine.
/// </summary>
/// <remarks>
/// These tests verify that script expressions constructed via <see cref="BdoScript"/> instantiate properly
/// and satisfy the core expression invariants validated by <see cref="BdoExpressionTests"/>.
/// </remarks>
[TestFixture]
public class BdoScopingExpressionTests
{
    private IBdoExpression _exp;
    private BdoExpressionTests _dataTests;

    /// <summary>
    /// Initializes the shared <see cref="BdoExpressionTests"/> helper and prepares the test dataset
    /// before any tests in this fixture are executed.
    /// </summary>
    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        // Use a shared data test instance to leverage common validation logic
        _dataTests = new BdoExpressionTests();
        _dataTests.OneTimeSetUp();
    }

    /// <summary>
    /// Verifies the creation of a script function expression via <see cref="BdoScript.Func"/>
    /// using a generated function name from <see cref="BdoExpressionFaker"/>, and validates its structure.
    /// </summary>
    [Test]
    public void Create3Test()
    {
        // 1. Retrieve test data (function names, etc.) from the faker
        var valueSet = BdoExpressionFaker.GetValueSet();
        string functionName = valueSet.ScriptwordName as string;

        // 2. Create the script expression.
        // Ensure it is identified as a 'Script' to trigger the proper interpreter logic.
        _exp = BdoScript.Func(functionName);

        // 3. Perform shared validation tests
        _dataTests.Test(_exp);

        // 4. Specific scoping assertions
        Assert.That(_exp, Is.Not.Null, "The scoping expression failed to instantiate.");
    }
}