using System.ComponentModel;
using System.Linq;
using BindOpen.Framework.Core.Application.Scopes;
using BindOpen.Framework.Core.Data.Elements.Carrier;
using BindOpen.Framework.Core.Data.Elements.Factories;
using BindOpen.Framework.Core.Data.Helpers.Objects;
using BindOpen.Framework.Core.Extensions.Attributes;
using BindOpen.Framework.Core.Extensions.Carriers;
using BindOpen.Framework.Core.Extensions.Items.Carriers.Definition;
using BindOpen.Framework.Core.System.Diagnostics;
using BindOpen.Framework.Core.System.Scripting;

namespace BindOpen.Framework.Core.Extensions.Items.Carriers
{
    /// <summary>
    /// This class represents a carrier.
    /// </summary>
    public abstract class Carrier : TAppExtensionItem<ICarrierDefinition>, ICarrier
    {
        // -----------------------------------------------
        // VARIABLES
        // -----------------------------------------------

        #region Variables

        public new ICarrierConfiguration Configuration { get => base.Configuration as ICarrierConfiguration; }

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
                this.SetPath(null, value);
            }
        }

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the Carrier class.
        /// </summary>
        protected Carrier() : base()
        {
        }

        /// <summary>
        /// Instantiates a new instance of the Carrier class.
        /// </summary>
        /// <param name="config">The configuration of this instance.</param>
        protected Carrier(ICarrierConfiguration config): base(config)
        {
        }

        /// <summary>
        /// Instantiates a new instance of the Connector class.
        /// </summary>
        /// <param name="path">The path to consider.</param>
        /// <param name="relativePath">The path to consider.</param>
        protected Carrier(string path, string relativePath = null) : base()
        {
            SetPath(path, relativePath);
        }

        #endregion

        /// <summary>
        /// Returns a data element representing this instance.
        /// </summary>
        /// <param name="name">The name of the element to create.</param>
        /// <param name="log">The log of the operation.</param>
        /// <returns>Retuns the data element that represents this instace.</returns>
        public ICarrierElement AsElement(string name =null, ILog log = null)
        {
            UpdateStorageInfo(log);
            return ElementFactory.CreateCarrier(name ?? Name, Configuration as ICarrierConfiguration);
        }

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
        public virtual void SetPath(string path = null, string relativePath = null)
        {
            string absolutePath = (path ?? Path);

            if (!string.IsNullOrEmpty(relativePath))
                this._relativePath = relativePath;

            if ((!string.IsNullOrEmpty(this._relativePath)) && (!string.IsNullOrEmpty(absolutePath)))
            {
                string relativeFolder = this._relativePath.ToLower();
                absolutePath = absolutePath.ToLower();

                if (absolutePath.StartsWith(relativeFolder))
                    absolutePath = absolutePath.Substring(relativeFolder.Length);
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
        public override void UpdateStorageInfo(ILog log = null)
        {
            (Configuration ?? (_configuration = new CarrierConfiguration())).UpdateFromObject<DetailPropertyAttribute>(this);
            if (string.IsNullOrEmpty(Configuration.DefinitionUniqueId)
                && GetType().GetCustomAttributes(typeof(CarrierAttribute), false).FirstOrDefault() is CarrierAttribute attribute
                && attribute.Name.IndexOf("$") > 0)

            {
                Configuration.DefinitionUniqueId = attribute.Name;
            }
            Configuration.UpdateStorageInfo(log);
            if (string.IsNullOrEmpty(Configuration.Name))
                Configuration.Name = Name;
        }

        /// <summary>
        /// Updates information for runtime.
        /// </summary>
        /// <param name="log">The log to update.</param>
        public override void UpdateRuntimeInfo(IAppScope appScope = null, IScriptVariableSet scriptVariableSet = null, ILog log = null)
        {
            if (Configuration!=null)
            {
                if (string.IsNullOrEmpty(Name))
                {
                    Name = Configuration.Name?.IndexOf("$") > 0 ?
                       Configuration.Name.Substring(Configuration.Name.IndexOf("$") + 1) :
                       Configuration.Name;
                }

                Configuration.UpdateRuntimeInfo(appScope, scriptVariableSet, log);
                this.UpdateFromElementSet<DetailPropertyAttribute>(Configuration, appScope, scriptVariableSet);
            }
        }

        #endregion
    }
}
