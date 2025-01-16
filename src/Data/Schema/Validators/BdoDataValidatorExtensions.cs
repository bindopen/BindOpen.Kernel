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
    public static bool Check<TValidator, TSpecified, TSpec>(
        this TSpecified specified,
        TSpec defaultSpec,
        IBdoScope scope,
        IBdoMetaSet varSet = null,
        IBdoLog log = null)
            where TValidator : ITBdoDataValidator<TSpecified, TSpec>, new()
            where TSpecified : IBdoSchematized, IReferenced
            where TSpec : IBdoSchema
    {
        var validator = scope.CreateValidator<TValidator, TSpecified, TSpec>();

        var valid = validator.Check(specified, defaultSpec, varSet, log);

        return valid;
    }

    public static bool Check<TValidator, TSpecified, TSpec>(
        this TSpecified specified,
        IBdoScope scope,
        IBdoMetaSet varSet = null,
        IBdoLog log = null)
        where TValidator : ITBdoDataValidator<TSpecified, TSpec>, new()
        where TSpecified : IBdoSchematized, IReferenced
        where TSpec : IBdoSchema
    {
        var valid = specified.Check<TValidator, TSpecified, TSpec>(default, scope, varSet, log);

        return valid;
    }

    public static bool Check(
        this IBdoMetaData meta,
        IBdoSchema defaultSpec,
        IBdoScope scope,
        IBdoMetaSet varSet = null,
        IBdoLog log = null)
    {
        return meta.Check<BdoDataValidator, IBdoMetaData, IBdoSchema>(
            defaultSpec, scope, varSet, log);
    }

    public static bool Check(
        this IBdoMetaData meta,
        IBdoScope scope,
        IBdoMetaSet varSet = null,
        IBdoLog log = null)
    {
        return meta.Check<BdoDataValidator, IBdoMetaData, IBdoSchema>(
            default, scope, varSet, log);
    }
}