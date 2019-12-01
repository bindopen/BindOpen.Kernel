using System.Collections.Generic;
using System.Xml.Serialization;
using BindOpen.Framework.Core.Application.Scopes;
using BindOpen.Framework.Core.Data.Items;
using BindOpen.Framework.Core.Data.Items.Source;
using BindOpen.Framework.Core.System.Diagnostics;
using BindOpen.Framework.Core.System.Scripting;

namespace BindOpen.Framework.Core.Data.References
{
    /// <summary>
    /// This class represents a data reference.
    /// </summary>
    public class DataReference : DataItem, IDataReference
    {
        // ------------------------------------------
        // VARIABLES
        // ------------------------------------------

        #region Variables

        private IDataReferenceDto _item = null;

        #endregion

        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// The DTO item of this instance.
        /// </summary>
        public IDataReferenceDto Dto { get; }

        /// <summary>
        /// Source item of this instance.
        /// </summary>
        [XmlIgnore()]
        public object SourceObject => Dto?.SourceElement?.Items[0];

        /// <summary>
        /// Target item of this instance.
        /// </summary>
        [XmlIgnore()]
        public object TargetObject { get; set; }

        /// <summary>
        /// The root source of this instance.
        /// </summary>
        public IStoredDataItem RootSource => throw new global::System.NotImplementedException();

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the DataReference class.
        /// </summary>
        public DataReference()
            : base()
        {
        }

        /// <summary>
        /// Instantiates a new instance of the DataReference class.
        /// </summary>
        /// <param name="dto">The DTO item to consider.</param>
        public DataReference(IDataReferenceDto dto) : base()
        {
            Dto = dto;
        }

        #endregion

        // --------------------------------------------------
        // ACCESSORS
        // --------------------------------------------------

        #region Accessors

        /// <summary>
        /// Gets the initial data source of this instance.
        /// </summary>
        /// <returns>Returns the initial data source of this instance.</returns>
        public IDatasource GetDatasource()
        {
            return null;
        }

        #endregion

        // --------------------------------------------------
        // MUTATORS
        // --------------------------------------------------

        #region Mutators

        /// <summary>
        /// Sets the specified DTO item.
        /// </summary>
        public void SetDto(IDataReferenceDto item)
        {
            _item = item;
        }

        #endregion

        // --------------------------------------------------
        // ACCESSORS
        // --------------------------------------------------

        #region Accessors

        /// <summary>
        /// Gets the items from the source of this instance and update the target items.
        /// </summary>
        /// <param name="scope">The scope to consider.</param>
        /// <param name="scriptVariableSet">The script variable set to use.</param>
        /// <param name="log">The log to consider.</param>
        /// <returns>Returns the retrieved items.</returns>
        public object Get(
            IBdoScope scope = null,
            IBdoScriptVariableSet scriptVariableSet = null,
            IBdoLog log = null)
        {
            //this.SetDefinition((scope== null ? null : scope.BdoExtension));
            //log.AddEvents(this.Check());

            List<object> dataItems = new List<object>();
            //parameterDetail = (parameterDetail ?? new DataElementSet());

            //if (!log.HasErrorsOrExceptions())
            //    if (this.Definition == null)
            //        log.AddError(title: "Definition not found");
            //    else if (this.Definition.RuntimeFunction_Get == null)
            //        log.AddError(title: "Calling function missing");
            //    else if (aSourceElement == null)
            //        log.AddError(title: "Source element missing");
            //    else
            //        dataItems.AddRange(this.Definition.RuntimeFunction_Get(aSourceElement, parameterDetail, scope, scriptVariableSet, log));

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
        public override object Clone()
        {
            DataReference dataReference = base.Clone() as DataReference;
            dataReference.SetDto(Dto.Clone() as DataReferenceDto);

            return dataReference;
        }

        #endregion
    }
}