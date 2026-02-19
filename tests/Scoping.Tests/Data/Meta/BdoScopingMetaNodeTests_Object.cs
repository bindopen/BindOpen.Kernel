using BindOpen.Data.Meta.Reflection;
using BindOpen.Scoping.Tests;
using Bogus;
using NUnit.Framework;

namespace BindOpen.Data.Meta;

/// <summary>
/// Tests the creation, manipulation, and data retrieval of Meta-Object nodes
/// within a scoping context, including POCO-to-Meta conversion.
/// </summary>
[TestFixture, Order(200)]
public class BdoScopingMetaNodeTests_Object
{
    private dynamic _testData;
    private IBdoMetaNode _metaObjSet = null;

    /// <summary>
    /// Initializes randomized test data using the Bogus library.
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
    /// Shared validation logic to verify the integrity of a meta-node containing objects.
    /// </summary>
    /// <param name="metaSet">The meta-node to validate.</param>
    public void Test(IBdoMetaNode metaSet)
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
    /// Tests the different ways to instantiate and populate Meta-Objects.
    /// </summary>
    [Test, Order(1)]
    public void NewTest()
    {
        // A. Manual construction
        var metaObj1 = BdoData.NewObject("object1")
            .With(BdoData.NewScalar("path", _testData.path1 as string));

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
        _metaObjSet = BdoData.NewNode(
            metaObj1,
            metaObj2,
            metaObj3);

        _metaObjSet.Insert(metaObj4);

        // Execute the common validation test
        Test(_metaObjSet);
    }
}