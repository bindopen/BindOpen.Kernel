using BindOpen.Extensions.Runtime;

namespace BindOpen.Data.Elements
{
    /// <summary>
    /// 
    /// </summary>
    public interface ISourceElement : IDataElement
    {
        /// <summary>
        /// The configuration of this instance.
        /// </summary>
        /// <returns></returns>
        IBdoConnectorConfiguration Item();

        /// <summary>
        /// 
        /// </summary>
        string DefinitionUniqueId { get; set; }
    }
}