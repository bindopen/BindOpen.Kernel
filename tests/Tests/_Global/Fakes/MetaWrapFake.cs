using BindOpen.System.Data.Meta;

namespace BindOpen.System.Tests
{
    /// <summary>
    /// This class represents a database data field.
    /// </summary>
    public class MetaWrapFake : BdoMetaWrap
    {
        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// The boolean value of this instance.
        /// </summary>
        [BdoProperty("test1")]
        public string Test1 { get; set; }

        /// <summary>
        /// The string value of this instance.
        /// </summary>
        [BdoProperty("test2")]
        public int Test2 { get; set; }

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the DynamicObjectFake class.
        /// </summary>
        public MetaWrapFake() : base()
        {
        }

        #endregion
    }
}
