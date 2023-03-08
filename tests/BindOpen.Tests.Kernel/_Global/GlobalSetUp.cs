using NUnit.Framework;

namespace BindOpen.Tests.Kernel
{
    /// <summary>
    /// This class set the global setup.
    /// </summary>
    [SetUpFixture]
    public class GlobalSetUp
    {
        /// <summary>
        /// 
        /// </summary>
        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            // Setup singleton variables for the first time

            var _ = ScopingTests.Scope;
        }
    }
}