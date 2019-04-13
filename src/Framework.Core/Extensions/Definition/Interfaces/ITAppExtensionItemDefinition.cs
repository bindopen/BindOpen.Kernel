namespace BindOpen.Framework.Core.Extensions.Definition
{
    public interface ITAppExtensionItemDefinition<T> : IAppExtensionItemDefinition
        where T : IAppExtensionItemDefinitionDto
    {
        T Dto { get; }
    }
}