using BindOpen.Extensions.Runtime;

namespace BindOpen.Data.Elements
{
    /// <summary>
    /// 
    /// </summary>
    public interface ICarrierElement : IDataElement
    {
        /// <summary>
        /// The configuration of this instance.
        /// </summary>
        /// <returns></returns>
        IBdoCarrierConfiguration Item();

        /// <summary>
        /// 
        /// </summary>
        string DefinitionUniqueId { get; set; }

        /// <summary>
        /// Sets the specified configuration.
        /// </summary>
        /// <param name="configuration"></param>
        /// <returns></returns>
        ICarrierElement WithConfiguration(IBdoCarrierConfiguration configuration);

        /// <summary>
        /// 
        /// </summary>
        new CarrierElementSpec Specification { get; set; }
    }
}