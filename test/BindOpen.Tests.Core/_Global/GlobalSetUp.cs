using NUnit.Framework;

namespace BindOpen.Tests.Core
{
    /// <summary>
    /// This class set the global settings up.
    /// </summary>
    [SetUpFixture]
    public class GlobalSetUp
    {
        [OneTimeSetUp]
        public void Setup()
        {
            // Setup singleton variables for the first time

            var _ = GlobalVariables.Scope;
        }
    }
}