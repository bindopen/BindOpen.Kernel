using BindOpen.Data.Elements;

namespace BindOpen.Extensions.Modeling
{
    /// <summary>
    /// This class represents a carrier extension.
    /// </summary>
    public static class BdoCarrierExtension
    {
        /// <summary>
        /// Returns a data element representing this instance.
        /// </summary>
        /// <param name="carrier">The carrier to consider.</param>
        /// <param name="name">The name of the element to create.</param>
        /// <returns>Retuns the data element that represents this instace.</returns>
        public static ICarrierElement AsElement(
            this IBdoCarrier carrier,
            string name = null)
        {
            return BdoElements.NewCarrier(name)
                .WithItem(carrier?.Configuration);
        }
    }
}
