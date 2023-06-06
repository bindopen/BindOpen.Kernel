using BindOpen.Data.Meta;
using System;
using System.Collections.Generic;

namespace BindOpen.Extensions.Tasks
{
    public class BdoTaskConfiguration : BdoConfiguration, IBdoTaskConfiguration
    {
        public IList<IBdoTaskConfiguration> _Children { get; set; }

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

        #endregion
    }
}