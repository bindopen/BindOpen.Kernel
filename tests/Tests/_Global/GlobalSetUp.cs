using NUnit.Framework;
using System.IO;

namespace BindOpen.Kernel.Tests
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

            var _ = SystemData.Scope;

            // we delete the working folder

            if (Directory.Exists(SystemData.WorkingFolder))
            {
                Directory.Delete(SystemData.WorkingFolder, true);
            }
        }
    }
}