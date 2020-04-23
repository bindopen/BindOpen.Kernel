using BindOpen.Data.Elements;
using BindOpen.Extensions.Definition;
using BindOpen.System.Diagnostics;

namespace BindOpen.Extensions.Runtime
{
    /// <summary>
    /// This class represents an data entity item.
    /// </summary>
    public abstract class BdoEntity : TBdoExtensionItem<BdoEntityDefinition>, IBdoEntity
    {
        /// <summary>
        /// 
        /// </summary>
        new public IBdoEntityConfiguration Configuration { get => base.Configuration as IBdoEntityConfiguration; }

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the BdoEntity class.
        /// </summary>
        protected BdoEntity() : base()
        {
        }

        #endregion

        // --------------------------------------------------
        // ACCESSORS
        // --------------------------------------------------

        #region Accessors

        /// <summary>
        /// Returns a data element representing this instance.
        /// </summary>
        /// <param name="name">The name of the element to create.</param>
        /// <param name="log">The log of the operation.</param>
        /// <returns>Retuns the data element that represents this instace.</returns>
        public IObjectElement AsElement(string name = null, IBdoLog log = null)
        {
            return ElementFactory.CreateObject(name ?? Name, base.Configuration as IBdoEntityConfiguration);
        }

        #endregion
    }
}