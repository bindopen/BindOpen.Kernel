using BindOpen.Data.Conditions;
using BindOpen.Data.Items;
using System;
using System.Xml.Serialization;

namespace BindOpen.Data.Business
{
    /// <summary>
    /// This class represents a business case.
    /// </summary>
    [Serializable()]
    [XmlType("BusinessCase", Namespace = "https://bindopen.org/xsd")]
    [XmlRoot(ElementName = "businessCase", Namespace = "https://bindopen.org/xsd", IsNullable = false)]
    public class BusinessCase : DescribedDataItem
    {
        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// Business condition of this instance.
        /// </summary>
        [XmlElement("businessCondition")]
        public Condition BusinessCondition
        {
            get;
            set;
        }

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the BusinessCase class.
        /// </summary>
        public BusinessCase() : base()
        {
        }

        #endregion

        // --------------------------------------------------
        // ACCESSORS
        // --------------------------------------------------

        #region Accessors

        // Cloning ------------------------------------------

        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <returns>Returns the cloned instance.</returns>
        public override object Clone(params string[] areas)
        {
            var buinessCase = base.Clone(areas) as BusinessCase;
            return buinessCase;
        }

        #endregion

        // ------------------------------------------
        // IDISPOSABLE METHODS
        // ------------------------------------------

        #region IDisposable_Methods

        private bool _isDisposed = false;

        /// <summary>
        /// Disposes this instance. 
        /// </summary>
        /// <param name="isDisposing">Indicates whether this instance is disposing</param>
        protected override void Dispose(bool isDisposing)
        {
            if (_isDisposed)
            {
                return;
            }

            BusinessCondition?.Dispose();

            _isDisposed = true;

            base.Dispose(isDisposing);
        }

        #endregion
    }
}