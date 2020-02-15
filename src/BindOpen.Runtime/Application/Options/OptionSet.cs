using BindOpen.Data.Elements;
using BindOpen.System.Diagnostics;
using System;
using System.Xml.Serialization;

namespace BindOpen.Application.Options
{
    /// <summary>
    /// This class represents a option set.
    /// </summary>
    [XmlType("OptionSet", Namespace = "https://bindopen.org/xsd")]
    [XmlRoot("optionSet", Namespace = "https://bindopen.org/xsd", IsNullable = false)]
    public class OptionSet : DataElementSet, IOptionSet
    {
        // -------------------------------------------------------------
        // CONSTRUCTORS
        // -------------------------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the OptionSet class.
        /// </summary>
        public OptionSet()
            : base()
        {
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
        /// Gets the value of the specified option.
        /// </summary>
        /// <param name="name">Name of the option to consider.</param>
        public object GetOptionValue(String name)
        {
            return this.GetElementObject(name);
        }

        #endregion

        // ------------------------------------------
        // MUTATORS
        // ------------------------------------------

        #region Mutators

        /// <summary>
        /// Updates this instance with the specified string value.
        /// </summary>
        /// <param name="stringValue">The string value to consider.</param>
        public IBdoLog Update(
            string stringValue)
        {
            var log = new BdoLog();

            if (!string.IsNullOrEmpty(stringValue))
            {
                foreach (String optionString in stringValue.Split(';'))
                {
                    if (optionString.Contains("="))
                    {
                        int index = optionString.IndexOf("=");
                        String optionName = optionString.Substring(0, index);
                        String optionValue = optionString.Substring(index + 1);
                        this.AddElementItem(optionName, optionValue, log);
                    }
                }
            }

            return log;
        }

        #endregion
    }
}
