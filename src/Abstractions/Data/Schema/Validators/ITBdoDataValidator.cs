using BindOpen.Data.Meta;
using BindOpen.Logging;
using BindOpen.Scoping;

namespace BindOpen.Data.Schema;

/// <summary>
/// This interface defines a data validator.
/// </summary>
public interface ITBdoDataValidator<TSpecified, TSchema> : IBdoScoped
    where TSpecified : IBdoSchematized, IReferenced
    where TSchema : IBdoSchema
{
    /// <summary>
    /// Checks the specified meta data.
    /// </summary>
    /// <param name="meta">The meta data to check.</param>
    /// <param name="log">The log to consider.</param>
    /// <returns>Returns the check log./returns>
    bool Check(
        TSpecified obj,
        IBdoMetaSet varSet = null,
        IBdoLog log = null);

    /// <summary>
    /// Checks the specified meta data corresponding to the meta schema.
    /// </summary>
    /// <param name="meta">The meta data to check.</param>
    /// <param name="schema">The meta schema to consider.</param>
    /// <param name="log">The log to consider.</param>
    /// <returns>Returns the check log./returns>
    bool Check(
        TSpecified obj,
        TSchema schema,
        IBdoMetaSet varSet = null,
        IBdoLog log = null);
}