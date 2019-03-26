using System;
using System.Xml.Serialization;
using BindOpen.Framework.Core.Data.Business.Entities;

namespace BindOpen.Framework.Core.Application.Entities
{

    /// <summary>
    /// This class represents an application entity.
    /// </summary>
    [Serializable()]
    [XmlType("ApplicationEntity", Namespace = "http://meltingsoft.com/bindopen/xsd")]
    [XmlRoot(ElementName = "applicationEntity", Namespace = "http://meltingsoft.com/bindopen/xsd", IsNullable = false)]
    public class ApplicationEntity : BusinessEntity
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
        /// <param name="aScope">The entity scope to consider.</param>
        public ApplicationEntity(ApplicationEntityScope aScope) : base()
        {
            this.Scope = aScope;
        }

        #endregion
    }

}
