using BindOpen.Data.Schema;
using BindOpen.Scoping.Script;
using BindOpen.Scoping.Tests;
using BindOpen.Tests;
using NUnit.Framework;

namespace BindOpen.Data.Meta;

[TestFixture, Order(201)]
public class BdoScopingMetaDataTests
{
    private object _obj;
    private IBdoSchema _schema;

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        _obj = ClassObjectFaker.Fake();
        _schema = BdoScopingSchemaFaker.CreateSchema();
    }

    [Test, Order(1)]
    public void ReferenceTest()
    {
        var entity = BdoData.New<EntityFake>();

        var varSet = BdoData.NewSet(("entity", entity));

        var meta1 = BdoData.NewObject()
            .WithReference(
                BdoScript.Var("entity"));
        var value1 = meta1.GetData(ScopingTestData.Scope, varSet);
        Assert.That(value1 == entity, "Bad meta data reference");
    }

    [Test, Order(2)]
    public void LabelTest()
    {
        var meta1 = BdoData.NewScalar("toto", 23);
        meta1.WithLabel(LabelFormats.NameColonValue);
        Assert.That(meta1.GetOrAddSpec().Label == "{{$(this).(name)}}:{{$(this).value()}}", "Bad meta data label");

        var label = meta1.GetLabel(ScopingTestData.Scope);
        Assert.That(label == "toto:23", "Bad meta data label");
    }

    [Test, Order(3)]
    public void ConditionTest()
    {
        var meta0 = BdoData.NewNode("meta-test")
            .WithSchema(BdoData.NewSchema())
            .With(
                BdoData.NewScalar("title", "A"));

        var existence0 = meta0.GetConditionValue(ScopingTestData.Scope);
        Assert.That(existence0, "Statement - Error");

        var meta1 = BdoData.NewNode("meta-test")
            .WithSchema(_schema)
            .With(
                BdoData.NewScalar("title", "myTitle"));

        var existence1 = meta1.GetConditionValue(ScopingTestData.Scope);
        Assert.That(existence1, "Statement - Error");

        var meta2 = BdoData.NewNode("meta-test")
            .WithSchema(_schema)
            .With(
                BdoData.NewScalar("title", "A"));

        var existence2 = meta2.GetConditionValue(ScopingTestData.Scope);
        Assert.That(!existence2, "Statement - Error");
    }
}
