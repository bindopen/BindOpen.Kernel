using BindOpen.Framework.Core.Data.Elements;
using BindOpen.Framework.Core.Data.Elements._Object;
using BindOpen.Framework.Core.Data.Elements.Carrier;
using BindOpen.Framework.Core.Data.Elements.Document;
using BindOpen.Framework.Core.Data.Elements.Scalar;
using BindOpen.Framework.Core.Data.Elements.Sets;
using BindOpen.Framework.Core.Data.Elements.Source;
using System;
using System.Xml.Serialization;

namespace BindOpen.Framework.Core.Extensions.Definition.Items
{
    /// <summary>
    /// This class represents a handler definition.
    /// </summary>
    [Serializable()]
    [XmlType("BdoHandlerDefinition", Namespace = "https://bindopen.org/xsd")]
    [XmlRoot(ElementName = "dataHandler.definition", Namespace = "https://bindopen.org/xsd", IsNullable = false)]
    public class BdoHandlerDefinitionDto : BdoExtensionItemDefinitionDto, IBdoHandlerDefinitionDto
    {
        // --------------------------------------------------
        // PROPERTIES
        // --------------------------------------------------

        #region Properties

        /// <summary>
        /// The calling class of this instance.
        /// </summary>
        [XmlElement("callingClass")]
        public string CallingClass
        {
            get;
            set;
        }

        /// <summary>
        /// Name of the GET function.
        /// </summary>
        [XmlElement("getFunction")]
        public string GetFunctionName { get; set; } = "Get";

        /// <summary>
        /// Name of the POST function.
        /// </summary>
        [XmlElement("postFunction")]
        public string PostFunctionName { get; set; } = "Post";

        /// <summary>
        /// The source specification of this instance.
        /// </summary>
        [XmlElement("source-carrier.specification", typeof(CarrierElementSpec))]
        [XmlElement("source-document.specification", typeof(DocumentElementSpec))]
        [XmlElement("source-object.specification", typeof(ObjectElementSpec))]
        [XmlElement("source-scalar.specification", typeof(ScalarElementSpec))]
        [XmlElement("source-datasource.specification", typeof(SourceElementSpec))]
        public DataElementSpec SourceSpecification { get; set; } = null;

        /// <summary>
        /// The parameter specification of this instance.
        /// </summary>
        [XmlElement("parameter.specification")]
        public DataElementSpecSet ParameterSpecification { get; set; } = new DataElementSpecSet();

        /// <summary>
        /// The target specification of this instance.
        /// </summary>
        [XmlElement("target-carrier.specification", typeof(CarrierElementSpec))]
        [XmlElement("target-document.specification", typeof(DocumentElementSpec))]
        [XmlElement("target-object.specification", typeof(ObjectElementSpec))]
        [XmlElement("target-scalar.specification", typeof(ScalarElementSpec))]
        [XmlElement("target-datasource.specification", typeof(SourceElementSpec))]
        public DataElementSpec TargetSpecification { get; set; } = null;

        #endregion

        // --------------------------------------------------
        // CONSTRUCTORS
        // --------------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the HandlerDefinition class.
        /// </summary>
        public BdoHandlerDefinitionDto()
        {
        }

        #endregion
    }
}
