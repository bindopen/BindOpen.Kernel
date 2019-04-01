using System;
using System.ComponentModel;
using System.Xml.Serialization;
using BindOpen.Framework.Core.Data.Helpers.Objects;
using BindOpen.Framework.Core.Data.Items;
using BindOpen.Framework.Core.Data.Items.Sets;

namespace BindOpen.Framework.Runtime.Application.Modules
{
    /// <summary>
    /// This class represents a visitor application module section.
    /// </summary>
    public class AppSection : DescribedDataItem
    {
        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        // Tree ------------------------------------

        /// <summary>
        /// The module of this instance.
        /// </summary>
        [XmlElement("module")]
        public AppModule Module { get; set; } = null;

        /// <summary>
        /// The section parent of this instance.
        /// </summary>
        [XmlIgnore()]
        public AppSection Parent { get; set; } = null;

        /// <summary>
        /// The sub sections of this instance.
        /// </summary>
        [XmlArray("subSections")]
        [XmlArrayItem("section")]
        public DataItemSet<AppSection> SubSections { get; set; } = new DataItemSet<AppSection>();

        // Visibility ------------------------------

        /// <summary>
        /// Indicates whether this instance is visible.
        /// </summary>
        [XmlElement("isVisible")]
        public bool IsVisible { get; set; } = false;

        /// <summary>
        /// File name of the icon of this instance.
        /// </summary>
        [Bindable(false)]
        [DefaultValue("")]
        [XmlElement("iconFileName")]
        public string IconFileName { get; set; } = null;

        /// <summary>
        /// File name of the thumb icon of this instance.
        /// </summary>
        [Bindable(false)]
        [DefaultValue("")]
        [XmlElement("thumbIconFileName")]
        public string ThumbIconFileName { get; set; } = null;

        /// <summary>
        /// Rank of this instance.
        /// </summary>
        [Bindable(false)]
        [XmlElement("rank")]
        public int Rank { get; set; } = -1;

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the ApplicationModuleSection class.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="module">The application module of the instance.</param>
        /// <param name="parent">The parent section of the instance.</param>
        public AppSection(
            String name,
            AppModule module = null,
            AppSection parent = null) : base(name, "section_")
        {
            this.Module = module;
            this.Parent = parent;
//            if (aAPPLICATIONMODULESECTIONRow != null)
//--            {
//                this.Module=module;
//                this._APPLICATIONMODULESECTIONRow = aAPPLICATIONMODULESECTIONRow;
//                this._Name = aAPPLICATIONMODULESECTIONRow.NAME;
//                this._Parent = parent;
//                this._Rank= aAPPLICATIONMODULESECTIONRow.RANK;

//                //                
//                this._CompleteName = this.GetKeyName(this);
//                this._Description = (aAPPLICATIONMODULESECTIONRow.IsDESCRIPTIONNull() ? "" : aAPPLICATIONMODULESECTIONRow.DESCRIPTION);
//                this._Title = new DictionaryDataItem(aAPPLICATIONMODULESECTIONRow.GLOBALLABELRow);

//                if (!aAPPLICATIONMODULESECTIONRow.IsICONFILENAMENull())
//                {
//                    this._IconFileName = aAPPLICATIONMODULESECTIONRow.ICONFILENAME.ToString();
//                    if (this._IconFileName.Length>4)
//                        this._ThumbIconFileName = this._IconFileName.Substring(1,this._IconFileName.Length-4) + "_s" +
//                            this._IconFileName.Substring(this._IconFileName.Length - 4);
//                }

//                // we estimate the section user rule
//                // that is by default the parent rule
//                // or for the first level, the module rule
//                Boolean moduleSectionUserPermissionValue = visitor.GetRightStatement().GetRuleValue(
//                    UserPermissionEntityKind.Section.ToString(),
//                    this.CompleteName,
//                    ActionKind.View.ToString());

//                UserPermission userPermission = visitor.GetRightStatement().GetRight(
//                    UserPermissionEntityKind.Section.ToString(),
//                    this.CompleteName);

//                if (userPermission == null)
//                {
//                    userPermission = new UserPermission(
//                        UserPermissionEntityKind.Section.ToString(),
//                        this.CompleteName);
//                    userPermission.AddRule(ActionKind.View.ToString(), moduleSectionUserPermissionValue);

//                    // we add the section user rule to the vistor
//                    visitor.GetRightStatement().Add(userPermission);
//                }

//                this._IsVisible = moduleSectionUserPermissionValue &
//                    ((aAPPLICATIONMODULESECTIONRow.IsISINDEXEDNull())||(aAPPLICATIONMODULESECTIONRow.ISINDEXED));

//                if ((module!=null) &&
//                    ((!Directory.Exists(visitor.AppService.GetPath(SphereAppService.ApplicationPathKind.AppFolder) + "\\" + this.CompleteName.Replace("$", "\\").ToLower())) &
//                    (remoteExecutionUri!=null)))
//                {
//                    String aUrlFolder = this._CompleteName;
//                    if (aUrlFolder.Contains("$"))
//                        aUrlFolder = aUrlFolder.Substring(aUrlFolder.IndexOf("$") + 1).Replace("$", "/").ToLower();
//                    this._CompleteName = "URL:" + remoteExecutionUri + "/" + aUrlFolder;
//                }

//                if (this._IsVisible)
//                {
//                    foreach (PlatformDataSet.APPLICATIONMODULESECTIONRow aChildAPPLICATIONMODULESECTIONRow in
//                        aAPPLICATIONMODULESECTIONRow.GetAPPLICATIONMODULESECTIONRows())
//                    {
//                        ApplicationModuleSection moduleSection = new ApplicationModuleSection(
//                            this.Module,
//                            aChildAPPLICATIONMODULESECTIONRow,
//                            this,
//                            visitor,
//                            remoteExecutionUri);
//                        if (moduleSection.IsVisible)
//                            this.SubSections.Add(moduleSection);
//                    }
//                }
//            }
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
            if (this.SubSections == null)
                this.SubSections = new DataItemSet<AppSection>();

            this.SubSections.Add(section);
            section.Parent = this;
            section.Module = this.Module;
        }

        /// <summary>
        /// Adds the specified sections.
        /// </summary>
        /// <param name="sections">The sections to add.</param>
        /// <returns>Returns the specified sections.</returns>
        public AppSection AddSections(params AppSection[] sections)
        {
            foreach (AppSection section in sections)
                this.AddSection(section);
            return this;
        }

        #endregion

        // ------------------------------------------
        // ACCESSORS
        // ------------------------------------------

        #region Accessors

        /// <summary>
        /// Returns the identifier key.
        /// </summary>
        public override string Key()
        {
            return (this.Parent == null ? this.Module?.Name + "$" : this.Parent.Name + "$" + this.Name).ToUpper();
        }

        /// <summary>
        /// Returns the sub section with the specified name.
        /// </summary>
        /// <param name="name">The name of the sub application sections to return.</param>
        /// <returns>The sub application section with the specified name.</returns>
        public AppSection GetSubSectionWithName(String name)
        {
            if (this.Name.KeyEquals(name))
            {
                return this;
            }
            else
            {
                AppSection section = null;

                if (this.SubSections != null)
                {
                    foreach (AppSection moduleSection in this.SubSections.Items)
                    {
                        if ((section = moduleSection.GetSubSectionWithName(name)) != null)
                        {
                            return section;
                        }
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// Returns the sub section with the specified unique name.
        /// </summary>
        /// <param name="key">The key of the sub application sections to return.</param>
        /// <returns>The sub application sections with the specified unique name.</returns>
        public AppSection GetSubSectionWithUniqueName(string key)
        {
            if (this.KeyEquals(key))
            {
                return this;
            }
            else
            {
                AppSection section = null;

                if (this.SubSections != null)
                {
                    foreach (AppSection moduleSection in this.SubSections.Items)
                    {
                        if ((section = moduleSection.GetSubSectionWithUniqueName(key)) != null)
                        {
                            return section;
                        }
                    }
                }
            }
            return null;
        }

        #endregion
    }
}