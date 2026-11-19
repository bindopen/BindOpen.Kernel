using BindOpen.Data.Meta;
using BindOpen.Scoping.Script;
using System;

namespace BindOpen.Scoping.Tests;

/// <summary>
/// This class provides complex script function extensions used to test the 
/// BindOpen interpreter's ability to handle domains, parents, and chained calls.
/// </summary>
public static class ScriptWordDefinitionFake
{
    /// <summary>
    /// Evaluates 'Func1'. Illustrates direct argument access via the script domain.
    /// Usage: $Func1('myValue')
    /// </summary>
    [BdoFunction(
        Name = "Func1",
        Description = "Test function 1.",
        CreationDate = "2022-06-24")]
    public static object Fun_Func1(IBdoScriptDomain domain)
    {
        // Accessing the first argument (index 0) from the scriptword itself
        return domain?.Scriptword?.GetData<string>(0);
    }

    /// <summary>
    /// Evaluates 'subFun'. Demonstrates a "chained" or "extension" function.
    /// Usage: $FuncX().subFun('suffix')
    /// </summary>
    [BdoFunction(
        Name = "subFun",
        Description = "Test function 2.",
        CreationDate = "2022-06-24")]
    public static object Fun_Func2a(
        this object obj1, // The result of the preceding function in the chain
        string st,
        IBdoScriptDomain domain)
    {
        return Concat(domain, obj1, st);
    }

    /// <summary>
    /// Evaluates 'Func2'. Checks for case-insensitive equality between two arguments.
    /// Usage: $Func2('abc', 'ABC') -> returns true
    /// </summary>
    [BdoFunction(
        Name = "Func2",
        Description = "Test function 2.",
        CreationDate = "2022-06-24")]
    public static object Fun_Func2b(IBdoScriptDomain domain)
    {
        string value1 = domain?.Scriptword?.GetData<string>(0);
        string value2 = domain?.Scriptword?.GetData<string>(1);

        return value1?.Equals(value2, StringComparison.OrdinalIgnoreCase) ?? false;
    }

    /// <summary>
    /// Evaluates 'Func3'. Demonstrates standard parameter mapping without using the domain.
    /// </summary>
    [BdoFunction(
        Name = "Func3",
        CreationDate = "2022-06-24")]
    public static object Fun_Func3(
        [BdoParameter(Name = "Value1")] object value1,
        [BdoParameter(Name = "Value2")] object value2)
    {
        return value1?.ToString() == value2?.ToString();
    }

    /// <summary>
    /// Evaluates 'Fun_Func4'. Demonstrates tree traversal by accessing the parent word's data.
    /// </summary>
    [BdoFunction]
    public static object Fun_Func4(IBdoScriptDomain domain)
    {
        string value = domain?.Scriptword?.GetData<string>(0);
        // Accesses the result or data of the word this function is attached to
        string parentValue = domain?.Scriptword?.Parent?.GetData<string>();

        return $"{parentValue}:{value}";
    }

    /// <summary>
    /// Evaluates 'concatTest'. Demonstrates handling of variable argument lengths (params).
    /// </summary>
    [BdoFunction(Name = "concatTest")]
    public static object Concat(
        IBdoScriptDomain domain,
        params object[] values)
    {
        // Joins all provided arguments with a hyphen
        return string.Join('-', values);
    }
}