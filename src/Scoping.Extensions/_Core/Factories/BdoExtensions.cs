using BindOpen.System.Data;
using BindOpen.System.Data.Meta;
using BindOpen.System.Data.Meta.Reflection;

namespace BindOpen.System.Scoping
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
                config = Data.BdoData.NewConfig(name);

                config
                    .WithData(extension)
                    .UpdateTree(true);

                // we get definition unique name

                var extensionKind = extension.GetValueType().GetExtensionKind();
                var extensionDefinition = scope.ExtensionStore.GetDefinitionFromType(
                    extensionKind,
                    Data.BdoData.Class(extension.GetType()));
                config.WithDefinition(extensionDefinition?.UniqueName);

                return config;
            }

            return config;
        }
    }
}
