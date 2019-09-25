using BindOpen.Framework.Core.Data.Elements.Source;
using BindOpen.Framework.Core.Extensions.Items.Connectors.Definition;
using BindOpen.Framework.Core.System.Diagnostics;

namespace BindOpen.Framework.Core.Extensions.Items.Connectors
{
    /// <summary>
    /// 
    /// </summary>
    public interface IConnector : ITAppExtensionItem<IConnectorDefinition>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="log"></param>
        /// <returns></returns>
        ISourceElement AsElement(string name = null, ILog log = null);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        bool IsConnected();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        ILog Open();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        ILog Close();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionString"></param>
        void UpdateConnectionString(string connectionString = null);
    }
}