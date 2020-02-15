﻿using BindOpen.Application.Scopes;
using BindOpen.Data.Stores;
using BindOpen.Samples.SampleA.Services.Databases;
using BindOpen.System.Diagnostics;

namespace BindOpen.Samples.SampleA.Services
{
    public static class Service_Command
    {
        public static void Process(IBdoHost host, IBdoLog log)
        {
            var configuration = host.DataStore.GetDatasourceDepot()?.GetConnectorConfiguration("sphere.identity", "database.mssqlserver$client");

            var repo = new TestDbRepository(host?.GetModel<MyDbModel>(), host.CreatePostgreSqlConnector(""));
            repo.Test();
        }
    }
}