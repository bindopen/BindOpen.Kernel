using BindOpen.Data;
using BindOpen.Data.Meta;
using BindOpen.Data.Meta.Reflection;
using BindOpen.Extensions.Tasks;
using BindOpen.Scopes;

namespace BindOpen.Extensions
{
    /// <summary>
    /// This static class provides methods to handle configs.
    /// </summary>
    public static class BdoExtensions
    {
        public static IBdoConfiguration CreateConfigFrom(
            this IBdoScope scope,
            IBdoExtension extension,
            string name = null)
        {
            IBdoConfiguration config = null;

            if (scope != null && extension != null)
            {
                config = extension is IBdoTask ?
                    BdoTaskExtensions.NewTaskConfig(name) :
                    BdoConfig.New(name);

                config
                    .WithData(extension)
                    .UpdateTree(true);

                // we get definition unique name

                var extensionKind = extension.GetValueType().GetExtensionKind();
                var extensionDefintion = scope.ExtensionStore.GetDefinitionFromType(
                    extensionKind,
                    BdoData.Class(extension.GetType()));
                config.WithDefinition(extensionDefintion?.UniqueName);

                return config;
            }

            return config;
        }
    }
}
