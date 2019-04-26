namespace BindOpen.Framework.Core.Extensions.Definitions
{
    public interface ITAppExtensionItemDefinition<T> : IAppExtensionItemDefinition
        where T : IAppExtensionItemDefinitionDto
    {
        T Dto { get; }
    }
}