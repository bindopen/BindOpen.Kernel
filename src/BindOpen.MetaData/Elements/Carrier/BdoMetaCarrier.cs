using BindOpen.Extensions.Modeling;
using BindOpen.Logging;
using BindOpen.Runtime.Scopes;
using System.Collections.Generic;

namespace BindOpen.MetaData.Elements
{
    /// <summary>
    /// This class represents a carrier el.
    /// </summary>
    public class BdoMetaCarrier :
        TBdoMetaElement<IBdoMetaCarrier, IBdoMetaCarrierSpec, IBdoCarrierConfiguration>,
        IBdoMetaCarrier
    {
        // --------------------------------------------------
        // CONSTRUCTORS
        // --------------------------------------------------

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the CarrierElement class.
        /// </summary>
        public BdoMetaCarrier() : this(null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the CarrierElement class.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="id">The ID to consider.</param>
        public BdoMetaCarrier(string name = null, string id = null)
            : base(name, "carrier_", id)
        {
            WithValueType(DataValueTypes.Carrier);
        }

        #endregion

        // --------------------------------------------------
        // ICarrierElement Implementation
        // --------------------------------------------------

        #region ICarrierElement

        /// <summary>
        /// The definition unique ID of this instance.
        /// </summary>
        public string DefinitionUniqueId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="definitionUniqueId"></param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public IBdoMetaCarrier WithDefinitionUniqueId(string definitionUniqueId)
        {
            DefinitionUniqueId = definitionUniqueId;

            return this;
        }

        // Items ----------------------------

        /// <summary>
        /// Sets a new single item of this instance.
        /// </summary>
        /// <param name="item">The string item of this instance.</param>
        /// <remarks>Items of this instance must be allowed and must not be forbidden. Otherwise, the items will be the default ones..</remarks>
        /// <returns>Returns True if the specified has been well added.</returns>
        public override IBdoMetaCarrier WithItems(params IBdoCarrierConfiguration[] item)
        {
            if (item != null)
            {
                base.WithItems(item);

                if (_item is IBdoCarrierConfiguration configuration
                    && !string.IsNullOrEmpty(configuration.DefinitionUniqueId))
                {
                    DefinitionUniqueId = configuration?.DefinitionUniqueId;
                }
            }

            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IBdoMetaElementSpec IBdoMetaElement.NewSpecification()
        {
            return NewSpecification();
        }

        /// <summary>
        /// Returns a text node representing this instance.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return "";
        }

        // Sub items

        /// <summary>
        /// 
        /// </summary>
        /// <param name="scope"></param>
        /// <param name="varElementSet"></param>
        /// <param name="log"></param>
        /// <returns></returns>
        public object SubItem(
            string key,
            IBdoScope scope = null,
            IBdoMetaElementSet varElementSet = null,
            IBdoLog log = null)
        {
            var item = Item(scope, varElementSet, log);
            if (item != null)
            {
                return item.GetItem(key, scope, varElementSet, log);
            }
            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="scope"></param>
        /// <param name="varElementSet"></param>
        /// <param name="log"></param>
        /// <returns></returns>
        public Q SubItem<Q>(
            string key,
            IBdoScope scope = null,
            IBdoMetaElementSet varElementSet = null,
            IBdoLog log = null)
        {
            var item = Item(scope, varElementSet, log);
            if (item != null)
            {
                return item.GetItem<Q>(key, scope, varElementSet, log);
            }
            return default;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="scope"></param>
        /// <param name="varElementSet"></param>
        /// <param name="log"></param>
        /// <returns></returns>
        public List<object> SubItems(
            string key,
            IBdoScope scope = null,
            IBdoMetaElementSet varElementSet = null,
            IBdoLog log = null)
        {
            var item = Item(scope, varElementSet, log);
            if (item != null)
            {
                return item.GetItems(key, scope, varElementSet, log);
            }
            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="scope"></param>
        /// <param name="varElementSet"></param>
        /// <param name="log"></param>
        /// <returns></returns>
        public List<Q> SubItems<Q>(
            string key,
            IBdoScope scope = null,
            IBdoMetaElementSet varElementSet = null,
            IBdoLog log = null)
        {
            var item = Item(scope, varElementSet, log);
            if (item != null)
            {
                return item.GetItems<Q>(key, scope, varElementSet, log);
            }
            return null;
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
            var el = base.Clone<BdoMetaCarrier>(areas);
            return el;
        }

        #endregion
    }
}

