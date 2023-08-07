using BindOpen.System.Data.Meta;
using BindOpen.System.Tests;
using NUnit.Framework;

namespace BindOpen.System.Data
{
    [TestFixture, Order(100)]
    public class BdoSpecTests
    {
        private IBdoSpec _spec = null;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _spec = BdoSpecFaker.CreateSpec();
        }

        [Test, Order(1)]
        public void GetSpec()
        {
            var text = _spec.DataType;
        }
    }
}
