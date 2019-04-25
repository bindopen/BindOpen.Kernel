using System;
using System.Xml.Serialization;
using BindOpen.Framework.Core.Extensions.Attributes;
using BindOpen.Framework.Core.Extensions.Carriers;
using BindOpen.Framework.Core.System.Diagnostics;

namespace BindOpen.Framework.Standard.Extensions.Carriers
{
    /// <summary>
    /// This class represents a repository file.
    /// </summary>
    [Carrier(Name="standard$file")]
    public class RepositoryFile : RepositoryItem
    {
        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// The length of this instance.
        /// </summary>
        [XmlIgnore()]
        [DetailProperty(Name="length")]
        public ulong Length
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
        /// Instantiates a new instance of the RepositoryFile class.
        /// </summary>
        public RepositoryFile() : base()
        {
        }

        /// <summary>
        /// Instantiates a new instance of the RepositoryFile class.
        /// </summary>
        /// <param name="path">The path of the instance.</param>
        public RepositoryFile(string path) : base()
        {
            this.Path = path;
        }

        /// <summary>
        /// Instantiates a new instance of the RepositoryFile class.
        /// </summary>
        /// <param name="fileName">The file name of the instance.</param>
        /// <param name="folderPath">The folder path of the instance.</param>
        public RepositoryFile(string fileName, string folderPath) : base()
        {
            this.SetPath(fileName, folderPath);
        }

        #endregion

        // ------------------------------------------
        // CHECK, UPDATE, REPAIR
        // ------------------------------------------

        #region Check Repair

        /// <summary>
        /// Checks this instance.
        /// </summary>
        /// <param name="isExistenceChecked">Indicates whether the carrier existence is checked.</param>
        /// <param name="item">The item to consider.</param>
        /// <param name="specificationAreas">The specification areas to consider.</param>
        /// <returns>Returns the check log.</returns>
        public override ILog Check<T>(
            Boolean isExistenceChecked = true,
            T item = default,
            string[] specificationAreas = null)
        {
            ILog log = base.Check<T>(isExistenceChecked);

            if (string.IsNullOrEmpty(this.Path))
                log.AddError("File path missing");

            return log;
        }

        #endregion
    }
}
