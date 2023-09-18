using BindOpen.Kernel.Logging;
using System;
using System.Transactions;

namespace BindOpen.Kernel.Data.Services
{
    /// <summary>
    /// 
    /// </summary>
    public interface IBdoDataService
    {
        IResultItem ExecuteScoped(
            IBdoLog log,
            TransactionScope scope,
            Action<TransactionScope, IResultItem> action,
            ResourceStatus successStatus);
    }
}