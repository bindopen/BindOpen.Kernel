using BindOpen.System.Data;
using BindOpen.System.Data.Meta;
using System;

namespace BindOpen.System.Scoping
{
    /// <summary>
    /// This class represents a function definition.
    /// </summary>
    public class BdoFunctionDefinition : BdoExtensionDefinition, IBdoFunctionDefinition
    {
        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        public ITBdoSet<IBdoSpec> AdditionalSpecs { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public IBdoDataType ParentDataType { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public IBdoDataType OutputDataType { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Type RuntimeClassType { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Delegate RuntimeFunction { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public BdoFunctionDomainedDelegate RuntimeScopedFunction { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public BdoFunctionDelegate RuntimeBasicFunction { get; set; }

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the FunctionDefinition class. 
        /// </summary>
        /// <param key="name">The name to consider.</param>
        /// <param key="extensionDefinition">The extensition definition to consider.</param>
        public BdoFunctionDefinition(
            string name,
            IBdoPackageDefinition extensionDefinition,
            string namePreffix = "functionDef_")
            : base(name, namePreffix, extensionDefinition)
        {
        }

        #endregion
    }
}