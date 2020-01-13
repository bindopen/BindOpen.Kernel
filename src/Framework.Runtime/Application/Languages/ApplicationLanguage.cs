using BindOpen.Framework.Data.Items;
using System.Xml.Serialization;

namespace BindOpen.Framework.Application.Languages
{
    /// <summary>
    /// This class represents a application languages.
    /// </summary>
    public class ApplicationLanguage : DescribedDataItem, IApplicationLanguage
    {
        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// The UI culture of this instance.
        /// </summary>
        [XmlElement("uiCultureName")]
        public string UICultureName
        {
            get;
            set;
        }

        /// <summary>
        /// The culture name of this instance.
        /// </summary>
        [XmlElement("cultureName")]
        public string CultureName
        {
            get;
            set;
        }

        #endregion

        // -------------------------------------------------------
        // CONSTRUCTORS
        // -------------------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of ApplicationLanguage class.
        /// </summary>
        public ApplicationLanguage()
        {
        }

        #endregion
    }
}
