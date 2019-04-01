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
    public class RoutineDefinition : AppExtensionItemDefinition, IRoutineDefinition
    {
        // --------------------------------------------------
        // PROPERTIES
        // --------------------------------------------------

        #region Properties

        /// <summary>
        /// The item class of this instance.
        /// </summary>
        [XmlElement("itemClass")]
        public string ItemClass
        {
            get;
            set;
        }

        /// <summary>
        /// The parameter statement of this instance.
        /// </summary>
        [XmlElement("parameterStatement")]
        public IDataElementSet ParameterStatement { get; set; } = new DataElementSet();

        /// <summary>
        /// The commands of this instance.
        /// </summary>
        [XmlArray("commands")]
        [XmlArrayItem("add")]
        public List<ICommand> Commands { get; set; } = new List<ICommand>();

        /// <summary>
        /// The output result codes of this instance.
        /// </summary>
        [XmlArray("outputResultCodes")]
        [XmlArrayItem("add")]
        public List<IDescribedDataItem> OutputResultCodes { get; set; } = new List<IDescribedDataItem>();

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
