using BindOpen.Data;
using NUnit.Framework;

namespace BindOpen.Scoping.Script;

/// <summary>
/// Contains unit tests for Boolean-related scriptwords, verifying that equality and comparison
/// expressions are accurately evaluated by the BindOpen script interpreter.
/// </summary>
/// <remarks>
/// These tests verify <see cref="BdoScript.Eq"/> evaluation across nested function calls
/// (such as <see cref="BdoScript.Text"/>) and runtime variable lookups (<see cref="BdoScript.Var"/>)
/// resolved against a contextual <see cref="IBdoMetaSet"/>.
/// </remarks>
[TestFixture]
public class BdoScriptwordTests_Boolean
{
    /// <summary>
    /// Verifies that <see cref="BdoScript.Eq"/> evaluates to <see langword="true"/> when comparing
    /// a literal string operand against the output of a nested <see cref="BdoScript.Text"/> function call.
    /// </summary>
    [Test]
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
    /// Verifies that <see cref="BdoScript.Eq"/> resolves a <see cref="BdoScript.Var"/> operand
    /// from the supplied <see cref="IBdoMetaSet"/> context and evaluates numeric equality accurately.
    /// </summary>
    [Test]
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