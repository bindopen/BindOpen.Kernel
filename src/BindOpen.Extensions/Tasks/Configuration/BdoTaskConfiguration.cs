using BindOpen.Data.Meta;
using System;
using System.Linq;

namespace BindOpen.Extensions.Tasks
{
    public class BdoTaskConfiguration : BdoConfiguration,
        IBdoTaskConfiguration
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

        public IBdoTaskConfiguration WithInputs(params IBdoMetaData[] inputs)
        {
            With(Items?.Where(q => !q.OfGroup(IBdoTaskExtensions.__Token_Input)).ToArray());
            Array.ForEach(inputs, q => { q.AsInput(); });
            base.Add(inputs);
            return this;
        }

        public IBdoTaskConfiguration WithOutputs(params IBdoMetaData[] outputs)
        {
            With(Items?.Where(q => !q.OfGroup(IBdoTaskExtensions.__Token_Output)).ToArray());
            Array.ForEach(outputs, q => { q.AsOutput(); });
            base.Add(outputs);
            return this;
        }

        public new IBdoTaskConfiguration Add(params IBdoMetaData[] items)
        {
            Array.ForEach(items, q => { q.WithGroupId(null); });
            base.Add(items);
            return this;
        }

        public IBdoTaskConfiguration AddInputs(params IBdoMetaData[] inputs)
        {
            Array.ForEach(inputs, q => { q.AsInput(); });
            base.Add(inputs);
            return this;
        }

        public IBdoTaskConfiguration AddOutputs(params IBdoMetaData[] outputs)
        {
            Array.ForEach(outputs, q => { q.AsOutput(); });
            base.Add(outputs);
            return this;
        }

        #endregion
    }
}