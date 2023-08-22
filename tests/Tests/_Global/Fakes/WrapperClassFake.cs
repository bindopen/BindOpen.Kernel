using BindOpen.System.Data;
using BindOpen.System.Data.Meta;

namespace BindOpen.System.Tests
{
    /// <summary>
    /// This class represents a fake class.
    /// </summary>
    public class WrapperClassFake
    {
        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// The string value of this instance.
        /// </summary>
        [BdoProperty(Name = "stringValue")]
        public string StringValue { get; set; }

        /// <summary>
        /// The integer value of this instance.
        /// </summary>
        [BdoProperty(Name = "intValue")]
        public int IntValue { get; set; }

        /// <summary>
        /// Enumeration value of this instance.
        /// </summary>
        [BdoProperty(Name = "enumValue")]
        public ActionPriorities EnumValue { get; set; }

        #endregion
    }
}