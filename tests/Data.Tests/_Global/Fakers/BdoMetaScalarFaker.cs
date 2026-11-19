using BindOpen.Data.Meta;
using Bogus;
using NUnit.Framework;
using System.Linq;

namespace BindOpen.Tests;

/// <summary>
/// Provides fake data generation and assertion methods for <see cref="IBdoMetaScalar"/> objects.
/// </summary>
public static class BdoMetaScalarFaker
{
    /// <summary>
    /// The path to the XML file used for meta-scalar list testing.
    /// </summary>
    public static readonly string XmlFilePath = DataTestData.WorkingFolder + "MetaScalarList.xml";

    /// <summary>
    /// Generates a dynamic object containing various arrays of random data.
    /// </summary>
    /// <returns>Returns a dynamic object with numeric, string, integer, and byte array properties.</returns>
    /// <remarks>
    /// This method uses <see cref="Bogus"/> to generate realistic randomized data sets 
    /// for meta-scalar value validation.
    /// </remarks>
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

    /// <summary>
    /// Validates that two <see cref="IBdoMetaScalar"/> elements are identical based on their names.
    /// </summary>
    /// <param name="element">The meta-scalar element to check.</param>
    /// <param name="reference">The reference meta-scalar to compare against.</param>
    /// <exception cref="AssertionException">Thrown when the names of the elements do not match.</exception>
    public static void AssertFake(
        IBdoMetaScalar element,
        IBdoMetaScalar reference)
    {
        Assert.That(element?.Name, Is.EqualTo(reference?.Name), "The meta-scalar names are different.");
    }
}