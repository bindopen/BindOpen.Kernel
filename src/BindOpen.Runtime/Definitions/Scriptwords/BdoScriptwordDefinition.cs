using BindOpen.Data;
using BindOpen.Extensions;
using BindOpen.Extensions.Scripting;

namespace BindOpen.Runtime.Definitions
{
    /// <summary>
    /// This class represents a script word definition.
    /// </summary>
    public class BdoScriptwordDefinition : BdoFunctionDefinition, IBdoScriptwordDefinition
    {
        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// 
        /// </summary>
        public ScriptItemKinds Kind { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DataValueTypes ParentValueType { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public BdoScriptwordDomainedDelegate RuntimeScopedFunction { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public BdoScriptwordDelegate RuntimeBasicFunction { get; set; }

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the ScriptwordDefinition class.
        /// </summary>
        /// <param key="name">The name to consider.</param>
        /// <param key="extensionDefinition">The extensition definition to consider.</param>
        public BdoScriptwordDefinition(
            string name,
            IBdoPackageDefinition extensionDefinition)
            : base(name, extensionDefinition, "scriptwordDef_")
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
            return UniqueName;
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

        #endregion

        // ------------------------------------------
        // IDISPOSABLE METHODS
        // ------------------------------------------

        #region IDisposable_Methods

        private bool _isDisposed = false;

        /// <summary>
        /// Disposes this instance. 
        /// </summary>
        /// <param key="isDisposing">Indicates whether this instance is disposing</param>
        protected override void Dispose(bool isDisposing)
        {
            if (_isDisposed)
            {
                return;
            }

            _isDisposed = true;

            base.Dispose(isDisposing);
        }

        #endregion
    }
}
