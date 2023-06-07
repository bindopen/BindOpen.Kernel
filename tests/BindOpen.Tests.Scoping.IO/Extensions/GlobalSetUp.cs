using NUnit.Framework;
using System.IO;

namespace BindOpen.Tests.Scoping.IO.Extensions
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
            // we delete the working folder

            if (Directory.Exists(Tests.WorkingFolder))
            {
                Directory.Delete(Tests.WorkingFolder, true);
            }
        }
    }
}