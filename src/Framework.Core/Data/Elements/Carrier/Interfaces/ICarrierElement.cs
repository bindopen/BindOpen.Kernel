using BindOpen.Framework.Core.Extensions.Runtime.Items;

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
        new IBdoCarrierConfiguration this[int index] { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        new IBdoCarrierConfiguration this[string name] { get; }

        /// <summary>
        /// 
        /// </summary>
        new IBdoCarrierConfiguration First { get; }

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