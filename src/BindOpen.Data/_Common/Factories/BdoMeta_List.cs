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
        public static BdoMetaList NewList()
            => NewList<BdoMetaList>();

        /// <summary>
        /// Defines the parameters of this instance.
        /// </summary>
        /// <param name="elems">The parameters to consider.</param>
        /// <returns>Return this instance.</returns>
        public static BdoMetaList NewList(params IBdoMetaData[] elems)
            => NewList<BdoMetaList>(elems);

        /// <summary>
        /// Defines the parameters of this instance.
        /// </summary>
        /// <param name="pairs">The pairs to consider.</param>
        /// <returns>Return this instance.</returns>
        public static BdoMetaList NewList(params (string Name, object Value)[] pairs)
            => NewList<BdoMetaList>(pairs);

        /// <summary>
        /// Defines the parameters of this instance.
        /// </summary>
        /// <param name="pairs">The pairs to consider.</param>
        /// <returns>Return this instance.</returns>
        public static BdoMetaList NewList(params (string Name, DataValueTypes ValueType, object Value)[] triplets)
            => NewList<BdoMetaList>(triplets);

        /// <summary>
        /// Defines the parameters of this instance.
        /// </summary>
        /// <param name="objects">The parameters to consider.</param>
        /// <returns>Return this instance.</returns>
        public static BdoMetaList NewList(params object[] objects)
            => NewList<BdoMetaList>(objects);

        /// <summary>
        /// Creates a new instance of the IBdoElementSet class.
        /// </summary>
        /// <param name="stringObject">The string to consider.</param>
        /// <returns>The set.</returns>
        public static BdoMetaList NewList(string stringObject)
            => NewList<BdoMetaList>(stringObject);

        // Static T creators -------------------------

        /// <summary>
        /// Defines the parameters of this instance.
        /// </summary>
        /// <returns>Return this instance.</returns>
        public static T NewList<T>()
            where T : class, IBdoMetaList, new()
        {
            return BdoData.NewList<T, IBdoMetaData>();
        }

        /// <summary>
        /// Defines the parameters of this instance.
        /// </summary>
        /// <param name="elems">The parameters to consider.</param>
        /// <returns>Return this instance.</returns>
        public static T NewList<T>(params IBdoMetaData[] elems)
            where T : class, IBdoMetaList, new()
        {
            var set = NewList<T>();
            set.With(elems);

            return set;
        }

        /// <summary>
        /// Defines the parameters of this instance.
        /// </summary>
        /// <param name="pairs">The pairs to consider.</param>
        /// <returns>Return this instance.</returns>
        public static T NewList<T>(params (string Name, object Value)[] pairs)
            where T : class, IBdoMetaList, new()
        {
            var set = NewList<T>(
                pairs.Select(q => New(q.Name, q.Value)).ToArray());

            return set;
        }

        /// <summary>
        /// Defines the parameters of this instance.
        /// </summary>
        /// <param name="pairs">The pairs to consider.</param>
        /// <returns>Return this instance.</returns>
        public static T NewList<T>(params (string Name, DataValueTypes ValueType, object Value)[] triplets)
            where T : class, IBdoMetaList, new()
        {
            var set = NewList<T>(
                triplets.Select(q => New(q.Name, q.ValueType, q.Value)).ToArray());

            return set;
        }


        /// <summary>
        /// Defines the parameters of this instance.
        /// </summary>
        /// <param name="objects">The parameters to consider.</param>
        /// <returns>Return this instance.</returns>
        public static T NewList<T>(params object[] objects)
            where T : class, IBdoMetaList, new()
        {
            var index = 0;
            return NewList<T>(objects?.Select(p =>
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
        public static T NewList<T>(
            string stringObject)
            where T : class, IBdoMetaList, new()
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
