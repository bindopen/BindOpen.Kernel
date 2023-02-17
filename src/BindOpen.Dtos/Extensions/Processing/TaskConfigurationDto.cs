using BindOpen.Runtime.Dtos.Extensions;
using System.Xml.Serialization;

namespace BindOpen.Extensions.Processing
{
    /// <summary>
    /// This class represents a task configuration.
    /// </summary>
    [XmlType("TaskConfiguration", Namespace = "https://xsd.bindopen.org")]
    [XmlRoot(ElementName = "task", Namespace = "https://xsd.bindopen.org", IsNullable = false)]
    public class TaskConfigurationDto : ExtensionItemConfigurationDto
    {
        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the BdoTaskConfigurationDto class.
        /// </summary>
        public TaskConfigurationDto()
        {
        }

        #endregion
    }
}