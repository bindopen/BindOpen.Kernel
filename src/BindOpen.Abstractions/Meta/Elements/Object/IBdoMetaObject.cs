namespace BindOpen.Meta.Elements
{
    /// <summary>
    /// 
    /// </summary>
    public interface IBdoMetaObject :
        ITBdoMetaElement<IBdoMetaObject, IBdoMetaObjectSpec, object>
    {
        /// <summary>
        /// 
        /// </summary>
        string ClassFullName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        IBdoMetaObject WithClassFullName(string classFullName);

        /// <summary>
        /// 
        /// </summary>
        string DefinitionUniqueId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        IBdoMetaObject WithDefinitionUniqueId(string definitionUniqueId);
    }
}