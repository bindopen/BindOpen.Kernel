using BindOpen.Data.Meta;
using BindOpen.Logging;
using BindOpen.Scoping;
using System.Linq;

namespace BindOpen.Data
{
    /// <summary>
    /// 
    /// </summary>
    public static class IBdoSpecSetExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        public static IBdoSpec Child(
            this IBdoSpecSet specSet,
            IBdoScope scope,
            IBdoMetaSet varSet = null,
            IBdoLog log = null)
        {
            IBdoSpec spec = null;

            if (specSet != null)
            {
                spec = specSet.FirstOrDefault(
                    q => scope?.Interpreter?.Evaluate(q?.Condition, varSet, log) == true);

                spec ??= specSet?.FirstOrDefault(q => q?.Condition == null);
            }

            return spec;
        }
    }
}