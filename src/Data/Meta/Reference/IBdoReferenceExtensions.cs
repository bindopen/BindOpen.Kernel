using BindOpen.Data.Meta;
using BindOpen.Data.Objects.Interfaces;

namespace BindOpen.Data;

/// <summary>
/// This class provides extension methods for the IBdoReference interface.
/// </summary>
public static class IBdoReferenceExtensions
{
    /// <summary>
    /// Sets the specified expression to this reference.
    /// </summary>
    /// <typeparam name="T">The reference type.</typeparam>
    /// <param name="reference">The reference to configure.</param>
    /// <param name="exp">The expression to assign.</param>
    /// <returns>Returns the reference instance for fluent chaining.</returns>
    public static T WithExpression<T>(
        this T reference,
        IBdoExpression exp)
        where T : IBdoReference
    {
        if (reference != null)
        {
            reference.Kind = BdoReferenceKind.Expression;
            reference.Expression = exp;
        }

        return reference;
    }

    /// <summary>
    /// Sets the specified metadata to this reference.
    /// </summary>
    /// <typeparam name="T">The reference type.</typeparam>
    /// <param name="reference">The reference to configure.</param>
    /// <param name="meta">The metadata to assign.</param>
    /// <returns>Returns the reference instance for fluent chaining.</returns>
    public static T WithMetaData<T>(
        this T reference,
        IBdoMetaData meta)
        where T : IBdoReference
    {
        if (reference != null)
        {
            reference.Kind = BdoReferenceKind.MetaData;
            reference.MetaData = meta;
        }

        return reference;
    }

    /// <summary>
    /// Sets the specified identifier to this reference.
    /// </summary>
    /// <typeparam name="T">The reference type.</typeparam>
    /// <param name="reference">The reference to configure.</param>
    /// <param name="identifier">The identifier to assign.</param>
    /// <returns>Returns the reference instance for fluent chaining.</returns>
    public static T WithIdentifier<T>(
        this T reference,
        string identifier)
        where T : IBdoReference
    {
        if (reference != null)
        {
            reference.Kind = BdoReferenceKind.Identifier;
            reference.Identifier = identifier;
        }

        return reference;
    }
}