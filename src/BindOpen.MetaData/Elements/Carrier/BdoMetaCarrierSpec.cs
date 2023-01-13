﻿using BindOpen.MetaData.Specification;

namespace BindOpen.MetaData.Elements
{
    /// <summary>
    /// This class represents a carrier element specification.
    /// </summary>
    public class BdoMetaCarrierSpec : BdoMetaElementSpec, IBdoMetaCarrierSpec
    {
        // --------------------------------------------------
        // CONSTRUCTORS
        // --------------------------------------------------

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the CarrierElementSpec class.
        /// </summary>
        public BdoMetaCarrierSpec() : base()
        {
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
            var spec = base.Clone<BdoMetaCarrierSpec>(areas);
            spec.DefinitionFilter = DefinitionFilter?.Clone<DataValueFilter>();

            return spec;
        }

        #endregion

        // --------------------------------------------------
        // ICarrierElementSpec Implementation
        // --------------------------------------------------

        #region Properties

        /// <summary>
        /// The definition filter of this instance.
        /// </summary>
        public IDataValueFilter DefinitionFilter { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public IBdoMetaCarrierSpec WithDefinitionFilter(IDataValueFilter filter)
        {
            DefinitionFilter = filter;

            return this;
        }

        #endregion

        // ------------------------------------------
        // IDISPOSABLE METHODS
        // ------------------------------------------

        #region IDisposable_Methods

        private bool _isDisposed = false;

        /// <summary>
        /// Disposes this instance. 
        /// </summary>
        /// <param name="isDisposing">Indicates whether this instance is disposing</param>
        protected override void Dispose(bool isDisposing)
        {
            if (_isDisposed)
            {
                return;
            }

            DefinitionFilter?.Dispose();

            _isDisposed = true;

            base.Dispose(isDisposing);
        }

        #endregion
    }

}
