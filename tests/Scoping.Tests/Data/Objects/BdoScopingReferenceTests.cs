using BindOpen.Scoping.Script;
using BindOpen.Tests;
using NUnit.Framework;

namespace BindOpen.Data;

/// <summary>
/// This class provides unit tests for script-based references within the scoping engine,
/// verifying how metadata links to dynamic function results.
/// </summary>
[TestFixture, Order(210)]
public class BdoScopingReferenceTests
{
    /// <summary>
    /// The reference instance used across multiple tests.
    /// </summary>
    public IBdoReference _reference = null;

    /// <summary>
    /// Initializes the test environment and shared reference data.
    /// </summary>
    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        var dataTests = new BdoReferenceTests();
        dataTests.OneTimeSetUp();
    }

    /// <summary>
    /// Tests the creation of a reference from a scriptword name retrieved from a data faker.
    /// </summary>
    [Test, Order(3)]
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
    /// Tests the creation and data resolution of metadata nodes using references.
    /// </summary>
    [Test, Order(4)]
    public void NewReferenceTest()
    {
        // Create a node set where each element uses a script-based reference
        var set = BdoData.NewSet(
                BdoData.NewMeta().WithReference(BdoData.NewReference(BdoScript.Func("eq", 1, 1))),
                BdoData.NewMeta().WithReference(BdoScript.Func("eq", 1, 1))
            );

        // Resolve data using the test scope. 
        // The interpreter should evaluate $eq(1, 1) to true.
        var value1 = set[0].GetData<bool?>(ScopingTestData.Scope);
        var value2 = set[1].GetData<bool?>(ScopingTestData.Scope);

        Assert.That(value1 == true && value2 == true, Is.True, "Failed to resolve boolean data from script references.");
    }

    /// <summary>
    /// Tests the conversion of a standard scriptword into a reference object.
    /// </summary>
    [Test, Order(5)]
    public void NewReferenceFromScriptwordTest()
    {
        // Convert an 'Equals' scriptword directly to a reference
        _reference = BdoScript.Eq(1, 0).ToReference();

        // Verify the formatted script string (numeric arguments are treated as strings in basic scriptwords)
        Assert.That(_reference?.ToString(), Is.EqualTo($"$eq('1', '0')"), "Scriptword to reference conversion string mismatch.");
    }
}