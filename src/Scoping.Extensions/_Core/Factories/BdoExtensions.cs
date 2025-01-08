using BindOpen.Data;
using BindOpen.Data.Meta;
using BindOpen.Data.Meta.Reflection;

namespace BindOpen.Scoping
{
    /// <summary>
    /// This static class provides methods to handle configs.
    /// </summary>
    public static class BdoExtensions
    {
        public static IBdoMetaObject ToMeta(
            this IBdoExtension extension,
            IBdoScope scope,
            string name = null,
            bool onlyMetaAttributes = true)
        {
            if (scope != null && extension != null)
            {
                var extensionKind = extension.GetValueType().GetExtensionKind();
                var extensionDefinition = scope.ExtensionStore?.GetDefinitionFromType(extension.GetType());

                var meta = BdoData.NewObject(name)
                    .WithDataType(extensionKind, extensionDefinition?.UniqueName)
                    .WithId(extension?.Identifier)
                    .WithScope(scope)
                    .WithData(extension)
                    .UpdateTree(onlyMetaAttributes)
                    .WithName(name);

                return meta;
            }

            return null;
        }
    }
}
