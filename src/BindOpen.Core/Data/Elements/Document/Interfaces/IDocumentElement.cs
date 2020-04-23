namespace BindOpen.Data.Elements
{
    /// <summary>
    /// 
    /// </summary>
    public interface IDocumentElement : IDataElement
    {
        /// <summary>
        /// 
        /// </summary>
        CarrierElement ContainerElement { get; }

        /// <summary>
        /// 
        /// </summary>
        ObjectElement ContentElement { get; }

        /// <summary>
        /// 
        /// </summary>
        new DocumentElementSpec Specification { get; set; }
    }
}