using BindOpen.Data;
using BindOpen.Data.Meta;
using BindOpen.Scoping.Script;
using NUnit.Framework;

namespace BindOpen.Scoping.Script;

/// <summary>
/// Contains unit tests for fluid scriptword navigation within the BindOpen scoping engine,
/// verifying the interpreter's ability to traverse metadata hierarchies using contextual keywords
/// such as <c>this</c> and <c>parent</c>.
/// </summary>
/// <remarks>
/// These tests cover both raw script expressions (e.g., <c>$(this).(parent).value()</c>) and strongly-typed
/// fluent <see cref="BdoScript"/> builder chains across valid and broken parent-child metadata trees.
/// </remarks>
[TestFixture]
public class BdoScriptwordTests_Fluid
{
    /// <summary>
    /// Verifies tree traversal using a raw script expression (<c>$(this).(parent).value()</c>)
    /// to resolve the value of a parent metadata node from a child context mapped to <c>this</c>.
    /// </summary>
    [Test]
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
    /// Verifies sibling traversal using the fluent <see cref="BdoScript.This{T}"/> API
    /// (<c>this -> parent -> descendant("titi") -> value</c>) within an <see cref="IBdoMetaObject"/>.
    /// </summary>
    [Test]
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
    /// Verifies relative navigation starting directly from <see cref="BdoScript._Parent{T}"/>,
    /// ensuring sibling lookup succeeds when a parent exists and safely evaluates to <see langword="null"/>
    /// when the parent reference is missing.
    /// </summary>
    [Test]
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