using BindOpen.Extensions.Modeling;

namespace BindOpen.Data.Elements
{
    /// <summary>
    /// 
    /// </summary>
    public interface ICarrierElement :
        ITBdoElement<ICarrierElement, ICarrierElementSpec, IBdoCarrierConfiguration>
    {
        /// <summary>
        /// 
        /// </summary>
        string DefinitionUniqueId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        ICarrierElement WithDefinitionUniqueId(string definitionUniqueId);
    }
}