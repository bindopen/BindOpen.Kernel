using BindOpen.Data.Helpers;
using BindOpen.Data.Meta;
using BindOpen.Logging;
using BindOpen.Scoping;
using System.Data;
using System.Linq;

namespace BindOpen.Data.Schema;

/// <summary>
/// This class represents a data validator.
/// </summary>
public class BdoDataValidator : ITBdoDataValidator<IBdoMetaData, IBdoSchema>
{
    /// <summary>
    /// The scope of this instance.
    /// </summary>
    public IBdoScope Scope { get; set; }

    /// <summary>
    /// Checks the specified meta data.
    /// </summary>
    /// <param name="meta">The meta data to check.</param>
    /// <returns>Returns the check log./returns>
    public bool Check(
        IBdoMetaData meta,
        IBdoMetaSet varSet = null,
        IBdoLog log = null)
        => Check(meta, meta?.Schema, varSet, log);

    /// <summary>
    /// Checks the specified meta data corresponding to the meta schema.
    /// </summary>
    /// <param name="meta">The meta data to check.</param>
    /// <param name="defaultSpec">The meta schema to consider.</param>
    /// <returns>Returns the check log./returns>
    public virtual bool Check(
        IBdoMetaData meta,
        IBdoSchema defaultSpec,
        IBdoMetaSet varSet = null,
        IBdoLog log = null)
    {
        bool valid = true;

        if (meta != null)
        {
            var scope = meta.Scope ?? Scope;

            var localVarSet = BdoData.NewSet(varSet?.ToArray());
            localVarSet.Add(BdoData.__VarName_This, meta);

            var schema = meta.Schema ?? defaultSpec;

            // we get the data

            var data = meta.GetData(Scope, localVarSet, log);

            // check the value type

            if ((schema != null
                    && !(meta.DataType.IsCompatibleWithType(schema.DataType)
                    && schema.IsCompatibleWithData(data)))
                || (schema == null && !meta.IsCompatibleWithData(data)))
            {
                valid = false;
                log?.AddEvent(
                    EventKinds.Error,
                    "Bad value type",
                    string.Format("The type of data of element '{0}' is not compatible with '{1}' type",
                        meta.Name,
                        schema.DataType.ToString()),
                    resultCode: BdoSchemaRuleResultCodes.InvalidData);
            }

            if (schema != null)
            {
                // check requirement

                var requirementLevel = schema.GetRuleValue<RequirementLevels>(
                    BdoMetaDataProperties.RequirementLevel,
                    BdoSchemaRuleKinds.Requirement, Scope, localVarSet, log);

                switch (requirementLevel)
                {
                    case RequirementLevels.Forbidden:
                        if (meta != null)
                        {
                            log?.AddEvent(
                                EventKinds.Error,
                                "Element forbidden",
                                string.Format("The element '{0}' is forbidden", meta.Name),
                                resultCode: BdoSchemaRuleResultCodes.ElementForbidden);

                            return false;
                        }
                        break;
                    case RequirementLevels.Required:
                        if (meta == null)
                        {
                            log?.AddEvent(
                                EventKinds.Error,
                                "Element missing",
                                string.Format("The required element '{0}' is missing", schema.Name),
                                resultCode: BdoSchemaRuleResultCodes.ElementMissing);

                            return false;
                        }
                        break;
                }

                // check item requirement

                var itemRequirementLevel = schema.GetRuleValue<RequirementLevels>(
                    BdoMetaDataProperties.ItemRequirementLevel,
                    BdoSchemaRuleKinds.Requirement, Scope, localVarSet, log);

                switch (itemRequirementLevel)
                {
                    case RequirementLevels.Required:
                        if (meta == null)
                        {
                            log?.AddEvent(
                                EventKinds.Error,
                                "Value missing",
                                string.Format("The value of the element '{0}' is missing", meta.Name),
                                resultCode: BdoSchemaRuleResultCodes.ElementMissing);

                            return false;
                        }
                        break;
                    case RequirementLevels.Forbidden:
                        if (meta != null)
                        {
                            log?.AddEvent(
                                EventKinds.Error,
                                "Value forbidden",
                                string.Format("Any value of element '{0}' is forbidden", meta.Name),
                                resultCode: BdoSchemaRuleResultCodes.ElementForbidden);

                            return false;
                        }
                        break;
                }

                // check the item number

                var itemNumber = data.ToObjectList()?.Count ?? 0;
                var maxNumber = schema.MaxDataItemNumber ?? int.MaxValue;
                if (schema.DataType.ValueType != DataValueTypes.Null
                    && ((itemNumber > maxNumber)
                    || (itemNumber < schema.MinDataItemNumber)))
                {
                    valid = false;
                    log?.AddEvent(
                        EventKinds.Error,
                        "Invalid data item number",
                        string.Format("The element '{0}' must have between {1} and {2} data items ({3} found)",
                            meta.Name,
                            schema.MinDataItemNumber,
                            maxNumber,
                            itemNumber),
                        resultCode: BdoSchemaRuleResultCodes.BadItemNumber);
                }

                // check the rules

                if (schema.RuleSet != null)
                {
                    // we check requirements

                    var groupIds = schema.RuleSet.Where(q => q.Kind == BdoSchemaRuleKinds.Requirement)
                        .OrderBy(q => q.GetIndexValue())
                        .Select(q => q.GroupId).Distinct();

                    foreach (var groupId in groupIds)
                    {
                        var rule = meta.GetSchemaRule(scope, groupId, BdoSchemaRuleKinds.Requirement, localVarSet, log);

                        if (rule != null)
                        {
                            var expectedValue = rule.Value;

                            var exp = BdoData.NewExp(rule.GroupId);
                            var currentValue = Scope?.Interpreter.Evaluate(exp, localVarSet, log);

                            if ((currentValue == null && expectedValue != null)
                                || currentValue?.Equals(expectedValue) != true)
                            {
                                valid = false;

                                log?.AddEvent(
                                    rule.ResultEventKind,
                                    rule.ResultTitle,
                                    rule.ResultDescription,
                                    resultCode: rule.ResultCode);
                            }
                        }
                    }

                    // we check constraints

                    var constraints = schema.RuleSet?.Where(q => q.Kind == BdoSchemaRuleKinds.Constraint);

                    foreach (var constraint in constraints)
                    {
                        if (constraint != null)
                        {
                            if (constraint.Reference != null)
                            {
                                var referenceObj = Scope?.Interpreter.Evaluate(constraint.Reference, localVarSet, log);
                                localVarSet.Add(BdoData.__VarName_This, referenceObj);
                            }
                            else
                            {
                                localVarSet.Add(BdoData.__VarName_This, meta);
                            }

                            var conditionValue = Scope?.Interpreter.Evaluate(
                                constraint.Condition, localVarSet, log);

                            if (conditionValue != true)
                            {
                                valid = false;

                                log?.AddEvent(
                                    constraint.ResultEventKind,
                                    constraint.ResultTitle,
                                    constraint.ResultDescription,
                                    resultCode: constraint.ResultCode);
                            }
                        }
                    }
                }
            }

            // we check the sub schema for requirement

            var metaSet = meta as ITBdoSet<IBdoMetaData>;

            if (schema?._Children != null)
            {
                foreach (var childSpec in schema._Children)
                {
                    localVarSet.Add(BdoData.__VarName_This, childSpec);

                    var requirementLevel = childSpec.GetRuleValue<RequirementLevels>(
                        BdoMetaDataProperties.RequirementLevel, BdoSchemaRuleKinds.Requirement,
                        Scope, localVarSet, log);

                    switch (requirementLevel)
                    {
                        case RequirementLevels.Required:
                            if (metaSet?.Has(childSpec.Name) != true)
                            {
                                valid = false;
                                log?.AddEvent(
                                    EventKinds.Error,
                                    "Child element missing",
                                    "Option '" + childSpec.Name + "' missing");
                            }
                            break;
                        case RequirementLevels.Forbidden:
                            if (metaSet?.Has(childSpec.Name) == true)
                            {
                                valid = false;
                                log?.AddEvent(
                                    EventKinds.Error,
                                    "Child element forbidden",
                                    "Option '" + childSpec.Name + "' missing");
                            }
                            break;
                    }
                }
            }

            // we check sub meta data items

            if (metaSet != null)
            {
                // we check the sub meta elements

                foreach (var subMeta in metaSet)
                {
                    var subSpec = schema.Child(subMeta?.Name);
                    valid &= Check(subMeta, subSpec, varSet, log);
                }
            }
        }

        return valid;
    }
}