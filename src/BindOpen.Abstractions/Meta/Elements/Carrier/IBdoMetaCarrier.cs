using BindOpen.Extensions.Modeling;

namespace BindOpen.Meta.Elements
{
    /// <summary>
    /// 
    /// </summary>
    public interface IBdoMetaCarrier :
        ITBdoMetaElement<IBdoMetaCarrier, IBdoMetaCarrierSpec, IBdoCarrierConfiguration>
    {
        /// <summary>
        /// 
        /// </summary>
        string DefinitionUniqueId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        IBdoMetaCarrier WithDefinitionUniqueId(string definitionUniqueId);
    }
}