using BindOpen.Application.Scopes;
using BindOpen.Application.Services;
using BindOpen.System.Diagnostics;

namespace Samples.SampleA.Services
{
    public class TestService : BdoHostedService
    {
        public TestService(IBdoHost host) : base(host)
        {
        }

        protected override IBdoJob Process(IBdoLog log)
        {
            Service_Command.Process(Host, log);

            return this;
        }
    }
}