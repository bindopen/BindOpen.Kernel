using BindOpen.Data.Helpers;
using BindOpen.Logging;
using BindOpen.Scoping;
using System.Data;
using System.Linq;

namespace BindOpen.Data.Meta
{
    /// <summary>
    /// This class represents a data validator.
    /// </summary>
    public class BdoMetaDataValidator : ITBdoDataValidator<IBdoMetaData, IBdoSpec>
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
            => Check(meta, meta?.Spec, varSet, log);

        /// <summary>
        /// Checks the specified meta data corresponding to the meta specification.
        /// </summary>
        /// <param name="meta">The meta data to check.</param>
        /// <param name="defaultSpec">The meta specification to consider.</param>
        /// <returns>Returns the check log./returns>
        public virtual bool Check(
            IBdoMetaData meta,
            IBdoSpec defaultSpec,
            IBdoMetaSet varSet = null,
            IBdoLog log = null)
        {
            bool isOk = true;

            if (meta != null)
            {
                var localVarSet = BdoData.NewSet(varSet?.ToArray());
                localVarSet.Add(BdoData.__VarName_This, meta);

                var spec = meta.Spec ?? defaultSpec;


                if (spec != null)
                {
                    // check requirement

                    var requirementLevel = spec.GetRuleValue<RequirementLevels>(
                        BdoMetaDataProperties.RequirementLevel,
                        BdoSpecRuleKinds.Requirement, Scope, localVarSet, log);

                    switch (requirementLevel)
                    {
                        case RequirementLevels.Forbidden:
                            if (meta != null)
                            {
                                log?.AddEvent(
                                    EventKinds.Error,
                                    "Element forbidden",
                                    string.Format("The element '{0}' is forbidden", meta.Name),
                                    resultCode: BdoSpecRuleResultCodes.ElementForbidden);

                                return false;
                            }
                            break;
                        case RequirementLevels.Required:
                            if (meta == null)
                            {
                                log?.AddEvent(
                                    EventKinds.Error,
                                    "Element missing",
                                    string.Format("The required element '{0}' is missing", spec.Name),
                                    resultCode: BdoSpecRuleResultCodes.ElementMissing);

                                return false;
                            }
                            break;
                    }

                    // check item requirement

                    var data = meta?.GetData(Scope, localVarSet, log);

                    var itemRequirementLevel = spec.GetRuleValue<RequirementLevels>(
                        BdoMetaDataProperties.ItemRequirementLevel,
                        BdoSpecRuleKinds.Requirement, Scope, localVarSet, log);

                    switch (itemRequirementLevel)
                    {
                        case RequirementLevels.Required:
                            if (meta == null)
                            {
                                log?.AddEvent(
                                    EventKinds.Error,
                                    "Value missing",
                                    string.Format("The value of the element '{0}' is missing", meta.Name),
                                    resultCode: BdoSpecRuleResultCodes.ElementMissing);

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
                                    resultCode: BdoSpecRuleResultCodes.ElementForbidden);

                                return false;
                            }
                            break;
                    }

                    // check the value type

                    if (spec.DataType?.IsCompatibleWith(data?.GetType()) == false)
                    {
                        isOk = false;
                        log?.AddEvent(
                            EventKinds.Error,
                            "Bad value type",
                            string.Format("The value of element '{0}' is not compatible with '{1}' type",
                                meta.Name,
                                spec.DataType.ToString()),
                            resultCode: BdoSpecRuleResultCodes.InvalidData);
                    }

                    // check the item number

                    var itemNumber = data.ToObjectList()?.Count ?? 0;
                    var maxNumber = spec.MaxDataItemNumber ?? int.MaxValue;
                    if (spec.DataType.ValueType != DataValueTypes.Null
                        && ((itemNumber > maxNumber)
                        || (itemNumber < spec.MinDataItemNumber)))
                    {
                        isOk = false;
                        log?.AddEvent(
                            EventKinds.Error,
                            "Invalid data item number",
                            string.Format("The element '{0}' must have between {1} and {2} data items ({3} found)",
                                meta.Name,
                                spec.MinDataItemNumber,
                                maxNumber,
                                itemNumber),
                            resultCode: BdoSpecRuleResultCodes.BadItemNumber);
                    }

                    // check the rules

                    if (spec.RuleSet != null)
                    {
                        // we check requirements

                        var groupIds = spec.RuleSet.Where(q => q.Kind == BdoSpecRuleKinds.Requirement)
                            .OrderBy(q => q.GetIndexValue())
                            .Select(q => q.GroupId).Distinct();

                        foreach (var groupId in groupIds)
                        {
                            var rule = meta.GetSpecRule(groupId, BdoSpecRuleKinds.Requirement, Scope, localVarSet, log);

                            if (rule != null)
                            {
                                var expectedValue = rule.Value;

                                var exp = BdoData.NewExp(rule.GroupId);
                                var currentValue = Scope?.Interpreter.Evaluate(exp, localVarSet, log);

                                if ((currentValue == null && expectedValue != null)
                                    || currentValue?.Equals(expectedValue) != true)
                                {
                                    isOk = false;

                                    log?.AddEvent(
                                        rule.ResultEventKind,
                                        rule.ResultTitle,
                                        rule.ResultDescription,
                                        resultCode: rule.ResultCode);
                                }
                            }
                        }

                        // we check constraints

                        var constraints = spec.RuleSet?.Where(q => q.Kind == BdoSpecRuleKinds.Constraint);

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
                                    isOk = false;

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

                // we check the sub specification for requirement

                var metaSet = meta as ITBdoSet<IBdoMetaData>;

                if (spec?._Children != null)
                {
                    foreach (var childSpec in spec._Children)
                    {
                        localVarSet.Add(BdoData.__VarName_This, childSpec);

                        var requirementLevel = childSpec.GetRuleValue<RequirementLevels>(
                            BdoMetaDataProperties.RequirementLevel, BdoSpecRuleKinds.Requirement,
                            Scope, localVarSet, log);

                        switch (requirementLevel)
                        {
                            case RequirementLevels.Required:
                                if (metaSet?.Has(childSpec.Name) != true)
                                {
                                    isOk = false;
                                    log?.AddEvent(
                                        EventKinds.Error,
                                        "Child element missing",
                                        "Option '" + childSpec.Name + "' missing");
                                }
                                break;
                            case RequirementLevels.Forbidden:
                                if (metaSet?.Has(childSpec.Name) == true)
                                {
                                    isOk = false;
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
                        var subSpec = spec.Child(subMeta?.Name);
                        isOk &= Check(subMeta, subSpec, varSet, log);
                    }
                }
            }

            return isOk;
        }
    }
}