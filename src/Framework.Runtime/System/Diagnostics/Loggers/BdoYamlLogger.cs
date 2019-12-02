using BindOpen.Framework.Core.Data.Elements;
using BindOpen.Framework.Core.Data.Helpers.Objects;
using BindOpen.Framework.Core.Data.Helpers.Strings;
using BindOpen.Framework.Core.Data.Items.Source;
using BindOpen.Framework.Core.System.Diagnostics;
using BindOpen.Framework.Core.System.Diagnostics.Loggers;
using System;
using System.Collections.Generic;

namespace BindOpen.Framework.Runtime.System.Diagnostics.Loggers
{
    /// <summary>
    /// This class represents a YAML logger.
    /// </summary>
    public class BdoYamlLogger : BdoLogger
    {
        // ------------------------------------------------------
        // VARIABLES
        // ------------------------------------------------------

        #region Variables

        private String _CurrentLogId = null;
        private String _CurrentNode = null;

        #endregion

        // ------------------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the YamlLogger class.
        /// </summary>
        public BdoYamlLogger()
        {
        }

        /// <summary>
        /// Instantiates a new instance of the YamlLogger class.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="mode">The mode to consider.</param>
        /// <param name="folderPath">The folder path to consider.</param>
        /// <param name="fileName">The file name to consider.</param>
        /// <param name="outputKind">The output kind to consider.</param>
        /// <param name="isVerbose">Indicates whether .</param>
        /// <param name="uiCulture">The folder path to consider.</param>
        /// <param name="eventFinder">The function that filters event.</param>
        /// <remarks>With expiration day number equaling to -1, no files expires. Equaling to 0, all files except the current one expires.</remarks>
        public BdoYamlLogger(
            string name,
            BdoLoggerMode mode,
            string folderPath,
            string fileName = null,
            DatasourceKind outputKind = DatasourceKind.Repository,
            bool isVerbose = false,
            string uiCulture = null,
            Predicate<IBdoLogEvent> eventFinder = null)
            : base(name, BdoLoggerFormat.Xml, mode, outputKind, isVerbose, uiCulture, folderPath, fileName, eventFinder)
        {
        }

        #endregion

        // ------------------------------------------
        // SERIALIZATION / UNSERIALIZATION
        // ------------------------------------------

        #region

        /// <summary>
        /// Gets the string representing to the specified log.
        /// </summary>
        /// <param name="log">The log to consider.</param>
        /// <param name="attributeNames">The attribute names to consider.</param>
        /// <returns>The string representing to the specified log.</returns>
        public override string ToString(
            IBdoLog log,
            List<string> attributeNames = null)
        {
            if (log == null) return "";

            String st = "";
            String indent = new string('\t', log.Level);

            if (attributeNames == null)
            {
                attributeNames = new List<string>() { "*" };
                attributeNames.Add("detail.*");
            }

            if (log != null)
            {
                if (log.Id.KeyEquals(this._CurrentLogId))
                    st +=
                        indent + "log:\n" +
                        indent + '\t' + "id: " + log.Id + Environment.NewLine;

                foreach (String attributeName in attributeNames)
                    if (attributeName != null)
                    {
                        String attributeNameKey = attributeName.ToLower();

                        if (((attributeNameKey == "*") || (attributeNameKey == BdoLogEntries.__Id))
                            && !string.IsNullOrEmpty(log.Id))
                        {
                            st += indent + " id: " + log.Name + Environment.NewLine;
                            this._CurrentNode = attributeNameKey;
                        }
                        if (((attributeNameKey == "*") || (attributeNameKey == BdoLogEntries.__Name))
                            && !string.IsNullOrEmpty(log.Name))
                        {
                            st += indent + " name: " + log.Name + Environment.NewLine;
                            this._CurrentNode = attributeNameKey;
                        }
                        if (((attributeNameKey == "*") || (attributeNameKey == BdoLogEntries.__Title))
                                        && (log.Title != null))
                        {
                            st += indent + " title: " + log.GetTitle(this.UICulture) + Environment.NewLine;
                            this._CurrentNode = attributeNameKey;
                        }
                        if (((attributeNameKey == "*") || (attributeNameKey == BdoLogEntries.__Description))
                                            && (log.Description != null))
                        {
                            st += indent + " description: " + log.GetDescription(this.UICulture) + Environment.NewLine;
                            this._CurrentNode = attributeNameKey;
                        }
                        if ((attributeNameKey.StartsWith(BdoLogEntries.__Detail + ".")) && (log.Detail != null))
                        {
                            st += indent + " detail: " + Environment.NewLine;
                            String detailName = attributeNameKey.GetSubstring(BdoLogEntries.__Detail.Length + 1);
                            if (detailName == "*")
                                foreach (DataElement element in log.Detail.Elements)
                                {
                                    st += this.ToString(element, indent);
                                    this._CurrentNode = attributeNameKey;
                                }
                        }

                        if (((attributeNameKey == "*") || (attributeNameKey == BdoLogEntries.__Events))
                                && (log.Events != null))
                            foreach (BdoLogEvent logEvent in log.Events)
                                st += this.ToString(logEvent, attributeNames);
                    }

                this._CurrentLogId = log.Id;
            }

            return st;
        }

        private String ToString(DataElement element, String indent = "")
        {
            String st = "";
            if (element != null)
            {
                st += indent + element.Key() + ":" + Environment.NewLine;
                if (element.Items.Count == 1)
                    st += indent + " value: " + element.Items[0]?.ToString(element.ValueType) + Environment.NewLine;
                if (element.Items.Count > 1)
                {
                    st += indent + " values:" + Environment.NewLine;
                    foreach (object item in element.Items)
                        if (item != null)
                            st += indent + "  - " + item.ToString(element.ValueType) + Environment.NewLine;
                }
            }

            return st;
        }

        ///// <summary>
        ///// Gets the string representing to the specified event.
        ///// </summary>
        ///// <param name="logEvent">The log event to consider.</param>
        ///// <param name="attributeNames">The attribute names to consider.</param>
        ///// <returns>The string representing to the specified event.</returns>
        //public override string ToString(
        //    LogEvent logEvent,
        //    params string[] attributeNames)
        //{
        //    if (log == null) return "";

        //    String indent = new string('\t', log.Level);

        //    String st =
        //        indent + "log:\n" +
        //        indent + '\t' + "id: " + log.Id + Environment.NewLine;

        //    foreach (String attributeKey in attributeNames)
        //        st += this.ToString(attributeKey, indent);

        //    return st;
        //}

        #endregion
    }
}
