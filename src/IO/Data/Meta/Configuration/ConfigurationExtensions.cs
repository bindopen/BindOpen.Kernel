using BindOpen.System.Logging;
using BindOpen.System.Scoping;
using System.Xml.Schema;

namespace BindOpen.System.Data.Meta
{
    /// <summary>
    /// This static class provides methods to handle configs.
    /// </summary>
    public static class ConfigurationExtensions
    {
        /// <summary>
        /// Instantiates a new instance of Configuration class from a xml file.
        /// </summary>
        /// <param key="filePath">The file path to consider.</param>
        /// <param key="scope">The scope to consider.</param>
        /// <param key="varSet">The set of script variables to consider.</param>
        /// <param key="log">The log to consider.</param>
        /// <param key="xmlSchemaSet">The XML schema set to consider for checking.</param>
        /// <param key="mustFileExist">Indicates whether the file must exist.</param>
        /// <param key="isRuntimeUpdated">Indicates whether the runtime is updated.</param>
        /// <returns>The Xml operation project defined in the Xml file.</returns>
        public static TPoco LoadXml<TPoco, TDto>(
            string filePath,
            IBdoScope scope = null,
            IBdoLog log = null,
            XmlSchemaSet xmlSchemaSet = null,
            bool mustFileExist = true)
            where TPoco : BdoConfiguration, new()
            where TDto : ConfigurationDto, new()
        {
            TPoco unionConfiguration = new();

            if (XmlHelper.LoadXml<TDto>(filePath, log, xmlSchemaSet, mustFileExist) is TDto topConfigurationDto)
            {
                //var topConfiguration = topConfigurationDto?.ToPoco();

                //foreach (string usingFilePath in topConfiguration.)
                //{
                //    string completeUsingFilePath = (usingFilePath.Contains(':') ?
                //        usingFilePath :
                //        Path.GetDirectoryName(filePath).EndingWith(@"\") + usingFilePath).ToPath();
                //    if (LoadXml<T>(completeUsingFilePath, scope, varSet, log, xmlSchemaSet, mustFileExist) is T usingConfiguration)
                //        unionConfiguration.Update(usingConfiguration);
                //}
            }

            return unionConfiguration;
        }
    }
}
