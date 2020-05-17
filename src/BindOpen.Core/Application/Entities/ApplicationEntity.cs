using BindOpen.Data.Entities;
using System.Xml.Serialization;

namespace BindOpen.Application.Entities
{
    /// <summary>
    /// This class represents an application entity.
    /// </summary>
    [XmlType("ApplicationEntity", Namespace = "https://docs.bindopen.org/xsd")]
    [XmlRoot(ElementName = "applicationEntity", Namespace = "https://docs.bindopen.org/xsd", IsNullable = false)]
    public class ApplicationEntity : DataEntity
    {
        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// The scope of this instance.
        /// </summary>
        [XmlElement("scope")]
        public ApplicationEntityScope Scope { get; set; } = ApplicationEntityScope.Any;

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the Entity class.
        /// </summary>
        /// <param name="scope">The entity scope to consider.</param>
        public ApplicationEntity(ApplicationEntityScope scope) : base()
        {
            Scope = scope;
        }

        #endregion
    }
}
