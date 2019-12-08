using BindOpen.Framework.Core.Application.Scopes;
using BindOpen.Framework.Core.Data.Elements.Source;
using BindOpen.Framework.Core.Extensions.Definition.Items;
using BindOpen.Framework.Core.System.Diagnostics;

namespace BindOpen.Framework.Core.Extensions.Runtime.Items
{
    /// <summary>
    /// 
    /// </summary>
    public interface IBdoConnector : ITBdoExtensionItem<IBdoConnectorDefinition>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="log"></param>
        /// <returns></returns>
        ISourceElement AsElement(string name = null, IBdoLog log = null);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        bool IsConnected();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IBdoLog Open();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IBdoLog Close();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionString"></param>
        void UpdateConnectionString(string connectionString = null);

        /// <summary>
        /// Updates the instance considering the specified scope.
        /// </summary>
        /// <param name="scope">The scope to consider.</param>
        IBdoConnector WithScope(IBdoScope scope);
    }
}