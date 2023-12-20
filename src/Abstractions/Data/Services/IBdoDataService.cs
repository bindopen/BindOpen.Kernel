using BindOpen.Data;
using BindOpen.Logging;
using System;
using System.Transactions;

namespace BindOpen.Data.Services
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