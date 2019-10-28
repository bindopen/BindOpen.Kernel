using BindOpen.Framework.Core.Extensions.Items.Carriers;

namespace BindOpen.Framework.Core.Data.Elements.Carrier
{
    /// <summary>
    /// 
    /// </summary>
    public interface ICarrierElement : IDataElement
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        new ICarrierConfiguration this[int index] { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        new ICarrierConfiguration this[string name] { get; }

        /// <summary>
        /// 
        /// </summary>
        new ICarrierConfiguration First { get; }

        /// <summary>
        /// 
        /// </summary>
        string DefinitionUniqueId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        new CarrierElementSpec Specification { get; set; }
    }
}