using BindOpen.Framework.Core.Data.Items;
using System;
using System.Xml.Serialization;

namespace BindOpen.Framework.Runtime.Application.Languages
{

    /// <summary>
    /// This class represents a application languages.
    /// </summary>
    public class ApplicationLanguage : DescribedDataItem
    {

        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// The UI culture of this instance.
        /// </summary>
        [XmlElement("uiCultureName")]
        public String UICultureName
        {
            get;
            set;
        }

        /// <summary>
        /// The culture name of this instance.
        /// </summary>
        [XmlElement("cultureName")]
        public String CultureName
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
