using BindOpen.Framework.Core.Data.Common;
using BindOpen.Framework.Core.Data.Elements;
using BindOpen.Framework.Core.Data.Elements.Scalar;
using BindOpen.Framework.Core.Data.Elements.Sets;
using BindOpen.Framework.Core.Data.Helpers.Objects;
using BindOpen.Framework.Core.Data.Helpers.Strings;
using BindOpen.Framework.Core.Data.Specification.Constraints;
using BindOpen.Framework.Core.Extensions.Configuration.Routines;
using System;
using System.Linq;
using System.Xml.Serialization;

namespace BindOpen.Framework.Core.Application.Options
{
    /// <summary>
    /// This class represents an option specification.
    /// </summary>
    [XmlType("OptionSpecification", Namespace = "http://meltingsoft.com/bindopen/xsd")]
    [XmlRoot("optionSpecification", Namespace = "http://meltingsoft.com/bindopen/xsd", IsNullable = false)]
    public class OptionSpecification : ScalarElementSpecification
    {

        // -------------------------------------------------------------
        // CONSTRUCTORS
        // -------------------------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the OptionSpecification class.
        /// </summary>
        public OptionSpecification() : this(null)
        {
        }

        /// <summary>
        /// Instantiates a new instance of the OptionSpecification class.
        /// </summary>
        /// <param name="name">Name of the statement entry to add.</param>
        /// <param name="aliases">Aliases of the statement entry to add.</param>
        public OptionSpecification(
            String name,
            params String[] aliases) : this(name, RequirementLevel.Forbidden, RequirementLevel.Optional, aliases)
        {
        }

        /// <summary>
        /// Instantiates a new instance of the OptionSpecification class.
        /// </summary>
        /// <param name="name">Name of the statement entry to add.</param>
        /// <param name="valueRequirementLevel">The requirement level of the value to add.</param>
        /// <param name="aliases">Aliases of the statement entry to add.</param>
        public OptionSpecification(
            String name,
            RequirementLevel valueRequirementLevel,
            params String[] aliases) : this(name, valueRequirementLevel, RequirementLevel.Optional, aliases)
        {
        }

        /// <summary>
        /// Instantiates a new instance of the OptionSpecification class.
        /// </summary>
        /// <param name="name">Name of the statement entry to add.</param>
        /// <param name="dataValueType">The value type to consider.</param>
        /// <param name="valueRequirementLevel">The requirement level of the value to add.</param>
        /// <param name="aliases">Aliases of the statement entry to add.</param>
        public OptionSpecification(
            String name,
            DataValueType dataValueType,
            RequirementLevel valueRequirementLevel,
            params String[] aliases) : this(name, dataValueType, valueRequirementLevel, RequirementLevel.Optional, aliases)
        {
        }

        /// <summary>
        /// Instantiates a new instance of the OptionSpecification class.
        /// </summary>
        /// <param name="name">Name of the statement entry to add.</param>
        /// <param name="valueRequirementLevel">The requirement level of the value to add.</param>
        /// <param name="optionRequirementLevel">The requirement level of the entry to add.</param>
        /// <param name="aliases">Aliases of the statement entry to add.</param>
        public OptionSpecification(
            String name,
            RequirementLevel valueRequirementLevel,
            RequirementLevel optionRequirementLevel,
            params String[] aliases) : this(name, DataValueType.Text, valueRequirementLevel, optionRequirementLevel, aliases)
        {
        }

        /// <summary>
        /// Instantiates a new instance of the OptionSpecification class.
        /// </summary>
        /// <param name="name">Name of the statement entry to add.</param>
        /// <param name="dataValueType">The value type to consider.</param>
        /// <param name="valueRequirementLevel">The requirement level of the value to consider.</param>
        /// <param name="optionRequirementLevel">The requirement level of the entry to consider.</param>
        /// <param name="aliases">Aliases of the statement entry to add.</param>
        public OptionSpecification(
            String name,
            DataValueType dataValueType,
            RequirementLevel valueRequirementLevel,
            RequirementLevel optionRequirementLevel,
            params String[] aliases) : base()
        {
            this.Name = name;
            this.Aliases = (aliases ?? new String[0]).ToList();
            this.MinimumItemNumber = (valueRequirementLevel == RequirementLevel.Required ? 1 : 0);
            this.MaximumItemNumber = (valueRequirementLevel == RequirementLevel.Forbidden ? 0 : 1);
            this.RequirementLevel = optionRequirementLevel;
        }

        /// <summary>
        /// Instantiates a new instance of the OptionSpecification class.
        /// </summary>
        /// <param name="name">Name of the statement entry to add.</param>
        /// <param name="valueRequirementLevel">The requirement level of the value to consider.</param>
        /// <param name="optionRequirementLevel">The requirement level of the entry to consider.</param>
        /// <param name="type">The type to consider.</param>
        /// <param name="aliases">Aliases of the statement entry to add.</param>
        public OptionSpecification(
            String name,
            Type type,
            RequirementLevel valueRequirementLevel,
            RequirementLevel optionRequirementLevel,
            params String[] aliases) : this(name, type.GetValueType(), valueRequirementLevel, optionRequirementLevel, aliases)
        {
            if (type != null && type.IsEnum)
            {
                this.ConstraintStatement = new DataConstraintStatement();
                this.ConstraintStatement.AddConstraint(
                    null, "standard$" + BasicRoutineKind.ItemMustBeInList, new DataElementSet(
                        DataElement.Create(type.GetFields().Select(p => p.Name).ToList().Cast<Object>(), DataValueType.Text)));
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
        /// <param name="argumentString">The argument to consider.</param>
        /// <param name="aliasIndex">The alias index to consider. -2 not found. -1 Name matches. otherwise the index of matched alias.</param>
        /// <returns>Returns True if the specified matches this instance.</returns>
        public Boolean IsArgumentMarching(string argumentString, out int aliasIndex)
        {
            aliasIndex = -2;
            if (argumentString != null)
                if (this.IsNameMatching(this.Name, argumentString))
                    aliasIndex = -1;
                else if (this.Aliases != null)
                    for (int i = 0; i<this.Aliases.Count; i++)
                        if (this.IsNameMatching(this.Aliases[i], argumentString))
                        {
                            aliasIndex = i;
                            break;
                        }

            return aliasIndex > -2;
        }

        /// <summary>
        /// Indicates whether the specified argument matches with this instance.
        /// </summary>
        /// <param name="argumentString">The argument to consider.</param>
        /// <returns>Returns True if the specified matches this instance.</returns>
        public Boolean IsArgumentMarching(string argumentString)
        {
            int i = -2;
            return this.IsArgumentMarching(argumentString, out i);
        }

        private Boolean IsNameMatching(String name1, String name2)
        {
            if ((name1 == null) || (name2 == null))
                return false;

            int i = name1.IndexOf(StringHelper.__PatternEmptyValue);
            if (i > -1)
            {
                name1 = name1.GetSubstring(0, i-1);
                name2 = name2.GetSubstring(0, i - 1);
            }
            return name1.KeyEquals(name2);
        }

        #endregion

    }
}
