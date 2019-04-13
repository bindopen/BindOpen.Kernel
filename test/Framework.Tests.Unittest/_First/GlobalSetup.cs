using BindOpen.Framework.Core.Extensions;
using BindOpen.Framework.UnitTest.Setup;
using NUnit.Framework;

namespace BindOpen.Framework.UnitTest._First
{
    /// <summary>
    /// This class set the global settings up.
    /// </summary>
    [TestFixture]
    public class GlobalSetup
    {
        [SetUp]
        public void Setup()
        {
            // Setup variables for the first time
            IAppExtension appExtension = SetupVariables.AppScope.AppExtension;
        }

        [Test]
        public void TestGlobal()
        {
        }
    }
}