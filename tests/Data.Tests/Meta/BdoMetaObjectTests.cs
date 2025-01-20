using BindOpen.Data.Meta.Reflection;
using BindOpen.Tests;
using DeepEqual.Syntax;
using NUnit.Framework;

namespace BindOpen.Data.Meta;

[TestFixture, Order(201)]
public class BdoMetaObjectTests
{
    private object _obj = null;
    public IBdoMetaObject _metaObject;

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        _obj = ClassObjectFaker.Fake();
    }

    private void Test()
    {
        var obj = _metaObject?.GetData();
        Assert.That(obj?.IsDeepEqual(_obj) == true, "Bad obj element set - Count");
    }

    [Test, Order(1)]
    public void ToMetaTest()
    {
        _metaObject = _obj.ToMeta<BdoMetaObject>();
        Test();
    }

    [Test, Order(2)]
    public void NewTest()
    {
        _metaObject = BdoData.NewObject(_obj);
        Test();

        _metaObject = BdoData.NewObject()
            .WithData(_obj);
        Test();
    }
}
