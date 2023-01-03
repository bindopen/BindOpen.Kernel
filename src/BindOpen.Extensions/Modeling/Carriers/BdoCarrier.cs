using BindOpen.Data.Elements;
using BindOpen.Runtime.Definition;
using System;
using System.ComponentModel;

namespace BindOpen.Extensions.Modeling
{
    /// <summary>
    /// This class represents a carrier.
    /// </summary>
    public abstract class BdoCarrier :
        TBdoExtensionItem<IBdoCarrierDefinition, IBdoCarrierConfiguration, IBdoCarrier>,
        IBdoCarrier
    {
        // -----------------------------------------------
        // VARIABLES
        // -----------------------------------------------

        #region Variables

        private string _relativePath = null;

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the BdoCarrier class.
        /// </summary>
        protected BdoCarrier() : base()
        {
        }

        #endregion

        // -----------------------------------------------
        // IBdoCarrier Implementation
        // -----------------------------------------------

        #region Properties

        // Path --------------------------

        /// <summary>
        /// Path of this instance.
        /// </summary>
        [BdoElement("path")]
        public string Path { get; set; }

        /// <summary>
        /// The relative path of this instance.
        /// </summary>
        public string RelativePath
        {
            get
            {
                return _relativePath;
            }
            set
            {
                WithPath(null, value);
            }
        }
        /// <summary>
        /// Sets the path of this instance.
        /// </summary>
        /// <param name="path">The new path to consider. Null to update the existing one.</param>
        /// <param name="relativePath">The new relative path to consider. Null to keep the existing one.</param>
        /// <returns>Returns True if this instance exists. False otherwise.</returns>
        public virtual IBdoCarrier WithPath(string path = null, string relativePath = null)
        {
            string absolutePath = (path ?? Path);

            if (!string.IsNullOrEmpty(relativePath))
            {
                _relativePath = relativePath;
            }

            if ((!string.IsNullOrEmpty(_relativePath)) && (!string.IsNullOrEmpty(absolutePath)))
            {
                string relativeFolder = _relativePath;

                if (absolutePath.StartsWith(relativeFolder))
                {
                    absolutePath = absolutePath[relativeFolder.Length..];
                }
            }

            Path = absolutePath;

            return this;
        }

        // General --------------------------

        /// <summary>
        /// The creation date of this instance.
        /// </summary>
        [BdoElement("creationDate")]
        public DateTime? CreationDate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public IBdoCarrier WithCreationDate(DateTime? date)
        {
            CreationDate = date;

            return this;
        }

        /// <summary>
        /// The information flag of this instance.
        /// </summary>
        [BdoElement("flag")]
        public string Flag { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="flag"></param>
        /// <returns></returns>
        public IBdoCarrier WithFlag(string flag)
        {
            Flag = flag;

            return this;
        }

        /// <summary>
        /// Indicates whether this instance is read only.
        /// </summary>
        [BdoElement("isReadOnly")]
        [DefaultValue(false)]
        public bool IsReadonly { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="readOnly"></param>
        /// <returns></returns>
        public IBdoCarrier AsReadonly(bool readOnly = false)
        {
            IsReadonly = readOnly;

            return this;
        }

        /// <summary>
        /// The date of last access of this instance.
        /// </summary>
        [BdoElement("lastAccessDate")]
        public DateTime? LastAccessDate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public IBdoCarrier WithLastAccessDate(DateTime? date)
        {
            LastAccessDate = date;

            return this;
        }

        /// <summary>
        /// The parent path of this instance.
        /// </summary>
        [BdoElement("parentPath")]
        public string ParentPath { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public IBdoCarrier WithParentPath(string path)
        {
            ParentPath = path;

            return this;
        }

        /// <summary>
        /// The date of last write of this instance.
        /// </summary>
        [BdoElement("lastWriteDate")]
        public DateTime? LastWriteDate { get; set; }

        public IBdoCarrier WithLastWriteDate(DateTime? date)
        {
            LastWriteDate = date;

            return this;
        }

        #endregion
    }
}
