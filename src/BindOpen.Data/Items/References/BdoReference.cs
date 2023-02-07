using BindOpen.Data.Meta;
using BindOpen.Logging;
using BindOpen.Runtime.Scopes;
using System.Collections.Generic;

namespace BindOpen.Data.Items
{
    /// <summary>
    /// This class represents a data reference.
    /// </summary>
    public class BdoReference : BdoItem, IBdoReference
    {
        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// Source element of this instance.
        /// </summary>
        public IBdoMetaData SourceMetaData { get; set; }

        /// <summary>
        /// Source item of this instance.
        /// </summary>
        public object SourceObject => SourceMetaData?.GetData();

        /// <summary>
        /// Target item of this instance.
        /// </summary>
        public object TargetObject { get; set; }

        /// <summary>
        /// The data handler unique name of this instance.
        /// </summary>
        public string DataHandlerUniqueName { get; set; }

        /// <summary>
        /// The path detail of this instance.
        /// </summary>
        public IBdoMetaList PathDetail { get; set; }

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the DataReference class.
        /// </summary>
        public BdoReference()
            : base()
        {
        }

        #endregion

        // --------------------------------------------------
        // ACCESSORS
        // --------------------------------------------------

        #region Accessors

        /// <summary>
        /// The root element of this instance.
        /// </summary>
        public IBdoMetaData RootElement() =>
            SourceMetaData?.Reference?.SourceMetaData;

        /// <summary>
        /// Gets the initial data source of this instance.
        /// </summary>
        /// <returns>Returns the initial data source of this instance.</returns>
        public IBdoDatasource GetDatasource()
        {
            return null;
        }

        /// <summary>
        /// Gets the items from the source of this instance and update the target items.
        /// </summary>
        /// <param name="scope">The scope to consider.</param>
        /// <param name="varSet">The variable element set to use.</param>
        /// <param name="log">The log to consider.</param>
        /// <returns>Returns the retrieved items.</returns>
        public object Get(
            IBdoScope scope = null,
            IBdoMetaList varSet = null,
            IBdoLog log = null)
        {
            //SetDefinition((scope== null ? null : scope.BdoExtension));
            //log.AddEvents(Check());

            var dataItems = new List<object>();
            //parameterDetail = (parameterDetail ?? new BdoElementSet());

            //if (!log.HasErrorsOrExceptions())
            //    if (Definition == null)
            //        log.AddError(title: "Definition not found");
            //    else if (Definition.RuntimeFunction_Get == null)
            //        log.AddError(title: "Calling function missing");
            //    else if (aSourceElement == null)
            //        log.AddError(title: "Source element missing");
            //    else
            //        dataItems.AddRange(Definition.RuntimeFunction_Get(aSourceElement, parameterDetail, scope, varSet, log));

            return dataItems;
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
            BdoReference dataReference = Clone<BdoReference>(areas);
            dataReference.SourceMetaData = SourceMetaData.Clone<IBdoMetaData>();
            dataReference.PathDetail = PathDetail.Clone<BdoMetaList>();

            return dataReference;
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

            _isDisposed = true;

            base.Dispose(isDisposing);
        }

        #endregion
    }
}