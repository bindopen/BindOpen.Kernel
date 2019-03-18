using System;
using System.Xml.Serialization;

namespace BindOpen.Framework.Core.System.Diagnostics.Loggers
{

    /// <summary>
    /// This enumeration lists the possible log attributes.
    /// </summary>
    public static class LogAttribute
    {
        /// <summary>
        /// The ID of the log.
        /// </summary>
        public const String __Id = "id";
        /// <summary>
        /// The name of the log.
        /// </summary>
        public const String __Name = "name";
        /// <summary>
        /// The title of the log.
        /// </summary>
        public const String __Title = "title";
        /// <summary>
        /// The description of the log.
        /// </summary>
        public const String __Description = "description";
        /// <summary>
        /// The detail of the log.
        /// </summary>
        public const String __Detail = "detail";
        /// <summary>
        /// The events of the log.
        /// </summary>
        public const String __Events = "events";
        /// <summary>
        /// The ID of the task of the log.
        /// </summary>
        public const String __Task_Id = "task.id";
        /// <summary>
        /// The name of the task of the log.
        /// </summary>
        public const String __Task_Name = "task.name";
        /// <summary>
        /// The title of the task of the log.
        /// </summary>
        public const String __Task_Title = "task.title";
        /// <summary>
        /// The description of the task of the log.
        /// </summary>
        public const String __Task_Description = "task.description";
        /// <summary>
        /// The maximum index of the task of the log.
        /// </summary>
        public const String __Task_MaximumIndex = "task.maximumIndex";
        /// <summary>
        /// The detail of the task of the log.
        /// </summary>
        public const String __Task_Detail = "task.detail";
        /// <summary>
        /// The input detail of the task of the log.
        /// </summary>
        public const String __Task_InputDetail = "task.inputDetail";
        /// <summary>
        /// The output detail of the task of the log.
        /// </summary>
        public const String __Task_OutputDetail = "task.outputDetail";
        /// <summary>
        /// The detail of the process execution of the log.
        /// </summary>
        public const String __ProcessExecution_Detail = "processExecution.detail";
        /// <summary>
        /// The location of the process execution of the log.
        /// </summary>
        public const String __ProcessExecution_Location = "processExecution.location";
        /// <summary>
        /// The status of the process execution of the log.
        /// </summary>
        public const String __ProcessExecution_Status = "processExecution.status";
        /// <summary>
        /// The state of the process execution of the log.
        /// </summary>
        public const String __ProcessExecution_State = "processExecution.state";
        /// <summary>
        /// The custom status of the process execution of the log.
        /// </summary>
        public const String __ProcessExecution_CustomStatus = "processExecution.customStatus";
        /// <summary>
        /// The progress index of the process execution of the log.
        /// </summary>
        public const String __ProcessExecution_ProgressIndex = "processExecution.progressIndex";
        /// <summary>
        /// The progress maximum of the process execution of the log.
        /// </summary>
        public const String __ProcessExecution_ProgressMax = "processExecution.progressMax";
        /// <summary>
        /// The start date of the process execution of the log.
        /// </summary>
        public const String __ProcessExecution_StartDate = "processExecution.startDate";
        /// <summary>
        /// The restart date of the process execution of the log.
        /// </summary>
        public const String __ProcessExecution_RestartDate = "processExecution.restartDate";
        /// <summary>
        /// The end date of the process execution of the log.
        /// </summary>
        public const String __ProcessExecution_EndDate = "processExecution.endDate";
        /// <summary>
        /// The duration of the process execution of the log.
        /// </summary>
        public const String __ProcessExecution_Duration = "processExecution.duration";
        /// <summary>
        /// The result level of the process execution of the log.
        /// </summary>
        public const String __ProcessExecution_ResultLevel = "processExecution.resultLevel";
    };

}
