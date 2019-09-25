namespace BindOpen.Framework.Core.Extensions.Items
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ITAppExtensionItemDefinition<T> : IAppExtensionItemDefinition
        where T : IAppExtensionItemDefinitionDto
    {
        /// <summary>
        /// 
        /// </summary>
        T Dto { get; }
    }
}