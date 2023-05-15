using BindOpen.Data.Meta;
using System;
using System.Linq;

namespace BindOpen.Extensions.Tasks
{
    public class BdoTaskConfiguration : BdoConfiguration, IBdoTaskConfiguration
    {
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
            With(Items?.Where(q => !q.OfGroup(null)).ToArray());
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