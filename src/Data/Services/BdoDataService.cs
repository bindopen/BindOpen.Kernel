﻿using BindOpen.Data;
using BindOpen.Logging;
using System;
using System.Transactions;

namespace BindOpen.Data.Services
{
    /// <summary>
    /// 
    /// </summary>
    public class BdoDataService : BdoObject, IBdoDataService
    {
        public IResultItem ExecuteScoped(
            TransactionScope scope,
            Action<TransactionScope, IResultItem> action,
            ResourceStatus successStatus,
            IBdoLog log = null)
        {
            var result = BdoData.NewResultItem();

            var newlyScoped = scope == null;
            if (newlyScoped) scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

            try
            {
                action?.Invoke(scope, result);

                if (result?.Status != ResourceStatus.None)
                {
                    if (newlyScoped)
                    {
                        scope?.Complete();
                    }

                    result.Status = successStatus;
                }
            }
            catch (Exception ex)
            {
                log?.AddException(ex);
                scope?.Dispose();
            }
            finally
            {
                if (newlyScoped)
                {
                    scope?.Dispose();
                }
            }

            return result;
        }
    }
}