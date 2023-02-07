using BindOpen.Data.Meta;
using Bogus;
using NUnit.Framework;
using System.Linq;

namespace BindOpen.Tests
{
    public static class BdoMetaScalarFaker
    {
        public static readonly string XmlFilePath = GlobalVariables.WorkingFolder + "ScalarElementSet.xml";

        public static dynamic Fake()
        {
            var f = new Faker();
            return new
            {
                arrayNumber = Enumerable.Range(0, 10).Select(p => f.Random.Double()).ToArray(),
                arrayString = Enumerable.Range(0, 10).Select(p => f.Random.Word()).ToArray(),
                arrayInteger = Enumerable.Range(0, 10).Select(p => f.Random.Int()).ToArray(),
                arrayArrayByte = Enumerable.Range(1, 2).Select(p => f.Random.Bytes(5000)).ToArray()
            };
        }

        public static void AssertFake(
            IBdoMetaScalar element,
            IBdoMetaScalar reference)
        {
            Assert.That(element?.Name == reference?.Name, "Name different");
        }
    }
}
