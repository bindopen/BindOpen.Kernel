using BindOpen.Framework.Core.Data.Common;
using System.Linq;

namespace BindOpen.Framework.Core.Data.Elements
{
    /// <summary>
    /// This static class provides methods to create data element set.
    /// </summary>
    public static partial class ElementSetFactory
    {
        /// <summary>
        /// Defines the parameters of this instance.
        /// </summary>
        /// <param name="parameters">The parameters to consider.</param>
        /// <returns>Return this instance.</returns>
        public static DataElementSet Create(params DataElement[] parameters)
        {
            return new DataElementSet(parameters);
        }

        /// <summary>
        /// Defines the parameters of this instance.
        /// </summary>
        /// <param name="parameters">The parameters to consider.</param>
        /// <returns>Return this instance.</returns>
        public static DataElementSet Create(params (string, object)[] parameters)
        {
            return new DataElementSet(parameters?.Select(p => ElementFactory.CreateScalar(p.Item1, DataValueType.Any, p.Item2)).ToArray());
        }

        /// <summary>
        /// Defines the parameters of this instance.
        /// </summary>
        /// <param name="parameters">The parameters to consider.</param>
        /// <returns>Return this instance.</returns>
        public static DataElementSet Create(params (string, DataValueType valueType, object)[] parameters)
        {
            return new DataElementSet(parameters?.Select(p => ElementFactory.CreateScalar(p.Item1, p.Item2, p.Item3)).ToArray());
        }

        /// <summary>
        /// Defines the parameters of this instance.
        /// </summary>
        /// <param name="parameterValues">The parameters to consider.</param>
        /// <returns>Return this instance.</returns>
        public static DataElementSet Create(params object[] parameterValues)
        {
            var index = 0;
            return new DataElementSet(parameterValues?.Select(p =>
            {
                var scalar = ElementFactory.CreateScalar("", DataValueType.Any, p);
                scalar.Index = ++index;
                return scalar;
            }).ToArray());
        }
    }
}
