using BindOpen.Kernel.Data;
using BindOpen.Kernel.Data.Meta;
using System.Collections.Generic;

namespace BindOpen.Kernel.Tests
{
    /// <summary>
    /// This class represents a database data field.
    /// </summary>
    public class MetaWrapperFake : BdoConfigurationMetaWrapper
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

        /// <summary>
        /// The News URIs of this instance.
        /// </summary>
        [BdoProperty("testDico")]
        public TBdoDictionary<int> Dico { get; set; }

        /// <summary>
        /// The News URIs of this instance.
        /// </summary>
        [BdoProperty("testList")]
        public List<string> List { get; set; }


        [BdoProperty("entityFake")]
        public WrapperClassFake EntityFake { get; set; }

        /// <summary>
        /// Enumeration value of this instance.
        /// </summary>
        [BdoProperty(Name = "subEnumValue", Reference = "entityFake/enumValue")]
        public AccessibilityLevels SubEnumValue { get; set; }

        [BdoProperty(Name = "configEntityFake", Reference = "^$entityFake/node1")]
        public WrapperClassFake ConfigEntityFake { get; set; }

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the DynamicObjectFake class.
        /// </summary>
        public MetaWrapperFake() : base()
        {
        }

        #endregion
    }
}
