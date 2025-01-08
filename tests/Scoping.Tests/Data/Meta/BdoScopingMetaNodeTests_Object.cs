using BindOpen.Data.Meta.Reflection;
using BindOpen.Tests;
using Bogus;
using NUnit.Framework;

namespace BindOpen.Data.Meta;

[TestFixture, Order(200)]
public class BdoScopingMetaNodeTests_Object
{
    private dynamic _testData;

    private IBdoMetaNode _metaObjSet = null;

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

    public void Test(IBdoMetaNode metaSet)
    {
        // set
        //  - "object1"
        //      - "path1"
        //          

        Assert.That(metaSet?.Count == 4, "Bad object element set - Count");

        var metaObj1 = metaSet.Object("object1");
        var metaObj2 = (metaSet["object2"] as IBdoMetaObject);
        var metaObj3 = metaSet.Get<IBdoMetaObject>(2);
        var metaObj4 = metaSet.Get<IBdoMetaObject>("object4");

        var path1 = metaObj1?.GetData<string>("path");
        Assert.That(
            path1 == _testData.path1
            , "Bad object element - Set1");

        var path2 = metaObj2?.GetData<string>("path");
        Assert.That(
            path2 == _testData.path2
            , "Bad object element - Set2");

        var path3 = metaObj3?.GetData<string>("path");
        Assert.That(
            path3 == _testData.path3
            , "Bad object element - Set3");

        var path4 = metaObj4?.GetData<string>("path");
        Assert.That(
            path4 == _testData.path4
            , "Bad object element - Set4");
    }

    [Test, Order(1)]
    public void NewTest()
    {
        var metaObj1 = BdoData.NewObject("object1")
            .With(
                BdoData.NewScalar("path", _testData.path1 as string));

        var metaObj2 = BdoData.NewObject("object2", "bindopen.scoping.tests$testEntity")
            .WithData(new EntityFake()
            {
                Path = _testData.path2 as string
            })
            .UpdateTree();

        var obj3 = new EntityFake(
            _testData.path3,
            _testData.folderPath3,
            new EntityFake(_testData.path1 as string, _testData.folderPath3 as string));
        var metaObj3 = obj3?.ToMeta();

        var obj4 = new EntityFake(_testData.path4 as string);
        var metaObj4 = obj4?.ToMeta("object4");

        _metaObjSet = BdoData.NewNode(
            metaObj1,
            metaObj2,
            metaObj3);
        _metaObjSet.Insert(metaObj4);

        Test(_metaObjSet);
    }
}
