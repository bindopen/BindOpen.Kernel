using System;
using System.Collections.Generic;
using BindOpen.Framework.Core.Application.Datasources;
using BindOpen.Framework.Core.Application.Scopes;
using BindOpen.Framework.Core.Data.Helpers.Strings;
using BindOpen.Framework.Core.Data.Items.Source;
using BindOpen.Framework.Core.Extensions.Configuration.Connectors;
using BindOpen.Framework.Core.System.Diagnostics;
using BindOpen.Framework.Databases.Extensions.Runtime.Connectors;
using NUnit.Framework;

namespace BindOpen.Framework.UnitTest.Setup
{
    public static class SetupVariables
    {
        static String _WorkingFolder = null;
        static RuntimeAppScope _AppScope = null;
        static DataSourceService _DataSourceService = null;

        public static String WorkingFolder
        {
            get
            {
                String workingFolder = SetupVariables._WorkingFolder;
                if (workingFolder == null)
                    SetupVariables._WorkingFolder = workingFolder = (AppDomain.CurrentDomain.BaseDirectory.GetEndedString(@"\") + @"temp\").ToPath();

                return workingFolder;
            }
        }

        public static RuntimeAppScope AppScope
        {
            get
            {
                RuntimeAppScope appScope = SetupVariables._AppScope;
                if (appScope == null)
                {
                    appScope = new RuntimeAppScope(AppDomain.CurrentDomain);
                    Log log = appScope.AppExtension.LoadLibrary(
                        new List<string>() {
                            "BindOpen.Framework.Standard",
                            "BindOpen.Framework.Databases",
                            "BindOpen.Framework.Databases.MSSqlServer"
                        });
                    appScope.DataSourceService = SetupVariables.DataSourceService;
                    appScope.Update<RuntimeAppScope>();
                    SetupVariables._AppScope = appScope;

                    Assert.IsFalse(log.HasErrorsOrExceptions(), log.ToXml());
                }

                return appScope;
            }
        }

        public static DataSourceService DataSourceService
        {
            get
            {
                DataSourceService dataSourceManager = SetupVariables._DataSourceService;
                if (dataSourceManager == null)
                {
                    dataSourceManager = new DataSourceService();
                    dataSourceManager = new DataSourceService(
                        new IDataSource("prd@ptf_central_db", DataSourceKind.Database,
                            new ConnectorConfiguration(
                                null,
                                DatabaseConnectorKind.MSSqlServer.GetUniqueName(),
                                @"<Enter.Connection.String.Here>")
                        )
                    );
                    SetupVariables._DataSourceService = dataSourceManager;
                }

                return dataSourceManager;
            }
        }

    }

}
