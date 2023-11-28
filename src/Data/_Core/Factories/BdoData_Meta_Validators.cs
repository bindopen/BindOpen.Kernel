using BindOpen.Kernel.Data.Meta;
using BindOpen.Kernel.Scoping;
using System;

namespace BindOpen.Kernel.Data
{
    /// <summary>
    /// This class represents a data item factory.
    /// </summary>
    /// <example>Titles, Descriptions.</example>
    public static partial class BdoData
    {
        /// <summary>
        /// Instantiates a new instance of the NewResultItem class.
        /// </summary>
        /// <returns>The new instance of the NewResultItem class.</returns>
        public static BdoMetaDataValidator CreateValidator(this IBdoScope scope)
            => scope.CreateValidator<BdoMetaDataValidator, IBdoMetaData, IBdoSpec>();

        /// <summary>
        /// Instantiates a new instance of the NewResultItem class.
        /// </summary>
        /// <returns>The new instance of the NewResultItem class.</returns>
        public static BdoMetaDataValidator CreateValidator<T, TSpecified, TSpec>(this IBdoScope scope)
            where T : ITBdoDataValidator<TSpecified, TSpec>, new()
            where TSpecified : IBdoSpecified
            where TSpec : IBdoSpec
        {
            var result = new BdoMetaDataValidator()
                .WithScope(scope);

            return result;
        }
    }
}
