using BindOpen.Framework.Core.Data.Elements._Object;
using BindOpen.Framework.Core.Data.Elements;
using BindOpen.Framework.Core.Extensions.Items.Entities.Definition;
using BindOpen.Framework.Core.System.Diagnostics;

namespace BindOpen.Framework.Core.Extensions.Items.Entities
{
    /// <summary>
    /// This class represents an data entity item.
    /// </summary>
    public abstract class Entity : TAppExtensionItem<EntityDefinition>, IEntity
    {
        /// <summary>
        /// 
        /// </summary>
        new public IEntityConfiguration Configuration { get => base.Configuration as IEntityConfiguration; }

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the Entity class.
        /// </summary>
        protected Entity() : base()
        {
        }

        /// <summary>
        /// Instantiates a new instance of the Entity class.
        /// </summary>
        /// <param name="config">The configuration of this instance.</param>
        protected Entity(IEntityConfiguration config) : base(config)
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
        public IObjectElement AsElement(string name=null, ILog log = null)
        {
            return ElementFactory.CreateObject(name ?? Name, base.Configuration as IEntityConfiguration);
        }

        #endregion
    }
}