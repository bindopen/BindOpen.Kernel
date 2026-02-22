using BindOpen.Logging;
using System;
using System.Transactions;

namespace BindOpen.Data.Services;

/// <summary>
/// Defines a BindOpen data service.
/// A data service provides a standardized way to execute transactional operations 
/// while maintaining consistency and logging state.
/// </summary>
public interface IBdoDataService
{
    /// <summary>
    /// Executes an action within a specific transaction scope.
    /// </summary>
    /// <param name="scope">The transaction scope to use for the operation.</param>
    /// <param name="action">The action to execute, receiving the scope and a result item for feedback.</param>
    /// <param name="successStatus">The resource status to return if the operation is successful.</param>
    /// <param name="log">The log used for tracking the execution process.</param>
    /// <returns>Returns a result item describing the outcome of the operation.</returns>
    IResultItem ExecuteScoped(
        TransactionScope scope,
        Action<TransactionScope, IResultItem> action,
        ResourceStatus successStatus,
        IBdoLog log = null);
}