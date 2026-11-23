using BindOpen.Data.Assemblies;
using BindOpen.Data.Objects;
using BindOpen.Data.Schema;
using BindOpen.Logging;
using BindOpen.Scoping;
using System.Collections.Generic;

namespace BindOpen.Data.Meta;

/// <summary>
/// Defines the fundamental contract for all metadata elements within the BindOpen framework.
/// It provides unified access to data retrieval, schema validation, and scoped resolution.
/// </summary>
public interface IBdoMetaData :
    IBdoObjectNotMetable, IBdoReferenced, IBdoConditional, IBdoScoped, IIdentified,
    INamed, IReferenced, IIndexed, IBdoDataTyped,
    ITChild<IBdoMetaData>, IBdoSchematized,
    IUpdatable
{
    // ------------------------------------------
    // DATA RETRIEVAL (OBJECT)
    // ------------------------------------------

    /// <summary>
    /// Retrieves the data value of this instance using the specified scope.
    /// </summary>
    /// <param name="scope">The resolution scope.</param>
    /// <param name="varSet">The set of variables for resolution.</param>
    /// <param name="log">The log for tracking.</param>
    /// <returns>The resolved data object.</returns>
    object GetData(
        IBdoScope scope,
        IBdoMetaSet varSet = null,
        IBdoLog log = null);

    /// <summary>
    /// Retrieves the data value of this instance.
    /// </summary>
    object GetData(
        IBdoMetaSet varSet = null,
        IBdoLog log = null);

    // ------------------------------------------
    // DATA RETRIEVAL (TYPED)
    // ------------------------------------------

    /// <summary>
    /// Retrieves and casts the data value of this instance using the specified scope.
    /// </summary>
    T GetData<T>(
        IBdoScope scope,
        IBdoMetaSet varSet = null,
        IBdoLog log = null);

    /// <summary>
    /// Retrieves and casts the data value of this instance.
    /// </summary>
    T GetData<T>(
        IBdoMetaSet varSet = null,
        IBdoLog log = null);

    // ------------------------------------------
    // LIST RETRIEVAL
    // ------------------------------------------

    /// <summary>
    /// Retrieves the list of data values using the specified scope.
    /// </summary>
    IList<object> GetDataList(
        IBdoScope scope,
        IBdoMetaSet varSet = null,
        IBdoLog log = null);

    /// <summary>
    /// Retrieves the list of data values.
    /// </summary>
    IList<object> GetDataList(
        IBdoMetaSet varSet = null,
        IBdoLog log = null);

    /// <summary>
    /// Retrieves and casts the list of data values using the specified scope.
    /// </summary>
    IList<T> GetDataList<T>(
        IBdoScope scope,
        IBdoMetaSet varSet = null,
        IBdoLog log = null);

    /// <summary>
    /// Retrieves and casts the list of data values.
    /// </summary>
    IList<T> GetDataList<T>(
        IBdoMetaSet varSet = null,
        IBdoLog log = null);

    // ------------------------------------------
    // SCHEMA & VALIDATION
    // ------------------------------------------

    /// <summary>
    /// Retrieves the schema rule associated with this metadata.
    /// </summary>
    /// <param name="scope">The resolution scope.</param>
    /// <param name="groupId">The group ID of the rule.</param>
    /// <param name="ruleKind">The kind of schema rule.</param>
    /// <param name="varSet">The set of variables for resolution.</param>
    /// <param name="log">The log for tracking.</param>
    IBdoSchemaRule GetSchemaRule(
        IBdoScope scope,
        string groupId,
        BdoSchemaRuleKinds ruleKind = BdoSchemaRuleKinds.Requirement,
        IBdoMetaSet varSet = null,
        IBdoLog log = null);

    /// <summary>
    /// Retrieves the schema rule associated with this metadata.
    /// </summary>
    IBdoSchemaRule GetSchemaRule(
        string groupId,
        BdoSchemaRuleKinds ruleKind = BdoSchemaRuleKinds.Requirement,
        IBdoMetaSet varSet = null,
        IBdoLog log = null);

    /// <summary>
    /// Evaluates the conditional value of this metadata.
    /// </summary>
    bool GetConditionValue(
        IBdoMetaSet varSet = null,
        IBdoLog log = null);

    // ------------------------------------------
    // DATA SETTING
    // ------------------------------------------

    /// <summary>
    /// Sets the internal data of this instance.
    /// </summary>
    /// <param name="obj">The data object to store.</param>
    void SetData(object obj);
}