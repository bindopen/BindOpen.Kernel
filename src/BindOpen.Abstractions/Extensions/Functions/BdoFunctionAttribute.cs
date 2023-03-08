using BindOpen.Data;
using BindOpen.Scripting;
using System;

namespace BindOpen.Extensions.Functions
{
    /// <summary>
    /// This class represents a script word attribute.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Property, AllowMultiple = true)]
    public class BdoFunctionAttribute : MetaExtensionAttribute
    {
        // --------------------------------------------------
        // PROPERTIES
        // --------------------------------------------------

        #region Properties

        /// <summary>
        /// The king of this instance.
        /// </summary>
        public ScriptItemKinds Kind { get; set; } = ScriptItemKinds.None;

        // Repeated parameters

        /// <summary>
        /// The name of repeated parameters of this instance.
        /// </summary>
        public string RepeatedParameterName { get; set; }

        /// <summary>
        /// The value type of repeated parameters of this instance.
        /// </summary>
        public DataValueTypes RepeatedParameterValueType { get; set; } = DataValueTypes.Object;

        // Parameter 1

        /// <summary>
        /// The name of parameter 1 of this instance.
        /// </summary>
        public string Parameter1Name { get; set; }

        /// <summary>
        /// The value type of parameter 1 of this instance.
        /// </summary>
        public DataValueTypes Parameter1ValueType { get; set; } = DataValueTypes.Object;

        // Parameter 2

        /// <summary>
        /// The name of parameter 2 of this instance.
        /// </summary>
        public string Parameter2Name { get; set; }

        /// <summary>
        /// The value type of parameter 2 of this instance.
        /// </summary>
        public DataValueTypes Parameter2ValueType { get; set; } = DataValueTypes.Object;

        // Parameter 3

        /// <summary>
        /// The name of parameter 3 of this instance.
        /// </summary>
        public string Parameter3Name { get; set; }

        /// <summary>
        /// The value type of parameter 3 of this instance.
        /// </summary>
        public DataValueTypes Parameter3ValueType { get; set; } = DataValueTypes.Object;

        // Parameter 4

        /// <summary>
        /// The name of parameter 4 of this instance.
        /// </summary>
        public string Parameter4Name { get; set; }

        /// <summary>
        /// The value type of parameter 4 of this instance.
        /// </summary>
        public DataValueTypes Parameter4ValueType { get; set; } = DataValueTypes.Object;

        // Parameter 5

        /// <summary>
        /// The name of parameter 5 of this instance.
        /// </summary>
        public string Parameter5Name { get; set; }

        /// <summary>
        /// The value type of parameter 5 of this instance.
        /// </summary>
        public DataValueTypes Parameter5ValueType { get; set; } = DataValueTypes.Object;

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the BdoScriptwordAttribute class.
        /// </summary>
        public BdoFunctionAttribute() : base()
        {
        }

        /// <summary>
        /// Instantiates a new instance of the BdoScriptwordAttribute class.
        /// </summary>
        public BdoFunctionAttribute(string name) : base()
        {
            Name = name;
        }

        #endregion
    }
}
