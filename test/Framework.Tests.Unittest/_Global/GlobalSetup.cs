using BindOpen.Framework.Core.Extensions;
using NUnit.Framework;

namespace BindOpen.Framework.UnitTest
{
    /// <summary>
    /// This class set the global settings up.
    /// </summary>
    [SetUpFixture]
    public class GlobalSetup
    {
        [OneTimeSetUp]
        public void Setup()
        {
            // Setup variables for the first time
            IAppExtension appExtension = SetupVariables.AppScope.AppExtension;
        }
    }
}