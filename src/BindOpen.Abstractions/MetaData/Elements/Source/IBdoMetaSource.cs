using BindOpen.Extensions.Connecting;

namespace BindOpen.MetaData.Elements
{
    /// <summary>
    /// 
    /// </summary>
    public interface IBdoMetaSource :
        ITBdoMetaElement<IBdoMetaSource, IBdoMetaSourceSpec, IBdoConnectorConfiguration>
    {
        /// <summary>
        /// 
        /// </summary>
        string DefinitionUniqueId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        IBdoMetaSource WithDefinitionUniqueId(string definitionUniqueId);
    }
}