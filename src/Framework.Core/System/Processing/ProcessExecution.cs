using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Xml;
using System.Xml.Serialization;
using BindOpen.Framework.Core.Data.Common;
using BindOpen.Framework.Core.Data.Elements.Sets;
using BindOpen.Framework.Core.Data.Helpers.Objects;
using BindOpen.Framework.Core.Data.Helpers.Strings;
using BindOpen.Framework.Core.Data.Items;
using BindOpen.Framework.Core.System.Diagnostics;
using BindOpen.Framework.Core.System.Diagnostics.Events;

namespace BindOpen.Framework.Core.System.Processing
{
    /// <summary>
    /// This class represents the process execution.
    /// </summary>
    [Serializable()]
    [XmlType("ProcessExecution", Namespace = "http://meltingsoft.com/bindopen/xsd")]
    [XmlRoot(ElementName = "processExecution", Namespace = "http://meltingsoft.com/bindopen/xsd", IsNullable = false)]
    public class ProcessExecution : DataItem, IProcessExecution
    {
        // ------------------------------------------
        // VARIABLES
        // ------------------------------------------

        #region Variables

        private readonly Log _Log = null;

        private ProcessExecutionStatus _Status = ProcessExecutionStatus.None;
        private ProcessExecutionState _State = ProcessExecutionState.None;
        private String _CustomStatus = null;
        private float _ProgressIndex = 0;
        private float _ProgressMax = 100;

        private String _StartDate = null;
        private String _RestartDate = null;
        private String _EndDate = null;
        private String _Duration = null;
        private int _ResultLevel = 0;   // Estimated by the programmer. Over a certain number the result could be considered as satisfying.

        #endregion

        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// Detail of this instance.
        /// </summary>
        [XmlElement("detail")]
        public IDataElementSet Detail { get; set; } = new DataElementSet();

        /// <summary>
        /// Specification of the PropertyDetail property of this instance.
        /// </summary>
        [XmlIgnore()]
        public bool DetailSpecified
        {
            get
            {
                return this.Detail != null && (this.Detail.ElementsSpecified || this.Detail.DescriptionSpecified);
            }
        }

        // Location -------------------------------------

        /// <summary>
        /// Location of this instance.
        /// </summary>
        [XmlElement("location")]
        public string Location { get; set; } = "";

        /// <summary>
        /// Specification of the Location of this instance.
        /// </summary>
        [XmlIgnore()]
        public bool LocationSpecified
        {
            get
            {
                return !string.IsNullOrEmpty(this.Location);
            }
        }

        // Progression -------------------------------------

        /// <summary>
        /// Progression index of this instance. By default it is set to 0.
        /// </summary>
        /// <seealso cref="ProgressMax"/>
        [XmlElement("progressIndex")]
        [DefaultValue(0)]
        public float ProgressIndex
        {
            get { return this._ProgressIndex; }
            set
            {
                this._ProgressIndex = value;
                this._Log?.WriteLog("execution.progressIndex", this._ProgressIndex, LoggerMode.Auto);
            }
        }

        /// <summary>
        /// Maximum progression of this instance. By default, it is set to 100.
        /// </summary>
        /// <seealso cref="ProgressIndex"/>
        [XmlElement("progressMax")]
        [DefaultValue(100)]
        public float ProgressMax
        {
            get { return this._ProgressMax; }
            set
            {
                this._ProgressMax = value;
                this._Log?.WriteLog("execution.progressMax", this._ProgressMax, LoggerMode.Auto);
            }
        }

        // States -------------------------------------

        /// <summary>
        /// Status of this instance.
        /// </summary>
        [XmlElement("status")]
        [DefaultValue(ProcessExecutionStatus.None)]
        public ProcessExecutionStatus Status
        {
            get { return this._Status; }
            set
            {
                this._Status = value;
                ProcessExecutionState aProcessExecutionState = this._State;
                if (this._State != (aProcessExecutionState = ProcessExecution.GetState(this._Status)))
                    this._State = aProcessExecutionState;

                this._Log?.WriteLog("execution.status", this._Status, LoggerMode.Auto);
            }
        }

        /// <summary>
        /// State of this instance.
        /// </summary>
        [XmlElement("state")]
        [DefaultValue(ProcessExecutionState.None)]
        public ProcessExecutionState State
        {
            get { return this._State; }
            set
            {
                this._State = value;

                ProcessExecutionStatus aProcessExecutionStatus = this._Status;
                if (!ProcessExecution.GetStatuses(this._State).Contains(this._Status))
                    this._Status = ProcessExecution.GetDefaultStatus(this._State);

                this._Log?.WriteLog("execution.state", this._State, LoggerMode.Auto);
            }
        }

        /// <summary>
        /// Custom status of this instance.
        /// </summary>
        [XmlElement("customStatus")]
        public string CustomStatus
        {
            get { return this._CustomStatus; }
            set
            {
                this._CustomStatus = value;
                this._Log?.WriteLog("execution.customStatus", this._CustomStatus, LoggerMode.Auto);
            }
        }

        /// <summary>
        /// Specification of the CustomStatus of this instance.
        /// </summary>
        [XmlIgnore()]
        public bool CustomStatusSpecified
        {
            get
            {
                return !string.IsNullOrEmpty(this._CustomStatus);
            }
        }

        // Time -------------------------------------

        /// <summary>
        /// Start date of this instance.
        /// </summary>
        [XmlElement("startDate")]
        public string StartDate
        {
            get { return this._StartDate; }
            set
            {
                this._StartDate = value;
                this._Log?.WriteLog("execution.startDate", this._StartDate, LoggerMode.Auto);
            }
        }

        /// <summary>
        /// Specification of the StartDate of this instance.
        /// </summary>
        [XmlIgnore()]
        public bool StartDateSpecified
        {
            get
            {
                return !string.IsNullOrEmpty(this._StartDate);
            }
        }

        /// <summary>
        /// Re-start date of this instance.
        /// </summary>
        [XmlElement("restartDate")]
        public string RestartDate
        {
            get { return this._RestartDate; }
            set
            {
                this._RestartDate = value;
                this._Log?.WriteLog("execution.restartDate", this._RestartDate, LoggerMode.Auto);
            }
        }

        /// <summary>
        /// Specification of the RestartDate of this instance.
        /// </summary>
        [XmlIgnore()]
        public bool RestartDateSpecified
        {
            get
            {
                return !string.IsNullOrEmpty(this._RestartDate);
            }
        }

        /// <summary>
        /// End date of this instance.
        /// </summary>
        [XmlElement("endDate")]
        public string EndDate
        {
            get { return this._EndDate; }
            set
            {
                this._EndDate = value;
                this._Log?.WriteLog("execution.endDate", this._EndDate, LoggerMode.Auto);
            }
        }

        /// <summary>
        /// Specification of the EndDate of this instance.
        /// </summary>
        [XmlIgnore()]
        public bool EndDateSpecified
        {
            get
            {
                return !string.IsNullOrEmpty(this.EndDate);
            }
        }

        /// <summary>
        /// End date of this instance.
        /// </summary>
        [XmlElement("duration")]
        public string Duration
        {
            get
            {
                if (DateTime.TryParseExact(this._StartDate, StringHelper.__DateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime aExecutionStartDate))
                {
                    if (DateTime.TryParseExact(this._EndDate, StringHelper.__DateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime aExecutionEndDate))
                    {
                        return aExecutionEndDate.Subtract(aExecutionStartDate).ToString();
                    }
                }

                return "";
            }
            set
            {
                this._Duration = value;
                this._Log?.WriteLog("execution.duration", this._Duration, LoggerMode.Auto);
            }
        }

        /// <summary>
        /// Specification of the Duration of this instance.
        /// </summary>
        [XmlIgnore()]
        public bool DurationSpecified
        {
            get
            {
                return !string.IsNullOrEmpty(this._Duration);
            }
        }

        // Result -------------------------------------

        /// <summary>
        /// Result level of this instance. Over a certain value the result can be considered 
        /// as satisfying.
        /// </summary>
        [XmlElement("resultLevel")]
        [DefaultValue(0)]
        public int ResultLevel
        {
            get { return this._ResultLevel; }
            set
            {
                this._ResultLevel = value;
                this._Log?.WriteLog("execution.resultLevel", this._ResultLevel, LoggerMode.Auto);
            }
        }

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the LogEventExecution class.
        /// </summary>
        public ProcessExecution()
        {
        }

        /// <summary>
        /// Instantiates a new instance of the LogEventExecution class.
        /// </summary>
        public ProcessExecution(Log log) : this()
        {
            this._Log = log;
        }

        #endregion


        // ------------------------------------------
        // ACCESSORS
        // ------------------------------------------

        #region Accessors

        /// <summary>
        /// Get the process execution state.
        /// </summary>
        /// <param name="aString">The execution state string.</param>
        public static ProcessExecutionState GetState(String aString)
        {
            ProcessExecutionState aState = ProcessExecutionState.None;
            if (!Enum.TryParse<ProcessExecutionState>(aString, true, out aState))
                aState = ProcessExecutionState.None;

            return aState;
        }

        /// <summary>
        /// Get the process execution status.
        /// </summary>
        /// <param name="aString">The execution status string.</param>
        public static ProcessExecutionStatus GetStatus(String aString)
        {
            ProcessExecutionStatus status = ProcessExecutionStatus.None;
            if (!Enum.TryParse<ProcessExecutionStatus>(aString, true, out status))
                status = ProcessExecutionStatus.None;

            return status;
        }

        /// <summary>
        /// Get the process execution statuses corresponding to the specified state.
        /// </summary>
        /// <param name="aState">The state to consider.</param>
        public static List<ProcessExecutionStatus> GetStatuses(ProcessExecutionState aState)
        {
            switch(aState)
            {
                case ProcessExecutionState.Ended:
                    return new List<ProcessExecutionStatus>()
                    {
                        ProcessExecutionStatus.Completed,
                        ProcessExecutionStatus.NothingDone,
                        ProcessExecutionStatus.Stopped,
                        ProcessExecutionStatus.Stopped_Error,
                        ProcessExecutionStatus.Stopped_Exception,
                        ProcessExecutionStatus.Stopped_User
                    };
                case ProcessExecutionState.Pending:
                    return new List<ProcessExecutionStatus>()
                    {
                        ProcessExecutionStatus.Processing,
                        ProcessExecutionStatus.Queueing,
                        ProcessExecutionStatus.Waiting
                    };
                default:
                    return new List<ProcessExecutionStatus>();
            }
        }

        /// <summary>
        /// Gets the default status of the specified state.
        /// </summary>
        /// <param name="aProcessExecutionState">The state to consider.</param>
        public static ProcessExecutionStatus GetDefaultStatus(ProcessExecutionState aProcessExecutionState)
        {
            switch (aProcessExecutionState)
            {
                case ProcessExecutionState.Pending:
                    return ProcessExecutionStatus.Processing;
                case ProcessExecutionState.Ended:
                    return ProcessExecutionStatus.Stopped;
            }
            return ProcessExecutionStatus.NothingDone;
        }

        /// <summary>
        /// Get the process execution statuse corresponding to the specified state.
        /// </summary>
        /// <param name="status">The status to consider.</param>
        public static ProcessExecutionState GetState(ProcessExecutionStatus status)
        {
            switch (status)
            {
                case ProcessExecutionStatus.Completed:
                case ProcessExecutionStatus.Stopped:
                case ProcessExecutionStatus.Stopped_Error:
                case ProcessExecutionStatus.Stopped_Exception:
                case ProcessExecutionStatus.Stopped_User:
                    return ProcessExecutionState.Ended;
                case ProcessExecutionStatus.Queueing:
                case ProcessExecutionStatus.Waiting:
                case ProcessExecutionStatus.Processing:
                    return ProcessExecutionState.Pending;
            }
            return ProcessExecutionState.None;
        }

        #endregion

        // ------------------------------------------
        // MUTATORS
        // ------------------------------------------

        #region Mutators

        /// <summary>
        /// Starts this instance.
        /// </summary>
        public void Start()
        {
            this._StartDate = DateTime.Now.GetString();
            this._RestartDate = "";
            this._EndDate = "";
            this._State = ProcessExecutionState.Pending;
            this._Status = ProcessExecutionStatus.Processing;
            this._ProgressIndex = 0;

            if (this._Log!=null)
                this._Log.AddEvent(EventKind.Checkpoint, "Task started");
        }

        /// <summary>
        /// Ends this instance specifying the status.
        /// </summary>
        /// <param name="status">The new status to consider.</param>
        public void End(ProcessExecutionStatus status = ProcessExecutionStatus.Completed)
        {
            if (!ProcessExecution.GetStatuses(ProcessExecutionState.Ended).Contains(status)) return;

            this._EndDate = DateTime.Now.GetString();
            this._State = ProcessExecutionState.Ended;
            this._Status = status;

            if (this._Log != null)
                switch(status)
                {
                    case ProcessExecutionStatus.Completed:
                        this._ProgressIndex = this._ProgressMax;
                        this._Log.AddEvent(EventKind.Checkpoint, "Task completed");
                        break;
                    case ProcessExecutionStatus.NothingDone:
                        this._ProgressIndex = this._ProgressMax;
                        this._Log.AddEvent(EventKind.Checkpoint, "Task completed with nothing done");
                        break;
                    case ProcessExecutionStatus.Stopped_Exception:
                        this._Log.AddEvent(EventKind.Checkpoint, "Task stopped by exception");
                        break;
                    case ProcessExecutionStatus.Stopped_User:
                        this._Log.AddEvent(EventKind.Checkpoint, "Task stopped by user");
                        break;
                    default:
                        this._Log.AddEvent(EventKind.Checkpoint, "Task stopped");
                        break;
                }
        }

        /// <summary>
        /// Restarts this instance.
        /// </summary>
        public void Restart()
        {
            this._RestartDate = DateTime.Now.GetString();
            this._EndDate = "";
            this._State = ProcessExecutionState.Pending;
            this._Status = ProcessExecutionStatus.Processing;
            this._ProgressIndex = 0;

            if (this._Log != null)
                this._Log.AddEvent(EventKind.Checkpoint, "Task re-started");
        }

        /// <summary>
        /// Resumes this instance.
        /// </summary>
        public void Resume()
        {
            this._RestartDate = DateTime.Now.GetString();
            this._EndDate = "";
            this._State = ProcessExecutionState.Pending;
            this._Status = ProcessExecutionStatus.Processing;
            this._ProgressIndex = 0;

            if (this._Log != null)
                this._Log.AddEvent(EventKind.Checkpoint, "Task resume");
        }

        /// <summary>
        /// Sets the specified detail attribute.
        /// </summary>
        /// <param name="name">The name of the attribute to set.</param>
        /// <param name="value">The value of the attribute to set.</param>
        public void AddDetail(string name, object value)
        {
            this.Detail.AddElement(name, (value ?? "").ToString(), DataValueType.Text);
            if (this._Log != null)
                this._Log.WriteLog(name, value, LoggerMode.Auto);
        }

        #endregion
    }
}