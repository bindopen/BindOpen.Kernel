using System;
using System.Xml.Serialization;
using BindOpen.Framework.Core.Data.Entities;

namespace BindOpen.Framework.Core.Application.Entities
{
    /// <summary>
    /// This class represents an application entity.
    /// </summary>
    [Serializable()]
    [XmlType("ApplicationEntity", Namespace = "https://bindopen.org/xsd")]
    [XmlRoot(ElementName = "applicationEntity", Namespace = "https://bindopen.org/xsd", IsNullable = false)]
    public class ApplicationEntity : Entity
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
            this.Scope = scope;
        }

        #endregion
    }
}
