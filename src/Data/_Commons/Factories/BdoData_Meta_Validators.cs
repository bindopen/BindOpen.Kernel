using BindOpen.Data.Meta;
using BindOpen.Data.Schema;
using BindOpen.Scoping;

namespace BindOpen.Data
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
        public static BdoDataValidator CreateValidator(this IBdoScope scope)
            => scope.CreateValidator<BdoDataValidator, IBdoMetaData, IBdoSchema>();

        /// <summary>
        /// Instantiates a new instance of the NewResultItem class.
        /// </summary>
        /// <returns>The new instance of the NewResultItem class.</returns>
        public static T CreateValidator<T, TSpecified, TSpec>(this IBdoScope scope)
            where T : ITBdoDataValidator<TSpecified, TSpec>, new()
            where TSpecified : IBdoSchematized, IReferenced
            where TSpec : IBdoSchema
        {
            var result = new T()
                .WithScope(scope);

            return result;
        }
    }
}
