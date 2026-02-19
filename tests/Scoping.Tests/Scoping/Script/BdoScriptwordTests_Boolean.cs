using BindOpen.Data;
using NUnit.Framework;

namespace BindOpen.Scoping.Script;

/// <summary>
/// This class provides unit tests for Boolean-related scriptwords, ensuring that 
/// equality and comparison logic are correctly interpreted by the scoping engine.
/// </summary>
[TestFixture, Order(401)]
public class BdoScriptwordTests_Boolean
{
    /// <summary>
    /// Tests the basic equality function ($eq) between a literal string 
    /// and the result of a nested script function ($text).
    /// </summary>
    [Test, Order(201)]
    public void EqTest()
    {
        // 1. Create a scriptword representing $eq('MYTABLE', $text('MYTABLE'))
        var word = BdoScript.Eq("MYTABLE", BdoScript.Text("MYTABLE"));

        // 2. Retrieve the interpreter from the scope
        var interpreter = ScopingTestData.Scope.Interpreter;

        // 3. Evaluate the scriptword as a nullable Boolean
        var result = interpreter.Evaluate<bool?>(word);

        // 4. Assert that the strings are recognized as equal
        Assert.That(result, Is.True, "The equality scriptword evaluation failed for identical strings.");
    }

    /// <summary>
    /// Tests equality logic where one operand is a variable ($var) 
    /// that must be resolved from a provided metadata set.
    /// </summary>
    [Test, Order(202)]
    public void SubwordTest()
    {
        // 1. Create a scriptword representing $eq($var('MYTABLE'), 135)
        var word = BdoScript.Eq(BdoScript.Var("MYTABLE"), 135);

        var interpreter = ScopingTestData.Scope.Interpreter;

        // 2. Prepare the variable set (the execution context)
        var metaSet = BdoData.NewSet(
            ("MYTABLE", 135)
        );

        // 3. Evaluate the scriptword while providing the context for variable resolution
        var result = interpreter.Evaluate<bool?>(word, metaSet);

        // 4. Assert that the variable was correctly resolved and compared
        Assert.That(result, Is.True, "The equality check failed during variable resolution.");
    }
}