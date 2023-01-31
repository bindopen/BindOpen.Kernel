using BindOpen.Data;
using BindOpen.Data.Items;
using BindOpen.Extensions.Scripting;
using System;
using BindOpen.Data.Meta;
using BindOpen.Data;

namespace BindOpen.Runtime.Definition
{
    /// <summary>
    /// This class represents a script word definition.
    /// </summary>
    public class BdoScriptwordDefinition : BdoExtensionItemDefinition, IBdoScriptwordDefinition
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
        /// The input value type of this instance.
        /// </summary>
        public DataValueTypes InputValueType { get; set; } = DataValueTypes.Text;

        /// <summary>
        /// Indicates whether this instance has unlimited parameters. If true, parameters have 
        /// the same value type.
        /// </summary>
        /// <seealso cref="RepeatedParameterValueType"/>
        /// <seealso cref="RepeatedParameterName"/>
        public bool IsRepeatedParameters { get; set; } = false;

        /// <summary>
        /// Kind of this instance.
        /// </summary>
        public ScriptItemKinds Kind { get; set; } = ScriptItemKinds.None;

        /// <summary>
        /// Maximum number of parameters of this instance.
        /// </summary>
        public int MaxParameterNumber { get; set; } = -1;

        /// <summary>
        /// Minimum number of parameters of this instance.
        /// </summary>
        public int MinParameterNumber { get; set; } = -1;

        /// <summary>
        /// Parameter specification of this instance.
        /// </summary>
        public IBdoMetaSpecSet ParameterSpecification { get; set; }

        /// <summary>
        /// Description of parameters of this instance when parameters are repeated.
        /// </summary>
        public IBdoDictionary RepeatedParameterDescription { get; set; }

        /// <summary>
        /// Name of parameters of this instance when parameters are repeated.
        /// </summary>
        /// <seealso cref="IsRepeatedParameters"/>
        /// <seealso cref="RepeatedParameterValueType"/>
        public string RepeatedParameterName { get; set; }

        /// <summary>
        /// Value type of parameters of this instance when parameters are repeated.
        /// </summary>
        /// <seealso cref="IsRepeatedParameters"/>
        /// <seealso cref="RepeatedParameterName"/>
        public DataValueTypes RepeatedParameterValueType { get; set; }

        /// <summary>
        /// The output value type of this instance.
        /// </summary>
        public DataValueTypes OutputValueType { get; set; } = DataValueTypes.Text;

        /// <summary>
        /// The runtime basic function of this instance.
        /// </summary>
        public BdoScriptwordDelegate RuntimeBasicFunction { get; set; }

        /// <summary>
        /// Name of the runtime function.
        /// </summary>
        public string RuntimeFunctionName { get; set; }

        /// <summary>
        /// The runtime scoped function of this instance.
        /// </summary>
        public BdoScriptwordDomainedDelegate RuntimeScopedFunction { get; set; }

        /// <summary>
        /// The runtime type of this instance.
        /// </summary>
        public Type RuntimeType { get; set; }

        /// <summary>
        /// The unique ID of this instance.
        /// </summary>
        public new string UniqueId { get => ExtensionDefinition?.UniqueId + "$" + Name; }

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the ScriptwordDefinition class.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="extensionDefinition">The extensition definition to consider.</param>
        public BdoScriptwordDefinition(
            string name,
            IBdoExtensionDefinition extensionDefinition) : base(name, "scriptWordDef_", extensionDefinition)
        {
        }

        #endregion

        // ------------------------------------------
        // ACCESSORS
        // ------------------------------------------

        #region Accessors

        /// <summary>
        /// Returns the key of this instance.
        /// </summary>
        /// <returns></returns>
        public override string Key()
        {
            return UniqueId;
        }

        /// <summary>
        /// Returns the default runtime function name.
        /// </summary>
        public string GetDefaultRuntimeFunctionName()
        {
            var functionName = string.Empty;
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
        /// <param name="key">The key to consider.</param>
        /// <param name="defaultKey">The default variant name to consider.</param>
        public string GetRepeatedParameterDescriptionText(String key = StringHelper.__Star, String defaultKey = StringHelper.__Star)
        {
            return RepeatedParameterDescription?[key, defaultKey];
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

            ParameterSpecification?.Dispose();
            RepeatedParameterDescription?.Dispose();

            _isDisposed = true;

            base.Dispose(isDisposing);
        }

        #endregion
    }
}
