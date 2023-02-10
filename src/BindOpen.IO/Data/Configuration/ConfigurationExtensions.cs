using BindOpen.Data.Meta;
using BindOpen.Logging;
using BindOpen.Runtime.Scopes;
using System.Xml.Schema;

namespace BindOpen.Data.Configuration
{
    /// <summary>
    /// This static class provides methods to handle configs.
    /// </summary>
    public static class ConfigurationExtensions
    {
        /// <summary>
        /// Instantiates a new instance of Configuration class from a xml file.
        /// </summary>
        /// <param name="filePath">The file path to consider.</param>
        /// <param name="scope">The scope to consider.</param>
        /// <param name="varSet">The set of script variables to consider.</param>
        /// <param name="log">The log to consider.</param>
        /// <param name="xmlSchemaSet">The XML schema set to consider for checking.</param>
        /// <param name="mustFileExist">Indicates whether the file must exist.</param>
        /// <param name="isRuntimeUpdated">Indicates whether the runtime is updated.</param>
        /// <returns>The Xml operation project defined in the Xml file.</returns>
        public static TPoco LoadXml<TPoco, TDto>(
            string filePath,
            IBdoScope scope = null,
            IBdoMetaList varSet = null,
            IBdoLog log = null,
            XmlSchemaSet xmlSchemaSet = null,
            bool mustFileExist = true)
            where TPoco : BdoConfiguration, new()
            where TDto : ConfigurationDto, new()
        {
            TPoco unionConfiguration = new();

            if (XmlHelper.LoadXml<TDto>(filePath, log, xmlSchemaSet, mustFileExist) is TDto topConfigurationDto)
            {
                var topConfiguration = topConfigurationDto?.ToPoco();

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
