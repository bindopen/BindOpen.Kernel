using BindOpen.Application.Options;
using BindOpen.Data.Common;
using BindOpen.Data.Elements;
using BindOpen.Data.Helpers.Objects;
using BindOpen.Data.Helpers.Strings;
using BindOpen.Data.Specification;
using BindOpen.System.Diagnostics;
using System.Linq;

namespace BindOpen.Application.Arguments
{
    /// <summary>
    /// This class represents the application argument parser.
    /// </summary>
    public static class AppArgumentHelper
    {
        // ------------------------------------------
        // ACCESSORS
        // ------------------------------------------

        #region Accessors

        // Arguments --------------------------------

        /// <summary>
        /// Retrieves the arguments from the specified argument string values.
        /// </summary>
        /// <param name="arguments">The argument string values to consider.</param>
        /// <param name="optionSpecificationSet">The option specification set to consider.</param>
        /// <param name="allowMissingItems">Indicates whether the items can be missing.</param>
        /// <param name="log">The log to consider.</param>
        /// <returns>Returns the log of argument building.</returns>
        public static IOptionSet UpdateOptions(
            this string[] arguments,
            IOptionSpecSet optionSpecificationSet,
            bool allowMissingItems = false,
            IBdoLog log = null)
        {
            IOptionSet optionSet = new OptionSet();

            int index = 0;
            if (arguments != null)
            {
                while (index < arguments.Length)
                {
                    string currentArgumentString = arguments[index];

                    if (currentArgumentString != null)
                    {
                        IScalarElement argument = null;

                        OptionSpec argumentSpecification = null;

                        int aliasIndex = -2;
                        if (optionSpecificationSet != null)
                        {
                            argumentSpecification = optionSpecificationSet.Items
                               .Find(p => p.IsArgumentMarching(currentArgumentString, out aliasIndex))
                               as OptionSpec;
                        }

                        if (optionSpecificationSet == null || (argumentSpecification == null && allowMissingItems))
                        {
                            argument = ElementFactory.CreateScalar(currentArgumentString, DataValueType.Text);
                            argument.SetItem(arguments.GetStringAtIndex(index));
                            optionSet.AddElement(argument);
                        }
                        else if (argumentSpecification != null && optionSpecificationSet != null)
                        {
                            if (argumentSpecification.ValueType == DataValueType.Any)
                                argumentSpecification.ValueType = DataValueType.Text;
                            argument = ElementFactory.CreateScalar(argumentSpecification.Name, null, argumentSpecification.ValueType, argumentSpecification);

                            argument.Specification = argumentSpecification;
                            if (argumentSpecification.ItemRequirementLevel.IsPossible())
                            {
                                if (argumentSpecification.Name.Contains(StringHelper.__PatternEmptyValue))
                                {
                                    argument.Name = argumentSpecification.Name.GetSubstring(0, argumentSpecification.Name.Length - StringHelper.__PatternEmptyValue.Length - 2);

                                    int valueIndex = -1;
                                    if (aliasIndex == -1)
                                        valueIndex = argumentSpecification.Name.IndexOf(StringHelper.__PatternEmptyValue);
                                    else if (aliasIndex > -1)
                                        valueIndex = argumentSpecification.Aliases[aliasIndex].IndexOf(StringHelper.__PatternEmptyValue);

                                    argument.SetItem(valueIndex < 0 ? "" : currentArgumentString.GetSubstring(valueIndex));
                                }
                                else
                                {
                                    index++;
                                    if (index < arguments.Length)
                                        argument.SetItem(arguments.GetStringAtIndex(index));
                                }
                            }

                            optionSet.AddElement(argument);
                        }
                    }
                    index++;
                }
            }

            optionSet.Check(optionSpecificationSet).AddEventsTo(log);

            return optionSet;
        }

        /// <summary>
        /// Checks this instance.
        /// </summary>
        /// <param name="optionSet">The set of options to consider.</param>
        /// <param name="optionSpecificationSet">The set of option specifications to consider.</param>
        /// <param name="allowMissingItems">Indicates whether the items can be missing.</param>
        /// <returns>Returns the log of check.</returns>
        public static IBdoLog Check(this IDataElementSet optionSet,
            IOptionSpecSet optionSpecificationSet,
            bool allowMissingItems = false)
        {
            var log = new BdoLog();

            if (optionSet?.Elements != null && optionSpecificationSet != null)
            {
                if (!allowMissingItems)
                {
                    foreach (IDataSpecification optionSpecification in optionSpecificationSet.Items.Where(p => p.RequirementLevel == RequirementLevel.Required))
                    {
                        if (!optionSet.HasItem(optionSpecification.Name))
                            log.AddError("Option '" + optionSpecification.Name + "' missing");
                    }
                }

                foreach (IScalarElement option in optionSet.Items)
                {
                    if (option?.Specification != null)
                    {
                        switch (option.Specification.ItemRequirementLevel)
                        {
                            case RequirementLevel.Required:
                                if (string.IsNullOrEmpty(option.Items[0] as string))
                                    log.AddError("Option '" + option.Name + "' requires value");
                                break;
                            case RequirementLevel.Forbidden:
                                if (!string.IsNullOrEmpty(option.Items[0] as string))
                                    log.AddError("Option '" + option.Name + "' does not allow value");
                                break;
                        }
                    }
                }
            }

            return log;
        }

        #endregion
    }
}