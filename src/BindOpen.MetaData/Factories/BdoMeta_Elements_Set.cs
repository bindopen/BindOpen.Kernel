using BindOpen.MetaData.Elements;
using System.Linq;

namespace BindOpen.MetaData
{
    /// <summary>
    /// This static class provides methods to create data element set.
    /// </summary>
    public static partial class BdoMeta
    {
        /// <summary>
        /// Defines the parameters of this instance.
        /// </summary>
        /// <returns>Return this instance.</returns>
        public static BdoMetaElementSet NewSet()
            => NewSet<BdoMetaElementSet>();

        /// <summary>
        /// Defines the parameters of this instance.
        /// </summary>
        /// <param name="elems">The parameters to consider.</param>
        /// <returns>Return this instance.</returns>
        public static BdoMetaElementSet NewSet(params IBdoMetaElement[] elems)
            => NewSet<BdoMetaElementSet>(elems);

        /// <summary>
        /// Defines the parameters of this instance.
        /// </summary>
        /// <param name="pairs">The pairs to consider.</param>
        /// <returns>Return this instance.</returns>
        public static BdoMetaElementSet NewSet(params (string Name, object Value)[] pairs)
            => NewSet<BdoMetaElementSet>(pairs);

        /// <summary>
        /// Defines the parameters of this instance.
        /// </summary>
        /// <param name="pairs">The pairs to consider.</param>
        /// <returns>Return this instance.</returns>
        public static BdoMetaElementSet NewSet(params (string Name, DataValueTypes ValueType, object Value)[] triplets)
            => NewSet<BdoMetaElementSet>(triplets);


        /// <summary>
        /// Defines the parameters of this instance.
        /// </summary>
        /// <param name="objects">The parameters to consider.</param>
        /// <returns>Return this instance.</returns>
        public static BdoMetaElementSet NewSet(params object[] objects)
            => NewSet<BdoMetaElementSet>(objects);

        /// <summary>
        /// Creates a new instance of the IBdoElementSet class.
        /// </summary>
        /// <param name="stringObject">The string to consider.</param>
        /// <returns>The collection.</returns>
        public static BdoMetaElementSet NewSet(string stringObject)
            => NewSet<BdoMetaElementSet>(stringObject);

        // Static T creators -------------------------

        /// <summary>
        /// Defines the parameters of this instance.
        /// </summary>
        /// <returns>Return this instance.</returns>
        public static BdoMetaElementSet NewSet<T>()
            where T : class, IBdoElementSet, new()
        {
            return BdoMeta.NewItemSet<BdoMetaElementSet, IBdoMetaElement>();
        }

        /// <summary>
        /// Defines the parameters of this instance.
        /// </summary>
        /// <param name="elems">The parameters to consider.</param>
        /// <returns>Return this instance.</returns>
        public static BdoMetaElementSet NewSet<T>(params IBdoMetaElement[] elems)
            where T : class, IBdoElementSet, new()
        {
            var elemSet = NewSet<T>();
            elemSet.WithItems(elems);

            return elemSet;
        }

        /// <summary>
        /// Defines the parameters of this instance.
        /// </summary>
        /// <param name="pairs">The pairs to consider.</param>
        /// <returns>Return this instance.</returns>
        public static BdoMetaElementSet NewSet<T>(params (string Name, object Value)[] pairs)
            where T : class, IBdoElementSet, new()
        {
            var elemSet = NewSet<T>();
            elemSet.WithItems(pairs.Select(q => BdoMeta.NewElement(q.Name, q.Value)).ToArray());

            return elemSet;
        }

        /// <summary>
        /// Defines the parameters of this instance.
        /// </summary>
        /// <param name="pairs">The pairs to consider.</param>
        /// <returns>Return this instance.</returns>
        public static BdoMetaElementSet NewSet<T>(params (string Name, DataValueTypes ValueType, object Value)[] triplets)
            where T : class, IBdoElementSet, new()
        {
            var elemSet = NewSet<T>();
            elemSet.WithItems(triplets.Select(q => BdoMeta.NewElement(q.Name, q.ValueType, q.Value)).ToArray());

            return elemSet;
        }


        /// <summary>
        /// Defines the parameters of this instance.
        /// </summary>
        /// <param name="objects">The parameters to consider.</param>
        /// <returns>Return this instance.</returns>
        public static BdoMetaElementSet NewSet<T>(params object[] objects)
            where T : class, IBdoElementSet, new()
        {
            var index = 0;
            return NewSet<T>(objects?.Select(p =>
            {
                var scalar = BdoMeta.NewScalar(DataValueTypes.Any, p);
                scalar.WithIndex(++index);
                return scalar;
            }).ToArray());
        }

        /// <summary>
        /// Creates a new instance of the IBdoElementSet class.
        /// </summary>
        /// <param name="stringObject">The string to consider.</param>
        /// <returns>The collection.</returns>
        public static BdoMetaElementSet NewSet<T>(
            string stringObject)
            where T : class, IBdoElementSet, new()
        {
            var elemSet = new BdoMetaElementSet();
            if (stringObject != null)
            {
                foreach (var subString in stringObject.Split(';'))
                {
                    if (subString.IndexOf("=") > 0)
                    {
                        int i = subString.IndexOf("=");
                        elemSet.Add(
                            BdoMeta.NewScalar(
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
