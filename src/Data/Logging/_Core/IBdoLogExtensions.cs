using BindOpen.Data.Meta;
using System;

namespace BindOpen.Logging;

/// <summary>
/// This static class provides extension methods to manage BindOpen logs fluently.
/// </summary>
public static class IBdoLogExtensions
{
    /// <summary>
    /// Attaches an execution tracker to the specified log.
    /// </summary>
    /// <param name="log">The log to consider.</param>
    /// <param name="execution">The execution information to attach.</param>
    public static T WithExecution<T>(
        this T log,
        IBdoProcessExecution execution)
        where T : IBdoLog
    {
        if (log != null)
        {
            log.Execution = execution;
        }
        return log;
    }

    /// <summary>
    /// Sets the result code for the specified log.
    /// </summary>
    public static T WithResultCode<T>(
        this T log,
        string resultCode)
        where T : IBdoLog
    {
        if (log != null)
        {
            log.ResultCode = resultCode;
        }
        return log;
    }

    /// <summary>
    /// Attaches a task configuration to the log.
    /// </summary>
    public static T WithTask<T>(
        this T log,
        IBdoConfiguration config)
        where T : IBdoLog
    {
        if (log != null)
        {
            log.TaskConfig = config;
        }
        return log;
    }

    /// <summary>
    /// Checks if the log contains events of the specified kinds.
    /// </summary>
    /// <param name="kinds">The event kinds to look for (e.g., Error, Warning).</param>
    /// <returns>True if at least one matching event is found.</returns>
    public static bool HasEvent<T>(
        this T log,
        params BdoEventKinds[] kinds)
        where T : IBdoLog
    {
        return log?.HasEvent(true, kinds) ?? false;
    }

    /// <summary>
    /// Adds an exception event to the log.
    /// </summary>
    /// <param name="exception">The exception to record.</param>
    /// <param name="title">The title of the event. Defaults to Exception.ToString().</param>
    /// <param name="date">The date of the event.</param>
    /// <param name="resultCode">The associated result code.</param>
    public static T AddException<T>(
        this T log,
        Exception exception,
        string title = null,
        DateTime? date = null,
        string resultCode = null)
        where T : IBdoLog
    {
        log?.AddEvent(
            BdoEventKinds.Exception,
            exception?.Message,
            title ?? exception?.ToString(),
            date,
            resultCode);

        return log;
    }
}