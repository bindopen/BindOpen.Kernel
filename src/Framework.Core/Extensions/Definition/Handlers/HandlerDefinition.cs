using System;
using System.Xml.Serialization;
using BindOpen.Framework.Core.Data.Elements;
using BindOpen.Framework.Core.Data.Elements.Carrier;
using BindOpen.Framework.Core.Data.Elements.Document;
using BindOpen.Framework.Core.Data.Elements.Entity;
using BindOpen.Framework.Core.Data.Elements.Scalar;
using BindOpen.Framework.Core.Data.Elements.Sets;
using BindOpen.Framework.Core.Data.Elements.Source;
using BindOpen.Framework.Core.Extensions.Runtime.Handlers;

namespace BindOpen.Framework.Core.Extensions.Definition.Handlers
{
    /// <summary>
    /// This class represents a handler definition.
    /// </summary>
    [Serializable()]
    [XmlType("HandlerDefinition", Namespace = "http://meltingsoft.com/bindopen/xsd")]
    [XmlRoot(ElementName = "dataHandler.definition", Namespace = "http://meltingsoft.com/bindopen/xsd", IsNullable = false)]
    public class HandlerDefinition : AppExtensionItemDefinition, IHandlerDefinition
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
        [XmlElement("getFunctionName")]
        public string GetFunctionName { get; set; } = "Get";

        /// <summary>
        /// Name of the POST function.
        /// </summary>
        [XmlElement("postFunctionName")]
        public string PostFunctionName { get; set; } = "Post";

        /// <summary>
        /// The source specification of this instance.
        /// </summary>
        [XmlElement("source-carrier.specification", typeof(CarrierElementSpec))]
        [XmlElement("source-document.specification", typeof(DocumentElementSpec))]
        [XmlElement("source-entity.specification", typeof(EntityElementSpec))]
        [XmlElement("source-scalar.specification", typeof(ScalarElementSpec))]
        [XmlElement("source-datasource.specification", typeof(SourceElementSpec))]
        public IDataElementSpec SourceSpecification { get; set; } = null;

        /// <summary>
        /// The parameter specification of this instance.
        /// </summary>
        [XmlElement("parameter.specification")]
        public IDataElementSpecSet ParameterSpecification { get; set; } = new DataElementSpecSet();

        /// <summary>
        /// The target specification of this instance.
        /// </summary>
        [XmlElement("target-carrier.specification", typeof(CarrierElementSpec))]
        [XmlElement("target-document.specification", typeof(DocumentElementSpec))]
        [XmlElement("target-entity.specification", typeof(EntityElementSpec))]
        [XmlElement("target-scalar.specification", typeof(ScalarElementSpec))]
        [XmlElement("target-datasource.specification", typeof(SourceElementSpec))]
        public IDataElementSpec TargetSpecification { get; set; } = null;

        /// <summary>
        /// Runtime GET function of this instance.
        /// </summary>
        [XmlIgnore()]
        public HandlerGetFunction RuntimeFunction_Get;

        /// <summary>
        /// Runtime POST function of this instance.
        /// </summary>
        [XmlIgnore()]
        public HandlerPostFunction RuntimeFunction_Post;

        #endregion

        // --------------------------------------------------
        // CONSTRUCTORS
        // --------------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the HandlerDefinition class.
        /// </summary>
        public HandlerDefinition()
        {
        }

        #endregion
    }
}
