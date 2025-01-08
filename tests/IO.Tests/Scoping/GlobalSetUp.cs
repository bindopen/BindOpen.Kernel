using NUnit.Framework;

namespace BindOpen.Scoping
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
            //// we delete the working folder

            //if (Directory.Exists(DataTestData.WorkingFolder))
            //{
            //    Directory.Delete(DataTestData.WorkingFolder, true);
            //}
        }
    }
}