using BindOpen.System.Data;
using BindOpen.System.Data.Meta;
using System.Collections.Generic;

namespace BindOpen.System.Tests
{
    /// <summary>
    /// This class represents a database data field.
    /// </summary>
    public class MetaWrapperFake : TBdoMetaWrapper<BdoConfiguration>
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
