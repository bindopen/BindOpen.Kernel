using BindOpen.Data;
using BindOpen.Data.Meta;
using BindOpen.Scoping.Script;
using NUnit.Framework;

namespace BindOpen.Scoping.Script;

/// <summary>
/// This class provides unit tests for fluid scriptword navigation, verifying the 
/// interpreter's ability to traverse metadata hierarchies using keywords like 'this' and 'parent'.
/// </summary>
[TestFixture]
public class BdoScriptwordTests_Fluid
{
    /// <summary>
    /// Tests tree navigation using a raw script string. 
    /// Path: current object -> parent -> value.
    /// </summary>
    [Test, Order(201)]
    public void InterpreteStringThisTest()
    {
        // 1. Define the script: Get current item, move to parent, get its value
        var exp = BdoData.NewExp("$(this).(parent).value()");

        var interpreter = ScopingTestData.Scope.Interpreter;

        // 2. Set up the hierarchy: 123 is the child of 125
        var meta = BdoData.NewMeta(123).WithParent(BdoData.NewMeta(125));

        // 3. Create the execution context by mapping the 'this' keyword to our object
        var metaSet = BdoData.NewSet((BdoData.__VarName_This, meta));

        // 4. Evaluate and expect the parent's value
        var result = interpreter.Evaluate<int?>(exp, metaSet);

        Assert.That(result, Is.EqualTo(125), "Fluid string-based navigation failed.");
    }

    /// <summary>
    /// Tests tree navigation using the fluent C# API to build the scriptword.
    /// Path: current item -> parent -> find sibling 'titi' -> get value.
    /// </summary>
    [Test, Order(201)]
    public void InterpreteWordThisTest()
    {
        // 1. Build the scriptword fluently
        var exp = BdoScript.This<IBdoMetaData>()._Parent()._Descendant("titi")._Value();

        var interpreter = ScopingTestData.Scope.Interpreter;

        // 2. Set up a node set (parent) containing two children (toto and titi)
        var set = BdoData.NewObject().Add(("toto", 123), ("titi", 125));
        var meta = set[0]; // 'this' will be 'toto'

        var metaSet = BdoData.NewSet((BdoData.__VarName_This, meta));

        // 3. Evaluate: 'toto' -> parent -> find 'titi' -> 125
        var result = interpreter.Evaluate<int?>(exp, metaSet);

        Assert.That(result, Is.EqualTo(125), "Fluid word-based navigation failed.");
    }

    /// <summary>
    /// Tests relative navigation starting directly from the parent context.
    /// Also verifies that navigation safely returns null if the hierarchy is broken.
    /// </summary>
    [Test, Order(201)]
    public void InterpreteWordParentTest()
    {
        // 1. Build script: start from parent -> find descendant 'titi' -> get value
        var exp = BdoScript._Parent<IBdoMetaData>()._Descendant("titi")._Value();

        var interpreter = ScopingTestData.Scope.Interpreter;

        // 2. Valid hierarchy test
        var set = BdoData.NewObject()
            .Add(("toto", 123), ("titi", 125));
        var meta = set[0];

        var varSet = BdoData.NewSet((BdoData.__VarName_This, meta));

        var result = interpreter.Evaluate<int?>(exp, varSet);
        Assert.That(result, Is.EqualTo(125), "Relative parent navigation failed.");

        // 3. Broken hierarchy test (Parent is null)
        BdoMetaData meta1 = ("toto", 123); // No parent assigned
        varSet.Add(BdoData.__VarName_This, meta1);

        result = interpreter.Evaluate<int?>(exp, varSet);

        // The interpreter should handle the null parent gracefully and return null
        Assert.That(result, Is.Null, "Navigation should return null when the parent is missing.");
    }
}