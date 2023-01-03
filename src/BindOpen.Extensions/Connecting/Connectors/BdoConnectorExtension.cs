using BindOpen.Data.Elements;

namespace BindOpen.Extensions.Connecting
{
    /// <summary>
    /// This class represents a connector extension.
    /// </summary>
    public static class BdoConnectionExtension
    {
        /// <summary>
        /// Returns a data element representing this instance.
        /// </summary>
        /// <param name="connector">The connector to consider.</param>
        /// <param name="name">The name of the element to create.</param>
        /// <returns>Retuns the data element that represents this instace.</returns>
        public static ISourceElement AsElement(
            this IBdoConnector connector,
            string name = null)
        {
            return BdoElements.NewSource(name, connector?.Configuration);
        }
    }
}
