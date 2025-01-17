﻿using BindOpen.Data.Conditions;
using BindOpen.Data.Meta;
using BindOpen.Logging;
using BindOpen.Scoping;

namespace BindOpen.Data.Schema;

/// <summary>
/// 
/// </summary>
public class BdoSchemaRule : BdoObject, IBdoSchemaRule
{
    // --------------------------------------------------
    // PROPERTIES
    // --------------------------------------------------

    #region Properties

    /// <summary>
    /// The mode.
    /// </summary>
    public BdoSchemaRuleKinds Kind { get; set; }

    /// <summary>
    /// The group identifier.
    /// </summary>
    public string GroupId { get; set; }

    /// <summary>
    /// The group identifier.
    /// </summary>
    public IBdoReference Reference { get; set; }

    /// <summary>
    /// The result code.
    /// </summary>
    public string ResultCode { get; set; }

    public BdoEventLevels ResultEventLevel { get; set; }

    public string ResultTitle { get; set; }

    public string ResultDescription { get; set; }

    /// <summary>
    /// The value.
    /// </summary>
    public object Value { get; set; }

    #endregion

    // --------------------------------------------------
    // CONVERTERS
    // --------------------------------------------------

    #region Converters

    public static implicit operator BdoSchemaRule((string Reference, object Value, IBdoCondition Condition) item)
    {
        var rule = BdoData.NewRequirement(item.Reference, item.Value, item.Condition);

        return rule;
    }

    #endregion

    // ------------------------------------------
    // CONSTRUCTORS
    // ------------------------------------------

    #region Constructors

    /// <summary>
    /// Instantiates a new instance of the BdoSchemaRule class.
    /// </summary>
    public BdoSchemaRule()
    {
    }

    #endregion

    // ------------------------------------------
    // IReferenced Implementation
    // ------------------------------------------

    #region IReferenced

    /// <summary>
    /// 
    /// </summary>
    public string Key() => Identifier;

    #endregion

    // ------------------------------------------
    // IIndexed Implementation
    // ------------------------------------------

    #region IIndexed

    /// <summary>
    /// The index of this instance.
    /// </summary>
    public int? Index { get; set; }

    #endregion

    // ------------------------------------------
    // IIdentified Implementation
    // ------------------------------------------

    #region IIdentified

    /// <summary>
    /// 
    /// </summary>
    public string Identifier { get; set; }

    #endregion

    // ------------------------------------------
    // IConditional Implementation
    // ------------------------------------------

    #region IConditional

    /// <summary>
    /// The condition.
    /// </summary>
    public IBdoCondition Condition { get; set; }

    /// <summary>
    /// The item requirement level of this instance.
    /// </summary>
    public bool GetConditionValue(
        IBdoScope scope = null,
        IBdoMetaSet varSet = null,
        IBdoLog log = null)
    {
        var b = scope?.Interpreter?.Evaluate(Condition, varSet, log) == true;

        return b;
    }

    #endregion
}