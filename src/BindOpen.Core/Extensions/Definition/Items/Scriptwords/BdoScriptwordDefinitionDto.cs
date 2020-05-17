using BindOpen.Data.Common;
using BindOpen.Data.Elements;
using BindOpen.Data.Items;
using BindOpen.System.Diagnostics.Loggers;
using BindOpen.System.Scripting;
using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace BindOpen.Extensions.Definition
{
    /// <summary>
    /// This class represents a script word definition.
    /// </summary>
    [XmlType("BdoScriptwordDefinitionDto", Namespace = "https://docs.bindopen.org/xsd")]
    [XmlRoot(ElementName = "scriptWord.definition", Namespace = "https://docs.bindopen.org/xsd", IsNullable = false)]
    public class BdoScriptwordDefinitionDto : BdoExtensionItemDefinitionDto, IBdoScriptwordDefinitionDto
    {
        // ------------------------------------------
        // VARIABLES
        // ------------------------------------------

        #region Variables

        private DataElementSpecSet _parameterSpecification = null;

        private List<BdoScriptwordDefinitionDto> _children = null;

        #endregion

        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

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
        /// Kind of this instance.
        /// </summary>
        [XmlElement("kind")]
        public ScriptItemKinds Kind { get; set; } = ScriptItemKinds.None;

        /// <summary>
        /// Name of the runtime function.
        /// </summary>
        [XmlElement("functionName")]
        public string RuntimeFunctionName { get; set; } = "";

        /// <summary>
        /// Reference unique ID of this instance.
        /// </summary>
        [XmlAttribute("referenceUniqueName")]
        public string ReferenceUniqueName { get; set; } = "";

        /// <summary>
        /// The return value type of this instance.
        /// </summary>
        [XmlElement("returnValueType")]
        public DataValueTypes ReturnValueType { get; set; } = DataValueTypes.Text;

        /// <summary>
        /// Parameter specification of this instance.
        /// </summary>
        [XmlElement("parameter.specification")]
        public DataElementSpecSet ParameterSpecification
        {
            get
            {
                return _parameterSpecification ?? (_parameterSpecification = new DataElementSpecSet());
            }
            set
            {
                _parameterSpecification = value;
            }
        }

        /// <summary>
        /// Indicates whether this instance has unlimited parameters. If true, parameters have 
        /// the same value type.
        /// </summary>
        /// <seealso cref="RepeatedParameterValueType"/>
        /// <seealso cref="RepeatedParameterName"/>
        [XmlElement("isRepeatedParameters")]
        public bool IsRepeatedParameters { get; set; } = false;

        /// <summary>
        /// Value type of parameters of this instance when parameters are repeated.
        /// </summary>
        /// <seealso cref="IsRepeatedParameters"/>
        /// <seealso cref="RepeatedParameterName"/>
        [XmlElement("repeatedParameterValueType")]
        public DataValueTypes RepeatedParameterValueType { get; set; }

        /// <summary>
        /// Description of parameters of this instance when parameters are repeated.
        /// </summary>
        /// <seealso cref="IsRepeatedParameters"/>
        /// <seealso cref="RepeatedParameterName"/>
        [XmlElement("repeatedParameterDescription")]
        public DictionaryDataItem RepeatedParameterDescription { get; set; }

        /// <summary>
        /// Name of parameters of this instance when parameters are repeated.
        /// </summary>
        /// <seealso cref="IsRepeatedParameters"/>
        /// <seealso cref="RepeatedParameterValueType"/>
        [XmlElement("repeatedParameterName")]
        public string RepeatedParameterName { get; set; }

        /// <summary>
        /// Maximum number of parameters of this instance.
        /// </summary>
        [XmlElement("maxParameterNumber")]
        public int MaxParameterNumber { get; set; } = -1;

        /// <summary>
        /// Minimum number of parameters of this instance.
        /// </summary>
        [XmlElement("minParameterNumber")]
        public int MinParameterNumber { get; set; } = -1;

        /// <summary>
        /// The definitions of the sub words of this instance.
        /// </summary>
        [XmlArray("children")]
        [XmlArrayItem("add.definition")]
        public List<BdoScriptwordDefinitionDto> Children
        {
            get
            {
                return _children ?? (_children = new List<BdoScriptwordDefinitionDto>());
            }
            set
            {
                _children = value;
            }
        }

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the ScriptwordDefinition class.
        /// </summary>
        public BdoScriptwordDefinitionDto()
        {
        }

        #endregion

        // ------------------------------------------
        // ACCESSORS
        // ------------------------------------------

        #region Accessors

        /// <summary>
        /// Returns the default runtime function name.
        /// </summary>
        public string GetDefaultRuntimeFunctionName()
        {
            String functionName = "";
            switch (Kind)
            {
                case ScriptItemKinds.Function:
                    functionName += "Fun_";
                    break;
                case ScriptItemKinds.Variable:
                    functionName += "Var_";
                    break;
            };
            functionName += Name;

            return functionName;
        }

        /// <summary>
        /// Returns the repeated parameter description text.
        /// </summary>
        /// <param name="variantName">The variant variant name to consider.</param>
        /// <param name="defaultVariantName">The default variant name to consider.</param>
        public string GetRepeatedParameterDescriptionText(String variantName = "*", String defaultVariantName = "*")
        {
            if (RepeatedParameterDescription == null) return "";
            String label = RepeatedParameterDescription.GetContent(variantName);
            if (string.IsNullOrEmpty(label))
                label = RepeatedParameterDescription.GetContent(defaultVariantName);
            if (string.IsNullOrEmpty(label))
                label = Name;
            return label ?? "";
        }

        /// <summary>
        /// Returns a text summarizing this instance.
        /// </summary>
        /// <param name="logFormat">The log format to consider.</param>
        /// <param name="uiCulture">The UI culture to consider.</param>
        /// <returns>A text summarizing this instance.</returns>
        public override string GetText(BdoDefaultLoggerFormat logFormat = BdoDefaultLoggerFormat.Xml, String uiCulture = "*")
        {
            String st = "";
            switch (logFormat)
            {
                case BdoDefaultLoggerFormat.Xml:
                    st += "<span style='color: blue;' >" + Name + "</span> (" + Kind.ToString() + ")<br>";
                    st += "<br>";
                    st += "Modified: " + LastModificationDate + "<br>";
                    st += "<br>";
                    st += Description.GetContent(uiCulture);
                    st += "<br>";
                    st += "<strong>Library: " + LibraryId + "</strong>";
                    st += "<br>";
                    st += "<strong>Syntax</strong>";
                    st += "<br>";

                    string parameterString = "";
                    if (IsRepeatedParameters)
                        parameterString +=
                            "<span style='color: blue;'>&lt;" + RepeatedParameterValueType.ToString() + "&gt;</span> parameter1 ... <Min: " + MinParameterNumber.ToString() + ";Max: " + MaxParameterNumber.ToString() + ">";
                    else
                        foreach (DataElementSpec elementSpecification in ParameterSpecification.Items)
                            parameterString += (string.IsNullOrEmpty(parameterString) ? "" : ",") +
                                "<span style='color: blue;'>&lt;" + elementSpecification.ValueType.ToString() + "&gt;</span> " + elementSpecification.Name + ",";
                    st += Name + "(" + parameterString + ")";

                    break;
            }

            return st;
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

            _parameterSpecification?.Dispose();

            _isDisposed = true;

            base.Dispose(isDisposing);
        }

        #endregion
    }
}
