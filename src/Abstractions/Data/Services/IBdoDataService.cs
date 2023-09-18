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
            TransactionScope scope,
            Action<TransactionScope, IResultItem> action,
            ResourceStatus successStatus,
            IBdoLog log = null);
    }
}