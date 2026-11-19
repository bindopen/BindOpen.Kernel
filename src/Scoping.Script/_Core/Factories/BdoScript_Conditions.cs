using BindOpen.Data;
using BindOpen.Data.Conditions;
using System;

namespace BindOpen.Scoping.Script;

/// <summary>
/// This static class provides factory methods to create script-based conditions and logical operations.
/// </summary>
public static partial class BdoScript
{
    // ------------------------------------------
    // CONDITION FACTORIES
    // ------------------------------------------

    #region Conditions

    /// <summary>
    /// Creates a new script-based condition using a functional builder.
    /// </summary>
    /// <typeparam name="T">The type of the context object used in the builder.</typeparam>
    /// <param name="func">The function defining the scriptword logic.</param>
    /// <returns>Returns a new <see cref="IBdoCondition"/>.</returns>
    public static IBdoCondition NewCondition<T>(Func<ITBdoScriptword<T>, IBdoScriptword> func)
    {
        var word = func?.Invoke(This<T>());

        return BdoData.NewCondition(word);
    }

    /// <summary>
    /// Attaches a script-based condition to a conditional object.
    /// </summary>
    /// <typeparam name="T">The type of the conditional object.</typeparam>
    /// <typeparam name="TThis">The type of the context used for the script building.</typeparam>
    /// <param name="obj">The conditional object to update.</param>
    /// <param name="func">The function defining the condition logic.</param>
    /// <returns>Returns the updated conditional object.</returns>
    public static T WithCondition<T, TThis>(
        this T obj,
        Func<ITBdoScriptword<TThis>, IBdoScriptword> func)
        where T : IBdoConditional
    {
        var condition = NewCondition(func);

        return obj.WithCondition(condition);
    }

    #endregion

    // ------------------------------------------
    // LOGICAL OPERATORS
    // ------------------------------------------

    #region Logical Operators

    /// <summary>
    /// Creates an 'And' logical scriptword.
    /// </summary>
    public static ITBdoScriptword<bool> And(params object[] parameters)
        => Func<bool>("and", parameters);

    /// <summary>
    /// Creates an 'Or' logical scriptword.
    /// </summary>
    public static ITBdoScriptword<bool> Or(params object[] parameters)
        => Func<bool>("or", parameters);

    /// <summary>
    /// Creates a 'Not' logical scriptword.
    /// </summary>
    public static ITBdoScriptword<bool> Not(object obj)
        => Func<bool>("not", obj);

    /// <summary>
    /// Creates an 'Xor' logical scriptword.
    /// </summary>
    public static ITBdoScriptword<bool> Xor(params object[] parameters)
        => Func<bool>("xor", parameters);

    #endregion

    // ------------------------------------------
    // COMPARISON OPERATORS
    // ------------------------------------------

    #region Comparison Operators

    /// <summary>
    /// Creates an 'Equals' comparison scriptword.
    /// </summary>
    public static ITBdoScriptword<bool> Eq(this object obj1, object obj2)
        => Func<bool>("eq", obj1, obj2);

    /// <summary>
    /// Creates an 'InEnum' comparison scriptword to check if a value exists in an enumeration.
    /// </summary>
    public static ITBdoScriptword<bool> InEnum(this object obj1, string typeName)
        => Func<bool>("inEnum", obj1, typeName);

    /// <summary>
    /// Creates a 'Not Equals' comparison scriptword.
    /// </summary>
    public static ITBdoScriptword<bool> Ne(this object obj1, object obj2)
        => Func<bool>("ne", obj1, obj2);

    /// <summary>
    /// Creates a 'Greater Than' comparison scriptword.
    /// </summary>
    public static ITBdoScriptword<bool> Gt(this object obj1, object obj2)
        => Func<bool>("gt", obj1, obj2);

    /// <summary>
    /// Creates a 'Greater Than or Equal' comparison scriptword.
    /// </summary>
    public static ITBdoScriptword<bool> Gte(this object obj1, object obj2)
        => Func<bool>("gte", obj1, obj2);

    /// <summary>
    /// Creates a 'Less Than' comparison scriptword.
    /// </summary>
    public static ITBdoScriptword<bool> Lt(this object obj1, object obj2)
        => Func<bool>("lt", obj1, obj2);

    /// <summary>
    /// Creates a 'Less Than or Equal' comparison scriptword.
    /// </summary>
    public static ITBdoScriptword<bool> Lte(this object obj1, object obj2)
        => Func<bool>("lte", obj1, obj2);

    #endregion
}