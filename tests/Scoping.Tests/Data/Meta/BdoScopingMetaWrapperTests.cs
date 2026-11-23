using BindOpen.Tests;
using NUnit.Framework;

namespace BindOpen.Data.Meta;

/// <summary>
/// Contains unit tests for <see cref="BdoMetaWrapper"/> functionality, ensuring that
/// metadata sets can be bidirectionally mapped to and from strongly-typed C# wrapper objects.
/// </summary>
/// <remarks>
/// These tests verify property synchronization (<c>UpdateDetail</c> and <c>UpdateProperties</c>),
/// collection and dictionary bindings, nested object hydration, and descendant property traversal.
/// </remarks>
[TestFixture]
public class BdoScopingMetaWrapperTests
{
    /// <summary>
    /// Performs one-time setup before any tests in this fixture are executed.
    /// </summary>
    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        // Setup logic if needed
    }

    /// <summary>
    /// Verifies basic <see cref="MetaWrapperFake"/> instantiation and bidirectional synchronization
    /// between strongly-typed C# properties and the underlying <see cref="IBdoMetaSet"/> detail.
    /// </summary>
    [Test]
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
    /// Verifies the retrieval of deeply nested descendant metadata elements using both
    /// named path syntax (<c>^config1A</c>) and ordinal index syntax (<c>^:0</c>).
    /// </summary>
    [Test]
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
    /// Verifies that scalar array metadata entries map cleanly onto strongly-typed C# <see cref="System.Collections.Generic.IList{T}"/> properties.
    /// </summary>
    [Test]
    public void ListTest()
    {
        var values = new[] { "daily", "month" };

        var obj = ScopingTestData.Scope.NewMetaWrapper<MetaWrapperFake>(
            BdoData.NewConfig(
                BdoData.NewScalar("testList", values)));

        Assert.That(obj?.List?.Count, Is.EqualTo(2), "Mapping of scalar arrays to List failed.");
    }

    /// <summary>
    /// Verifies that child <see cref="IBdoMetaSet"/> nodes map cleanly onto strongly-typed C# dictionary properties.
    /// </summary>
    [Test]
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
    /// Verifies that consecutive calls to <c>UpdateDetail</c> merge new entries and overwrite
    /// duplicate keys cumulatively without duplicating existing elements.
    /// </summary>
    [Test]
    public void UpdateDetailTest()
    {
        var obj = ScopingTestData.Scope.NewMetaWrapper<MetaWrapperFake>();
        obj.UpdateDetail(BdoData.NewConfig(BdoData.NewScalar("test1", "monthA")), true);
        obj.UpdateDetail(BdoData.NewConfig(BdoData.NewScalar("test2", "dailyA")), true);
        obj.UpdateDetail(BdoData.NewConfig(BdoData.NewScalar("test2", "dailyB")), true);

        Assert.That(obj?.Detail?.Count, Is.EqualTo(2), "Cumulative Detail updates failed to merge correctly.");
    }

    /// <summary>
    /// Verifies that nested <see cref="IBdoMetaObject"/> nodes hydrate strongly-typed complex child properties
    /// on the wrapper instance when <c>UpdateProperties</c> is invoked.
    /// </summary>
    [Test]
    public void WithObjectTest()
    {
        var obj = ScopingTestData.Scope.NewMetaWrapper<MetaWrapperFake>();
        obj.UpdateDetail(
            BdoData.NewConfig(
                BdoData.NewScalar("testList", "monthA"),
                BdoMetaSetExtensions.Add(
                    BdoData.NewObject("entityFake"),
                    BdoData.NewScalar("stringValue", "_string"),
                    BdoData.NewScalar("intValue", 1500),
                    BdoData.NewScalar("enumValue", AccessibilityLevels.Restricted)
                )));

        obj.UpdateProperties();

        Assert.That(obj?.EntityFake?.StringValue, Is.EqualTo("_string"));
        Assert.That(obj?.EntityFake?.IntValue, Is.EqualTo(1500));
        Assert.That(obj?.EntityFake?.EnumValue, Is.EqualTo(AccessibilityLevels.Restricted));
    }

    /// <summary>
    /// Verifies that wrapper properties mapped to nested descendant paths accurately read from
    /// and write back to the underlying metadata hierarchy during <c>UpdateProperties</c> and <c>UpdateDetail</c>.
    /// </summary>
    [Test]
    public void DescendantPropertiesTest()
    {
        var obj = ScopingTestData.Scope.NewMetaWrapper<MetaWrapperFake>();
        obj.UpdateDetail(
            BdoData.NewConfig(
                BdoData.NewScalar("testListA", "monthA"),
                BdoMetaSetExtensions.Add(
                    BdoData.NewObject("entityFake"),
                    BdoData.NewScalar("stringValue", "_string"),
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