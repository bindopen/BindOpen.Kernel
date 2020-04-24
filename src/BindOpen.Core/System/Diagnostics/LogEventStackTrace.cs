using System.Xml.Serialization;

namespace BindOpen.System.Diagnostics
{
    /// <summary>
    /// This structures defines the stack trace of a task result.
    /// </summary>
    [XmlType("LogEventStackTrace", Namespace = "https://bindopen.org/xsd")]
    public class LogEventStackTrace
    {
        /// <summary>
        /// The name of the full class.
        /// </summary>
        [XmlElement("fullClassName")]
        public string FullClassName;

        /// <summary>
        /// The name of the called method.
        /// </summary>
        [XmlElement("methodName")]
        public string MethodName;

        /// <summary>
        /// Parameters of the called method.
        /// </summary>
        [XmlElement("methodParams")]
        public string MethodParams;

        /// <summary>
        /// Path of the called file.
        /// </summary>
        [XmlElement("filePath")]
        public string FilePath;

        /// <summary>
        /// Called line number.
        /// </summary>
        [XmlElement("lineNumber")]
        public string LineNumber;

        public LogEventStackTrace()
        {
        }
    }
}
