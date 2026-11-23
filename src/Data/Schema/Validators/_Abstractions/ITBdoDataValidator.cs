using BindOpen.Data.Meta;
using BindOpen.Logging;
using BindOpen.Scoping;

namespace BindOpen.Data.Schema;

/// <summary>
/// Defines a data validator responsible for verifying that a schematized object 
/// adheres to its associated schema requirements and constraints.
/// </summary>
/// <typeparam name="TSchematized">The type of the schematized and referenced object to validate.</typeparam>
/// <typeparam name="TSchema">The type of schema to validate against.</typeparam>
public interface ITBdoDataValidator<TSchematized, TSchema> : IBdoScoped
    where TSchematized : IBdoSchematized, IReferenced
    where TSchema : IBdoSchema
{
    /// <summary>
    /// Validates the schematized object against its own internal schema.
    /// </summary>
    /// <param name="obj">The object to validate.</param>
    /// <param name="varSet">The set of variables to use for dynamic rule resolution.</param>
    /// <param name="log">The log used to track validation results and errors.</param>
    /// <returns>Returns true if the object is valid; otherwise, false.</returns>
    bool Check(
        TSchematized obj,
        IBdoMetaSet varSet = null,
        IBdoLog log = null);

    /// <summary>
    /// Validates the schematized object against an explicit external schema.
    /// </summary>
    /// <param name="obj">The object to validate.</param>
    /// <param name="schema">The schema to validate against.</param>
    /// <param name="varSet">The set of variables to use for dynamic rule resolution.</param>
    /// <param name="log">The log used to track validation results and errors.</param>
    /// <returns>Returns true if the object is valid; otherwise, false.</returns>
    bool Check(
        TSchematized obj,
        TSchema schema,
        IBdoMetaSet varSet = null,
        IBdoLog log = null);
}