namespace BindOpen.Data.Elements
{
    /// <summary>
    /// 
    /// </summary>
    public interface IDocumentElement : ITBdoElement<IDocumentElement, IDocumentElementSpec, IObjectElement>
    {
        /// <summary>
        /// 
        /// </summary>
        ICarrierElement ContainerElement { get; set; }
    }
}