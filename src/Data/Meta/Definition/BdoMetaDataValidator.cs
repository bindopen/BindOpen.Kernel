using BindOpen.Kernel.Logging;

namespace BindOpen.Kernel.Data.Meta
{
    /// <summary>
    /// This class represents a data validator.
    /// </summary>
    public class BdoMetaDataValidator : ITBdoDataValidator<IBdoMetaData, IBdoSpec>
    {
        /// <summary>
        /// Checks the specified meta data.
        /// </summary>
        /// <param name="meta">The meta data to check.</param>
        /// <returns>Returns the check log./returns>
        public bool Check(IBdoMetaData meta, IBdoLog log = null)
            => Check(meta, meta?.Spec, log);

        /// <summary>
        /// Checks the specified meta data corresponding to the meta specification.
        /// </summary>
        /// <param name="meta">The meta data to check.</param>
        /// <param name="spec">The meta specification to consider.</param>
        /// <returns>Returns the check log./returns>
        public virtual bool Check(IBdoMetaData meta, IBdoSpec spec, IBdoLog log = null)
        {
            bool isOk = true;

            if (meta !=null && spec !=null)
            {
                //spec.ItemSpecLevels
            }

            return isOk;
        }
    }
}