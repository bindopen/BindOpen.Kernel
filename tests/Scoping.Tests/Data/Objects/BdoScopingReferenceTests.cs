using BindOpen.Scoping.Script;
using BindOpen.Tests;
using NUnit.Framework;

namespace BindOpen.Data;

/// <summary>
/// Contains unit tests for script-based <see cref="IBdoReference"/> instances within the BindOpen scoping engine,
/// verifying how metadata elements link to and evaluate dynamic script function results.
/// </summary>
/// <remarks>
/// These tests verify script reference serialization syntax, scope-based expression evaluation on metadata sets,
/// and direct conversion from <see cref="IBdoScriptword"/> instances into <see cref="IBdoReference"/> objects.
/// </remarks>
[TestFixture]
public class BdoScopingReferenceTests
{
    /// <summary>
    /// Gets or sets the <see cref="IBdoReference"/> instance currently under test.
    /// </summary>
    public IBdoReference _reference = null;

    /// <summary>
    /// Initializes the shared reference test environment before any tests in this fixture are executed.
    /// </summary>
    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        var dataTests = new BdoReferenceTests();
        dataTests.OneTimeSetUp();
    }

    /// <summary>
    /// Verifies that creating an <see cref="IBdoReference"/> from a <see cref="BdoScript.Func"/> scriptword
    /// serializes to the expected <c>$funcName()</c> script syntax.
    /// </summary>
    [Test]
    public void Create3Test()
    {
        // 1. Retrieve test data
        var valueSet = BdoExpressionFaker.GetValueSet();
        string funcName = valueSet.ScriptwordName as string;

        // 2. Create the reference using a function scriptword
        _reference = BdoData.NewReference(BdoScript.Func(funcName));

        // 3. Verify the string representation matches the expected script syntax
        Assert.That(_reference?.ToString(), Is.EqualTo($"${funcName}()"), "The reference string representation is incorrect.");
    }

    /// <summary>
    /// Verifies that metadata elements configured with script function references (<c>$eq(1, 1)</c>)
    /// dynamically evaluate and resolve their expected boolean values within an <see cref="IBdoScope"/>.
    /// </summary>
    [Test]
    public void NewReferenceTest()
    {
        // Create a node set where each element uses a script-based reference
        var set = BdoData.NewSet(
            BdoData.NewMeta().WithReference(BdoData.NewReference(BdoScript.Func("eq", 1, 1))),
            BdoData.NewMeta().WithReference(BdoData.NewReference(BdoScript.Func("eq", 1, 1)))
        );

        // Resolve data using the test scope.
        // The interpreter should evaluate $eq(1, 1) to true.
        var value1 = set[0].GetData<bool?>(ScopingTestData.Scope);
        var value2 = set[1].GetData<bool?>(ScopingTestData.Scope);

        Assert.Multiple(() =>
        {
            Assert.That(value1, Is.True, "Failed to resolve boolean data from the first script reference.");
            Assert.That(value2, Is.True, "Failed to resolve boolean data from the second script reference.");
        });
    }

    /// <summary>
    /// Verifies that converting an <see cref="IBdoScriptword"/> (such as <see cref="BdoScript.Eq"/>)
    /// into an <see cref="IBdoReference"/> via <c>ToReference()</c> formats arguments using the expected script syntax.
    /// </summary>
    [Test]
    public void NewReferenceFromScriptwordTest()
    {
        // Convert an 'Equals' scriptword directly to a reference
        _reference = BdoScript.Eq(1, 0).ToReference();

        // Verify the formatted script string (numeric arguments are treated as strings in basic scriptwords)
        Assert.That(_reference?.ToString(), Is.EqualTo("$eq('1', '0')"), "Scriptword to reference conversion string mismatch.");
    }
}