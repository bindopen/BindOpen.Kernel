using BindOpen.Data.Meta;
using System;

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
            Items?.RemoveAll(q => q.OfGroup(null));
            Array.ForEach(items, q => { q.GroupId = null; });
            base.Add(items);
            return this;
        }

        public IBdoTaskConfiguration WithInputs(params IBdoMetaData[] inputs)
        {
            Items?.RemoveAll(q => q.OfGroup(IBdoTaskExtensions.__Token_Input));
            Array.ForEach(inputs, q => { q.AsInput(); });
            base.Add(inputs);
            return this;
        }

        public IBdoTaskConfiguration WithOutputs(params IBdoMetaData[] outputs)
        {
            Items?.RemoveAll(q => q.OfGroup(IBdoTaskExtensions.__Token_Output));
            Array.ForEach(outputs, q => { q.AsOutput(); });
            base.Add(outputs);
            return this;
        }

        public new IBdoTaskConfiguration Add(params IBdoMetaData[] items)
        {
            Array.ForEach(items, q => { q.GroupId = null; });
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