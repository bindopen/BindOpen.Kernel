using BindOpen.Data.Helpers;
using BindOpen.Logging;
using BindOpen.Scopes;
using System.Linq;

namespace BindOpen.Data.Meta
{
    /// <summary>
    /// This class represents a scalar meta that is an meta whose items are scalars.
    /// </summary>
    public partial class BdoMetaScalar : BdoMetaData,
        IBdoMetaScalar
    {
        // --------------------------------------------------
        // CONVERTERS
        // --------------------------------------------------

        #region Converters

        // String

        /// <summary>
        /// Converts from string.
        /// </summary>
        /// <param key="st">The string to consider.</param>
        public static explicit operator BdoMetaScalar(string st)
            => BdoMeta.NewScalar(DataValueTypes.Any, st);

        /// <summary>
        /// Converts to string.
        /// </summary>
        /// <param key="meta">The meta to consider.</param>
        public static explicit operator string(BdoMetaScalar meta)
        {
            return meta?.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param key="items"></param>
        public static implicit operator BdoMetaScalar((string Name, object Value) item)
        {
            var meta = BdoMeta.NewScalar(item.Name, item.Value);

            return meta;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param key="items"></param>
        public static implicit operator BdoMetaScalar((string Name, DataValueTypes ValueType, object Value) item)
        {
            var meta = BdoMeta.NewScalar(item.Name, item.ValueType, item.Value);

            return meta;
        }

        #endregion

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
            return _data.ToString(this.GetSpec()?.DataType.ValueType ?? DataValueTypes.Any);
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
            IBdoBaseLog log = null)
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
            IBdoBaseLog log = null)
        {
            var obj = GetData<object>(index, scope, varSet, log); ;
            return obj;
        }

        public Q GetData<Q>(
            int index,
            IBdoScope scope = null,
            IBdoMetaSet varSet = null,
            IBdoBaseLog log = null)
        {
            var list = GetDataList<Q>(scope, varSet, log); ;
            var obj = list.GetAt(index);
            return obj;
        }

        #endregion
    }
}
