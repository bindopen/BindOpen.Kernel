using BindOpen.Data.Meta;
using BindOpen.Logging;
using BindOpen.Scoping;

namespace BindOpen.Data.Schema;

/// <summary>
/// 
/// </summary>
public static partial class BdoDataValidatorExtensions
{
    /// <summary>
    /// Checks this instance.
    /// </summary>
    /// <param name="paramSet">The set of options to consider.</param>
    /// <param name="optionSet">The set of option schemas to consider.</param>
    /// <param name="allowMissingItems">Indicates whether the items can be missing.</param>
    /// <param name="log">The log to consider.</param>
    /// <returns>Returns the log of check.</returns>
    public static bool Check<TValidator, TSchemaified, TSchema>(
        this IBdoScope scope,
        TSchemaified schemaified,
        TSchema defaultSchema,
        IBdoMetaSet varSet = null,
        IBdoLog log = null)
            where TValidator : ITBdoDataValidator<TSchemaified, TSchema>, new()
            where TSchemaified : IBdoSchematized, IReferenced
            where TSchema : IBdoSchema
    {
        var validator = scope.CreateValidator<TValidator, TSchemaified, TSchema>();

        var valid = validator.Check(schemaified, defaultSchema, varSet, log);

        return valid;
    }

    public static bool Check<TValidator, TSchemaified, TSchema>(
        this IBdoScope scope,
        TSchemaified schemaified,
        IBdoMetaSet varSet = null,
        IBdoLog log = null)
        where TValidator : ITBdoDataValidator<TSchemaified, TSchema>, new()
        where TSchemaified : IBdoSchematized, IReferenced
        where TSchema : IBdoSchema
    {
        var valid = scope.Check<TValidator, TSchemaified, TSchema>(schemaified, default,varSet, log);

        return valid;
    }

    public static bool Check(
        this IBdoScope scope, 
        IBdoMetaData meta,
        IBdoSchema defaultSchema,
        IBdoMetaSet varSet = null,
        IBdoLog log = null)
    {
        return scope.Check<BdoDataValidator, IBdoMetaData, IBdoSchema>(meta, defaultSchema, varSet, log);
    }

    public static bool Check(
        this IBdoScope scope, 
        IBdoMetaData meta,        
        IBdoMetaSet varSet = null,
        IBdoLog log = null)
    {
        return scope.Check<BdoDataValidator, IBdoMetaData, IBdoSchema>(meta, default, varSet, log);
    }
}