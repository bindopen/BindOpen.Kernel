using BindOpen.Framework.Core.Data.Elements;
using BindOpen.Framework.Core.Data.Elements.Carrier;
using BindOpen.Framework.Core.Data.Elements.Document;
using BindOpen.Framework.Core.Data.Elements.Entity;
using BindOpen.Framework.Core.Data.Elements.Scalar;
using BindOpen.Framework.Core.Data.Elements.Sets;
using BindOpen.Framework.Core.Data.Elements.Source;
using BindOpen.Framework.Core.Extensions.Runtime.Handlers;
using System;
using System.Xml.Serialization;

namespace BindOpen.Framework.Core.Extensions.Definition.Handlers
{

    /// <summary>
    /// This class represents a handler definition.
    /// </summary>
    [Serializable()]
    [XmlType("HandlerDefinition", Namespace = "http://meltingsoft.com/bindopen/xsd")]
    [XmlRoot(ElementName = "dataHandler.definition", Namespace = "http://meltingsoft.com/bindopen/xsd", IsNullable = false)]
    public class HandlerDefinition : AppExtensionItemDefinition
    {

        // --------------------------------------------------
        // VARIABLES
        // --------------------------------------------------

        #region Variables

        private DataElementSpec _SourceSpecification = null;
        private DataElementSpecSet _ParameterSpecification = new DataElementSpecSet();
        private DataElementSpec _TargetSpecification = null;

        private String _GetFunctionName = "Get";
        private String _PostFunctionName = "Post";

        #endregion


        // --------------------------------------------------
        // PROPERTIES
        // --------------------------------------------------

        #region Properties

        /// <summary>
        /// The calling class of this instance.
        /// </summary>
        [XmlElement("callingClass")]
        public String CallingClass
        {
            get;
            set;
        }

        /// <summary>
        /// Name of the GET function.
        /// </summary>
        [XmlElement("getFunctionName")]
        public String GetFunctionName
        {
            get { return this._GetFunctionName; }
            set { this._GetFunctionName = value; }
        }

        /// <summary>
        /// Name of the POST function.
        /// </summary>
        [XmlElement("postFunctionName")]
        public String PostFunctionName
        {
            get { return this._PostFunctionName; }
            set { this._PostFunctionName = value; }
        }

        /// <summary>
        /// The source specification of this instance.
        /// </summary>
        [XmlElement("source-carrier.specification", typeof(CarrierElementSpec))]
        [XmlElement("source-document.specification", typeof(DocumentElementSpec))]
        [XmlElement("source-entity.specification", typeof(EntityElementSpec))]
        [XmlElement("source-scalar.specification", typeof(ScalarElementSpec))]
        [XmlElement("source-datasource.specification", typeof(SourceElementSpec))]
        public DataElementSpec SourceSpecification
        {
            get { return this._SourceSpecification; }
            set { this._SourceSpecification = value; }
        }

        /// <summary>
        /// The parameter specification of this instance.
        /// </summary>
        [XmlElement("parameter.specification")]
        public DataElementSpecSet ParameterSpecification
        {
            get { return this._ParameterSpecification; }
            set { this._ParameterSpecification = value; }
        }

        /// <summary>
        /// The target specification of this instance.
        /// </summary>
        [XmlElement("target-carrier.specification", typeof(CarrierElementSpec))]
        [XmlElement("target-document.specification", typeof(DocumentElementSpec))]
        [XmlElement("target-entity.specification", typeof(EntityElementSpec))]
        [XmlElement("target-scalar.specification", typeof(ScalarElementSpec))]
        [XmlElement("target-datasource.specification", typeof(SourceElementSpec))]
        public DataElementSpec TargetSpecification
        {
            get { return this._TargetSpecification; }
            set { this._TargetSpecification = value; }
        }

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
