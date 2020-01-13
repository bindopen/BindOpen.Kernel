using BindOpen.Framework.Data.Items;
using BindOpen.Framework.Application.Limitations;
using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace BindOpen.Framework.Application.Products
{
    /// <summary>
    /// This class represents the information about the product.
    /// </summary>
    public class ProductInformation : NamedDataItem
    {
        // ---------------------------------------
        // ENUMERATIONS
        // ---------------------------------------

        #region Enumerations

        /// <summary>
        /// This enumeration lists the supported languages.
        /// </summary>
        public enum SupportedLanguage
        {
            /// <summary>
            /// English
            /// </summary>
            English
        }

        /// <summary>
        /// This enumeration lists all the possible product kinds.
        /// </summary>
        public enum ProductKind
        {
            /// <summary>
            /// Any
            /// </summary>
            Any,

            /// <summary>
            /// Application
            /// </summary>
            Application,

            /// <summary>
            /// Module
            /// </summary>
            Module
        }

        /// <summary>
        /// This enumeration lists all the possible product affiliate network.
        /// </summary>
        public enum ProductAffiliateNetwork
        {
            /// <summary>
            /// None
            /// </summary>
            None,

            /// <summary>
            /// MyCommerce
            /// </summary>
            MyCommerce
        }

        #endregion

        // ---------------------------------------
        // PROPERTIES
        // ---------------------------------------

        #region Properties

        /// <summary>
        /// Product kind of the product.
        /// </summary>
        [XmlElement("kind")]
        public ProductKind Kind
        {
            get;
            set;
        }

        /// <summary>
        /// Version of the product.
        /// </summary>
        [XmlElement("version")]
        public string Version
        {
            get;
            set;
        }

        /// <summary>
        /// The author of this instance.
        /// </summary>
        [XmlElement("author")]
        public string Author
        {
            get;
            set;
        }

        /// <summary>
        /// The image file name of this instance.
        /// </summary>
        [XmlElement("imageFileName")]
        public string ImageFileName
        {
            get;
            set;
        }

        /// <summary>
        /// Release date of the product.
        /// </summary>
        [XmlElement("releaseDate")]
        public string ReleaseDate
        {
            get;
            set;
        }

        /// <summary>
        /// URL of the description of the product.
        /// </summary>
        [XmlElement("descriptionUrl")]
        public string DescriptionUrl
        {
            get;
            set;
        }

        /// <summary>
        /// URL to purchase the product.
        /// </summary>
        [XmlElement("productPurchaseUrl")]
        public string ProductPurchaseUrl
        {
            get;
            set;
        }

        /// <summary>
        /// URL to technical support.
        /// </summary>
        [XmlElement("technicalSupportUrl")]
        public string TechnicalSupportUrl
        {
            get;
            set;
        }

        /// <summary>
        /// URL to give feed back.
        /// </summary>
        [XmlElement("giveFeedBackUrl")]
        public string GiveFeedBackUrl
        {
            get;
            set;
        }

        /// <summary>
        /// Limitations of this instance.
        /// </summary>
        [XmlArray("limitations")]
        [XmlArrayItem("limitation")]
        public List<ProductLimitation> Limitations
        {
            get;
            set;
        }

        /// <summary>
        /// Languages to consider.
        /// </summary>
        [XmlArray("languages")]
        [XmlArrayItem("language")]
        public List<string> SupportedLanguages
        {
            get;
            set;
        }

        /// <summary>
        /// Copyright statement of the product.
        /// </summary>
        [XmlElement("copyrightStatement")]
        public string CopyrightStatement
        {
            get;
            set;
        }

        /// <summary>
        /// Copyright year of the product.
        /// </summary>
        [XmlElement("copyrightYear")]
        public string CopyrightYear
        {
            get;
            set;
        }

        /// <summary>
        /// Company of the product.
        /// </summary>
        [XmlElement("company")]
        public string Company
        {
            get;
            set;
        }

        /// <summary>
        /// URL of the company of the product.
        /// </summary>
        [XmlElement("companyUrl")]
        public string CompanyUrl
        {
            get;
            set;
        }

        /// <summary>
        /// Affiliate network of the product.
        /// </summary>
        [XmlElement("affiliateNetwork")]
        public ProductAffiliateNetwork AffiliateNetwork
        {
            get;
            set;
        }

        /// <summary>
        /// Help file name of the product.
        /// </summary>
        [XmlElement("helpFileName")]
        public string HelpFileName
        {
            get;
            set;
        }

        /// <summary>
        /// The URI of the package manager file.
        /// </summary>
        [XmlElement("packageManagerUri")]
        public string PackageManagerUri
        {
            get;
            set;
        }

        /// <summary>
        /// URI of the extension versioning information file.
        /// </summary>
        [XmlElement("extensionPackageManagerUri")]
        public string ExtensionPackageManagerUri
        {
            get;
            set;
        }

        /// <summary>
        /// Indicates when a version can be considered as major.
        /// </summary>
        /// <remarks>Versions are formated as a.b.c.d.
        /// When the major update level equals to 0, the version will be considered as major only if its a.b.c.d is greater than the current ones.
        /// When the major update level equals to 1, the version will be considered as major only if its a.b.c is greater than the current ones.
        /// When the major update level equals to 2, the version will be considered as major only if its a.b is greater than the current ones.
        /// When the major update level equals to 3, the version will be considered as major only if its a is greater than the current ones.
        /// </remarks>
        [XmlElement("majorUpdateLevel")]
        public int MajorUpdateLevel
        {
            get;
            set;
        }

        #endregion

        // -------------------------------------------------------------
        // CONSTRUCTORS
        // -------------------------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the ProductInformation class.
        /// </summary>
        public ProductInformation(): base()
        {
        }

        #endregion
    }
}
