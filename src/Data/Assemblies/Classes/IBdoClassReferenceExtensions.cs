using BindOpen.Data.Assemblies;

namespace BindOpen.Data;

/// <summary>
/// Provides extension methods for IBdoClassReference to facilitate fluent configuration 
/// of dynamic type references.
/// </summary>
public static partial class IBdoClassReferenceExtensions
{
    /// <summary>
    /// Sets the class name of the specified class reference.
    /// </summary>
    /// <typeparam name="T">The type implementing IBdoClassReference.</typeparam>
    /// <param name="obj">The class reference instance to update.</param>
    /// <param name="className">The full name of the class (e.g., "Namespace.ClassName").</param>
    /// <returns>Returns the updated class reference instance for fluent chaining.</returns>
    public static T WithClassName<T>(
        this T obj,
        string className)
        where T : IBdoClassReference
    {
        if (obj != null)
        {
            obj.ClassName = className;
        }
        return obj;
    }

    /// <summary>
    /// Sets the class name based on the specified .NET type.
    /// </summary>
    /// <typeparam name="T">The type implementing IBdoClassReference.</typeparam>
    /// <param name="obj">The class reference instance.</param>
    /// <param name="type">The .NET type to extract the class name from.</param>
    /// <returns>Returns the updated class reference instance.</returns>
    public static T WithClassName<T>(
        this T obj,
        System.Type type)
        where T : IBdoClassReference
    {
        if (obj != null && type != null)
        {
            obj.ClassName = type.FullName;
        }
        return obj;
    }
}