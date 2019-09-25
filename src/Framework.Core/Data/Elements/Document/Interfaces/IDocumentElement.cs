using BindOpen.Framework.Core.Data.Elements._Object;
using BindOpen.Framework.Core.Data.Elements.Carrier;

namespace BindOpen.Framework.Core.Data.Elements.Document
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