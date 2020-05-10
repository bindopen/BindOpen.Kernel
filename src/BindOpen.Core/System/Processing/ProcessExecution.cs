using BindOpen.Data.Common;
using BindOpen.Data.Elements;
using BindOpen.Data.Helpers.Objects;
using BindOpen.Data.Helpers.Strings;
using BindOpen.Data.Items;
using BindOpen.System.Diagnostics;
using BindOpen.System.Diagnostics.Events;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Xml;
using System.Xml.Serialization;

namespace BindOpen.System.Processing
{
    /// <summary>
    /// This class represents the process execution.
    /// </summary>
    [XmlType("ProcessExecution", Namespace = "https://docs.bindopen.org/xsd")]
    [XmlRoot(ElementName = "processExecution", Namespace = "https://docs.bindopen.org/xsd", IsNullable = false)]
    public class ProcessExecution : DataItem, IProcessExecution
    {
        // ------------------------------------------
        // VARIABLES
        // ------------------------------------------

        #region Variables

        private readonly IBdoLog _log = null;

        private ProcessExecutionStatus _status = ProcessExecutionStatus.None;
        private ProcessExecutionState _state = ProcessExecutionState.None;
        private string _customStatus = null;
        private float _progressIndex = 0;
        private float _progressMax = 100;

        private string _startDate = null;
        private string _restartDate = null;
        private string _endDate = null;
        private string _duration = null;
        private int _resultLevel = 0;   // Estimated by the programmer. Over a certain number the result could be considered as satisfying.

        #endregion

        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// Detail of this instance.
        /// </summary>
        [XmlElement("detail")]
        [DefaultValue("")]
        public DataElementSet Detail { get; set; } = new DataElementSet();

        // Location -------------------------------------

        /// <summary>
        /// Location of this instance.
        /// </summary>
        [XmlElement("location")]
        [DefaultValue("")]
        public string Location { get; set; } = "";

        // Progression -------------------------------------

        /// <summary>
        /// Progression index of this instance. By default it is set to 0.
        /// </summary>
        /// <seealso cref="ProgressMax"/>
        [XmlElement("progressIndex")]
        [DefaultValue(0)]
        public float ProgressIndex
        {
            get { return _progressIndex; }
            set
            {
                _progressIndex = value;
                _log?.WriteLog("execution.progressIndex", _progressIndex, BdoLoggerMode.Auto);
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
            get { return _progressMax; }
            set
            {
                _progressMax = value;
                _log?.WriteLog("execution.progressMax", _progressMax, BdoLoggerMode.Auto);
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
            get { return _status; }
            set
            {
                _status = value;
                ProcessExecutionState processExecutionState;
                if (_state != (processExecutionState = ProcessExecution.GetState(_status)))
                    _state = processExecutionState;

                _log?.WriteLog("execution.status", _status, BdoLoggerMode.Auto);
            }
        }

        /// <summary>
        /// State of this instance.
        /// </summary>
        [XmlElement("state")]
        [DefaultValue(ProcessExecutionState.None)]
        public ProcessExecutionState State
        {
            get { return _state; }
            set
            {
                _state = value;
                if (!ProcessExecution.GetStatuses(_state).Contains(_status))
                    _status = ProcessExecution.GetDefaultStatus(_state);

                _log?.WriteLog("execution.state", _state, BdoLoggerMode.Auto);
            }
        }

        /// <summary>
        /// Custom status of this instance.
        /// </summary>
        [XmlElement("customStatus")]
        [DefaultValue("")]
        public string CustomStatus
        {
            get { return _customStatus; }
            set
            {
                _customStatus = value;
                _log?.WriteLog("execution.customStatus", _customStatus, BdoLoggerMode.Auto);
            }
        }

        // Time -------------------------------------

        /// <summary>
        /// Start date of this instance.
        /// </summary>
        [XmlElement("startDate")]
        [DefaultValue("")]
        public string StartDate
        {
            get { return _startDate; }
            set
            {
                _startDate = value;
                _log?.WriteLog("execution.startDate", _startDate, BdoLoggerMode.Auto);
            }
        }

        /// <summary>
        /// Re-start date of this instance.
        /// </summary>
        [XmlElement("restartDate")]
        [DefaultValue("")]
        public string RestartDate
        {
            get { return _restartDate; }
            set
            {
                _restartDate = value;
                _log?.WriteLog("execution.restartDate", _restartDate, BdoLoggerMode.Auto);
            }
        }

        /// <summary>
        /// End date of this instance.
        /// </summary>
        [XmlElement("endDate")]
        [DefaultValue("")]
        public string EndDate
        {
            get { return _endDate; }
            set
            {
                _endDate = value;
                _log?.WriteLog("execution.endDate", _endDate, BdoLoggerMode.Auto);
            }
        }

        /// <summary>
        /// End date of this instance.
        /// </summary>
        [XmlElement("duration")]
        [DefaultValue("")]
        public string Duration
        {
            get
            {
                if (DateTime.TryParseExact(_startDate, StringHelper.__DateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime aExecutionStartDate))
                {
                    if (DateTime.TryParseExact(_endDate, StringHelper.__DateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime aExecutionEndDate))
                    {
                        return aExecutionEndDate.Subtract(aExecutionStartDate).ToString();
                    }
                }

                return "";
            }
            set
            {
                _duration = value;
                _log?.WriteLog("execution.duration", _duration, BdoLoggerMode.Auto);
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
            get { return _resultLevel; }
            set
            {
                _resultLevel = value;
                _log?.WriteLog("execution.resultLevel", _resultLevel, BdoLoggerMode.Auto);
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
        public ProcessExecution(IBdoLog log) : this()
        {
            _log = log;
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
            if (!Enum.TryParse<ProcessExecutionState>(aString, true, out ProcessExecutionState state))
                state = ProcessExecutionState.None;

            return state;
        }

        /// <summary>
        /// Get the process execution status.
        /// </summary>
        /// <param name="aString">The execution status string.</param>
        public static ProcessExecutionStatus GetStatus(String aString)
        {
            if (!Enum.TryParse<ProcessExecutionStatus>(aString, true, out ProcessExecutionStatus status))
                status = ProcessExecutionStatus.None;

            return status;
        }

        /// <summary>
        /// Get the process execution statuses corresponding to the specified state.
        /// </summary>
        /// <param name="aState">The state to consider.</param>
        public static List<ProcessExecutionStatus> GetStatuses(ProcessExecutionState aState)
        {
            switch (aState)
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
            _startDate = DateTime.Now.ToString(DataValueTypes.Date);
            _restartDate = "";
            _endDate = "";
            _state = ProcessExecutionState.Pending;
            _status = ProcessExecutionStatus.Processing;
            _progressIndex = 0;

            _log?.AddEvent(EventKinds.Checkpoint, "Task started");
        }

        /// <summary>
        /// Ends this instance specifying the status.
        /// </summary>
        /// <param name="status">The new status to consider.</param>
        public void End(ProcessExecutionStatus status = ProcessExecutionStatus.Completed)
        {
            if (!ProcessExecution.GetStatuses(ProcessExecutionState.Ended).Contains(status)) return;

            _endDate = DateTime.Now.ToString(DataValueTypes.Date);
            _state = ProcessExecutionState.Ended;
            _status = status;

            if (_log != null)
            {
                switch (status)
                {
                    case ProcessExecutionStatus.Completed:
                        _progressIndex = _progressMax;
                        _log.AddEvent(EventKinds.Checkpoint, "Task completed");
                        break;
                    case ProcessExecutionStatus.NothingDone:
                        _progressIndex = _progressMax;
                        _log.AddEvent(EventKinds.Checkpoint, "Task completed with nothing done");
                        break;
                    case ProcessExecutionStatus.Stopped_Exception:
                        _log.AddEvent(EventKinds.Checkpoint, "Task stopped by exception");
                        break;
                    case ProcessExecutionStatus.Stopped_User:
                        _log.AddEvent(EventKinds.Checkpoint, "Task stopped by user");
                        break;
                    default:
                        _log.AddEvent(EventKinds.Checkpoint, "Task stopped");
                        break;
                }
            }
        }

        /// <summary>
        /// Restarts this instance.
        /// </summary>
        public void Restart()
        {
            _restartDate = DateTime.Now.ToString(DataValueTypes.Date);
            _endDate = "";
            _state = ProcessExecutionState.Pending;
            _status = ProcessExecutionStatus.Processing;
            _progressIndex = 0;

            _log?.AddEvent(EventKinds.Checkpoint, "Task re-started");
        }

        /// <summary>
        /// Resumes this instance.
        /// </summary>
        public void Resume()
        {
            _restartDate = DateTime.Now.ToString(DataValueTypes.Date);
            _endDate = "";
            _state = ProcessExecutionState.Pending;
            _status = ProcessExecutionStatus.Processing;
            _progressIndex = 0;

            _log?.AddEvent(EventKinds.Checkpoint, "Task resume");
        }

        /// <summary>
        /// Sets the specified detail attribute.
        /// </summary>
        /// <param name="name">The name of the attribute to set.</param>
        /// <param name="value">The value of the attribute to set.</param>
        public void AddDetail(string name, object value)
        {
            Detail.Add(ElementFactory.CreateScalar(name, DataValueTypes.Text, (value ?? "").ToString()));
            _log?.WriteLog(name, value, BdoLoggerMode.Auto);
        }

        #endregion

        // ------------------------------------------
        // IDISPOSABLE METHODS
        // ------------------------------------------

        #region IDisposable_Methods

        private bool _isDisposed = false;

        /// <summary>
        /// Disposes this instance. 
        /// </summary>
        /// <param name="isDisposing">Indicates whether this instance is disposing</param>
        protected override void Dispose(bool isDisposing)
        {
            if (_isDisposed)
            {
                return;
            }

            _log?.Dispose();

            _isDisposed = true;

            base.Dispose(isDisposing);
        }

        #endregion   
    }
}