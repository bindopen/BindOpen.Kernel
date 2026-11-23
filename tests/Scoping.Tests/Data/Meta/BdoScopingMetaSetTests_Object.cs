using BindOpen.Data.Meta.Reflection;
using BindOpen.Scoping.Tests;
using Bogus;
using NUnit.Framework;

namespace BindOpen.Data.Meta;

/// <summary>
/// Contains unit tests for the creation, manipulation, and data retrieval of <see cref="IBdoMetaObject"/>
/// nodes within a scoping context, including POCO-to-metadata reflection conversions.
/// </summary>
/// <remarks>
/// These tests verify that meta-objects constructed manually, hydrated via <c>UpdateTree</c>,
/// or converted from plain CLR objects via <c>ToMeta</c> maintain structural integrity and property accuracy
/// when aggregated inside an <see cref="IBdoMetaSet"/>.
/// </remarks>
[TestFixture]
public class BdoScopingMetaSetTests_Object
{
    private dynamic _testData;
    private IBdoMetaSet _metaObjSet = null;

    /// <summary>
    /// Initializes randomized file and folder path test data using <see cref="Faker"/>
    /// before any tests in this fixture are executed.
    /// </summary>
    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        var f = new Faker();
        _testData = new
        {
            path1 = f.Random.Word() + "_1.txt",
            path2 = f.Random.Word() + "_2.txt",
            path3 = f.Random.Word() + "_3.txt",
            folderPath3 = f.Random.Word() + "_3.txt",
            path4 = f.Random.Word() + "_4.txt"
        };
    }

    /// <summary>
    /// Validates the structural integrity, element count, and property values of an <see cref="IBdoMetaSet"/>
    /// containing <see cref="IBdoMetaObject"/> instances accessed by name, indexer, and ordinal position.
    /// </summary>
    /// <param name="metaSet">The metadata set containing object nodes to validate.</param>
    public void Test(IBdoMetaSet metaSet)
    {
        // 1. Verify the total count of root objects
        Assert.That(metaSet?.Count, Is.EqualTo(4), "The meta-set should contain exactly 4 elements.");

        // 2. Retrieve objects using various access patterns (Name, Index, and Explicit Interface)
        var metaObj1 = metaSet.Object("object1");
        var metaObj2 = metaSet["object2"] as IBdoMetaObject;
        var metaObj3 = metaSet.Get<IBdoMetaObject>(2);
        var metaObj4 = metaSet.Get<IBdoMetaObject>("object4");

        // 3. Validate Data for Object 1 (Manually created)
        var path1 = metaObj1?.GetData<string>("path");
        Assert.That(path1, Is.EqualTo(_testData.path1), "Object1 path mismatch.");

        // 4. Validate Data for Object 2 (Created from Data with UpdateTree)
        var path2 = metaObj2?.GetData<string>("path");
        Assert.That(path2, Is.EqualTo(_testData.path2), "Object2 path mismatch.");

        // 5. Validate Data for Object 3 (Converted from POCO via ToMeta)
        var path3 = metaObj3?.GetData<string>("path");
        Assert.That(path3, Is.EqualTo(_testData.path3), "Object3 path mismatch.");

        // 6. Validate Data for Object 4 (Inserted manually)
        var path4 = metaObj4?.GetData<string>("path");
        Assert.That(path4, Is.EqualTo(_testData.path4), "Object4 path mismatch.");
    }

    /// <summary>
    /// Verifies the various instantiation and population workflows for <see cref="IBdoMetaObject"/> instances
    /// (manual scalar addition, <c>UpdateTree</c> hydration, and reflection-based <c>ToMeta</c> conversion)
    /// and validates their aggregation within an <see cref="IBdoMetaSet"/>.
    /// </summary>
    [Test]
    public void NewTest()
    {
        // A. Manual construction
        var metaObj1 = BdoMetaSetExtensions.Add(
            BdoData.NewObject("object1"),
            BdoData.NewScalar("path", _testData.path1 as string));

        // B. Construction from existing POCO with UpdateTree resolution
        var metaObj2 = BdoData.NewObject("object2", "scoping.tests$testEntity")
            .WithData(new EntityFake()
            {
                Path = _testData.path2 as string
            })
            .UpdateTree();

        // C. Conversion using reflection (.ToMeta() extension)
        var obj3 = new EntityFake(
            _testData.path3,
            _testData.folderPath3,
            new EntityFake(_testData.path1 as string, _testData.folderPath3 as string));
        var metaObj3 = obj3?.ToMeta(); // Name is taken from the object if not specified

        // D. Conversion with explicit naming
        var obj4 = new EntityFake(_testData.path4 as string);
        var metaObj4 = obj4?.ToMeta("object4");

        // E. Composite Node creation
        _metaObjSet = BdoData.NewSet(
            metaObj1,
            metaObj2,
            metaObj3);

        _metaObjSet.Insert(metaObj4);

        // Execute the common validation test
        Test(_metaObjSet);
    }
}