using System;
using System.Linq;
using BindOpen.Framework.Core.System.Diagnostics;
using BindOpen.Framework.Runtime.Application.Services;
using BindOpen.Framework.Tests.TestConsole.Settings;

namespace BindOpen.Framework.Tests.TestConsole.Services
{
    public class TestService : THostedAppService<TestServiceSettings>
    {
        public override IAppService Start(ILog log = null)
        {
            base.Start(log);

            Console.WriteLine("Test service: Hello, I'm working");
            Console.WriteLine("Host settings:" + Host.GetSettings<TestAppSettings>().TestFolderPath);
            Console.WriteLine("Service settings:" + Settings?.TestFolderPath);
            Console.WriteLine("Test Uri value:" + Host.GetSettings<TestAppSettings>()?.Uris?.FirstOrDefault().Value);

            Test_It.Start();

            return this;
        }
    }
}