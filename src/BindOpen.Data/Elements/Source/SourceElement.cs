using BindOpen.Extensions.Connecting;

namespace BindOpen.Data.Elements
{
    /// <summary>
    /// This class represents a data source element.
    /// </summary>
    /// <remarks>A data source element can only have one item maximum.</remarks>
    public class SourceElement :
        TBdoElement<ISourceElement, ISourceElementSpec, IBdoConnectorConfiguration>,
        ISourceElement
    {
        // --------------------------------------------------
        // CONSTRUCTORS
        // --------------------------------------------------

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the SourceElement class.
        /// </summary>
        public SourceElement() : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the SourceElement class.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="id">The ID to consider.</param>
        public SourceElement(string name = null, string id = null)
            : base(name, "source_", id)
        {
            WithValueType(DataValueTypes.Datasource);
        }

        #endregion

        // --------------------------------------------------
        // ISourceElement Implementation
        // --------------------------------------------------

        #region ISourceElement

        /// <summary>
        /// The definition unique ID of this instance.
        /// </summary>
        public string DefinitionUniqueId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="definitionUniqueId"></param>
        /// <returns></returns>
        public ISourceElement WithDefinitionUniqueId(string definitionUniqueId)
        {
            DefinitionUniqueId = definitionUniqueId;
            return this;
        }

        #endregion

        // --------------------------------------------------
        // ITEMS
        // --------------------------------------------------

        #region Items

        // Specification ---------------------

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IBdoElementSpec IBdoElement.NewSpecification()
        {
            return NewSpecification();
        }

        // Items ----------------------------

        /// <summary>
        /// Sets a new single item of this instance.
        /// </summary>
        /// <param name="item">The string item of this instance.</param>
        /// <remarks>Items of this instance must be allowed and must not be forbidden. Otherwise, the items will be the default ones..</remarks>
        /// <returns>Returns True if the specified has been well added.</returns>
        public ISourceElement WithItem(IBdoConnectorConfiguration item)
        {
            if (item != null)
            {
                base.WithItem(item);

                if (_item is IBdoConnectorConfiguration configuration
                    && !string.IsNullOrEmpty(configuration.DefinitionUniqueId))
                {
                    DefinitionUniqueId = configuration?.DefinitionUniqueId;
                }
            }

            return this;
        }

        /// <summary>
        /// Returns a text node representing this instance.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return "";
        }

        #endregion

        // --------------------------------------------------
        // CLONING
        // --------------------------------------------------

        #region Cloning

        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <returns>Returns a cloned instance.</returns>
        public override object Clone(params string[] areas)
        {
            SourceElement dataSourceElement = base.Clone(areas) as SourceElement;
            return dataSourceElement;
        }

        #endregion
    }
}
