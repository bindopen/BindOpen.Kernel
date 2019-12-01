using System.IO;
using BindOpen.Framework.Core.Application.Configuration;
using BindOpen.Framework.Core.Data.Common;
using BindOpen.Framework.Core.Data.Elements;
using BindOpen.Framework.Core.Data.Helpers.Serialization;
using BindOpen.Framework.Core.System.Diagnostics;
using NUnit.Framework;

namespace BindOpen.Framework.Tests.UnitTest.Application.Hosts
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
            SetupVariables.BdoHost.Log.AddError("This is a test");
        }
    }
}
