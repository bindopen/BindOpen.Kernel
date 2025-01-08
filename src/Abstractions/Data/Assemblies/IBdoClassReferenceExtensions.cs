using BindOpen.Data.Assemblies;

namespace BindOpen.Data;

/// <summary>
/// This class provides extensions of IBdoClassReference class.
/// </summary>
public static partial class IBdoClassReferenceExtensions
{
    /// <summary>
    /// Sets the file name of the specified class reference.
    /// </summary>
    /// <typeparam name="T">The class of the specified class reference.</typeparam>
    /// <param name="className">The class name to consider.</param>
    /// <returns>Returns the class reference.</returns>
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
}
