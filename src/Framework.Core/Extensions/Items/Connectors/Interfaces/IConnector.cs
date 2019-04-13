using BindOpen.Framework.Core.Extensions.Definition.Connectors;
using BindOpen.Framework.Core.System.Diagnostics;

namespace BindOpen.Framework.Core.Extensions.Items.Connectors
{
    public interface IConnector : ITAppExtensionItem<IConnectorDefinition>
    {
        ILog Close();
        bool IsConnected();
        ILog Open();
        void UpdateConnectionString(string connectionString = null);
    }
}