using System;

namespace BindOpen.Extensions.Carriers
{
    /// <summary>
    /// This class represents a field attribute.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class BdoDbTableAttribute : Attribute
    {
        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// The name of this instance.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The schema of this instance.
        /// </summary>
        public string Schema { get; set; }

        /// <summary>
        /// Data module of this instance.
        /// </summary>
        public string DataModule { get; set; }

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the DbTableAttribute class.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="schema">The schema to consider.</param>
        /// <param name="dataModuleName">The name of the data module to consider.</param>
        public BdoDbTableAttribute(string name, string schema = null, string dataModuleName = null)
        {
            Name = name;
            Schema = schema;
            DataModule = dataModuleName;
        }

        #endregion
    }
}
