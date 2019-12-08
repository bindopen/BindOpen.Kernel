using BindOpen.Framework.Core.Extensions.Runtime.Items;

namespace BindOpen.Framework.Core.Data.Elements
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