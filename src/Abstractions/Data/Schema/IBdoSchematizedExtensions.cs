namespace BindOpen.Data.Schema;

/// <summary>
/// Provides extension methods for IBdoSchematized to facilitate the fluent assignment 
/// of schemas to data objects.
/// </summary>
public static partial class IBdoSchematizedExtensions
{
    /// <summary>
    /// Sets the schema for the specified object and returns the instance for fluent chaining.
    /// </summary>
    /// <typeparam name="T">The type implementing IBdoSchematized.</typeparam>
    /// <param name="obj">The object to which the schema will be applied.</param>
    /// <param name="schema">The schema (blueprint) to associate with the object.</param>
    /// <returns>Returns the original object instance with the schema applied.</returns>
    public static T WithSchema<T>(
        this T obj,
        IBdoSchema schema)
        where T : IBdoSchematized
    {
        if (obj != null)
        {
            obj.Schema = schema;
        }

        return obj;
    }
}