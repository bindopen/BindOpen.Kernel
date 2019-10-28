using System.Collections.Generic;
using System.Linq;
using BindOpen.Framework.Core.Data.Items;
using BindOpen.Framework.Core.Extensions.Items;

namespace BindOpen.Framework.Core.Extensions.Indexes
{
    /// <summary>
    /// This class represents an application extension item index.
    /// </summary>
    /// <typeparam name="T">The class of this extension item definition.</typeparam>
    public class TAppExtensionItemIndex<T> : DataItem, ITAppExtensionItemIndex<T>
        where T : IAppExtensionItemDefinition
    {
        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// The ID of this instance.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Definitions of this instance.
        /// </summary>
        public List<T> Definitions { get; set; } = new List<T>();

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the TAppExtensionItemIndex class.
        /// </summary>
        public TAppExtensionItemIndex()
        {
        }

        /// <summary>
        /// Instantiates a new instance of the TAppExtensionItemIndex class.
        /// </summary>
        /// <param name="definitions">The definitions to consider.</param>
        public TAppExtensionItemIndex(params T[] definitions)
        {
            SetDefinitions(definitions.ToList());
        }

        #endregion

        /// <summary>
        /// Sets the specified definitions of this instance.
        /// </summary>
        /// <param name="definitions">The definitions to consider.</param>
        public virtual void SetDefinitions(List<T> definitions)
        {
            Definitions = definitions;
        }
    }
}
