using NUnit.Framework;

namespace BindOpen.Tests.Core.Application.Hosts
{
    [TestFixture]
    public class AppHostTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TestLoggers()
        {
            Assert.That(GlobalVariables.AppHost.IsLoaded, "Application host not load failed");
        }
    }
}
