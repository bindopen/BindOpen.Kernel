using BindOpen.Data.Assemblies;
using Bogus;
using NUnit.Framework;

namespace BindOpen.Data
{
    [TestFixture, Order(210)]
    public class BdoAssemblyReferenceTests
    {
        private IBdoAssemblyReference _assemblyRef1 = null, _assemblyRef2 = null;
        private IBdoClassReference _classRef1 = null, _classRef2 = null;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            var f = new Faker();
            _assemblyRef1 = BdoData.Assembly(null, null);
            _assemblyRef2 = BdoData.Assembly(f.Random.Word());
            _classRef1 = BdoData.Class(_assemblyRef1, f.Random.Word());
            _classRef2 = BdoData.Class(_assemblyRef2, f.Random.Word());
        }

        [Test, Order(1)]
        public void AssemblyRefEqualsTest()
        {
            Assert.That(_assemblyRef1 != _assemblyRef2, "Bad assembly reference");
        }

        [Test, Order(2)]
        public void ClassRefEqualsTest()
        {
            Assert.That(_classRef1 != _classRef2, "Bad class reference");
        }
    }
}
