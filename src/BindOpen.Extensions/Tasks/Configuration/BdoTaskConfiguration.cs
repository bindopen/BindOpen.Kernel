using BindOpen.Data;
using BindOpen.Data.Meta;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BindOpen.Extensions.Tasks
{
    public class BdoTaskConfiguration : BdoConfiguration, IBdoTaskConfiguration
    {
        public IList<IBdoTaskConfiguration> Children { get; set; }

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

        public IEnumerable<IBdoTaskConfiguration> GetChildren(Predicate<IBdoTaskConfiguration> filter)
            => Children?.Where(p => filter(p)).ToList();

        public IBdoTaskConfiguration Child(Predicate<IBdoTaskConfiguration> filter = null, bool isRecursive = false)
        {
            if (filter == null || filter(this)) return this;

            if (isRecursive && Children != null)
            {
                foreach (var child in Children)
                {
                    var subChild = child.Child(filter, true);
                    if (subChild != null) return subChild;
                }
            }

            return null;
        }

        public bool HasChild(Predicate<IBdoTaskConfiguration> filter = null)
            => Children?.Any(p => filter == null || filter(p)) ?? false;

        public IBdoTaskConfiguration AddChild(IBdoTaskConfiguration child)
        {
            Children ??= new List<IBdoTaskConfiguration>();
            Children.Add(child);

            return this;
        }

        public IBdoTaskConfiguration InsertChild(Action<IBdoTaskConfiguration> updater)
        {
            var child = BdoData.New<BdoTaskConfiguration>(updater);

            AddChild(child);

            return child;
        }

        #endregion
    }
}