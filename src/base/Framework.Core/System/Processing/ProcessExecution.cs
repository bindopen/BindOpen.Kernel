﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Xml;
using System.Xml.Serialization;
using BindOpen.Framework.Core.Data.Common;
using BindOpen.Framework.Core.Data.Elements;
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
    [XmlType("ProcessExecution", Namespace = "https://bindopen.org/xsd")]
    [XmlRoot(ElementName = "processExecution", Namespace = "https://bindopen.org/xsd", IsNullable = false)]
    public class ProcessExecution : DataItem, IProcessExecution
    {
        // ------------------------------------------
        // VARIABLES
        // ------------------------------------------

        #region Variables

        private readonly ILog _log = null;

        private ProcessExecutionStatus _status = ProcessExecutionStatus.None;
        private ProcessExecutionState _state = ProcessExecutionState.None;
        private String _customStatus = null;
        private float _progressIndex = 0;
        private float _progressMax = 100;

        private String _startDate = null;
        private String _restartDate = null;
        private String _endDate = null;
        private String _duration = null;
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
        public DataElementSet Detail { get; set; } = new DataElementSet();

        /// <summary>
        /// Specification of the PropertyDetail property of this instance.
        /// </summary>
        [XmlIgnore()]
        public bool DetailSpecified
        {
            get
            {
                return Detail != null && (Detail.ElementsSpecified);
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
                return !string.IsNullOrEmpty(Location);
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
            get { return _progressIndex; }
            set
            {
                _progressIndex = value;
                _log?.WriteLog("execution.progressIndex", _progressIndex, LoggerMode.Auto);
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
                _log?.WriteLog("execution.progressMax", _progressMax, LoggerMode.Auto);
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
                ProcessExecutionState aProcessExecutionState = _state;
                if (_state != (aProcessExecutionState = ProcessExecution.GetState(_status)))
                    _state = aProcessExecutionState;

                _log?.WriteLog("execution.status", _status, LoggerMode.Auto);
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

                ProcessExecutionStatus aProcessExecutionStatus = _status;
                if (!ProcessExecution.GetStatuses(_state).Contains(_status))
                    _status = ProcessExecution.GetDefaultStatus(_state);

                _log?.WriteLog("execution.state", _state, LoggerMode.Auto);
            }
        }

        /// <summary>
        /// Custom status of this instance.
        /// </summary>
        [XmlElement("customStatus")]
        public string CustomStatus
        {
            get { return _customStatus; }
            set
            {
                _customStatus = value;
                _log?.WriteLog("execution.customStatus", _customStatus, LoggerMode.Auto);
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
                return !string.IsNullOrEmpty(_customStatus);
            }
        }

        // Time -------------------------------------

        /// <summary>
        /// Start date of this instance.
        /// </summary>
        [XmlElement("startDate")]
        public string StartDate
        {
            get { return _startDate; }
            set
            {
                _startDate = value;
                _log?.WriteLog("execution.startDate", _startDate, LoggerMode.Auto);
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
                return !string.IsNullOrEmpty(_startDate);
            }
        }

        /// <summary>
        /// Re-start date of this instance.
        /// </summary>
        [XmlElement("restartDate")]
        public string RestartDate
        {
            get { return _restartDate; }
            set
            {
                _restartDate = value;
                _log?.WriteLog("execution.restartDate", _restartDate, LoggerMode.Auto);
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
                return !string.IsNullOrEmpty(_restartDate);
            }
        }

        /// <summary>
        /// End date of this instance.
        /// </summary>
        [XmlElement("endDate")]
        public string EndDate
        {
            get { return _endDate; }
            set
            {
                _endDate = value;
                _log?.WriteLog("execution.endDate", _endDate, LoggerMode.Auto);
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
                return !string.IsNullOrEmpty(EndDate);
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
                _log?.WriteLog("execution.duration", _duration, LoggerMode.Auto);
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
                return !string.IsNullOrEmpty(_duration);
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
                _log?.WriteLog("execution.resultLevel", _resultLevel, LoggerMode.Auto);
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
        public ProcessExecution(ILog log) : this()
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
            _startDate = ObjectHelper.ToString(DateTime.Now);
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

            _endDate = ObjectHelper.ToString(DateTime.Now);
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
            _restartDate = ObjectHelper.ToString(DateTime.Now);
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
            _restartDate = ObjectHelper.ToString(DateTime.Now);
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
            Detail.AddElement(ElementFactory.CreateScalar(name, DataValueType.Text, (value ?? "").ToString()));
            _log?.WriteLog(name, value, LoggerMode.Auto);
        }

        #endregion
    }
}