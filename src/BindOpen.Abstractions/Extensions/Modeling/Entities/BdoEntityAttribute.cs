using BindOpen.Data;
using System;

namespace BindOpen.Extensions.Modeling
{
    /// <summary>
    /// This class represents a entity attribute.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class BdoEntityAttribute : MetaExtensionAttribute
    {
        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// The data source kind of this instance.
        /// </summary>
        public DatasourceKind DatasourceKind { get; set; } = DatasourceKind.None;

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the EntityAttribute class.
        /// </summary>
        public BdoEntityAttribute() : base()
        {
        }

        #endregion
    }
}
