using BindOpen.Data.Common;
using BindOpen.Data.Elements;
using BindOpen.Data.Helpers.Objects;
using BindOpen.Data.Helpers.Strings;
using BindOpen.Data.Specification;
using BindOpen.Extensions.Runtime;
using System;
using System.Linq;
using System.Xml.Serialization;

namespace BindOpen.Application.Options
{
    /// <summary>
    /// This class represents an option specification.
    /// </summary>
    [XmlType("OptionSpec", Namespace = "https://bindopen.org/xsd")]
    [XmlRoot("optionSpec", Namespace = "https://bindopen.org/xsd", IsNullable = false)]
    public class OptionSpec : ScalarElementSpec, IOptionSpec
    {
        // -------------------------------------------------------------
        // CONSTRUCTORS
        // -------------------------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the OptionSpec class.
        /// </summary>
        public OptionSpec() : this(null)
        {
        }

        /// <summary>
        /// Instantiates a new instance of the OptionSpec class.
        /// </summary>
        /// <param name="aliases">Aliases of the option to add.</param>
        public OptionSpec(
            params string[] aliases) : this(OptionNameKind.OnlyName, aliases)
        {
        }

        /// <summary>
        /// Instantiates a new instance of the OptionSpec class.
        /// </summary>
        /// <param name="nameKind">The name kind to consider.</param>
        /// <param name="aliases">Aliases of the option to add.</param>
        public OptionSpec(
            OptionNameKind nameKind,
            params string[] aliases) : this(DataValueType.Text, RequirementLevel.Optional, OptionNameKind.OnlyName, aliases)
        {
        }

        /// <summary>
        /// Instantiates a new instance of the OptionSpec class.
        /// </summary>
        /// <param name="dataValueType">The value type to consider.</param>
        /// <param name="nameKind">The name kind to consider.</param>
        /// <param name="aliases">Aliases of the option to add.</param>
        public OptionSpec(
            DataValueType dataValueType,
            OptionNameKind nameKind,
            params string[] aliases) : this(dataValueType, RequirementLevel.Required, OptionNameKind.OnlyName, aliases)
        {
        }

        /// <summary>
        /// Instantiates a new instance of the OptionSpec class.
        /// </summary>
        /// <param name="requirementLevel">The requirement level of the entry to add.</param>
        /// <param name="aliases">Aliases of the option to add.</param>
        public OptionSpec(
            RequirementLevel requirementLevel,
            params string[] aliases) : this(
                DataValueType.Text,
                requirementLevel,
                aliases.Any(p => p?.Contains(StringHelper.__PatternEmptyValue) == true) ? OptionNameKind.NameWithValue : OptionNameKind.OnlyName,
                aliases)
        {
        }

        /// <summary>
        /// Instantiates a new instance of the OptionSpec class.
        /// </summary>
        /// <param name="requirementLevel">The requirement level of the entry to add.</param>
        /// <param name="nameKind">The name kind to consider.</param>
        /// <param name="aliases">Aliases of the option to add.</param>
        public OptionSpec(
            RequirementLevel requirementLevel,
            OptionNameKind nameKind,
            params string[] aliases) : this(DataValueType.Text, requirementLevel, nameKind, aliases)
        {
        }

        /// <summary>
        /// Instantiates a new instance of the OptionSpec class.
        /// </summary>
        /// <param name="dataValueType">The value type to consider.</param>
        /// <param name="requirementLevel">The requirement level of the entry to consider.</param>
        /// <param name="nameKind">The name kind to consider.</param>
        /// <param name="aliases">Aliases of the option to add.</param>
        public OptionSpec(
            DataValueType dataValueType,
            RequirementLevel requirementLevel,
            OptionNameKind nameKind,
            params string[] aliases) : base()
        {
            this.Aliases = (aliases ?? new string[1] { "{{*}}" }).ToList();
            this.MinimumItemNumber = nameKind.HasValue() ? 1 : 0;
            this.MaximumItemNumber = nameKind.HasName() ? 0 : 1;
            this.RequirementLevel = requirementLevel;
        }

        /// <summary>
        /// Instantiates a new instance of the OptionSpec class.
        /// </summary>
        /// <param name="type">The type to consider.</param>
        /// <param name="requirementLevel">The requirement level of the option to consider.</param>
        /// <param name="nameKind">The name kind to consider.</param>
        /// <param name="aliases">Aliases of the option to add.</param>
        public OptionSpec(
            Type type,
            RequirementLevel requirementLevel,
            OptionNameKind nameKind,
            params string[] aliases) : this(type.GetValueType(), requirementLevel, nameKind, aliases)
        {
            if (type?.IsEnum == true)
            {
                this.ConstraintStatement = new DataConstraintStatement();
                this.ConstraintStatement.AddConstraint(
                    null, "standard$" + KnownRoutineKind.ItemMustBeInList, new DataElementSet(
                        ElementFactory.CreateScalar(DataValueType.Text, type.GetFields().Select(p => p.Name).ToList().Cast<Object>())));
            }
        }

        #endregion

        // -------------------------------------------------------------
        // ACCESSORS
        // -------------------------------------------------------------

        #region Accessors

        /// <summary>
        /// Indicates whether the specified argument matches with this instance.
        /// </summary>
        /// <param name="argumentstring">The argument to consider.</param>
        /// <param name="aliasIndex">The alias index to consider. -2 not found. -1 Name matches. otherwise the index of matched alias.</param>
        /// <returns>Returns True if the specified matches this instance.</returns>
        public bool IsArgumentMarching(string argumentstring, out int aliasIndex)
        {
            aliasIndex = -2;
            if (argumentstring != null)
            {
                if (this.IsNameMatching(this.Name, argumentstring))
                {
                    aliasIndex = -1;
                }
                else if (this.Aliases != null)
                {
                    for (int i = 0; i < this.Aliases.Count; i++)
                    {
                        if (this.IsNameMatching(this.Aliases[i], argumentstring))
                        {
                            aliasIndex = i;
                            break;
                        }
                    }
                }
            }

            return aliasIndex > -2;
        }

        /// <summary>
        /// Indicates whether the specified argument matches with this instance.
        /// </summary>
        /// <param name="argumentstring">The argument to consider.</param>
        /// <returns>Returns True if the specified matches this instance.</returns>
        public bool IsArgumentMarching(string argumentstring)
        {
            int i = -2;
            return this.IsArgumentMarching(argumentstring, out i);
        }

        private bool IsNameMatching(string name1, string name2)
        {
            if ((name1 == null) || (name2 == null))
                return false;

            int i = name1.IndexOf(StringHelper.__PatternEmptyValue);
            if (i > -1)
            {
                name1 = name1.GetSubstring(0, i - 1);
                name2 = name2.GetSubstring(0, i - 1);
            }
            return name1.KeyEquals(name2);
        }

        #endregion
    }
}
