namespace BindOpen.Data.Schema;

/// <summary>
/// This static class extends schematized data.
/// </summary>
public static partial class IBdoSchematizedExtensions
{
    /// <summary>
    /// Sets the schema of the specified object.
    /// </summary>
    /// <typeparam name="T">The IBdoSchematized type to consider.</typeparam>
    /// <param name="obj">The object to consider.</param>
    /// <param name="schema">The schema to set.</param>
    /// <returns>Returns the object.</returns>
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
