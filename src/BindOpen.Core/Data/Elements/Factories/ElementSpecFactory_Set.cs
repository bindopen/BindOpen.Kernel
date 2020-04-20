using System.Linq;

namespace BindOpen.Data.Elements
{
    /// <summary>
    /// This static class provides methods to create element specifications.
    /// </summary>
    public static partial class ElementSpecFactory
    {
        /// <summary>
        /// Instantiates a new instance of the DataElementSet class.
        /// </summary>
        /// <param name="elements">The elements to consider.</param>
        public static DataElementSpecSet CreateSet(params IDataElementSpec[] elements)
        {
            var elementSpecSet = new DataElementSpecSet()
            {
                Items = elements?.Cast<DataElementSpec>().ToList()
            };

            return elementSpecSet;
        }
    }
}
