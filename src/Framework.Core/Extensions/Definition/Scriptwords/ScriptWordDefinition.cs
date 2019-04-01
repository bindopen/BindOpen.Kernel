﻿using System;
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
    public class ScriptWordDefinition : AppExtensionItemDefinition, IScriptWordDefinition
    {
        // ------------------------------------------
        // VARIABLES
        // ------------------------------------------

        #region Variables

        private IDataElementSpecSet _parameterSpecification = null;

        private List<IScriptWordDefinition> _children = null;

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
        public ScriptItemKind Kind { get; set; } = ScriptItemKind.None;

        /// <summary>
        /// Name of the runtime function.
        /// </summary>
        [XmlElement("functionName")]
        public string RuntimeFunctionName { get; set; } = "";

        /// <summary>
        /// Reference unique name of this instance.
        /// </summary>
        [XmlAttribute("referenceUniqueName")]
        public string ReferenceUniqueName { get; set; } = "";

        /// <summary>
        /// The return value type of this instance.
        /// </summary>
        [XmlElement("returnValueType")]
        public DataValueType ReturnValueType { get; set; } = DataValueType.Text;

        /// <summary>
        /// Parameter specification of this instance.
        /// </summary>
        [XmlElement("parameter.specification")]
        public IDataElementSpecSet ParameterSpecification
        {
            get {
                return this._parameterSpecification ?? (this._parameterSpecification = new DataElementSpecSet());
            }
            set {
                this._parameterSpecification = value;
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
        public DataValueType RepeatedParameterValueType { get; set; }

        /// <summary>
        /// Description of parameters of this instance when parameters are repeated.
        /// </summary>
        /// <seealso cref="IsRepeatedParameters"/>
        /// <seealso cref="RepeatedParameterName"/>
        [XmlElement("repeatedParameterDescription")]
        public IDictionaryDataItem RepeatedParameterDescription { get; set; }

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
        public List<IScriptWordDefinition> Children
        {
            get {
                return this._children ?? (this._children = new List<IScriptWordDefinition>());
            }
            set {
                this._children = value;
            }
        }

        /// <summary>
        /// Calling function of this instance.
        /// </summary>
        [XmlIgnore()]
        public ScriptWordFunction RuntimeFunction { get; set; }

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
        public string GetDefaultRuntimeFunctionName()
        {
            String functionName = "";
            switch (this.Kind)
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
        public string GetRepeatedParameterDescriptionText(String variantName = "*", String defaultVariantName = "*")
        {
            if (this.RepeatedParameterDescription == null) return "";
            String label = this.RepeatedParameterDescription.GetContent(variantName);
            if (string.IsNullOrEmpty(label))
                label = this.RepeatedParameterDescription.GetContent(defaultVariantName);
            if (string.IsNullOrEmpty(label))
                label = this.Name;
            return label ?? "";
        }

        /// <summary>
        /// Returns a text summarizing this instance.
        /// </summary>
        /// <param name="logFormat">The log format to consider.</param>
        /// <param name="uiCulture">The UI culture to consider.</param>
        /// <returns>A text summarizing this instance.</returns>
        public override string GetText(LogFormat logFormat= LogFormat.Xml, String uiCulture = "*")
        {
            String st = "";
            switch(logFormat)
            {
                case LogFormat.Xml:
                    st += "<span style='color: blue;' >" + this.Name + "</span> (" + this.Kind.ToString() + ")<br>";
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
