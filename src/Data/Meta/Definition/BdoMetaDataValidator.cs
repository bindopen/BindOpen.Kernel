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
        /// <param name="spec">The meta specification to consider.</param>
        /// <returns>Returns the check log./returns>
        public virtual bool Check(
            IBdoMetaData meta,
            IBdoSpec spec,
            IBdoMetaSet varSet = null,
            IBdoLog log = null)
        {
            bool isOk = true;

            if (spec != null)
            {
                var localVarSet = BdoData.NewSet(varSet?.ToArray());
                localVarSet.Add(BdoData.__VarName_This, meta);

                // check requirement

                var requirementLevel = spec.GetValue<RequirementLevels>(
                    BdoMetaDataProperties.RequirementLevel,
                    BdoSpecRuleKinds.Requirement, Scope, varSet, log);

                switch (requirementLevel)
                {
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
                    case RequirementLevels.Forbidden:
                        if (meta != null)
                        {
                            log?.AddEvent(
                                EventKinds.Error,
                                "Element forbidden",
                                string.Format("The element '{0}' is forbidden", spec.Name),
                                resultCode: BdoSpecRuleResultCodes.ElementForbidden);

                            return false;
                        }
                        break;
                }

                // check item requirement

                var data = meta?.GetData(Scope, varSet, log);

                var itemRequirementLevel = spec.GetValue<RequirementLevels>(
                    BdoMetaDataProperties.ItemRequirementLevel,
                    BdoSpecRuleKinds.Requirement, Scope, varSet, log);

                switch (itemRequirementLevel)
                {
                    case RequirementLevels.Required:
                        if (meta == null)
                        {
                            log?.AddEvent(
                                EventKinds.Error,
                                "Value missing",
                                string.Format("The value of the element '{0}' is missing", spec.Name),
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
                                string.Format("Any value of element '{0}' is forbidden", spec.Name),
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
                            spec.Name,
                            spec.DataType.ToString()),
                        resultCode: BdoSpecRuleResultCodes.InvalidData);
                }

                // check the item number

                var itemNumber = data.ToObjectList()?.Count ?? 0;
                var maxNumber = spec.MaxDataItemNumber ?? int.MaxValue;
                if ((itemNumber > maxNumber)
                    || (itemNumber < spec.MinDataItemNumber))
                {
                    isOk = false;
                    log?.AddEvent(
                        EventKinds.Error,
                        "Invalid data item number",
                        string.Format("The element '{0}' must have between {0} and {1} data items",
                            spec.MinDataItemNumber,
                            maxNumber),
                        resultCode: BdoSpecRuleResultCodes.BadItemNumber);
                }

                // check the rules

                if (spec.Items != null)
                {
                    // we check requirements

                    var groupIds = spec.Where(q => q.Kind == BdoSpecRuleKinds.Requirement)
                        .OrderBy(q => q.GetIndexValue())
                        .Select(q => q.GroupId).Distinct();

                    foreach (var groupId in groupIds)
                    {
                        var rule = meta.GetSpecRule(groupId, BdoSpecRuleKinds.Requirement, Scope, varSet, log);

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

                    var constraints = spec.Where(q => q.Kind == BdoSpecRuleKinds.Constraint);

                    foreach (var constraint in constraints)
                    {
                        if (constraint != null)
                        {
                            if (constraint.Reference != null)
                            {
                                var referenceObj = Scope?.Interpreter.Evaluate(constraint.Reference, localVarSet, log);
                                localVarSet.Add(BdoData.__VarName_This, referenceObj);
                            }
                            localVarSet.Add(BdoData.__VarName_This, meta);

                            var conditionValue = Scope?.Interpreter.Evaluate(
                                constraint.Condition, localVarSet, log);

                            if (conditionValue != false)
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

                // we check sub meta data items

                if (meta is ITBdoSet<IBdoMetaData> metaSet)
                {
                    if (spec._Children != null)
                    {
                        var requiredSpecs = spec._Children.Where(q =>
                        {
                            var requirementLevel = q.GetValue<RequirementLevels>(
                                BdoMetaDataProperties.RequirementLevel, BdoSpecRuleKinds.Requirement,
                                Scope, varSet, log);
                            return requirementLevel == RequirementLevels.Required;
                        });

                        foreach (var childSpec in requiredSpecs)
                        {
                            if (!metaSet.Has(childSpec.Name))
                            {
                                isOk = false;
                                log?.AddEvent(EventKinds.Error, "Option '" + spec.Name + "' missing");
                            }
                        }
                    }

                    // we check the sub meta elements

                    foreach (var subMeta in metaSet)
                    {
                        var subSpec = spec.Child(subMeta?.Name);
                        if (subSpec != null)
                        {
                            subSpec = subMeta?.Spec;
                        }

                        isOk &= Check(subMeta, spec, varSet, log);
                    }
                }
            }

            return isOk;
        }
    }
}