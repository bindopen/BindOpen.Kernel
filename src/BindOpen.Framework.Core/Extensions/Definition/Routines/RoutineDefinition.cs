using BindOpen.Framework.Core.Application.Commands;
using BindOpen.Framework.Core.Data.Elements.Sets;
using BindOpen.Framework.Core.Data.Items;
using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace BindOpen.Framework.Core.Extensions.Definition.Routines
{

    /// <summary>
    /// This class represents a routine definition.
    /// </summary>
    [Serializable()]
    [XmlType("RoutineDefinition", Namespace = "http://meltingsoft.com/bindopen/xsd")]
    [XmlRoot(ElementName = "routine.definition", Namespace = "http://meltingsoft.com/bindopen/xsd", IsNullable = false)]
    public class RoutineDefinition : AppExtensionItemDefinition
    {

        // --------------------------------------------------
        // VARIABLES
        // --------------------------------------------------

        #region Variables

        private DataElementSet _ParameterStatement = new DataElementSet();
        private List<Command> _Commands = new List<Command>();

        private List<DescribedDataItem> _OutputResultCodes = new List<DescribedDataItem>();

        #endregion


        // --------------------------------------------------
        // PROPERTIES
        // --------------------------------------------------

        #region Properties

        /// <summary>
        /// The item class of this instance.
        /// </summary>
        [XmlElement("itemClass")]
        public String ItemClass
        {
            get;
            set;
        }

        /// <summary>
        /// The parameter statement of this instance.
        /// </summary>
        [XmlElement("parameterStatement")]
        public DataElementSet ParameterStatement
        {
            get
            {
                return this._ParameterStatement;
            }
            set { this._ParameterStatement = value; }
        }

        /// <summary>
        /// The commands of this instance.
        /// </summary>
        [XmlArray("commands")]
        [XmlArrayItem("add")]
        public List<Command> Commands
        {
            get
            {
                return this._Commands;
            }
            set { this._Commands = value; }
        }

        /// <summary>
        /// The output result codes of this instance.
        /// </summary>
        [XmlArray("outputResultCodes")]
        [XmlArrayItem("add")]
        public List<DescribedDataItem> OutputResultCodes
        {
            get
            {
                return this._OutputResultCodes;
            }
            set { this._OutputResultCodes = value; }
        }

        #endregion


        // --------------------------------------------------
        // CONSTRUCTORS
        // --------------------------------------------------

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the RoutineDefinition class.
        /// </summary>
        public RoutineDefinition()
        {
        }

        #endregion

    }

}
