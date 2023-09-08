using BindOpen.Kernel.Data.Meta;
using BindOpen.Kernel.Data.Helpers;
using BindOpen.Kernel.Logging;
using BindOpen.Kernel.Scoping;
using System.Linq;

namespace BindOpen.Kernel.Data.Meta
{
    /// <summary>
    /// This class represents a scalar meta that is an meta whose items are scalars.
    /// </summary>
    public partial class BdoMetaScalar : BdoMetaData,
        IBdoMetaScalar
    {
        // --------------------------------------------------
        // CONSTRUCTORS
        // --------------------------------------------------

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the ScalarElement class.
        /// </summary>
        public BdoMetaScalar() : this(null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the ScalarElement class.
        /// </summary>
        /// <param key="name">The name to consider.</param>
        /// <param key="id">The ID to consider.</param>
        public BdoMetaScalar(string name = null, string id = null)
            : base(name, "scalar_", id)
        {
        }

        #endregion

        // --------------------------------------------------
        // IBdoMetaScalar Implementation
        // --------------------------------------------------

        #region IBdoMetaScalar

        // Items ----------------------------

        /// <summary>
        /// Returns a text node representing this instance.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return _data.ToString(DataType.ValueType);
        }

        // Data ----------------------------

        /// <summary>
        /// Returns the item TItem of this instance.
        /// </summary>
        /// <param key="log">The log to populate.</param>
        /// <param key="scope">The scope to consider.</param>
        /// <param key="varSet">The variable meta set to use.</param>
        /// <returns>Returns the items of this instance.</returns>
        public override Q GetData<Q>(
            IBdoScope scope = null,
            IBdoMetaSet varSet = null,
            IBdoLog log = null)
        {
            var list = GetDataList<Q>(scope, varSet, log);
            if (list == null)
            {
                return default;
            }

            return list.FirstOrDefault();
        }

        public object GetData(
            int index,
            IBdoScope scope = null,
            IBdoMetaSet varSet = null,
            IBdoLog log = null)
        {
            var obj = GetData<object>(index, scope, varSet, log); ;
            return obj;
        }

        public Q GetData<Q>(
            int index,
            IBdoScope scope = null,
            IBdoMetaSet varSet = null,
            IBdoLog log = null)
        {
            var list = GetDataList<Q>(scope, varSet, log); ;
            var obj = list.GetAt(index);
            return obj;
        }

        #endregion
    }
}
