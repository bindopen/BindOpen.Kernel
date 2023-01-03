namespace BindOpen.Data.Elements
{
    /// <summary>
    /// 
    /// </summary>
    public interface IObjectElement :
        ITBdoElement<IObjectElement, IObjectElementSpec, object>
    {
        /// <summary>
        /// 
        /// </summary>
        string ClassFullName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        IObjectElement WithClassFullName(string classFullName);

        /// <summary>
        /// 
        /// </summary>
        string DefinitionUniqueId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        IObjectElement WithDefinitionUniqueId(string definitionUniqueId);
    }
}