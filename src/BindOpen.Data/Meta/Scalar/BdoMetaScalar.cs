using BindOpen.Logging;
using BindOpen.Runtime.Scopes;
using System.Collections.Generic;
using System.Linq;

namespace BindOpen.Data.Meta
{
    /// <summary>
    /// This class represents a scalar element that is an element whose items are scalars.
    /// </summary>
    public class BdoMetaScalar :
        TBdoMetaData<IBdoMetaScalar, IBdoMetaScalarSpec, object>,
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
        /// <param name="st">The string to consider.</param>
        public static explicit operator BdoMetaScalar(string st)
            => BdoMeta.NewScalar(DataValueTypes.Any, st) as BdoMetaScalar;

        /// <summary>
        /// Converts to string.
        /// </summary>
        /// <param name="element">The element to consider.</param>
        public static explicit operator string(BdoMetaScalar element)
        {
            return element?.ToString();
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
        /// <param name="name">The name to consider.</param>
        /// <param name="id">The ID to consider.</param>
        public BdoMetaScalar(string name = null, string id = null)
            : base(name, "scalar_", id)
        {
        }

        #endregion

        // --------------------------------------------------
        // IBdoMetaScalar Implementation
        // --------------------------------------------------

        #region IBdoMetaScalar

        IBdoMetaSpec IBdoMetaData.NewSpec()
        {
            return NewSpec();
        }

        // Items ----------------------------

        /// <summary>
        /// Returns a text node representing this instance.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return _data.ToString(DataValueType);
        }

        // Data ----------------------------

        /// <summary>
        /// Sets the item of this instance.
        /// </summary>
        /// <param name="item">The string item of this instance.</param>
        /// <remarks>Items of this instance must be allowed and must not be forbidden. Otherwise, the items will be the default ones..</remarks>
        /// <returns>Returns True if the specified has been well added.</returns>
        public IBdoMetaScalar WithData(object obj)
        {
            _data = obj.ToBdoData(GetSpec());

            return this;
        }

        /// <summary>
        /// Sets the item of this instance.
        /// </summary>
        /// <param name="item">The string item of this instance.</param>
        /// <remarks>Items of this instance must be allowed and must not be forbidden. Otherwise, the items will be the default ones..</remarks>
        /// <returns>Returns True if the specified has been well added.</returns>
        public IBdoMetaScalar WithDataList(params object[] objs)
        {
            _data = objs.ToBdoData(GetSpec()).ToObjectList();

            return this;
        }

        /// <summary>
        /// Returns the item object of this instance.
        /// </summary>
        /// <param name="log">The log to populate.</param>
        /// <param name="scope">The scope to consider.</param>
        /// <param name="varSet">The variable element set to use.</param>
        /// <returns>Returns the items of this instance.</returns>
        public override object GetData(
            IBdoScope scope = null,
            IBdoMetaList varSet = null,
            IBdoLog log = null)
        {
            var list = GetDataList(scope, varSet, log);
            return list?.FirstOrDefault();
        }

        /// <summary>
        /// Returns the item object of this instance.
        /// </summary>
        /// <param name="log">The log to populate.</param>
        /// <param name="scope">The scope to consider.</param>
        /// <param name="varSet">The variable element set to use.</param>
        /// <returns>Returns the items of this instance.</returns>
        public override Q GetData<Q>(
            IBdoScope scope = null,
            IBdoMetaList varSet = null,
            IBdoLog log = null)
        {
            var list = GetDataList<Q>(scope, varSet, log);
            if (list == null)
            {
                return default;
            }

            return list.FirstOrDefault();
        }

        /// <summary>
        /// Returns the item object of this instance.
        /// </summary>
        /// <param name="log">The log to populate.</param>
        /// <param name="scope">The scope to consider.</param>
        /// <param name="varSet">The variable element set to use.</param>
        /// <returns>Returns the items of this instance.</returns>
        public object GetData(
            int index,
            IBdoScope scope = null,
            IBdoMetaList varSet = null,
            IBdoLog log = null)
        {
            var obj = GetData<object>(index, scope, varSet, log); ;
            return obj;
        }

        /// <summary>
        /// Returns the item object of this instance.
        /// </summary>
        /// <param name="log">The log to populate.</param>
        /// <param name="scope">The scope to consider.</param>
        /// <param name="varSet">The variable element set to use.</param>
        /// <returns>Returns the items of this instance.</returns>
        public Q GetData<Q>(
            int index,
            IBdoScope scope = null,
            IBdoMetaList varSet = null,
            IBdoLog log = null)
        {
            var list = GetDataList<Q>(scope, varSet, log); ;
            var obj = list.GetAt(index);
            return obj;
        }

        /// <summary>
        /// Returns the item object of this instance.
        /// </summary>
        /// <param name="log">The log to populate.</param>
        /// <param name="scope">The scope to consider.</param>
        /// <param name="varSet">The variable element set to use.</param>
        /// <returns>Returns the items of this instance.</returns>
        public List<object> GetDataList(
            IBdoScope scope = null,
            IBdoMetaList varSet = null,
            IBdoLog log = null)
        {
            var obj = DataObject(scope, varSet, log);

            var list = obj?.ToObjectList();
            return list;
        }

        /// <summary>
        /// Returns the item object of this instance.
        /// </summary>
        /// <param name="log">The log to populate.</param>
        /// <param name="scope">The scope to consider.</param>
        /// <param name="varSet">The variable element set to use.</param>
        /// <returns>Returns the items of this instance.</returns>
        public List<Q> GetDataList<Q>(
            IBdoScope scope = null,
            IBdoMetaList varSet = null,
            IBdoLog log = null)
        {
            var list = GetDataList(scope, varSet, log);
            return list?.Select(q =>
            {
                if (q is Q q_Q)
                    return q_Q;

                return default;
            }).ToList();
        }

        #endregion

        // --------------------------------------------------
        // CLONING
        // --------------------------------------------------

        #region Cloning

        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <returns>Returns a cloned instance.</returns>
        public override object Clone(params string[] areas)
        {
            var scalarElement = base.Clone(areas) as BdoMetaScalar;
            return scalarElement;
        }

        #endregion
    }
}
