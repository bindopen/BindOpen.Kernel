namespace BindOpen.Framework.Core.Extensions.Definition.Items
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ITBdoExtensionItemDefinition<T> : IBdoExtensionItemDefinition
        where T : IBdoExtensionItemDefinitionDto
    {
        /// <summary>
        /// 
        /// </summary>
        T Dto { get; }
    }
}