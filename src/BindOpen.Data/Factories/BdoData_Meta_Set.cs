using BindOpen.Data.Meta;
using System.Linq;

namespace BindOpen.Data
{
    /// <summary>
    /// This static class provides methods to create data element set.
    /// </summary>
    public static partial class BdoData
    {
        /// <summary>
        /// Defines the parameters of this instance.
        /// </summary>
        /// <returns>Return this instance.</returns>
        public static BdoMetaSet NewMetaSet()
            => NewMetaSet<BdoMetaSet>();

        /// <summary>
        /// Defines the parameters of this instance.
        /// </summary>
        /// <param name="elems">The parameters to consider.</param>
        /// <returns>Return this instance.</returns>
        public static BdoMetaSet NewMetaSet(params IBdoMetaData[] elems)
            => NewMetaSet<BdoMetaSet>(elems);

        /// <summary>
        /// Defines the parameters of this instance.
        /// </summary>
        /// <param name="pairs">The pairs to consider.</param>
        /// <returns>Return this instance.</returns>
        public static BdoMetaSet NewMetaSet(params (string Name, object Value)[] pairs)
            => NewMetaSet<BdoMetaSet>(pairs);

        /// <summary>
        /// Defines the parameters of this instance.
        /// </summary>
        /// <param name="pairs">The pairs to consider.</param>
        /// <returns>Return this instance.</returns>
        public static BdoMetaSet NewMetaSet(params (string Name, DataValueTypes ValueType, object Value)[] triplets)
            => NewMetaSet<BdoMetaSet>(triplets);


        /// <summary>
        /// Defines the parameters of this instance.
        /// </summary>
        /// <param name="objects">The parameters to consider.</param>
        /// <returns>Return this instance.</returns>
        public static BdoMetaSet NewMetaSet(params object[] objects)
            => NewMetaSet<BdoMetaSet>(objects);

        /// <summary>
        /// Creates a new instance of the IBdoElementSet class.
        /// </summary>
        /// <param name="stringObject">The string to consider.</param>
        /// <returns>The collection.</returns>
        public static BdoMetaSet NewMetaSet(string stringObject)
            => NewMetaSet<BdoMetaSet>(stringObject);

        // Static T creators -------------------------

        /// <summary>
        /// Defines the parameters of this instance.
        /// </summary>
        /// <returns>Return this instance.</returns>
        public static BdoMetaSet NewMetaSet<T>()
            where T : class, IBdoMetaSet, new()
        {
            return BdoData.NewItemSet<BdoMetaSet, IBdoMetaData>();
        }

        /// <summary>
        /// Defines the parameters of this instance.
        /// </summary>
        /// <param name="elems">The parameters to consider.</param>
        /// <returns>Return this instance.</returns>
        public static BdoMetaSet NewMetaSet<T>(params IBdoMetaData[] elems)
            where T : class, IBdoMetaSet, new()
        {
            var elemSet = NewMetaSet<T>();
            elemSet.WithItems(elems);

            return elemSet;
        }

        /// <summary>
        /// Defines the parameters of this instance.
        /// </summary>
        /// <param name="pairs">The pairs to consider.</param>
        /// <returns>Return this instance.</returns>
        public static BdoMetaSet NewMetaSet<T>(params (string Name, object Value)[] pairs)
            where T : class, IBdoMetaSet, new()
        {
            var elemSet = NewMetaSet<T>();
            elemSet.WithItems(pairs.Select(q => BdoData.NewMeta(q.Name, q.Value)).ToArray());

            return elemSet;
        }

        /// <summary>
        /// Defines the parameters of this instance.
        /// </summary>
        /// <param name="pairs">The pairs to consider.</param>
        /// <returns>Return this instance.</returns>
        public static BdoMetaSet NewMetaSet<T>(params (string Name, DataValueTypes ValueType, object Value)[] triplets)
            where T : class, IBdoMetaSet, new()
        {
            var elemSet = NewMetaSet<T>();
            elemSet.WithItems(triplets.Select(q => BdoData.NewMeta(q.Name, q.ValueType, q.Value)).ToArray());

            return elemSet;
        }


        /// <summary>
        /// Defines the parameters of this instance.
        /// </summary>
        /// <param name="objects">The parameters to consider.</param>
        /// <returns>Return this instance.</returns>
        public static BdoMetaSet NewMetaSet<T>(params object[] objects)
            where T : class, IBdoMetaSet, new()
        {
            var index = 0;
            return NewMetaSet<T>(objects?.Select(p =>
            {
                var scalar = BdoData.NewMetaScalar(DataValueTypes.Any, p);
                scalar.WithIndex(++index);
                return scalar;
            }).ToArray());
        }

        /// <summary>
        /// Creates a new instance of the IBdoElementSet class.
        /// </summary>
        /// <param name="stringObject">The string to consider.</param>
        /// <returns>The collection.</returns>
        public static BdoMetaSet NewMetaSet<T>(
            string stringObject)
            where T : class, IBdoMetaSet, new()
        {
            var elemSet = new BdoMetaSet();
            if (stringObject != null)
            {
                foreach (var subString in stringObject.Split(';'))
                {
                    if (subString.IndexOf("=") > 0)
                    {
                        int i = subString.IndexOf("=");
                        elemSet.Add(
                            BdoData.NewMetaScalar(
                                subString[..i],
                                DataValueTypes.Text,
                                subString[(i + 1)..]));
                    }
                }
            }
            return elemSet;
        }
    }
}
