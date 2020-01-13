using BindOpen.Framework.Data.Elements;
using BindOpen.Framework.Extensions.Definition;
using BindOpen.Framework.System.Diagnostics;

namespace BindOpen.Framework.Extensions.Runtime
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
        /// Instantiates a new instance of the Entity class.
        /// </summary>
        protected BdoEntity() : base()
        {
        }

        /// <summary>
        /// Instantiates a new instance of the Entity class.
        /// </summary>
        /// <param name="config">The configuration of this instance.</param>
        protected BdoEntity(IBdoEntityConfiguration config) : base(config)
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