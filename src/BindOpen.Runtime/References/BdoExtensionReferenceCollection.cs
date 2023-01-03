using System.Collections.Generic;

namespace BindOpen.Runtime.References
{
    /// <summary>
    /// This class represents a data reference.
    /// </summary>
    public class BdoExtensionReferenceCollection : List<IBdoExtensionReference>, IBdoExtensionReferenceCollection
    {
        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the BdoExtensionReferenceCollection class.
        /// </summary>
        /// <param name="references">The references to consider.</param>
        public BdoExtensionReferenceCollection(IEnumerable<IBdoExtensionReference> references = null)
        {
            if (references != null)
            {
                foreach (var reference in references)
                {
                    Add(reference);
                }
            }
        }

        #endregion
    }
}