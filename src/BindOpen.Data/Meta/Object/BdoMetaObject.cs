using BindOpen.Data.Assemblies;
using BindOpen.Logging;
using BindOpen.Runtime.Scopes;
using System;

namespace BindOpen.Data.Meta
{
    /// <summary>
    /// This class represents a catalog el that is an el whose els are carriers.
    /// </summary>
    public class BdoMetaObject :
        TBdoMetaData<IBdoMetaObject, IBdoMetaObjectSpec, object>,
        IBdoMetaObject
    {
        // --------------------------------------------------
        // CONSTRUCTORS
        // --------------------------------------------------

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the CollectionElement class.
        /// </summary>
        public BdoMetaObject() : this(null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the CollectionElement class.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="id">The ID to consider.</param>
        public BdoMetaObject(string name = null, string id = null)
            : base(name, "object_", id)
        {
            WithValueType(DataValueTypes.Object);
        }

        #endregion

        // --------------------------------------------------
        // IBdoDataObject Implementation
        // --------------------------------------------------

        #region IBdoDataObject

        /// <summary>
        /// 
        /// </summary>
        public IBdoMetaSet SubSet { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public IBdoMetaObject WithSubSet(IBdoMetaSet set)
        {
            SubSet = set;
            //var item = set?.FirstOrDefault();
            //if (item is IBdoConfiguration config
            //    && !string.IsNullOrEmpty(item.DefinitionUniqueId))
            //{
            //    ClassReference = BdoData.ClassFromEntity(
            //        item?.DefinitionUniqueId);
            //}
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        public IBdoMetaObject WithSubSet(params IBdoMetaData[] metas)
            => WithSubSet(BdoMeta.NewSet(metas));

        /// <summary>
        /// The class full name of this instance.
        /// </summary>
        public IBdoClassReference ClassReference { get; set; }

        public IBdoMetaObject WithClassReference(IBdoClassReference reference)
        {
            ClassReference = reference;

            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IBdoMetaObject UpdateTree(
            IBdoScope scope = null,
            IBdoLog log = null)
        {
            var obj = Item();
            SubSet = obj.ToMetaSet(
                GetItemType(scope, log));

            return this;
        }

        public Type GetItemType(
            IBdoScope scope = null,
            IBdoLog log = null)
        {
            var type = scope?.CreateType(ClassReference);
            return type;
        }

        #endregion

        // --------------------------------------------------
        // ITEMS
        // --------------------------------------------------

        #region Items

        // Items ----------------------------

        /// <summary>
        /// Sets a new single item of this instance.
        /// </summary>
        /// <param name="item">The string item of this instance.</param>
        /// <remarks>Items of this instance must be allowed and must not be forbidden. Otherwise, the items will be the default ones..</remarks>
        /// <returns>Returns True if the specified has been well added.</returns>
        public override IBdoMetaObject WithItems(params object[] item)
        {
            if (item != null)
            {
                base.WithItems(item);
            }

            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IBdoMetaDataSpec IBdoMetaData.NewSpecification()
        {
            return NewSpecification();
        }

        // Items ----------------------------

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
            var el = base.Clone(areas) as BdoMetaObject;
            return el;
        }

        #endregion
    }
}
