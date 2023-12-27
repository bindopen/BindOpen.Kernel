using BindOpen.Logging;
using BindOpen.Scoping;

namespace BindOpen.Data.Meta
{
    /// <summary>
    /// This interface defines a data validator.
    /// </summary>
    public interface ITBdoDataValidator<TSpecified, TSpec> : IBdoScoped
        where TSpecified : IBdoSpecified, IReferenced
        where TSpec : IBdoNodeSpec
    {
        /// <summary>
        /// Checks the specified meta data.
        /// </summary>
        /// <param name="meta">The meta data to check.</param>
        /// <param name="log">The log to consider.</param>
        /// <returns>Returns the check log./returns>
        bool Check(
            TSpecified obj,
            IBdoMetaSet varSet = null,
            IBdoLog log = null);

        /// <summary>
        /// Checks the specified meta data corresponding to the meta specification.
        /// </summary>
        /// <param name="meta">The meta data to check.</param>
        /// <param name="spec">The meta specification to consider.</param>
        /// <param name="log">The log to consider.</param>
        /// <returns>Returns the check log./returns>
        bool Check(
            TSpecified obj,
            TSpec spec,
            IBdoMetaSet varSet = null,
            IBdoLog log = null);
    }
}