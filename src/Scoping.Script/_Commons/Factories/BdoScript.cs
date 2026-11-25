using BindOpen.Data;
using BindOpen.Data.Meta;
using BindOpen.Data.Meta.Reflection;
using BindOpen.Data.Objects;
using System;

namespace BindOpen.Scoping.Script;

/// <summary>
/// This static class provides factory methods to create and manipulate script elements.
/// </summary>
/// <remarks>
/// It acts as the central hub for programmatically building scriptword chains 
/// (Functions, Variables, etc.) using a fluent API.
/// </remarks>
public static partial class BdoScript
{
    /// <summary>
    /// Converts a generic script word to a specific implementation type.
    /// </summary>
    /// <typeparam name="T">The target script word implementation type.</typeparam>
    /// <param name="word">The source script word to convert.</param>
    /// <returns>Returns a new instance of T populated with the source word's data.</returns>
    public static T To<T>(this IBdoScriptword word) where T : BdoScriptword, new()
    {
        var toWord = BdoData.New<T>()
            .WithName(word?.Name)
            .WithTokenKind(word?.TokenKind ?? ScriptTokenKinds.None)
            .WithChild(word?.Child)
            .WithParent(word?.Parent as IBdoScriptword);

        if (word != null)
        {
            foreach (var param in word)
            {
                toWord.Add(param);
            }
        }

        return toWord;
    }

    // ------------------------------------------
    // WORD FACTORIES
    // ------------------------------------------

    #region Word

    /// <summary>
    /// Creates a new strongly-typed script word.
    /// </summary>
    /// <typeparam name="T">The type of the item associated with the word.</typeparam>
    /// <param name="kind">The token kind (Function, Variable, etc.).</param>
    /// <param name="name">The name of the script word.</param>
    public static ITBdoScriptword<T> NewWord<T>(
        ScriptTokenKinds kind,
        string name = null)
        => new TBdoScriptword<T>()
            .WithName(name)
            .WithTokenKind(kind);

    /// <summary>
    /// Creates a new strongly-typed script word from a meta object.
    /// </summary>
    public static ITBdoScriptword<T> NewWord<T>(IBdoMetaObject meta)
    {
        var word = new TBdoScriptword<T>();
        word.WithDataType(BdoExtensionKinds.Scriptword, meta?.DataType?.DefinitionUniqueName)
            .AddItems(meta?.Items?.ToArray());
        return word;
    }

    /// <summary>
    /// Creates a new script word.
    /// </summary>
    public static IBdoScriptword NewWord(
        ScriptTokenKinds kind,
        string name = null)
        => new BdoScriptword()
            .WithName(name)
            .WithTokenKind(kind);

    /// <summary>
    /// Creates a new script word from a meta object.
    /// </summary>
    public static IBdoScriptword NewWord(IBdoMetaObject meta)
    {
        var word = new BdoScriptword();
        word.WithDataType(BdoExtensionKinds.Scriptword, meta?.DataType?.DefinitionUniqueName)
            .AddItems(meta?.Items?.ToArray());
        return word;
    }

    /// <summary>
    /// Creates a new script word using a functional builder.
    /// </summary>
    public static IBdoScriptword NewWord<T>(Func<ITBdoScriptword<T>, IBdoScriptword> func)
    {
        return func?.Invoke(This<T>());
    }

    #endregion

    // ------------------------------------------
    // VARIABLE FACTORIES (Var)
    // ------------------------------------------

    #region Variable

    /// <summary>
    /// Creates a new script variable (e.g., $name).
    /// </summary>
    public static IBdoScriptword Variable(string name)
        => NewWord(ScriptTokenKinds.Variable, name);

    public static IBdoScriptword Var(string name) => Variable(name);

    /// <summary>
    /// Appends a variable to an existing script word chain.
    /// </summary>
    public static IBdoScriptword Variable(this IBdoScriptword word, string name)
        => Variable(name).WithParent(word);

    public static IBdoScriptword Var(this IBdoScriptword word, string name)
        => word.Variable(name);

    /// <summary>
    /// Creates a new strongly-typed script variable.
    /// </summary>
    public static ITBdoScriptword<T> Variable<T>(string name)
        => NewWord<T>(ScriptTokenKinds.Variable, name);

    public static ITBdoScriptword<T> Var<T>(string name) => Variable<T>(name);

    /// <summary>
    /// Appends a strongly-typed variable to an existing script word chain.
    /// </summary>
    public static ITBdoScriptword<T> Variable<T>(this IBdoScriptword word, string name)
        => Variable<T>(name).WithParent(word);

    public static ITBdoScriptword<T> Var<T>(this IBdoScriptword word, string name)
        => word.Variable<T>(name);

    #endregion

    // ------------------------------------------
    // FUNCTION FACTORIES (Func)
    // ------------------------------------------

    #region Function

    /// <summary>
    /// Creates a new strongly-typed script function with parameters.
    /// </summary>
    public static ITBdoScriptword<T> Function<T>(string name, params object[] parameters)
    {
        var word = NewWord<T>(ScriptTokenKinds.Function, name);
        if (parameters != null)
        {
            foreach (var param in parameters)
            {
                word.InsertData(param);
            }
        }
        return word;
    }

    /// <summary>
    /// Creates a new script function with parameters.
    /// </summary>
    public static IBdoScriptword Function(string name, params object[] parameters)
    {
        var word = NewWord(ScriptTokenKinds.Function, name);
        if (parameters != null)
        {
            foreach (var param in parameters)
            {
                word.InsertData(param);
            }
        }
        return word;
    }

    public static IBdoScriptword Func(string name, params object[] parameters)
        => Function(name, parameters);

    public static ITBdoScriptword<T> Func<T>(string name, params object[] parameters)
        => Function<T>(name, parameters);

    /// <summary>
    /// Appends a function to an existing script word chain.
    /// </summary>
    public static IBdoScriptword Function(this IBdoScriptword word, string name, params object[] parameters)
        => Function(name, parameters).WithParent(word);

    public static IBdoScriptword Func(this IBdoScriptword word, string name, params object[] parameters)
        => word.Function(name, parameters);

    /// <summary>
    /// Appends a strongly-typed function to an existing script word chain.
    /// </summary>
    public static ITBdoScriptword<T> Function<T>(this IBdoScriptword word, string name, params object[] parameters)
        => Function<T>(name, parameters).WithParent(word);

    public static ITBdoScriptword<T> Func<T>(this IBdoScriptword word, string name, params object[] parameters)
        => word.Function<T>(name, parameters);

    #endregion

    // ------------------------------------------
    // SPECIALIZED CONTEXT (This, Parent)
    // ------------------------------------------

    #region Context

    /// <summary>
    /// Creates a script word referring to the current context object ($this).
    /// </summary>
    public static ITBdoScriptword<T> This<T>()
        => Var<T>(BdoData.__VarName_This);

    /// <summary>
    /// Creates a script word chain referring to the parent of the current context ($this.parent).
    /// </summary>
    public static ITBdoScriptword<T> _Parent<T>()
        => Var(BdoData.__VarName_This).Var<T>("parent");

    #endregion

    // ------------------------------------------
    // REFLECTION HELPERS
    // ------------------------------------------

    #region Reflection

    /// <summary>
    /// Retrieves a property value from a BindOpen object using reflection.
    /// </summary>
    /// <param name="obj">The object to inspect.</param>
    /// <param name="propName">The name of the property to retrieve.</param>
    /// <returns>Returns the value of the specified property.</returns>
    [BdoFunction("prop")]
    public static object Property(
        this IBdoObject obj,
        string propName)
    {
        return obj?.GetPropertyValue(propName, typeof(BdoPropertyAttribute));
    }

    #endregion
}