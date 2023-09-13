using BindOpen.Kernel.Abstractions.Data._Core.Enums;
using BindOpen.Kernel.Data;
using BindOpen.Kernel.Data.Meta;

namespace BindOpen.Kernel.Tests
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