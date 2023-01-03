using BindOpen.Extensions.Connecting;

namespace BindOpen.Data.Elements
{
    /// <summary>
    /// 
    /// </summary>
    public interface ISourceElement :
        ITBdoElement<ISourceElement, ISourceElementSpec, IBdoConnectorConfiguration>
    {
        /// <summary>
        /// 
        /// </summary>
        string DefinitionUniqueId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        ISourceElement WithDefinitionUniqueId(string definitionUniqueId);
    }
}