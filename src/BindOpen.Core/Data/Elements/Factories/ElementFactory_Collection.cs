using BindOpen.Data.Elements;

namespace BindOpen.Data.Elements
{
    /// <summary>
    /// This static class provides methods to create data elements.
    /// </summary>
    public static partial class ElementFactory
    {
        /// <summary>
        /// Initializes a new collection element.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="elements">The elements to consider.</param>
        public static CollectionElement CreateCollection(
            string name,
            params DataElement[] elements)
        {
            return CreateCollection(name, null, null, elements);
        }

        /// <summary>
        /// Initializes a new collection element.
        /// </summary>
        /// <param name="elements">The elements to consider.</param>
        public static CollectionElement CreateCollection(
            params DataElement[] elements)
        {
            return CreateCollection(null, null, null, elements);
        }

        /// <summary>
        /// Initializes a new collection element.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="id">The ID to consider.</param>
        /// <param name="specification">The specification to consider.</param>
        /// <param name="elements">The elements to consider.</param>
        public static CollectionElement CreateCollection(
            string name,
            string id,
            ICollectionElementSpec specification,
            params DataElement[] elements)
        {
            CollectionElement element = new CollectionElement(name, id, specification, elements);
            element.SetItem(elements);

            return element;
        }
    }
}
