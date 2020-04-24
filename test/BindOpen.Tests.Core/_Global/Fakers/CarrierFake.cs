using BindOpen.Data.Common;
using BindOpen.Data.Items;
using BindOpen.Extensions.Runtime;
using System.Xml.Serialization;

namespace BindOpen.Tests.Core.Fakers
{
    /// <summary>
    /// This class represents a database data field.
    /// </summary>
    [XmlType("DbField", Namespace = "https://storage.bindopen.org/pgrkhpym/docs/code/xsd/bindopen")]
    [XmlRoot(ElementName = "testCarrier", Namespace = "https://storage.bindopen.org/pgrkhpym/docs/code/xsd/bindopen", IsNullable = false)]
    [BdoCarrier(
        Name = "tests.core$testCarrier",
        DatasourceKind = DatasourceKind.Database,
        Description = "Database field.",
        CreationDate = "2016-09-14"
    )]
    public class CarrierFake : BdoCarrier
    {
        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// The boolean value of this instance.
        /// </summary>
        [DetailProperty(Name = "boolValue")]
        public bool BoolValue { get; set; }

        /// <summary>
        /// The string value of this instance.
        /// </summary>
        [DetailProperty(Name = "stringValue")]
        public string StringValue { get; set; }

        /// <summary>
        /// The integer value of this instance.
        /// </summary>
        [DetailProperty(Name = "intValue")]
        public int IntValue { get; set; }

        /// <summary>
        /// Enumeration value of this instance.
        /// </summary>
        [DetailProperty(Name = "enumValue")]
        public ActionPriorities EnumValue { get; set; }

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the TestCarrier class.
        /// </summary>
        public CarrierFake() : base()
        {
        }

        /// <summary>
        /// Instantiates a new instance of the TestCarrier class.
        /// </summary>
        /// <param name="path">The path of the instance.</param>
        public CarrierFake(string path) : base()
        {
            this.Path = path;
        }

        /// <summary>
        /// Instantiates a new instance of the TestCarrier class.
        /// </summary>
        /// <param name="fileName">The file name of the instance.</param>
        /// <param name="folderPath">The folder path of the instance.</param>
        public CarrierFake(string fileName, string folderPath) : base()
        {
            this.WithPath(fileName, folderPath);
        }

        #endregion
    }
}
