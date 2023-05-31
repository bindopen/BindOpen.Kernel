using BindOpen.Data;
using BindOpen.Data.Meta;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BindOpen.Extensions.Tasks
{
    public class BdoTaskConfiguration : BdoConfiguration, IBdoTaskConfiguration
    {

        private IList<IBdoTaskConfiguration> _children;

        public new IBdoTaskConfiguration Parent { get => base.Parent as IBdoTaskConfiguration; set { base.Parent = value; } }

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the TaskDefinition class. 
        /// </summary>
        public BdoTaskConfiguration() : base()
        {
        }

        #endregion

        // ------------------------------------------
        // ACCESSORS
        // ------------------------------------------

        #region Accessors

        public new IBdoTaskConfiguration With(params IBdoMetaData[] items)
        {
            Remove(q => q.OfGroup(null));
            Array.ForEach(items, q => { q.WithGroupId(null); });
            base.Add(items);
            return this;
        }

        public new IBdoTaskConfiguration Add(params IBdoMetaData[] items)
        {
            Array.ForEach(items, q => { q.WithGroupId(null); });
            base.Add(items);
            return this;
        }

        public IEnumerable<IBdoTaskConfiguration> Children(Predicate<IBdoTaskConfiguration> filter)
            => _children?.Where(p => filter == null || filter(p)).ToList();

        public IBdoTaskConfiguration Child(Predicate<IBdoTaskConfiguration> filter = null, bool isRecursive = false)
        {
            if (filter == null || filter(this)) return this;

            if (isRecursive && _children != null)
            {
                foreach (var child in _children)
                {
                    var subChild = child.Child(filter, true);
                    if (subChild != null) return subChild;
                }
            }

            return null;
        }

        public bool HasChild(Predicate<IBdoTaskConfiguration> filter = null)
            => _children?.Any(p => filter == null || filter(p)) ?? false;

        public IBdoTaskConfiguration InsertChild(IBdoTaskConfiguration child)
        {
            _children ??= new List<IBdoTaskConfiguration>();
            _children.Add(child);

            return this;
        }

        public IBdoTaskConfiguration InsertChild(Action<IBdoTaskConfiguration> updater)
        {
            var child = BdoData.New<BdoTaskConfiguration>(updater);
            return InsertChild(child);
        }

        #endregion
    }
}