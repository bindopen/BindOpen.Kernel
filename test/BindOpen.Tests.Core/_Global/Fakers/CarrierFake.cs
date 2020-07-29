using BindOpen.Data.Common;
using BindOpen.Data.Items;
using BindOpen.Extensions.Runtime;
using System.Xml.Serialization;

namespace BindOpen.Tests.Core.Fakers
{
    /// <summary>
    /// This class represents a database data field.
    /// </summary>
    [XmlType("DbField", Namespace = "https://docs.bindopen.org/xsd")]
    [XmlRoot(ElementName = "testCarrier", Namespace = "https://docs.bindopen.org/xsd", IsNullable = false)]
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
        /// Instantiates a new instance of the CarrierFake class.
        /// </summary>
        public CarrierFake() : base()
        {
        }

        /// <summary>
        /// Instantiates a new instance of the CarrierFake class.
        /// </summary>
        /// <param name="path">The path of the instance.</param>
        public CarrierFake(string path) : base()
        {
            Path = path;
        }

        /// <summary>
        /// Instantiates a new instance of the TestCarrier class.
        /// </summary>
        /// <param name="fileName">The file name of the instance.</param>
        /// <param name="folderPath">The folder path of the instance.</param>
        public CarrierFake(string fileName, string folderPath) : base()
        {
            WithPath(fileName, folderPath);
        }

        #endregion

        /// <summary>
        /// Converts this instance to string.
        /// </summary>
        /// <returns>Returns this instance to string.</returns>
        public override string ToString()
        {
            return Path;
        }
    }
}
