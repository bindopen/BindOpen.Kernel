using BindOpen.Application.Scopes;
using BindOpen.Data.Helpers.Strings;
using BindOpen.Extensions.References;
using BindOpen.Tests.Core.Extensions.Carriers;
using System;
using System.IO;

namespace BindOpen.Tests.Core
{
    public static class GlobalVariables
    {
        static string _workingFolder = null;
        static IBdoScope _appScope = null;

        public static string WorkingFolder
        {
            get
            {
                if (_workingFolder == null)
                {
                    _workingFolder = (Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory).GetEndedString(@"\")
                        + @"bdo\temp\").ToPath();
                }

                return _workingFolder;
            }
        }

        public static IBdoScope Scope
        {
            get
            {
                if (_appScope == null)
                {
                    _appScope = BdoScopeFactory.CreateScope();
                    _appScope.LoadExtensions(BdoExtensionReferenceFactory.CreateFrom<CarrierFake>());
                }

                return _appScope;
            }
        }

    }

}
