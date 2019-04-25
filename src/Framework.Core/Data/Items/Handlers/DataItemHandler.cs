using System.Xml.Schema;
using BindOpen.Framework.Core.Data.Helpers.Serialization;
using BindOpen.Framework.Core.System.Diagnostics;

namespace BindOpen.Framework.Core.Data.Items.Handlers
{
    /// <summary>
    /// This static class provides methods to handle data items.
    /// </summary>
    public static class DataItemHandler
    {
        // --------------------------------------------------
        // SERIALIZATION
        // --------------------------------------------------

        #region Serialization

        /// <summary>
        /// Gets the xml string of this instance.
        /// </summary>
        /// <param name="item">The item to consider.</param>
        /// <param name="log">The log to consider.</param>
        /// <returns>The Xml string of this instance.</returns>
        public static string ToXml(this DataItem item, ILog log = null)
        {
            if (item == null) return null;

            item.UpdateStorageInfo();
            return XmlHelper.ToXml(item, log);
        }

        /// <summary>
        /// Saves this instance to the specified file path.
        /// </summary>
        /// <param name="item">The item to consider.</param>
        /// <param name="filePath">The path of the file to save.</param>
        /// <param name="log">The log to consider.</param>
        /// <returns>True if the saving operation has been done. False otherwise.</returns>
        public static bool SaveXml(this DataItem item, string filePath, ILog log = null)
        {
            if (item == null) return false;

            item.UpdateStorageInfo(log);
            return XmlHelper.SaveXml(item, filePath, log);
        }

        /// <summary>
        /// Instantiates a new instance of ILog class from a xml file.
        /// </summary>
        /// <param name="filePath">The path of the Xml file to load.</param>
        /// <param name="loadLog">The output log of the load task.</param>
        /// <param name="xmlSchemaSet">The XML schema set to consider for checking.</param>
        /// <param name="mustFileExist">Indicates whether the file must exist.</param>
        /// <returns>The load log.</returns>
        public static T Load<T>(
            string filePath,
            ILog loadLog = null,
            XmlSchemaSet xmlSchemaSet = null,
            bool mustFileExist = true) where T : DataItem, new()
        {
            T dataItem = XmlHelper.Load<T>(filePath, loadLog, xmlSchemaSet, mustFileExist);
            dataItem?.UpdateRuntimeInfo(loadLog);

            return dataItem;
        }

        #endregion
    }
}
