using System;

namespace BindOpen.Logging;

/// <summary>
/// This static class provides extension methods for <see cref="IBdoProcessExecution"/>.
/// </summary>
public static class IBdoProcessExecutionExtensions
{
    /// <summary>
    /// Sets the custom status of the specified execution.
    /// </summary>
    /// <param name="status">The custom status to set.</param>
    public static T WithCustomStatus<T>(
        this T execution,
        string status)
        where T : IBdoProcessExecution
    {
        if (execution != null)
        {
            execution.CustomStatus = status;
        }

        return execution;
    }

    /// <summary>
    /// Sets the end date of the specified execution.
    /// </summary>
    /// <param name="endDate">The end date to set.</param>
    public static T WithEndDate<T>(
        this T execution,
        DateTime? endDate)
        where T : IBdoProcessExecution
    {
        if (execution != null)
        {
            execution.EndDate = endDate;
        }

        return execution;
    }

    /// <summary>
    /// Sets the location of the specified execution.
    /// </summary>
    /// <param name="location">The location (e.g., server name, URI) to set.</param>
    public static T WithLocation<T>(
        this T execution,
        string location)
        where T : IBdoProcessExecution
    {
        if (execution != null)
        {
            execution.Location = location;
        }

        return execution;
    }

    /// <summary>
    /// Sets the progress index of the specified execution.
    /// </summary>
    /// <param name="index">The current progress index.</param>
    public static T WithProgressIndex<T>(
        this T execution,
        float index)
        where T : IBdoProcessExecution
    {
        if (execution != null)
        {
            execution.ProgressIndex = index;
        }

        return execution;
    }

    /// <summary>
    /// Sets the maximum progress value of the specified execution.
    /// </summary>
    /// <param name="max">The maximum progress value.</param>
    public static T WithProgressMax<T>(
        this T execution,
        float max)
        where T : IBdoProcessExecution
    {
        if (execution != null)
        {
            execution.ProgressMax = max;
        }

        return execution;
    }

    /// <summary>
    /// Sets the restart date of the specified execution.
    /// </summary>
    /// <param name="date">The restart date to set.</param>
    public static T WithRestartDate<T>(
        this T execution,
        DateTime? date)
        where T : IBdoProcessExecution
    {
        if (execution != null)
        {
            execution.RestartDate = date;
        }

        return execution;
    }

    /// <summary>
    /// Sets the result level of the specified execution.
    /// </summary>
    /// <param name="level">The result level to set.</param>
    public static T WithResultLevel<T>(
        this T execution,
        int level)
        where T : IBdoProcessExecution
    {
        if (execution != null)
        {
            execution.ResultLevel = level;
        }

        return execution;
    }

    /// <summary>
    /// Sets the start date of the specified execution.
    /// </summary>
    /// <param name="date">The start date to set.</param>
    public static T WithStartDate<T>(
        this T execution,
        DateTime? date)
        where T : IBdoProcessExecution
    {
        if (execution != null)
        {
            execution.StartDate = date;
        }

        return execution;
    }

    /// <summary>
    /// Sets the high-level state of the specified execution.
    /// </summary>
    /// <param name="state">The execution state to set.</param>
    public static T WithState<T>(
        this T execution,
        ProcessExecutionState state)
        where T : IBdoProcessExecution
    {
        if (execution != null)
        {
            execution.State = state;
        }

        return execution;
    }

    /// <summary>
    /// Sets the detailed status of the specified execution.
    /// </summary>
    /// <param name="status">The execution status to set.</param>
    public static T WithStatus<T>(
        this T execution,
        ProcessExecutionStatus status)
        where T : IBdoProcessExecution
    {
        if (execution != null)
        {
            execution.Status = status;
        }

        return execution;
    }

    // Actions

    /// <summary>
    /// Indicates whether the specified execution is currently alive (pending state with active status).
    /// </summary>
    public static bool IsAlive<T>(
        this T execution)
        where T : IBdoProcessExecution
        => execution?.State == ProcessExecutionState.Pending
            && (execution?.Status == ProcessExecutionStatus.Paused
                || execution?.Status == ProcessExecutionStatus.Processing
                || execution?.Status == ProcessExecutionStatus.Waiting);

    /// <summary>
    /// Indicates whether the specified execution is currently running.
    /// </summary>
    public static bool IsRunning<T>(
        this T execution)
        where T : IBdoProcessExecution
        => execution?.State == ProcessExecutionState.Pending
            && (execution?.Status == ProcessExecutionStatus.Paused
                || execution?.Status == ProcessExecutionStatus.Processing
                || execution?.Status == ProcessExecutionStatus.Waiting);

    /// <summary>
    /// Indicates whether the specified execution is currently processing.
    /// </summary>
    public static bool IsProcessing<T>(
        this T execution)
        where T : IBdoProcessExecution
        => execution?.State == ProcessExecutionState.Pending
            && execution?.Status == ProcessExecutionStatus.Processing;

    /// <summary>
    /// Sets the execution as started, initializing the start date and resetting progress.
    /// </summary>
    public static void SetAsStarted<T>(
        this T execution)
        where T : IBdoProcessExecution
    {
        execution
            .WithStartDate(DateTime.Now)
            .WithRestartDate(null)
            .WithEndDate(null)
            .WithState(ProcessExecutionState.Pending)
            .WithStatus(ProcessExecutionStatus.Processing)
            .WithProgressIndex(0);
    }

    /// <summary>
    /// Sets the execution as restarted, updating the restart date and resetting progress.
    /// </summary>
    public static void SetAsRestarted<T>(
        this T execution)
        where T : IBdoProcessExecution
    {
        execution
            .WithRestartDate(DateTime.Now)
            .WithEndDate(null)
            .WithState(ProcessExecutionState.Pending)
            .WithStatus(ProcessExecutionStatus.Processing)
            .WithProgressIndex(0);
    }

    /// <summary>
    /// Sets the execution as resumed if it was previously paused.
    /// </summary>
    public static void SetAsResumed<T>(
        this T execution)
        where T : IBdoProcessExecution
    {
        if (!(execution?.State == ProcessExecutionState.Pending && execution?.Status == ProcessExecutionStatus.Paused)) return;

        execution
            .WithEndDate(null)
            .WithState(ProcessExecutionState.Pending)
            .WithStatus(ProcessExecutionStatus.Processing);
    }

    /// <summary>
    /// Sets the execution as paused.
    /// </summary>
    public static void SetAsPaused<T>(
        this T execution)
        where T : IBdoProcessExecution
    {
        if (execution?.State != ProcessExecutionState.Pending) return;

        execution
            .WithEndDate(null)
            .WithState(ProcessExecutionState.Pending)
            .WithStatus(ProcessExecutionStatus.Paused);
    }

    /// <summary>
    /// Sets the execution as ended with the specified final status.
    /// </summary>
    /// <param name="status">The final status (e.g., Completed, Stopped_Error).</param>
    public static void SetAsEnded<T>(
        this T execution,
        ProcessExecutionStatus status = ProcessExecutionStatus.Completed)
        where T : IBdoProcessExecution
    {
        if (!ProcessExecutionState.Ended.ToStatuses().Contains(status)) return;

        execution
            .WithEndDate(DateTime.Now)
            .WithState(ProcessExecutionState.Ended)
            .WithStatus(status);
    }
}