using BindOpen.Data.Items;
using System.Linq;

namespace BindOpen.Data.Elements
{
    /// <summary>
    /// This static class provides methods to create data element set.
    /// </summary>
    public static partial class BdoElements
    {
        /// <summary>
        /// Defines the parameters of this instance.
        /// </summary>
        /// <returns>Return this instance.</returns>
        public static BdoElementSet NewSet()
            => NewSet<BdoElementSet>();

        /// <summary>
        /// Defines the parameters of this instance.
        /// </summary>
        /// <param name="elements">The parameters to consider.</param>
        /// <returns>Return this instance.</returns>
        public static BdoElementSet NewSet(params IBdoElement[] elements)
            => NewSet<BdoElementSet>(elements);

        /// <summary>
        /// Defines the parameters of this instance.
        /// </summary>
        /// <param name="pairs">The pairs to consider.</param>
        /// <returns>Return this instance.</returns>
        public static BdoElementSet NewSet(params (string Name, object Value)[] pairs)
            => NewSet<BdoElementSet>(pairs);

        /// <summary>
        /// Defines the parameters of this instance.
        /// </summary>
        /// <param name="pairs">The pairs to consider.</param>
        /// <returns>Return this instance.</returns>
        public static BdoElementSet NewSet(params (string Name, DataValueTypes ValueType, object Value)[] triplets)
            => NewSet<BdoElementSet>(triplets);


        /// <summary>
        /// Defines the parameters of this instance.
        /// </summary>
        /// <param name="objects">The parameters to consider.</param>
        /// <returns>Return this instance.</returns>
        public static BdoElementSet NewSet(params object[] objects)
            => NewSet<BdoElementSet>(objects);

        /// <summary>
        /// Creates a new instance of the IBdoElementSet class.
        /// </summary>
        /// <param name="stringObject">The string to consider.</param>
        /// <returns>The collection.</returns>
        public static BdoElementSet NewSet(string stringObject)
            => NewSet<BdoElementSet>(stringObject);

        // Static T creators -------------------------

        /// <summary>
        /// Defines the parameters of this instance.
        /// </summary>
        /// <returns>Return this instance.</returns>
        public static BdoElementSet NewSet<T>()
            where T : class, IBdoElementSet, new()
        {
            return BdoItems.NewSet<BdoElementSet, IBdoElement>();
        }

        /// <summary>
        /// Defines the parameters of this instance.
        /// </summary>
        /// <param name="elements">The parameters to consider.</param>
        /// <returns>Return this instance.</returns>
        public static BdoElementSet NewSet<T>(params IBdoElement[] elements)
            where T : class, IBdoElementSet, new()
        {
            var elementSet = NewSet<T>();
            elementSet.WithItems(elements);

            return elementSet;
        }

        /// <summary>
        /// Defines the parameters of this instance.
        /// </summary>
        /// <param name="pairs">The pairs to consider.</param>
        /// <returns>Return this instance.</returns>
        public static BdoElementSet NewSet<T>(params (string Name, object Value)[] pairs)
            where T : class, IBdoElementSet, new()
        {
            var elementSet = NewSet<T>();
            elementSet.WithItems(pairs.Select(q => BdoElements.NewElement(q.Name, q.Value)).ToArray());

            return elementSet;
        }

        /// <summary>
        /// Defines the parameters of this instance.
        /// </summary>
        /// <param name="pairs">The pairs to consider.</param>
        /// <returns>Return this instance.</returns>
        public static BdoElementSet NewSet<T>(params (string Name, DataValueTypes ValueType, object Value)[] triplets)
            where T : class, IBdoElementSet, new()
        {
            var elementSet = NewSet<T>();
            elementSet.WithItems(triplets.Select(q => BdoElements.NewElement(q.Name, q.ValueType, q.Value)).ToArray());

            return elementSet;
        }


        /// <summary>
        /// Defines the parameters of this instance.
        /// </summary>
        /// <param name="objects">The parameters to consider.</param>
        /// <returns>Return this instance.</returns>
        public static BdoElementSet NewSet<T>(params object[] objects)
            where T : class, IBdoElementSet, new()
        {
            var index = 0;
            return NewSet<T>(objects?.Select(p =>
            {
                var scalar = BdoElements.NewScalar(DataValueTypes.Any, p);
                scalar.WithIndex(++index);
                return scalar;
            }).ToArray());
        }

        /// <summary>
        /// Creates a new instance of the IBdoElementSet class.
        /// </summary>
        /// <param name="stringObject">The string to consider.</param>
        /// <returns>The collection.</returns>
        public static BdoElementSet NewSet<T>(
            string stringObject)
            where T : class, IBdoElementSet, new()
        {
            var elementSet = new BdoElementSet();
            if (stringObject != null)
            {
                foreach (var subString in stringObject.Split(';'))
                {
                    if (subString.IndexOf("=") > 0)
                    {
                        int i = subString.IndexOf("=");
                        elementSet.Add(
                            BdoElements.NewScalar(
                                subString[..i],
                                DataValueTypes.Text,
                                subString[(i + 1)..]));
                    }
                }
            }
            return elementSet;
        }
    }
}
