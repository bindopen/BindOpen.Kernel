using BindOpen.Framework.Application.Services;
using BindOpen.Framework.Samples.SampleA.Settings;
using BindOpen.Framework.System.Diagnostics;

namespace BindOpen.Framework.Samples.SampleA.Services
{
    public class TestService : TBdoHostedService<TestServiceSettings>
    {
        protected override ITBdoService<TestServiceSettings> Process(IBdoLog log)
        {
            Service_Command.Process(Host, log);

            return this;
        }
    }
}