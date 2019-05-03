using BindOpen.Framework.Core.Data.Elements.Source;
using BindOpen.Framework.Core.Extensions.Items.Connectors.Definition;
using BindOpen.Framework.Core.System.Diagnostics;

namespace BindOpen.Framework.Core.Extensions.Items.Connectors
{
    public interface IConnector : ITAppExtensionItem<IConnectorDefinition>
    {
        ISourceElement AsElement(string name = null, ILog log = null);

        ILog Close();
        bool IsConnected();
        ILog Open();
        void UpdateConnectionString(string connectionString = null);
    }
}