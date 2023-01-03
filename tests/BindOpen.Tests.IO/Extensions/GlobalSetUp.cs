using BindOpen.Runtime.Tests;
using NUnit.Framework;
using System.IO;

namespace BindOpen.Runtime.IO.Tests.Extensions
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

            if (Directory.Exists(GlobalVariables.WorkingFolder))
            {
                Directory.Delete(GlobalVariables.WorkingFolder, true);
            }
        }
    }
}