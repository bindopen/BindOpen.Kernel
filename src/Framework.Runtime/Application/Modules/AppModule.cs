using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Xml.Serialization;
using BindOpen.Framework.Core.Application.Scopes;
using BindOpen.Framework.Core.Data.Common;
using BindOpen.Framework.Core.Data.Helpers.Objects;
using BindOpen.Framework.Core.Data.Items;
using BindOpen.Framework.Core.Data.Items.Sets;
using BindOpen.Framework.Core.System.Diagnostics;
using BindOpen.Framework.Core.System.Scripting;
using BindOpen.Framework.Runtime.Application.Languages;

namespace BindOpen.Framework.Runtime.Application.Modules
{

    /// <summary>
    /// This class represents a Sphere application module.
    /// </summary>
    public class AppModule : DescribedDataItem
    {

        // ------------------------------------------
        // VARIABLES
        // ------------------------------------------

        #region Variables

        private Boolean _IsVisible;
        private String _DefaultUICultureName;

        private DataItemSet<AppSection> _Sections = null;
        private DataItemSet<ApplicationLanguage> _Languages = null;

        #endregion

        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        // Visibility ------------------------------

        /// <summary>
        /// File name of the icon of this instance.
        /// </summary>
        [DefaultValue("")]
        [XmlElement("iconFileName")]
        public String IconFileName { get; set; }

        /// <summary>
        /// Rank of this instance.
        /// </summary>
        [XmlElement("rank")]
        public int Rank { get; set; }

        /// <summary>
        /// File name of the thumb icon of this instance.
        /// </summary>
        [DefaultValue("")]
        [XmlElement("thumbIconFileName")]
        public String ThumbIconFileName { get; set; }

        /// <summary>
        /// Indicates whether this instance is visible.
        /// </summary>
        [XmlElement("isVisible")]
        public Boolean IsVisible
        {
            get { return this._IsVisible; }
            set { this._IsVisible = value; }
        }

        // Tree ------------------------------------

        ///// <summary>
        ///// The parent of this instance.
        ///// </summary>
        //[XmlIgnore()]
        //public ApplicationModule Parent
        //{
        //    get { return this._Parent; }
        //    set { this._Parent = value; }
        //}

        ///// <summary>
        ///// The parent of this instance.
        ///// </summary>
        //[XmlIgnore()]
        //public ApplicationModule Root
        //{
        //    get { return this._Parent == null ? this : this._Parent.Parent; }
        //}

        /// <summary>
        /// The sub sections of this instance.
        /// </summary>
        [XmlElement("sections")]
        public DataItemSet<AppSection> Sections
        {
            get
            {
                return this._Sections;
            }
            set
            {
                this._Sections = value;
            }
        }

        // Languages ------------------------------------

        /// <summary>
        /// The languages of this instance.
        /// </summary>
        [XmlElement("languages")]
        public DataItemSet<ApplicationLanguage> Languages
        {
            get
            {
                return this._Languages;
            }
            set
            {
                this._Languages = value;
            }
        }

        /// <summary>
        /// The default UI culture name of this instance.
        /// </summary>
        [XmlIgnore()]
        [DefaultValue("")]
        public String DefaultUICulture
        {
            get { return this._DefaultUICultureName; }
            set { this._DefaultUICultureName = value; }
        }

        #endregion


        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the ApplicationModule class.
        /// </summary>
        /// <param name="name">The name of the instance.</param>
        public AppModule(
            String name) : base(name, "module_")
        {
        }

        #endregion


        // ------------------------------------------
        // ACCESSORS
        // ------------------------------------------

        #region Accessors

        /// <summary>
        /// Returns the sub section with the specified name.
        /// </summary>
        /// <param name="name">The name of the sub application sections to return.</param>
        /// <returns>The sub application section with the specified name.</returns>
        public AppSection GetSubSectionWithName(String name)
        {
            AppSection section = null;

            if (this._Sections != null)
                foreach (AppSection moduleSection in this._Sections.Items)
                    if ((section = moduleSection.GetSubSectionWithName(name)) != null)
                        return section;

            return null;
        }

        /// <summary>
        /// Returns the sub section with the specified unique name.
        /// </summary>
        /// <param name="completeName">The complete name of the sub application sections to return.</param>
        /// <returns>The sub application sections with the specified unique name.</returns>
        public AppSection GetSubSectionWithUniqueName(String completeName)
        {
            AppSection section = null;

            if (this._Sections != null)
                foreach (AppSection moduleSection in this._Sections.Items)
                    if ((section = moduleSection.GetSubSectionWithUniqueName(completeName)) != null)
                        return section;

            return null;
        }

        // Languages ---------------------------------

        /// <summary>
        /// Gets the specified language.
        /// </summary>
        /// <param name="uiCulureName">The UI culture name.</param>
        /// <returns>The language to return.</returns>
        public ApplicationLanguage GetLanguageWithUICulture(String uiCulureName)
        {
            return this._Languages?.Items?.FirstOrDefault(p=>p.UICultureName.KeyEquals(uiCulureName));
        }

        #endregion


        // ----------------------------
        // MUTATORS
        // ----------------------------

        #region Mutators

        /// <summary>
        /// Adds the specified section.
        /// </summary>
        /// <param name="section">The section to add.</param>
        /// <returns>Returns the specified section.</returns>
        public void AddSection(AppSection section)
        {
            if (section == null) return;

            if (this._Sections == null)
                this._Sections = new DataItemSet<AppSection>();

            this._Sections.Add(section);
            section.Module = this;
        }

        /// <summary>
        /// Adds the specified sections.
        /// </summary>
        /// <param name="sections">The sections to add.</param>
        /// <returns>Returns the specified sections.</returns>
        public AppModule AddSections(params AppSection[] sections)
        {
            foreach (AppSection section in sections)
                this.AddSection(section);
            return this;
        }

        /// <summary>
        /// Updates this instance.
        /// </summary>
        /// <param name="item">The item to consider.</param>
        /// <param name="specificationAreas">The specification areas to consider.</param>
        /// <param name="updateModes">The update modes to consider.</param>
        /// <param name="appScope">The application scope to consider.</param>
        /// <param name="scriptVariableSet">The script variable set to use.</param>
        /// <returns>Log of the operation.</returns>
        /// <remarks>Put reference collections as null if you do not want to repair this instance.</remarks>
        public override Log Update<T>(
            T item = null,
            List<String> specificationAreas = null,
            List<UpdateMode> updateModes = null,
            IAppScope appScope = null,
            ScriptVariableSet scriptVariableSet = null)
        {
            Log log = new Log();

            if (item is AppModule)
            {
                AppModule module = item as AppModule;
                log.Append(this._Sections.Update(
                    module._Sections,
                    null,
                    new List<UpdateMode>() { UpdateMode.Incremental_AddItemsMissingInTarget },
                    appScope,
                    scriptVariableSet));
            }

            return log;
        }

        #endregion

    }
}