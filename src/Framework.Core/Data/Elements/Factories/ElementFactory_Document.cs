using BindOpen.Framework.Core.Data.Elements._Object;
using BindOpen.Framework.Core.Data.Elements.Carrier;
using BindOpen.Framework.Core.Data.Elements.Document;

namespace BindOpen.Framework.Core.Data.Elements
{
    /// <summary>
    /// This static class provides methods to create data elements.
    /// </summary>
    public static partial class ElementFactory
    {
        /// <summary>
        /// Initializes a new document element.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="id">The ID to consider.</param>
        /// <param name="containerElement">The container element to consider.</param>
        /// <param name="contentElement">The content element to consider.</param>
        public static DocumentElement CreateDocument(
            string name,
            string id,
            ICarrierElement containerElement = null,
            IObjectElement contentElement = null)
        {
            DocumentElement element = new DocumentElement(name, id);
            element.AddItem(containerElement ?? new CarrierElement());
            element.AddItem(contentElement ?? new ObjectElement());
            return element;
        }
    }
}
