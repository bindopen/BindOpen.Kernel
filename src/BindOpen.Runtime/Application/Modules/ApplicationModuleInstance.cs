using BindOpen.Application.Options;
using BindOpen.Data.Common;
using BindOpen.Data.Helpers.Objects;
using BindOpen.Data.Items;
using BindOpen.System;
using BindOpen.System.Diagnostics;
using System;
using System.ComponentModel;

namespace BindOpen.Application.Modules
{
    /// <summary>
    /// This class represents an application module instance accessible by a visitor.
    /// </summary>
    public class AppModuleInstance : DescribedDataItem, IAppModuleInstance
    {
        // ------------------------------------------
        // VARIABLES
        // ------------------------------------------

        #region Variables

        private readonly OptionSet _optionSet = null;

        #endregion

        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// The module of this instance.
        /// </summary>
        public IAppModule Module { get; set; } = null;

        /// <summary>
        /// The name of this instance.
        /// </summary>
        public string ModuleName
        {
            get { return Module?.Name; }
        }

        // Location ---------------------------------

        /// <summary>
        /// The URI of this instance.
        /// </summary>
        public string Uri { get; } = null;

        /// <summary>
        /// The URI of this instance.
        /// </summary>
        public string AbsoluteUri { get; } = null;

        /// <summary>
        /// The application execution path of this instance.
        /// </summary>
        public string ApplicationExecutionPath { get; } = null;

        /// <summary>
        /// Indicates whether this instance is local.
        /// </summary>
        public bool IsLocal { get; } = false;

        /// <summary>
        /// The accessibility level of this instance.
        /// </summary>
        public AccessibilityLevels AccessibilityLevel { get; set; } = AccessibilityLevels.Public;

        /// <summary>
        /// Indexation of this instance.
        /// </summary>
        [Bindable(false)]
        [DefaultValue("")]
        public InstanceIndexations Indexation { get; set; } = InstanceIndexations.None;

        /// <summary>
        /// Kind of this instance.
        /// </summary>
        public AppModuleKind Kind { get; set; } = AppModuleKind.None;

        /// <summary>
        /// Sub kind of this instance.
        /// </summary>
        public AppModuleSubKind SubKind { get; set; } = AppModuleSubKind.None;

        // Tree ------------------------------------

        /// <summary>
        /// The sections of this instance.
        /// </summary>
        public ITDataItemSet<AppSection> Sections
        {
            get;
            set;
        }

        // Options ----------------------------------

        /// <summary>
        /// The otpions of this instance.
        /// </summary>
        public IOptionSet OptionSet
        {
            get { return _optionSet; }
        }

        #endregion

        // ----------------------------
        // CONSTRUCTORS
        // ----------------------------

        #region Constructors

        /// <summary>
        /// Initializes a new instance of AppModuleInstance class.
        /// </summary>
        /// <param name="module">Module to consider.</param>
        /// <param name="uri">URI to consider.</param>
        public AppModuleInstance(
            IAppModule module,
            string uri)
        {
            Module = module;
            Uri = uri;
        }

        #endregion

        // ----------------------------
        // MUTATORS
        // ----------------------------

        #region Mutators

        /// <summary>
        /// Updates this instance.
        /// </summary>
        /// <param name="item">The item to consider.</param>
        /// <param name="specificationAreas">The specification areas to consider.</param>
        /// <param name="updateModes">The update modes to consider.</param>
        /// <returns>Log of the operation.</returns>
        /// <remarks>Put reference collections as null if you do not want to repair this instance.</remarks>
        public override IBdoLog Update<T>(
            T item = default,
            string[] specificationAreas = null,
            UpdateModes[] updateModes = null)
        {
            var log = new BdoLog();

            if (item is IAppModuleInstance)
            {
                IAppModuleInstance moduleInstance = item as AppModuleInstance;
                Module.Update(moduleInstance.Module);
            }

            return log;
        }

        #endregion

        // ----------------------------
        // ACCESSORS
        // ----------------------------

        #region Accessors

        /// <summary>
        /// Returns the application section of this instance with the specified complete name.
        /// </summary>
        /// <returns>The application section of this instance with the specified complete name.</returns>
        public IAppSection GetSectionWithCompleteName(String completeName)
        {
            if (completeName.Contains("$"))
                completeName = completeName.Substring(completeName.IndexOf('$') + 1);
            else
                completeName = "";
            string[] names = completeName.Split(new char[] { '$' });

            IAppSection moduleSection = null;
            foreach (string name in names)
            {
                if (!string.IsNullOrEmpty(name))
                {
                    if (moduleSection == null)
                        moduleSection = GetSectionWithName(name);
                    else
                        moduleSection = moduleSection.GetSubSectionWithName(name);
                }
            }

            return moduleSection;
        }

        /// <summary>
        /// Returns the application section of this instance with the specified name.
        /// </summary>
        /// <returns>The application section of this instance with the specified name.</returns>
        public IAppSection GetSectionWithName(string name)
        {
            if (Sections != null)
            {
                foreach (IAppSection moduleSection in Sections.Items)
                {
                    if (moduleSection.Name.KeyEquals(name.ToUpper()))
                    {
                        return moduleSection;
                    }
                }
            }

            return null;
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

            _optionSet?.Dispose();

            _isDisposed = true;

            base.Dispose(isDisposing);
        }

        #endregion
    }
}