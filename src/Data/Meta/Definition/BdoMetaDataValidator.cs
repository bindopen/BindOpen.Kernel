using BindOpen.Kernel.Logging;
using BindOpen.Kernel.Scoping;

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

            if (spec !=null)
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
                        resultCode: BdoMetaConstraintResultCodes.BadValueType);
                }

                // check the constraints

                if (spec.Items != null)
                {
                    foreach (var constraint in spec)
                    {
                        var conditionValue = Scope?.Interpreter.Evaluate(constraint.Condition, localVarSet, log);

                        switch(constraint.Mode)
                        {
                            case BdoConstraintModes.Requirement:
                                if (conditionValue == true)
                                {
                                    var expectedValue = constraint.Value;
                                    var exp = BdoData.NewExp(string.Format("$(this).prop('{0}')", constraint.Reference?.Identifier));

                                    var currentValue = Scope?.Interpreter.Evaluate(exp, localVarSet, log);


                                    log?.AddEvent(
                                        constraint.ResultEventKind,
                                        constraint.ResultTitle,
                                        constraint.ResultDescription,
                                        resultCode: constraint.ResultCode);
                                }
                                break;
                            case BdoConstraintModes.Rule:
                                if (conditionValue != false)
                                {
                                    log?.AddEvent(
                                        constraint.ResultEventKind,
                                        constraint.ResultTitle,
                                        constraint.ResultDescription,
                                        resultCode: constraint.ResultCode);
                                }
                                break;
                        }
                    }
                }
            }

            return isOk;
        }
    }
}