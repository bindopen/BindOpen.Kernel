using BindOpen.Framework.Extensions.Runtime;

namespace BindOpen.Framework.Data.Elements
{
    /// <summary>
    /// 
    /// </summary>
    public interface ISourceElement : IDataElement
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        new IBdoConnectorConfiguration this[int index] { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        new IBdoConnectorConfiguration this[string name] { get; }

        /// <summary>
        /// 
        /// </summary>
        new IBdoConnectorConfiguration First { get; }

        /// <summary>
        /// 
        /// </summary>
        string DefinitionUniqueId { get; set; }
    }
}