using BindOpen.Application.Scopes;
using BindOpen.Data.Elements;
using BindOpen.Data.Helpers.Objects;
using BindOpen.Extensions.Definition;
using BindOpen.System.Diagnostics;
using BindOpen.System.Scripting;
using System.ComponentModel;
using System.Linq;

namespace BindOpen.Extensions.Runtime
{
    /// <summary>
    /// This class represents a carrier.
    /// </summary>
    public abstract class BdoCarrier : TBdoExtensionItem<IBdoCarrierDefinition>, IBdoCarrier
    {
        // -----------------------------------------------
        // VARIABLES
        // -----------------------------------------------

        #region Variables

        /// <summary>
        /// The configuration of this instance.
        /// </summary>
        public new IBdoCarrierConfiguration Configuration { get => base.Configuration as IBdoCarrierConfiguration; }

        private string _relativePath = null;

        #endregion

        // -----------------------------------------------
        // PROPERTIES
        // -----------------------------------------------

        #region Properties

        // Path --------------------------

        /// <summary>
        /// Path of this instance.
        /// </summary>
        [DetailProperty("path")]
        public string Path { get; set; } = null;

        /// <summary>
        /// The parent path of this instance.
        /// </summary>
        [DetailProperty("parentPath")]
        public string ParentPath { get; set; } = null;

        // General --------------------------

        /// <summary>
        /// The creation date of this instance.
        /// </summary>
        [DetailProperty("creationDate")]
        public string CreationDate { get; set; } = null;

        /// <summary>
        /// The information flag of this instance.
        /// </summary>
        [DetailProperty("flag")]
        public string Flag { get; set; } = null;

        /// <summary>
        /// Indicates whether this instance is read only.
        /// </summary>
        [DetailProperty("isReadOnly")]
        [DefaultValue(false)]
        public bool IsReadonly { get; set; }

        /// <summary>
        /// The date of last access of this instance.
        /// </summary>
        [DetailProperty("lastAccessDate")]
        public string LastAccessDate { get; set; } = null;

        /// <summary>
        /// The date of last write of this instance.
        /// </summary>
        [DetailProperty("lastWriteDate")]
        public string LastWriteDate { get; set; } = null;

        /// <summary>
        /// The relative path of this instance.
        /// </summary>
        public string RelativePath
        {
            get
            {
                return this._relativePath;
            }
            set
            {
                this.WithPath(null, value);
            }
        }

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

        // ------------------------------------------
        // ACCESSORS
        // ------------------------------------------

        #region Accessors

        /// <summary>
        /// Returns a data element representing this instance.
        /// </summary>
        /// <param name="name">The name of the element to create.</param>
        /// <param name="log">The log of the operation.</param>
        /// <returns>Retuns the data element that represents this instace.</returns>
        public ICarrierElement AsElement(string name = null, IBdoLog log = null)
        {
            UpdateStorageInfo(log);

            return ElementFactory.CreateCarrier(name ?? Name)
                .WithConfiguration(Configuration);
        }

        #endregion

        //// ------------------------------------------
        //// MUTATORS
        //// ------------------------------------------

        #region Mutators

        /// <summary>
        /// Sets the path of this instance.
        /// </summary>
        /// <param name="path">The new path to consider. Null to update the existing one.</param>
        /// <param name="relativePath">The new relative path to consider. Null to keep the existing one.</param>
        /// <returns>Returns True if this instance exists. False otherwise.</returns>
        public virtual void WithPath(string path = null, string relativePath = null)
        {
            string absolutePath = (path ?? Path);

            if (!string.IsNullOrEmpty(relativePath))
            {
                this._relativePath = relativePath;
            }

            if ((!string.IsNullOrEmpty(this._relativePath)) && (!string.IsNullOrEmpty(absolutePath)))
            {
                string relativeFolder = this._relativePath;

                if (absolutePath.StartsWith(relativeFolder))
                {
                    absolutePath = absolutePath.Substring(relativeFolder.Length);
                }
            }

            Path = absolutePath;
        }

        #endregion

        // --------------------------------------------------
        // SERIALIZATION
        // --------------------------------------------------

        #region Serialization

        /// <summary>
        /// Updates information for storage.
        /// </summary>
        /// <param name="log">The log to update.</param>
        public override void UpdateStorageInfo(IBdoLog log = null)
        {
            (Configuration ?? (_configuration = new BdoCarrierConfiguration())).UpdateFromObject<DetailPropertyAttribute>(this);
            if (string.IsNullOrEmpty(Configuration.DefinitionUniqueId)
                && GetType().GetCustomAttributes(typeof(BdoCarrierAttribute), false).FirstOrDefault() is BdoCarrierAttribute attribute
                && attribute.Name.IndexOf("$") > 0)

            {
                Configuration.DefinitionUniqueId = attribute.Name;
            }
            Configuration.UpdateStorageInfo(log);
            if (string.IsNullOrEmpty(Configuration.Name))
            {
                Configuration.Name = Name;
            }
        }

        /// <summary>
        /// Updates information for runtime.
        /// </summary>
        /// <param name="scope">The scope to consider.</param>
        /// <param name="scriptVariableSet">The set of script variables to consider.</param>
        /// <param name="log">The log to update.</param>
        public override void UpdateRuntimeInfo(IBdoScope scope = null, IScriptVariableSet scriptVariableSet = null, IBdoLog log = null)
        {
            if (Configuration != null)
            {
                if (string.IsNullOrEmpty(Name))
                {
                    Name = Configuration.Name?.IndexOf("$") > 0 ?
                       Configuration.Name.Substring(Configuration.Name.IndexOf("$") + 1) :
                       Configuration.Name;
                }

                Configuration.UpdateRuntimeInfo(scope, scriptVariableSet, log);
                this.UpdateFromElementSet<DetailPropertyAttribute>(Configuration, scope, scriptVariableSet);
            }
        }

        #endregion
    }
}
