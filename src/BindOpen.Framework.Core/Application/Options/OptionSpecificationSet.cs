using System;
using System.Linq;
using System.Xml.Serialization;
using BindOpen.Framework.Core.Data.Common;
using BindOpen.Framework.Core.Data.Elements;
using BindOpen.Framework.Core.Data.Helpers.Objects;
using BindOpen.Framework.Core.Data.Items.Sets;

namespace BindOpen.Framework.Core.Application.Options
{
    /// <summary>
    /// This class represents a option specification set.
    /// </summary>
    [XmlType("OptionSpecificationSet", Namespace = "http://meltingsoft.com/bindopen/xsd")]
    [XmlRoot("optionSpecificationSet", Namespace = "http://meltingsoft.com/bindopen/xsd", IsNullable = false)]
    public class OptionSpecificationSet : DataItemSet<OptionSpecification>
    {

        // -------------------------------------------------------------
        // CONSTRUCTORS
        // -------------------------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the OptionSpecificationSet class.
        /// </summary>
        public OptionSpecificationSet()
            : base()
        {
        }

        /// <summary>
        /// Instantiates a new instance of the OptionSpecificationSet class.
        /// </summary>
        /// <param name="optionSpecifications">The option specifications to consider.</param>
        public OptionSpecificationSet(params OptionSpecification[] optionSpecifications)
        {
            this.Items = optionSpecifications.ToList();
        }

        #endregion


        // -------------------------------------------------------------
        // ACCESSORS
        // -------------------------------------------------------------

        #region Accessors

        /// <summary>
        /// Gets the help text.
        /// </summary>
        /// <param name="uiCulture">The UI culture to consider.</param>
        /// <returns>Returns the help text.</returns>
        public String GetHelpText(String uiCulture = "*")
        {
            String helpText = this.Description.GetContent(uiCulture);

            foreach (DataElementSpecification aElementSpecification in this.Items)
            {
                foreach(String aAlias in aElementSpecification.Aliases)
                    helpText += (helpText == String.Empty ? "" : ", ") + aAlias;
                helpText += ": " + aElementSpecification.Description.GetContent(uiCulture) + "\r\n";
            }

            return helpText;
        }

        #endregion



        // -------------------------------------------------------------
        // MUTATORS
        // -------------------------------------------------------------

        #region Mutators

        /// <summary>
        /// Clears the options.
        /// </summary>
        public void ClearOptions()
        {
            this.ClearItems();
        }

        /// <summary>
        /// Adds a new option that does not allow value.
        /// </summary>
        /// <param name="name">Name of the statement entry to add.</param>
        /// <param name="aliases">Aliases of the statement entry to add.</param>
        public OptionSpecification AddOption(
            String name,
            params String[] aliases)
        {
            return this.AddOption(name, DataValueType.Text, RequirementLevel.Optional, RequirementLevel.Optional, aliases);
        }

        /// <summary>
        /// Adds a new option that does not allow value.
        /// </summary>
        /// <param name="name">Name of the statement entry to add.</param>
        /// <param name="dataValueType">The data value type to consider.</param>
        /// <param name="aliases">Aliases of the statement entry to add.</param>
        public OptionSpecification AddOption(
            String name,
            DataValueType dataValueType,
            params String[] aliases)
        {
            return this.AddOption(name, dataValueType, RequirementLevel.Optional, RequirementLevel.Optional, aliases);
        }

        /// <summary>
        /// Adds a new option that does not allow value.
        /// </summary>
        /// <param name="name">Name of the statement entry to add.</param>
        /// <param name="type">The type to consider.</param>
        /// <param name="aliases">Aliases of the statement entry to add.</param>
        public OptionSpecification AddOption(
            String name,
            Type type,
            params String[] aliases)
        {
            return this.AddOption(name, type, RequirementLevel.Optional, RequirementLevel.Optional, aliases);
        }

        /// <summary>
        /// Adds a new option.
        /// </summary>
        /// <param name="name">Name of the statement entry to add.</param>
        /// <param name="itemRequirementLevel">The requirement level of the item to add.</param>
        /// <param name="aliases">Aliases of the statement entry to add.</param>
        public OptionSpecification AddOption(
            String name,
            RequirementLevel itemRequirementLevel,
            params String[] aliases)
        {
            return this.AddOption(name, DataValueType.Text, itemRequirementLevel, RequirementLevel.Optional, aliases);
        }

        /// <summary>
        /// Adds a new option.
        /// </summary>
        /// <param name="name">Name of the statement entry to add.</param>
        /// <param name="dataValueType">The data value type to consider.</param>
        /// <param name="itemRequirementLevel">The requirement level of the item to add.</param>
        /// <param name="aliases">Aliases of the statement entry to add.</param>
        public OptionSpecification AddOption(
            String name,
            DataValueType dataValueType,
            RequirementLevel itemRequirementLevel,
            params String[] aliases)
        {
            return this.AddOption(name, dataValueType, itemRequirementLevel, RequirementLevel.Optional, aliases);
        }

        /// <summary>
        /// Adds a new option.
        /// </summary>
        /// <param name="name">Name of the statement entry to add.</param>
        /// <param name="type">The type to consider.</param>
        /// <param name="itemRequirementLevel">The requirement level of the item to add.</param>
        /// <param name="aliases">Aliases of the statement entry to add.</param>
        public OptionSpecification AddOption(
            String name,
            Type type,
            RequirementLevel itemRequirementLevel,
            params String[] aliases)
        {
            return this.AddOption(name, type, itemRequirementLevel, RequirementLevel.Optional, aliases);
        }

        /// <summary>
        /// Adds a new option.
        /// </summary>
        /// <param name="name">Name of the statement entry to add.</param>
        /// <param name="itemRequirementLevel">The requirement level of the item to add.</param>
        /// <param name="optionRequirementLevel">The requirement level of the entry to add.</param>
        /// <param name="aliases">Aliases of the statement entry to add.</param>
        public OptionSpecification AddOption(
            String name,
            RequirementLevel itemRequirementLevel,
            RequirementLevel optionRequirementLevel,
            params String[] aliases)
        {
            return this.AddOption(name, DataValueType.Text, itemRequirementLevel, optionRequirementLevel, aliases);
        }

        /// <summary>
        /// Adds a new option.
        /// </summary>
        /// <param name="name">Name of the statement entry to add.</param>
        /// <param name="dataValueType">The data value type to consider.</param>
        /// <param name="itemRequirementLevel">The requirement level of the item to add.</param>
        /// <param name="optionRequirementLevel">The requirement level of the entry to add.</param>
        /// <param name="aliases">Aliases of the statement entry to add.</param>
        public OptionSpecification AddOption(
            String name,
            DataValueType dataValueType,
            RequirementLevel itemRequirementLevel,
            RequirementLevel optionRequirementLevel,
            params String[] aliases)
        {
            OptionSpecification optionSpecification = new OptionSpecification(name, dataValueType, itemRequirementLevel, optionRequirementLevel, aliases);
            this.Add(optionSpecification);
            return optionSpecification;
        }

        /// <summary>
        /// Adds a new option.
        /// </summary>
        /// <param name="name">Name of the statement entry to add.</param>
        /// <param name="type">The type to consider.</param>
        /// <param name="itemRequirementLevel">The requirement level of the item to add.</param>
        /// <param name="optionRequirementLevel">The requirement level of the entry to add.</param>
        /// <param name="aliases">Aliases of the statement entry to add.</param>
        public OptionSpecification AddOption(
            String name,
            Type type,
            RequirementLevel itemRequirementLevel,
            RequirementLevel optionRequirementLevel,
            params String[] aliases)
        {
            OptionSpecification optionSpecification = new OptionSpecification(name, type, itemRequirementLevel, optionRequirementLevel, aliases);
            this.Add(optionSpecification);
            return optionSpecification;
        }

        /// <summary>
        /// Deletes the specified option.
        /// </summary>
        /// <param name="name">Name of the statement entry to remove.</param>
        public void RemoveOption(String name)
        {
            this.Remove(name);
        }

        #endregion


        // ------------------------------------------
        // ACCESSORS
        // ------------------------------------------

        #region Accessors

        /// <summary>
        /// Indicates whether this instance has the specified option.
        /// </summary>
        /// <param name="name">Name of the option to consider.</param>
        public Boolean HasOption(String name)
        {
            return this.HasItem(name);
        }

        /// <summary>
        /// Returns the item with the specified name.
        /// </summary>
        /// <param name="uniqueName">The name of the item to return.</param>
        /// <returns>Returns the item with the specified name.</returns>
        public override OptionSpecification GetItem(String uniqueName)
        {
            if (uniqueName == null) return null;
            return this.Items.FirstOrDefault(p =>
                p.KeyEquals(uniqueName)
                || (p != null && p.Aliases != null &&
                    p.Aliases.Any(q => q.KeyEquals(uniqueName))));
        }

        #endregion

    }
}
