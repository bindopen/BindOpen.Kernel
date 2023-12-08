using BindOpen.Kernel.Data.Helpers;
using BindOpen.Kernel.Logging;
using BindOpen.Kernel.Scoping;
using System.Data;
using System.Linq;

namespace BindOpen.Kernel.Data.Meta
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
        public bool Check(IBdoMetaData meta, IBdoMetaSet varSet = null, IBdoLog log = null)
            => Check(meta, meta?.Spec, varSet, log);

        /// <summary>
        /// Checks the specified meta data corresponding to the meta specification.
        /// </summary>
        /// <param name="meta">The meta data to check.</param>
        /// <param name="spec">The meta specification to consider.</param>
        /// <returns>Returns the check log./returns>
        public virtual bool Check(IBdoMetaData meta, IBdoSpec spec, IBdoMetaSet varSet = null, IBdoLog log = null)
        {
            bool isOk = true;

            if (spec != null)
            {
                var localVarSet = BdoData.NewSet(varSet?.ToArray());
                localVarSet.Add(BdoData.__VarName_This, meta);

                // check the value type

                if (spec.IsCompatibleWithData(meta) == false)
                {
                    log?.AddEvent(
                        EventKinds.Error,
                        "Bas value type",
                        string.Format("Value not compatible with '{0}' type", spec.DataType.ToString()),
                        resultCode: BdoSpecRuleResultCodes.BadValueType);
                }

                // check the value type

                var data = meta?.GetData(Scope, varSet, log);
                if (!spec.IsCompatibleWithData(data))
                {
                    isOk = false;
                    log?.AddEvent(EventKinds.Error, "Invalid data").WithResultCode("CS1250");
                }

                // check the item number

                var itemNumber = data.ToObjectList()?.Count ?? 0;
                if ((itemNumber > (spec.MaxDataItemNumber ?? int.MaxValue))
                    || (itemNumber < spec.MinDataItemNumber))
                {
                    isOk = false;
                    log?.AddEvent(EventKinds.Error, "Invalid data item number").WithResultCode("CS1251");
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
            }

            return isOk;
        }
    }
}