using BindOpen.Framework.Core.Data.Elements;
using BindOpen.Framework.Core.Data.Items;
using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace BindOpen.Framework.Core.Extensions.Definition.Items
{
    /// <summary>
    /// This class represents a routine definition.
    /// </summary>
    [XmlType("BdoRoutineDefinitionDto", Namespace = "https://bindopen.org/xsd")]
    [XmlRoot(ElementName = "routine.definition", Namespace = "https://bindopen.org/xsd", IsNullable = false)]
    public class BdoRoutineDefinitionDto : BdoExtensionItemDefinitionDto, IBdoRoutineDefinitionDto
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
        public DataElementSet ParameterStatement { get; set; } = new DataElementSet();

        ///// <summary>
        ///// The commands of this instance.
        ///// </summary>
        //[XmlArray("commands")]
        //[XmlArrayItem("add")]
        //public List<Command> Commands { get; set; } = new List<Command>();

        /// <summary>
        /// The output result codes of this instance.
        /// </summary>
        [XmlArray("outputResultCodes")]
        [XmlArrayItem("add")]
        public List<DescribedDataItem> OutputResultCodes { get; set; } = new List<DescribedDataItem>();

        /// <summary>
        /// The runtime type of this instance.
        /// </summary>
        public Type RuntimeType { get; set; }


        #endregion

        // --------------------------------------------------
        // CONSTRUCTORS
        // --------------------------------------------------

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the RoutineDefinition class.
        /// </summary>
        public BdoRoutineDefinitionDto()
        {
        }

        #endregion
    }
}
