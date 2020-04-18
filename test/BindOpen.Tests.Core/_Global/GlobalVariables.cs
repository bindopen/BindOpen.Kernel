using BindOpen.Application.Scopes;
using BindOpen.Data.Helpers.Files;
using BindOpen.Extensions.References;
using BindOpen.Tests.Core.Extensions.Carriers;

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
                    _workingFolder = (FileHelper.GetAppRootFolderPath() + @"bdo\temp\").ToPath();
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
