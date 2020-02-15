using BindOpen.Data.Helpers.Objects;
using BindOpen.Data.Items;
using System;
using System.ComponentModel;
using System.Xml.Serialization;

namespace BindOpen.Application.Modules
{
    /// <summary>
    /// This class represents a visitor application module section.
    /// </summary>
    public class AppSection : DescribedDataItem, IAppSection
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
        public IAppModule Module { get; set; } = null;

        /// <summary>
        /// The section parent of this instance.
        /// </summary>
        [XmlIgnore()]
        public IAppSection Parent { get; set; } = null;

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
            string name,
            IAppModule module = null,
            IAppSection parent = null) : base(name, "section_")
        {
            this.Module = module;
            this.Parent = parent;
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
        public void AddSection(IAppSection section)
        {

            (this.SubSections ?? (this.SubSections = new DataItemSet<AppSection>())).Add(section as AppSection);
            section.Parent = this;
            section.Module = this.Module;
        }

        /// <summary>
        /// Adds the specified sections.
        /// </summary>
        /// <param name="sections">The sections to add.</param>
        /// <returns>Returns the specified sections.</returns>
        public IAppSection AddSections(params AppSection[] sections)
        {
            foreach (IAppSection section in sections)
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
        public IAppSection GetSubSectionWithName(String name)
        {
            if (this.Name.KeyEquals(name))
            {
                return this;
            }
            else
            {
                IAppSection section = null;

                if (this.SubSections != null)
                {
                    foreach (IAppSection moduleSection in this.SubSections.Items)
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
        public IAppSection GetSubSectionWithUniqueName(string key)
        {
            if (this.KeyEquals(key))
            {
                return this;
            }
            else
            {
                IAppSection section = null;

                if (this.SubSections != null)
                {
                    foreach (var moduleSection in this.SubSections.Items)
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