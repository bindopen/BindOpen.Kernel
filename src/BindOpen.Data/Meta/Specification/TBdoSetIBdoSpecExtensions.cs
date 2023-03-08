using BindOpen.Logging;
using BindOpen.Scoping.Scopes;

namespace BindOpen.Data.Meta
{
    /// <summary>
    /// This class represents a data element set.
    /// </summary>
    public static class TBdoSetIBdoSpecExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="set"></param>
        /// <returns></returns>
        public static bool IsCompatibleWith(
            this ITBdoSet<IBdoSpec> specs,
            ITBdoSet<IBdoMetaData> metas,
            IBdoScope scope = null,
            IBdoMetaSet varSet = null,
            IBdoLog log = null)
        {
            if (specs == null || metas == null) return false;

            var b = true;

            var iObj = 0;
            var iSpec = 0;
            while (iSpec < specs.Count && iObj < metas.Count)
            {
                var spec = specs[iSpec];

                var obj = metas[iObj].GetData(scope, varSet, log);
                b &= spec?.IsCompatibleWithData(obj) ?? true;

                if (!spec.IsRepeated)
                {
                    iSpec++;
                }

                iObj++;
            }

            return b;
        }
    }
}
