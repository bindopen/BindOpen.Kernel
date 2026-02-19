using BindOpen.Data.Meta;
using Bogus;
using NUnit.Framework;
using System.Linq;

namespace BindOpen.Scoping.Tests
{
    /// <summary>
    /// This class provides factory methods to generate randomized data and perform
    /// assertions for IBdoMetaScalar elements.
    /// </summary>
    public static class BdoMetaScalarFaker
    {
        public static readonly string XmlFilePath = DataTestData.WorkingFolder + "MetaScalarList.xml";

        /// <summary>
        /// Generates a randomized set of scalar arrays using the Bogus library.
        /// </summary>
        /// <returns>An anonymous object containing arrays of various primitive types.</returns>
        public static dynamic Fake()
        {
            var f = new Faker();
            return new
            {
                // Generates arrays of 10 elements each for diverse testing
                arrayNumber = Enumerable.Range(0, 10).Select(p => f.Random.Double()).ToArray(),
                arrayString = Enumerable.Range(0, 10).Select(p => f.Random.Word()).ToArray(),
                arrayInteger = Enumerable.Range(0, 10).Select(p => f.Random.Int()).ToArray(),

                // Generates an array containing two byte arrays (blobs) of 5000 bytes each
                arrayArrayByte = Enumerable.Range(1, 2).Select(p => f.Random.Bytes(5000)).ToArray()
            };
        }

        /// <summary>
        /// Validates that a generated MetaScalar element matches its reference.
        /// </summary>
        /// <param name="element">The MetaScalar to check.</param>
        /// <param name="reference">The reference MetaScalar to compare against.</param>
        public static void AssertFake(
            IBdoMetaScalar element,
            IBdoMetaScalar reference)
        {
            Assert.That(element, Is.Not.Null, "The MetaScalar element is missing.");
            Assert.That(reference, Is.Not.Null, "The reference MetaScalar is missing.");

            // Basic identity check
            Assert.That(element.Name, Is.EqualTo(reference.Name), "The MetaScalar names do not match.");

            // Advanced check: verify the count of items in the scalar
            Assert.That(element.Count, Is.EqualTo(reference.Count), "The number of items in the MetaScalar differs from the reference.");
        }
    }
}