using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using BindOpen.Framework.Core.Data.Common;
using BindOpen.Framework.Core.Data.Elements;
using BindOpen.Framework.Core.Data.Elements.Sets;
using BindOpen.Framework.Core.Data.Items.Dictionary;
using BindOpen.Framework.Core.Extensions.Runtime.Scriptwords;
using BindOpen.Framework.Core.System.Diagnostics.Loggers;
using BindOpen.Framework.Core.System.Scripting;

namespace BindOpen.Framework.Core.Extensions.Definition.Scriptwords
{
    /// <summary>
    /// This class represents a script word definition.
    /// </summary>
    [Serializable()]
    [XmlType("ScriptWordDefinition", Namespace = "http://meltingsoft.com/bindopen/xsd")]
    [XmlRoot(ElementName = "scriptWord.definition", Namespace = "http://meltingsoft.com/bindopen/xsd", IsNullable = false)]
    public class ScriptWordDefinition : AppExtensionItemDefinition
    {

        // ------------------------------------------
        // VARIABLES
        // ------------------------------------------

        #region Variables

        private String _ReferenceUniqueName="";
        //private Boolean _IsDynamic = true;
        private DataElementSpecSet _ParameterSpecification = null;
        private ScriptItemKind _Kind = ScriptItemKind.None;
        private String _RuntimeFunctionName = "";

        private DataValueType _ReturnValueType = DataValueType.Text;

        private Boolean _IsRepeatedParameters = false;
        private int _MaxRepeatedParameterNumber = -1;
        private int _MinRepeatedParameterNumber = -1;
        private String _RepeatedParameterName;
        private DataValueType _RepeatedParameterValueType;
        private DictionaryDataItem _RepeatedParameterDescription;

        private List<ScriptWordDefinition> _Children = null;

        #endregion


        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

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
        /// Kind of this instance.
        /// </summary>
        [XmlElement("kind")]
        public ScriptItemKind Kind
        {
            get { return this._Kind; }
            set { this._Kind = value; }
        }

        /// <summary>
        /// Name of the runtime function.
        /// </summary>
        [XmlElement("functionName")]
        public String RuntimeFunctionName
        {
            get { return this._RuntimeFunctionName; }
            set { this._RuntimeFunctionName = value; }
        }

        /// <summary>
        /// Reference unique name of this instance.
        /// </summary>
        [XmlAttribute("referenceUniqueName")]
        public String ReferenceUniqueName
        {
            get { return this._ReferenceUniqueName; }
            set { this._ReferenceUniqueName = value; }
        }

        /// <summary>
        /// The return value type of this instance.
        /// </summary>
        [XmlElement("returnValueType")]
        public DataValueType ReturnValueType
        {
            get { return this._ReturnValueType; }
            set { this._ReturnValueType = value; }
        }

        /// <summary>
        /// Parameter specification of this instance.
        /// </summary>
        [XmlElement("parameter.specification")]
        public DataElementSpecSet ParameterSpecification
        {
            get {
                if (this._ParameterSpecification == null) this._ParameterSpecification = new DataElementSpecSet();
                return this._ParameterSpecification;
            }
            set {
                this._ParameterSpecification = value;
            }
        }

        /// <summary>
        /// Indicates whether this instance has unlimited parameters. If true, parameters have 
        /// the same value type.
        /// </summary>
        /// <seealso cref="RepeatedParameterValueType"/>
        /// <seealso cref="RepeatedParameterName"/>
        [XmlElement("isRepeatedParameters")]
        public Boolean IsRepeatedParameters
        {
            get { return this._IsRepeatedParameters; }
            set { this._IsRepeatedParameters = value; }
        }

        /// <summary>
        /// Value type of parameters of this instance when parameters are repeated.
        /// </summary>
        /// <seealso cref="IsRepeatedParameters"/>
        /// <seealso cref="RepeatedParameterName"/>
        [XmlElement("repeatedParameterValueType")]
        public DataValueType RepeatedParameterValueType
        {
            get { return this._RepeatedParameterValueType; }
            set { this._RepeatedParameterValueType = value; }
        }

        /// <summary>
        /// Description of parameters of this instance when parameters are repeated.
        /// </summary>
        /// <seealso cref="IsRepeatedParameters"/>
        /// <seealso cref="RepeatedParameterName"/>
        [XmlElement("repeatedParameterDescription")]
        public DictionaryDataItem RepeatedParameterDescription
        {
            get { return this._RepeatedParameterDescription; }
            set { this._RepeatedParameterDescription = value; }
        }

        /// <summary>
        /// Name of parameters of this instance when parameters are repeated.
        /// </summary>
        /// <seealso cref="IsRepeatedParameters"/>
        /// <seealso cref="RepeatedParameterValueType"/>
        [XmlElement("repeatedParameterName")]
        public String RepeatedParameterName
        {
            get { return this._RepeatedParameterName; }
            set { this._RepeatedParameterName = value; }
        }

        /// <summary>
        /// Maximum number of parameters of this instance.
        /// </summary>
        [XmlElement("maxParameterNumber")]
        public int MaxParameterNumber
        {
            get { return this._MaxRepeatedParameterNumber; }
            set { this._MaxRepeatedParameterNumber = value; }
        }

        /// <summary>
        /// Minimum number of parameters of this instance.
        /// </summary>
        [XmlElement("minParameterNumber")]
        public int MinParameterNumber
        {
            get { return this._MinRepeatedParameterNumber; }
            set { this._MinRepeatedParameterNumber = value; }
        }

        /// <summary>
        /// The definitions of the sub words of this instance.
        /// </summary>
        [XmlArray("children")]
        [XmlArrayItem("add.definition")]
        public List<ScriptWordDefinition> Children
        {
            get {
                if (this._Children == null) this._Children = new List<ScriptWordDefinition>();
                return this._Children;
            }
            set {
                this._Children = value;
            }
        }

        /// <summary>
        /// Calling function of this instance.
        /// </summary>
        [XmlIgnore()]
        public ScriptWordFunction RuntimeFunction;

        #endregion


        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the ScriptWordDefinition class.
        /// </summary>
        public ScriptWordDefinition()
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
        public String GetDefaultRuntimeFunctionName()
        {
            String functionName = "";
            switch (this._Kind)
            {
                case ScriptItemKind.Function:
                    functionName += "Fun_";
                    break;
                case ScriptItemKind.Variable:
                    functionName += "Var_";
                    break;
            };
            functionName += this.Name;

            return functionName;
        }

        /// <summary>
        /// Returns the repeated parameter description text.
        /// </summary>
        /// <param name="variantName">The variant variant name to consider.</param>
        /// <param name="defaultVariantName">The default variant name to consider.</param>
        public String GetRepeatedParameterDescriptionText(String variantName = "*", String defaultVariantName = "*")
        {
            if (this._RepeatedParameterDescription == null) return "";
            String label = this._RepeatedParameterDescription.GetContent(variantName);
            if (String.IsNullOrEmpty(label))
                label = this._RepeatedParameterDescription.GetContent(defaultVariantName);
            if (String.IsNullOrEmpty(label))
                label = this.Name;
            return label ?? "";
        }

        /// <summary>
        /// Returns a text summarizing this instance.
        /// </summary>
        /// <param name="logFormat">The log format to consider.</param>
        /// <param name="uiCulture">The UI culture to consider.</param>
        /// <returns>A text summarizing this instance.</returns>
        public override String GetText(LogFormat logFormat= LogFormat.Xml, String uiCulture = "*")
        {
            String st = "";
            switch(logFormat)
            {
                case LogFormat.Xml:
                    st += "<span style='color: blue;' >" + this.Name + "</span> (" + this._Kind.ToString() + ")<br>";
                    st += "<br>";
                    st += "Modified: " + this.LastModificationDate + "<br>";
                    st += "<br>";
                    st += this.Description.GetContent(uiCulture);
                    st += "<br>";
                    st += "<strong>Library: " + this.LibraryName + "</strong>";
                    st += "<br>";
                    st += "<strong>Syntax</strong>";
                    st += "<br>";

                    String parameterString = "";
                    if (this.IsRepeatedParameters)
                        parameterString +=
                            "<span style='color: blue;'>&lt;" + this.RepeatedParameterValueType.ToString() + "&gt;</span> parameter1 ... <Min: " + this.MinParameterNumber.ToString() + ";Max: " + this.MaxParameterNumber.ToString() + ">";
                    else
                        foreach (DataElementSpec elementSpecification in this.ParameterSpecification.Items)
                            parameterString += (parameterString == String.Empty ? "" : ",") +
                                "<span style='color: blue;'>&lt;" + elementSpecification.ValueType.ToString() + "&gt;</span> " + elementSpecification.Name + ",";
                    st += this.Name + "(" + parameterString + ")";

                    break;
            }

            return st;
        }

        #endregion
    
    }
}
