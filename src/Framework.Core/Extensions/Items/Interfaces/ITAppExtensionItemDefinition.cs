namespace BindOpen.Framework.Core.Extensions.Items
{
    public interface ITAppExtensionItemDefinition<T> : IAppExtensionItemDefinition
        where T : IAppExtensionItemDefinitionDto
    {
        T Dto { get; }
    }
}