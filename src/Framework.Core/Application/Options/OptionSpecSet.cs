using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using BindOpen.Framework.Core.Data.Business.Conditions;
using BindOpen.Framework.Core.Data.Common;
using BindOpen.Framework.Core.Data.Elements;
using BindOpen.Framework.Core.Data.Helpers.Objects;
using BindOpen.Framework.Core.Data.Items.Sets;

namespace BindOpen.Framework.Core.Application.Options
{
    /// <summary>
    /// This class represents a option specification set.
    /// </summary>
    [XmlType("OptionSpecSet", Namespace = "http://meltingsoft.com/bindopen/xsd")]
    [XmlRoot("optionSpecSet", Namespace = "http://meltingsoft.com/bindopen/xsd", IsNullable = false)]
    public class OptionSpecSet : DataItemSet<OptionSpec>, IOptionSpecSet
    {
        // -------------------------------------------------------------
        // PROPERTIES
        // -------------------------------------------------------------

        #region Properties

        /// <summary>
        /// The name of this instance.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The index of this instance.
        /// </summary>
        public int Index { get; set; }

        /// <summary>
        /// The condition of this instance.
        /// </summary>
        public Condition Condition { get; set; }

        /// <summary>
        /// The sub sets of this instance.
        /// </summary>
        public List<OptionSpecSet> SubSets { get; set; } = new List<OptionSpecSet>();

        #endregion

        // -------------------------------------------------------------
        // CONSTRUCTORS
        // -------------------------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the OptionSpecSet class.
        /// </summary>
        public OptionSpecSet() : base()
        {
        }

        /// <summary>
        /// Instantiates a new instance of the OptionSpecSet class.
        /// </summary>
        /// <param name="optionSpecifications">The option specifications to consider.</param>
        public OptionSpecSet(params IOptionSpec[] optionSpecifications)
        {
            this.Items = optionSpecifications.ToList();
        }

        /// <summary>
        /// Instantiates a new instance of the OptionSpecSet class.
        /// </summary>
        /// <param name="condition">The condition to consider.</param>
        /// <param name="optionSpecifications">The option specifications to consider.</param>
        public OptionSpecSet(ICondition condition, params IOptionSpec[] optionSpecifications) : base(optionSpecifications)
        {
            this.Condition = condition;
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
        public string GetHelpText(String uiCulture = "*")
        {
            String helpText = this.Description.GetContent(uiCulture);

            foreach (DataElementSpec aElementSpec in this.Items)
            {
                foreach (String aAlias in aElementSpec.Aliases)
                    helpText += (helpText?.Length == 0 ? "" : ", ") + aAlias;
                helpText += ": " + aElementSpec.Description.GetContent(uiCulture) + "\r\n";
            }

            return helpText;
        }

        #endregion

        // -------------------------------------------------------------
        // MUTATORS
        // -------------------------------------------------------------

        #region Mutators

        // Clear ------------------------------------

        /// <summary>
        /// Clears the options.
        /// </summary>
        public void ClearOptions()
        {
            this.ClearItems();
        }

        // Add subset -------------------------------

        /// <summary>
        /// Adds the specified set of option specifications.
        /// </summary>
        /// <param name="subSet">The sub set to add.</param>
        public OptionSpecSet AddSubSet(IOptionSpecSet subSet)
        {
            this.SubSets?.Add(subSet);

            return this;
        }

        /// <summary>
        /// Adds a new set of option specifications.
        /// </summary>
        /// <param name="optionSpecifications">The option specifications to consider.</param>
        public OptionSpecSet AddSubSet(params IOptionSpec[] optionSpecifications)
        {
            this.SubSets?.Add(new OptionSpecSet(optionSpecifications));

            return this;
        }

        /// <summary>
        /// Instantiates a new instance of the OptionSpecSet class.
        /// </summary>
        /// <param name="condition">The condition to consider.</param>
        /// <param name="optionSpecifications">The option specifications to consider.</param>
        public OptionSpecSet AddSubSet(ICondition condition, params IOptionSpec[] optionSpecifications)
        {
            this.SubSets?.Add(new OptionSpecSet(condition, optionSpecifications));

            return this;
        }

        // Add option -------------------------------

        /// <summary>
        /// Adds a new option specification.
        /// </summary>
        /// <param name="aliases">Aliases of the option to add.</param>
        public OptionSpecSet AddOption(
            params string[] aliases)
        {
            return this.AddOption(OptionNameKind.OnlyValue, aliases);
        }

        /// <summary>
        /// Adds a new option specification.
        /// </summary>
        /// <param name="nameKind">The name kind to consider.</param>
        /// <param name="aliases">Aliases of the option to add.</param>
        public OptionSpecSet AddOption(
            OptionNameKind nameKind,
            params string[] aliases)
        {
            return this.AddOption(DataValueType.Text, RequirementLevel.Optional, OptionNameKind.OnlyValue, aliases);
        }

        /// <summary>
        /// Adds a new option specification.
        /// </summary>
        /// <param name="requirementLevel">The requirement level of the entry to add.</param>
        /// <param name="aliases">Aliases of the option to add.</param>
        public OptionSpecSet AddOption(
            RequirementLevel requirementLevel,
            params string[] aliases)
        {
            this.Add(new OptionSpec(requirementLevel, aliases));
            return this;
        }

        /// <summary>
        /// Adds a new option specification.
        /// </summary>
        /// <param name="dataValueType">The value type to consider.</param>
        /// <param name="nameKind">The name kind to consider.</param>
        /// <param name="aliases">Aliases of the option to add.</param>
        public OptionSpecSet AddOption(
            DataValueType dataValueType,
            OptionNameKind nameKind,
            params string[] aliases)
        {
            return this.AddOption(dataValueType, RequirementLevel.Required, OptionNameKind.OnlyValue, aliases);
        }

        /// <summary>
        /// Adds a new option specification.
        /// </summary>
        /// <param name="requirementLevel">The requirement level of the entry to add.</param>
        /// <param name="nameKind">The name kind to consider.</param>
        /// <param name="aliases">Aliases of the option to add.</param>
        public OptionSpecSet AddOption(
            RequirementLevel requirementLevel,
            OptionNameKind nameKind,
            params string[] aliases)
        {
            return this.AddOption(DataValueType.Text, requirementLevel, nameKind, aliases);
        }

        /// <summary>
        /// Adds a new option specification.
        /// </summary>
        /// <param name="dataValueType">The value type to consider.</param>
        /// <param name="requirementLevel">The requirement level of the entry to consider.</param>
        /// <param name="nameKind">The name kind to consider.</param>
        /// <param name="aliases">Aliases of the option to add.</param>
        public OptionSpecSet AddOption(
            DataValueType dataValueType,
            RequirementLevel requirementLevel,
            OptionNameKind nameKind,
            params string[] aliases)
        {
            this.Add(new OptionSpec(dataValueType, requirementLevel, nameKind, aliases));
            return this;
        }

        /// <summary>
        /// Adds a new option specification.
        /// </summary>
        /// <param name="type">The type to consider.</param>
        /// <param name="requirementLevel">The requirement level of the option to consider.</param>
        /// <param name="nameKind">The name kind to consider.</param>
        /// <param name="aliases">Aliases of the option to add.</param>
        public OptionSpecSet AddOption(
            Type type,
            RequirementLevel requirementLevel,
            OptionNameKind nameKind,
            params string[] aliases)
        {
            return this.AddOption(type.GetValueType(), requirementLevel, nameKind, aliases);
        }

        // Remove -----------------------------------

        /// <summary>
        /// Deletes the specified option.
        /// </summary>
        /// <param name="name">Name of the statement entry to remove.</param>
        public OptionSpecSet RemoveOption(String name)
        {
            this.Remove(name);

            return this;
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
        public bool HasOption(String name)
        {
            return this.HasItem(name);
        }

        /// <summary>
        /// Returns the item with the specified name.
        /// </summary>
        /// <param name="key">The name of the alias of the item to return.</param>
        /// <returns>Returns the item with the specified name.</returns>
        public override OptionSpec GetItem(string key)
        {
            if (key == null) return null;

            return this.Items.Find(p =>
                p.KeyEquals(key) || (p?.Aliases?.Any(q => q.KeyEquals(key)) == true));
        }

        #endregion
    }
}
