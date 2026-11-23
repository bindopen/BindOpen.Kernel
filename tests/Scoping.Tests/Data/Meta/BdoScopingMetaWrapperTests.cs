using BindOpen.Tests;
using NUnit.Framework;

namespace BindOpen.Data.Meta;

/// <summary>
/// This class provides unit tests for the BdoMetaWrapper functionality,
/// ensuring that metadata sets can be mapped to and from strongly-typed C# objects.
/// </summary>
[TestFixture, Order(100)]
public class BdoScopingMetaWrapperTests
{
    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        // Setup logic if needed
    }

    /// <summary>
    /// Tests the basic creation of a meta wrapper and its ability to sync 
    /// property values with an underlying metadata set.
    /// </summary>
    [Test, Order(1)]
    public void CreateTest()
    {
        // 1. Instantiate wrapper with an initial property set
        var obj = ScopingTestData.Scope.NewMetaWrapper<MetaWrapperFake>(
            BdoData.NewSet(("test1", "ABC")));

        Assert.That(obj.Test1, Is.EqualTo("ABC"), "Property initialization via wrapper failed.");

        // 2. Modify properties via lambda-based setter
        obj.SetProperty(q => q.Test1, "123");
        obj.SetProperty(q => q.Test2, 1234);

        // 3. Verify changes are reflected in the underlying metadata data
        Assert.That(obj.GetData<string>("test1"), Is.EqualTo(obj.Test1), "Sync from property to meta-data failed.");
        Assert.That(obj.GetData<int?>("test2"), Is.EqualTo(obj.Test2), "Sync from property to meta-data failed.");

        // 4. Test UpdateDetail (Pushing C# property changes to the Meta Detail)
        obj.Test2 = 150;
        obj.UpdateDetail();
        Assert.That(obj.Detail?.GetData<int?>("test2"), Is.EqualTo(150), "Detail update from property failed.");

        // 5. Test UpdateProperties (Pulling Meta changes to C# properties)
        obj.Detail["test1"]?.SetData("TEST1");
        obj.UpdateProperties();
        Assert.That(obj.Test1, Is.EqualTo("TEST1"), "Property update from detail failed.");
    }

    /// <summary>
    /// Tests the retrieval of deeply nested (descendant) metadata using path-based queries.
    /// </summary>
    [Test, Order(2)]
    public void DescendantTest()
    {
        var obj = ScopingTestData.Scope.NewMetaWrapper<MetaWrapperFake>(
            BdoData.NewConfig(
                BdoData.NewScalar("test1", "ABC"))
                .WithChildren(BdoData.NewConfig(
                    "config1A",
                    BdoData.NewScalar("test1A", "1_ABC"))));

        // Verify access to nested elements via path syntax (^ for children)
        var meta = obj.Detail.Descendant<IBdoMetaData>("^config1A", 0);
        Assert.That(meta?.GetData<string>(), Is.EqualTo("1_ABC"), "Path-based descendant retrieval failed.");

        meta = obj.Detail.Descendant<IBdoMetaData>("^:0", "test1A");
        Assert.That(meta?.GetData<string>(), Is.EqualTo("1_ABC"), "Index-based descendant retrieval failed.");
    }

    /// <summary>
    /// Tests the mapping of scalar arrays to C# lists within the wrapper.
    /// </summary>
    [Test, Order(3)]
    public void ListTest()
    {
        var values = new[] { "daily", "month" };

        var obj = ScopingTestData.Scope.NewMetaWrapper<MetaWrapperFake>(
            BdoData.NewConfig(
                BdoData.NewScalar("testList", values)));

        Assert.That(obj?.List?.Count, Is.EqualTo(2), "Mapping of scalar arrays to List failed.");
    }

    /// <summary>
    /// Tests the mapping of meta-nodes to C# dictionaries.
    /// </summary>
    [Test, Order(4)]
    public void DictionaryTest()
    {
        var obj = ScopingTestData.Scope.NewMetaWrapper<MetaWrapperFake>(
            BdoData.NewConfig(
                BdoData.NewSet("testDico")
                    .Add(
                        BdoData.NewScalar("day", 120),
                        BdoData.NewScalar("month", 130))));

        Assert.That(obj?.Dico?.Count, Is.EqualTo(2), "Mapping of MetaSet to Dictionary failed.");
    }

    /// <summary>
    /// Tests cumulative updates to the metadata detail.
    /// </summary>
    [Test, Order(5)]
    public void UpdateDetailTest()
    {
        var obj = ScopingTestData.Scope.NewMetaWrapper<MetaWrapperFake>();
        obj.UpdateDetail(BdoData.NewConfig(BdoData.NewScalar("test1", "monthA")), true);
        obj.UpdateDetail(BdoData.NewConfig(BdoData.NewScalar("test2", "dailyA")), true);
        obj.UpdateDetail(BdoData.NewConfig(BdoData.NewScalar("test2", "dailyB")), true);

        Assert.That(obj?.Detail?.Count, Is.EqualTo(2), "Cumulative Detail updates failed to merge correctly.");
    }

    /// <summary>
    /// Tests the mapping of nested object entities inside the metadata.
    /// </summary>
    [Test, Order(7)]
    public void WithObjectTest()
    {
        var obj = ScopingTestData.Scope.NewMetaWrapper<MetaWrapperFake>();
        obj.UpdateDetail(
            BdoData.NewConfig(
                BdoData.NewScalar("testList", "monthA"),
                BdoMetaSetExtensions.Add(
BdoData.NewObject("entityFake")
, BdoData.NewScalar("stringValue", "_string"),
                        BdoData.NewScalar("intValue", 1500),
                        BdoData.NewScalar("enumValue", AccessibilityLevels.Restricted)
                    )));

        obj.UpdateProperties();

        Assert.That(obj?.EntityFake?.StringValue, Is.EqualTo("_string"));
        Assert.That(obj?.EntityFake?.IntValue, Is.EqualTo(1500));
        Assert.That(obj?.EntityFake?.EnumValue, Is.EqualTo(AccessibilityLevels.Restricted));
    }

    /// <summary>
    /// Tests the wrapper's ability to update properties located in descendant nodes.
    /// </summary>
    [Test, Order(8)]
    public void DescendantPropertiesTest()
    {
        var obj = ScopingTestData.Scope.NewMetaWrapper<MetaWrapperFake>();
        obj.UpdateDetail(
            BdoData.NewConfig(
                BdoData.NewScalar("testListA", "monthA"),
                BdoMetaSetExtensions.Add(
BdoData.NewObject("entityFake")
, BdoData.NewScalar("stringValue", "_string"),
                        BdoData.NewScalar("intValue", 1500),
                        BdoData.NewScalar("enumValue", AccessibilityLevels.Restricted)
                    )));

        obj.UpdateProperties();
        Assert.That(obj?.SubEnumValue, Is.EqualTo(AccessibilityLevels.Restricted));

        // Modify C# property and push back to Meta
        obj.SubEnumValue = AccessibilityLevels.Private;
        obj.UpdateDetail();

        Assert.That(obj?.Detail["subEnumValue"].GetData<AccessibilityLevels>(), Is.EqualTo(AccessibilityLevels.Private));
    }
}