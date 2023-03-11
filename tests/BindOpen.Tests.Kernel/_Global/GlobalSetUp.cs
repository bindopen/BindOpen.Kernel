using NUnit.Framework;
using System.IO;

namespace BindOpen.Tests
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

            // we delete the working folder

            if (Directory.Exists(Tests.WorkingFolder))
            {
                Directory.Delete(Tests.WorkingFolder, true);
            }
        }
    }
}