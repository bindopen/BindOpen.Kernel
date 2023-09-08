using System;
using System.Linq;

namespace BindOpen.Kernel.Data.Meta.Reflection
{
    /// <summary>
    /// This class represents a meta data.
    /// </summary>
    public static partial class BdoMetaDataExtensions
    {
        /// <summary>
        /// Gets information of the specified property.
        /// </summary>
        /// <param key="objectType">The object type to consider.</param>
        /// <param key="propertyName">The property name to consider.</param>
        /// <param key="attributeTypes"></param>
        /// <param key="attribute">The attribute to return.</param>
        public static object GetPropertyValue(
            this object obj,
            string propertyName,
            Type attributeType)
        {
            if (obj != null && propertyName != null)
            {
                var propertyInfo = obj.GetType().GetProperties().FirstOrDefault(q =>
                {
                    var spec = BdoData.NewSpec();
                    spec.UpdateFrom(q, attributeType);

                    return spec.Name.Equals(propertyName, StringComparison.OrdinalIgnoreCase);
                });

                return propertyInfo?.GetValue(obj);
            }

            return null;
        }
    }
}
