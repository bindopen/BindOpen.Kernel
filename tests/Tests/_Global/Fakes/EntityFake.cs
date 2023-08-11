using BindOpen.System.Data;
using BindOpen.System.Data.Meta;
using BindOpen.System.Scoping;
using System;
using System.ComponentModel;

namespace BindOpen.System.Tests
{
    /// <summary>
    /// This class represents a database data field.
    /// </summary>
    [BdoEntity(
        Name = "testEntity",
        Description = "Database field.",
        CreationDate = "2016-09-14"
    )]
    public class EntityFake : BdoEntity
    {
        public static string __DefinitionUniqueName = "bindopen.system.tests$testEntity";

        // -----------------------------------------------
        // VARIABLES
        // -----------------------------------------------

        #region Variables

        private string _relativePath = null;

        #endregion

        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// The boolean value of this instance.
        /// </summary>
        [BdoProperty("boolValue", "b", "bValue")]
        [BdoProperty(Description = "This is a boolean property")]
        [BdoProperty(RequirementLevels.Optional, @"$eq($this(""stringValue""), ""AA"")")]
        [BdoProperty(0, 1)]
        [BdoProperty(SpecificationLevels.Configuration, SpecificationLevels.Definition)]
        public ITBdoMetaScalar<bool?> BoolValue { get; set; }

        /// <summary>
        /// The string value of this instance.
        /// </summary>
        [BdoProperty(Name = "stringValue")]
        public string StringValue { get; set; }

        /// <summary>
        /// The integer value of this instance.
        /// </summary>
        [BdoProperty(Name = "intValue")]
        public int IntValue { get; set; }

        /// <summary>
        /// Enumeration value of this instance.
        /// </summary>
        [BdoProperty(Name = "enumValue")]
        public ActionPriorities EnumValue { get; set; }

        /// <summary>
        /// Enumeration value of this instance.
        /// </summary>
        [BdoProperty(Name = "inputs")]
        [BdoFunction(Name = "input")]
        public BdoMetaComposite Inputs { get; set; }

        /// <summary>
        /// The sub entity of this instance.
        /// </summary>
        [BdoProperty(Name = "subEntity")]
        public EntityFake SubEntity { get; set; }

        // Path --------------------------

        /// <summary>
        /// Path of this instance.
        /// </summary>
        [BdoProperty("path")]
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
        /// <param key="path">The new path to consider. Null to update the existing one.</param>
        /// <param key="relativePath">The new relative path to consider. Null to keep the existing one.</param>
        /// <returns>Returns True if this instance exists. False otherwise.</returns>
        public virtual IBdoEntity WithPath(string path = null, string relativePath = null)
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
        [BdoProperty("creationDate")]
        public DateTime? CreationDate { get; set; }

        /// <summary>
        /// The information flag of this instance.
        /// </summary>
        [BdoProperty("flag")]
        public string Flag { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param key="flag"></param>
        /// <returns></returns>
        public IBdoEntity WithFlag(string flag)
        {
            Flag = flag;

            return this;
        }

        /// <summary>
        /// Indicates whether this instance is read only.
        /// </summary>
        [BdoProperty("isReadOnly")]
        [DefaultValue(false)]
        public bool IsReadonly { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param key="readOnly"></param>
        /// <returns></returns>
        public IBdoEntity AsReadonly(bool readOnly = false)
        {
            IsReadonly = readOnly;

            return this;
        }

        /// <summary>
        /// The date of last access of this instance.
        /// </summary>
        [BdoProperty("lastAccessDate")]
        public DateTime? LastAccessDate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param key="date"></param>
        /// <returns></returns>
        public IBdoEntity WithLastAccessDate(DateTime? date)
        {
            LastAccessDate = date;

            return this;
        }

        /// <summary>
        /// The parent path of this instance.
        /// </summary>
        [BdoProperty("parentPath")]
        public string ParentPath { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param key="path"></param>
        /// <returns></returns>
        public IBdoEntity WithParentPath(string path)
        {
            ParentPath = path;

            return this;
        }

        /// <summary>
        /// The date of last write of this instance.
        /// </summary>
        [BdoProperty("lastWriteDate")]
        public DateTime? LastWriteDate { get; set; }

        public IBdoEntity WithLastWriteDate(DateTime? date)
        {
            LastWriteDate = date;

            return this;
        }

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the EntityFake class.
        /// </summary>
        public EntityFake() : base()
        {
        }

        /// <summary>
        /// Instantiates a new instance of the EntityFake class.
        /// </summary>
        /// <param key="path">The path of the instance.</param>
        public EntityFake(string path) : base()
        {
            WithPath(path);
        }

        /// <summary>
        /// Instantiates a new instance of the TestEntity class.
        /// </summary>
        /// <param key="fileName">The file name of the instance.</param>
        /// <param key="folderPath">The folder path of the instance.</param>
        public EntityFake(
            string fileName,
            string folderPath,
            EntityFake subEntity = null) : base()
        {
            WithPath(fileName, folderPath);
            SubEntity = subEntity;
        }

        #endregion

        /// <summary>
        /// Converts this instance to string.
        /// </summary>
        /// <returns>Returns this instance to string.</returns>
        public override string ToString()
        {
            return Path;
        }
    }
}
