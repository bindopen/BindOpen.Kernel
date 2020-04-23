using NUnit.Framework;
using System.IO;

namespace BindOpen.Tests.Core
{
    /// <summary>
    /// This class set the global settings up.
    /// </summary>
    [SetUpFixture]
    public class GlobalSetUp
    {
        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            // Setup singleton variables for the first time

            var _ = GlobalVariables.Scope;

            // we delete the working folder

            if (Directory.Exists(GlobalVariables.WorkingFolder))
            {
                Directory.Delete(GlobalVariables.WorkingFolder, true);
            }
        }
    }
}