namespace BindOpen.Meta.Elements
{
    /// <summary>
    /// 
    /// </summary>
    public interface IBdoMetaDocument : ITBdoMetaElement<IBdoMetaDocument, IBdoMetaDocumentSpec, IBdoMetaObject>
    {
        /// <summary>
        /// 
        /// </summary>
        IBdoMetaCarrier ContainerElement { get; set; }
    }
}