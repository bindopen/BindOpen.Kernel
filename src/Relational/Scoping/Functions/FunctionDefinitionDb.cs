using BindOpen.Data;
using BindOpen.Data.Meta;
using BindOpen.Scoping.Script;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace BindOpen.Scoping.Functions
{
    /// <summary>
    /// This class represents a function definition database entity.
    /// </summary>
    public class FunctionDefinitionDb : ExtensionDefinitionDb
    {
        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// The calling class of this instance.
        /// </summary>
        public string CallingClass { get; set; }

        /// <summary>
        /// The kind of this instance.
        /// </summary>
        public ScriptTokenKinds Kind { get; set; } = ScriptTokenKinds.None;

        /// <summary>
        /// The maximum number of parameters of this instance.
        /// </summary>
        public int MaxParameterNumber { get; set; } = -1;

        /// <summary>
        /// The minimum number of parameters of this instance.
        /// </summary>
        public int MinParameterNumber { get; set; } = -1;

        /// <summary>
        /// The name of repeated parameters of this instance.
        /// </summary>
        /// <seealso cref="IsRepeatedParameters"/>
        /// <seealso cref="RepeatedParameterValueType"/>
        public string RepeatedParameterName { get; set; }

        /// <summary>
        /// The value type of repeated parameters of this instance.
        /// </summary>
        /// <seealso cref="IsRepeatedParameters"/>
        /// <seealso cref="RepeatedParameterName"/>
        public DataValueTypes RepeatedParameterValueType { get; set; }

        /// <summary>
        /// The description of repeated parameters of this instance.
        /// </summary>
        /// <seealso cref="IsRepeatedParameters"/>
        /// <seealso cref="RepeatedParameterName"/>
        [ForeignKey(nameof(RepeatedParameterDescriptionId))]
        public StringDictionaryDb RepeatedParameterDescription { get; set; }

        /// <summary>
        /// The class name of this instance.
        /// </summary>
        public string RepeatedParameterDescriptionId { get; set; }

        /// <summary>
        /// The rference unique name of this instance.
        /// </summary>
        public string ReferenceUniqueName { get; set; }

        /// <summary>
        /// The return value type of this instance.
        /// </summary>
        public DataValueTypes ReturnValueType { get; set; } = DataValueTypes.Text;

        /// <summary>
        /// The name of the runtime function.
        /// </summary>
        public string RuntimeFunctionName { get; set; }

        /// <summary>
        /// The children of this instance.
        /// </summary>
        [ForeignKey("RepeatedParameterDescriptionId")]
        public new List<FunctionDefinitionDb> Children { get; set; }

        /// <summary>
        /// The parameters of this instance.
        /// </summary>
        public List<MetaDataDb> ParameterSpecification { get; set; }

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the FunctionDefinitionDb class.
        /// </summary>
        public FunctionDefinitionDb()
        {
        }

        #endregion
    }
}
