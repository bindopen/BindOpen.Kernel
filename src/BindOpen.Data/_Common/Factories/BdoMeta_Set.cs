using BindOpen.Data.Meta;
using System.Linq;

namespace BindOpen.Data
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
        public static BdoMetaSet NewSet()
            => NewSet<BdoMetaSet>();

        /// <summary>
        /// Defines the parameters of this instance.
        /// </summary>
        /// <param name="elems">The parameters to consider.</param>
        /// <returns>Return this instance.</returns>
        public static BdoMetaSet NewSet(params IBdoMetaData[] elems)
            => NewSet<BdoMetaSet>(elems);

        /// <summary>
        /// Defines the parameters of this instance.
        /// </summary>
        /// <param name="pairs">The pairs to consider.</param>
        /// <returns>Return this instance.</returns>
        public static BdoMetaSet NewSet(params (string Name, object Value)[] pairs)
            => NewSet<BdoMetaSet>(pairs);

        /// <summary>
        /// Defines the parameters of this instance.
        /// </summary>
        /// <param name="pairs">The pairs to consider.</param>
        /// <returns>Return this instance.</returns>
        public static BdoMetaSet NewSet(params (string Name, DataValueTypes ValueType, object Value)[] triplets)
            => NewSet<BdoMetaSet>(triplets);

        /// <summary>
        /// Defines the parameters of this instance.
        /// </summary>
        /// <param name="objects">The parameters to consider.</param>
        /// <returns>Return this instance.</returns>
        public static BdoMetaSet NewSet(params object[] objects)
            => NewSet<BdoMetaSet>(objects);

        /// <summary>
        /// Creates a new instance of the IBdoElementSet class.
        /// </summary>
        /// <param name="stringObject">The string to consider.</param>
        /// <returns>The set.</returns>
        public static BdoMetaSet NewSet(string stringObject)
            => NewSet<BdoMetaSet>(stringObject);

        // Static T creators -------------------------

        /// <summary>
        /// Defines the parameters of this instance.
        /// </summary>
        /// <returns>Return this instance.</returns>
        public static T NewSet<T>()
            where T : class, IBdoMetaSet, new()
        {
            return BdoData.NewItemSet<T, IBdoMetaData>();
        }

        /// <summary>
        /// Defines the parameters of this instance.
        /// </summary>
        /// <param name="elems">The parameters to consider.</param>
        /// <returns>Return this instance.</returns>
        public static T NewSet<T>(params IBdoMetaData[] elems)
            where T : class, IBdoMetaSet, new()
        {
            var set = NewSet<T>();
            set.With(elems);

            return set;
        }

        /// <summary>
        /// Defines the parameters of this instance.
        /// </summary>
        /// <param name="pairs">The pairs to consider.</param>
        /// <returns>Return this instance.</returns>
        public static T NewSet<T>(params (string Name, object Value)[] pairs)
            where T : class, IBdoMetaSet, new()
        {
            var set = NewSet<T>(
                pairs.Select(q => New(q.Name, q.Value)).ToArray());

            return set;
        }

        /// <summary>
        /// Defines the parameters of this instance.
        /// </summary>
        /// <param name="pairs">The pairs to consider.</param>
        /// <returns>Return this instance.</returns>
        public static T NewSet<T>(params (string Name, DataValueTypes ValueType, object Value)[] triplets)
            where T : class, IBdoMetaSet, new()
        {
            var set = NewSet<T>(
                triplets.Select(q => New(q.Name, q.ValueType, q.Value)).ToArray());

            return set;
        }


        /// <summary>
        /// Defines the parameters of this instance.
        /// </summary>
        /// <param name="objects">The parameters to consider.</param>
        /// <returns>Return this instance.</returns>
        public static T NewSet<T>(params object[] objects)
            where T : class, IBdoMetaSet, new()
        {
            var index = 0;
            return NewSet<T>(objects?.Select(p =>
            {
                var meta = New(null, p)
                    .WithIndex(++index);
                return meta;
            }).ToArray());
        }

        /// <summary>
        /// Creates a new instance of the IBdoElementSet class.
        /// </summary>
        /// <param name="stringObject">The string to consider.</param>
        /// <returns>The set.</returns>
        public static T NewSet<T>(
            string stringObject)
            where T : class, IBdoMetaSet, new()
        {
            var set = new T();
            if (stringObject != null)
            {
                foreach (var subString in stringObject.Split(';'))
                {
                    if (subString.IndexOf("=") > 0)
                    {
                        int i = subString.IndexOf("=");
                        set.Add(
                            NewScalar(
                                subString[..i],
                                DataValueTypes.Text,
                                subString[(i + 1)..]));
                    }
                }
            }

            return set;
        }
    }
}
