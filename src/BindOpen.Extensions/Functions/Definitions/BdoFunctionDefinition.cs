using BindOpen.Data;
using BindOpen.Data.Assemblies;
using System;

namespace BindOpen.Extensions.Functions
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

        /// <summary>
        /// 
        /// </summary>
        public DataValueTypes OutputValueType { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public IBdoClassReference ClassReference { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string RuntimeFunctionName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Delegate RuntimeFunction { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DataValueTypes ParentValueType { get; set; }

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