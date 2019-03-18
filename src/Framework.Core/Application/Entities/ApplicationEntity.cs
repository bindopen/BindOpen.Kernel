using BindOpen.Framework.Core.Data.Entities;
using System;
using System.Xml.Serialization;

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
        // VARIABLES
        // ------------------------------------------

        #region Variables

        private ApplicationEntityScope _Scope = ApplicationEntityScope.Any;

        #endregion


        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// The scope of this instance.
        /// </summary>
        [XmlElement("scope")]
        public ApplicationEntityScope Scope
        {
            get { return this._Scope; }
            set { this._Scope = value; }
        }

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
            this._Scope = aScope;
        }

        #endregion

    }

}
