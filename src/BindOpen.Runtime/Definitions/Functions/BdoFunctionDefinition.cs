using BindOpen.Data;
using BindOpen.Data.Assemblies;
using BindOpen.Data.Meta;
using System;

namespace BindOpen.Runtime.Definitions
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
        public IBdoSpecSet SpecDetail { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string RuntimeFunctionName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Delegate RuntimeFunction { get; set; }

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