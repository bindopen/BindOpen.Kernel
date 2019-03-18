using System;
using System.Xml.Serialization;

namespace BindOpen.Framework.Core.System.Diagnostics.Loggers
{

    /// <summary>
    /// This enumeration lists the possible log event attributes.
    /// </summary>
    public static class LogEventAttribute
    {
        /// <summary>
        /// The ID of the log.
        /// </summary>
        public static String __Id = "id";
        /// <summary>
        /// The name of the log.
        /// </summary>
        public static String __Name = "name";
        /// <summary>
        /// The title of the log.
        /// </summary>
        public static String __Title = "title";
        /// <summary>
        /// The description of the log.
        /// </summary>
        public static String __Description = "description";
        /// <summary>
        /// The detail of the log.
        /// </summary>
        public static String __ResultCode = "resultCode";
        /// <summary>
        /// The ID of the task of the log.
        /// </summary>
        public static String __Source = "source";
        /// <summary>
        /// The name of the task of the log.
        /// </summary>
        public static String __StackTrace = "stackTrace";
    };

}
