using BindOpen.Kernel.Abstractions.Data._Core.Enums;
using BindOpen.Kernel.Data.Meta;
using BindOpen.Kernel.Tests;
using NUnit.Framework;

namespace BindOpen.Kernel.Data
{
    [TestFixture, Order(100)]
    public class BdoMetaWrapperTests
    {
        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
        }

        [Test, Order(1)]
        public void CreateTest()
        {
            var obj = SystemData.Scope.NewMetaWrapper<MetaWrapperFake>(
                BdoData.NewSet(("test1", "ABC")));

            var value1 = obj.Test1;
            Assert.That(value1 == "ABC", "Bad meta wrapper");

            obj.SetProperty(q => q.Test1, "123");
            obj.SetProperty(q => q.Test2, 1234);

            value1 = obj.GetData<string>("test1");
            Assert.That(value1 == obj.Test1, "Bad meta wrapper");

            var value2 = obj.GetData<int?>("test2");
            Assert.That(value2 == obj.Test2, "Bad meta wrapper");

            obj.Test2 = 150;
            obj.UpdateDetail();
            Assert.That(obj?.Detail?.GetData<int?>("test2") == 150, "Bad meta wrapper");

            obj.Detail["test1"]?.SetData("TEST1");
            obj.UpdateProperties();
            Assert.That(obj?.Test1 == "TEST1", "Bad meta wrapper");

            // null

            IBdoMetaSet metaSet = null;
            var obj1 = SystemData.Scope.NewMetaWrapper<MetaWrapperFake>(metaSet);
            Assert.That(obj?.Test1 == "TEST1", "Bad meta wrapper");
        }

        [Test, Order(2)]
        public void DescendantTest()
        {
            var obj = SystemData.Scope.NewMetaWrapper<MetaWrapperFake>(
                BdoData.NewConfig(
                    BdoData.NewScalar("test1", "ABC"))
                    .WithChildren(BdoData.NewConfig(
                        "config1A",
                        BdoData.NewScalar("test1A", "1_ABC"))));

            var meta = obj.Detail.Descendant<IBdoMetaData>("^config1A", 0);
            Assert.That(meta?.GetData<string>() == "1_ABC", "Bad meta wrapper");

            meta = obj.Detail.Descendant<IBdoMetaData>("^:0", "test1A");
            Assert.That(meta?.GetData<string>() == "1_ABC", "Bad meta wrapper");
        }

        [Test, Order(3)]
        public void ListTest()
        {
            var values = new[] { "daily", "month" };

            var obj = SystemData.Scope.NewMetaWrapper<MetaWrapperFake>(
                BdoData.NewConfig(
                    BdoData.NewScalar("testList", values)));

            Assert.That(obj?.List?.Count == 2, "Bad meta wrapper");
        }

        [Test, Order(4)]
        public void DictionaryTest()
        {
            var obj = SystemData.Scope.NewMetaWrapper<MetaWrapperFake>(
                BdoData.NewConfig(
                    BdoData.NewNode("testDico")
                        .With(
                            BdoData.NewScalar("day", 120),
                            BdoData.NewScalar("month", 130))));

            Assert.That(obj?.Dico?.Count == 2, "Bad meta wrapper");
        }

        [Test, Order(5)]
        public void UpdateDetailTest()
        {
            var obj = SystemData.Scope.NewMetaWrapper<MetaWrapperFake>();
            obj.UpdateDetail(
                BdoData.NewConfig(
                    BdoData.NewScalar("test1", "monthA")),
                true);
            obj.UpdateDetail(
                BdoData.NewConfig(
                    BdoData.NewScalar("test2", "dailyA")),
                true);
            obj.UpdateDetail(
                BdoData.NewConfig(
                    BdoData.NewScalar("test2", "dailyB")),
                true);

            Assert.That(obj?.Detail?.Count == 2, "Bad meta wrapper");
        }

        [Test, Order(6)]
        public void WithConfigurationTest()
        {
            var obj = SystemData.Scope.NewMetaWrapper<MetaWrapperFake>();
            obj.UpdateDetail(
                BdoData.NewConfig(
                    BdoData.NewScalar("testListA", "monthA")),
                true);

            Assert.That(obj?.Detail?.Count == 1, "Bad meta wrapper");

            // Sub configuration

            obj.UpdateDetail(
                BdoData.NewConfig(
                    BdoData.NewScalar("test2", "dailyB"))
                    .WithChildren(
                        BdoData.NewConfig(
                            BdoData.NewScalar("float2A", 1500))));

            Assert.That(obj?.Detail?._Children?.Count == 1, "Bad meta wrapper");
        }

        [Test, Order(7)]
        public void WithObjectTest()
        {
            var obj = SystemData.Scope.NewMetaWrapper<MetaWrapperFake>();
            obj.UpdateDetail(
                BdoData.NewConfig(
                    BdoData.NewScalar("testList", "monthA"),
                    BdoData.NewObject("entityFake")
                        .With(
                            BdoData.NewScalar("stringValue", "_string"),
                            BdoData.NewScalar("intValue", 1500),
                            BdoData.NewScalar("enumValue", ActionPriorities.Low)
                        )));

            obj.UpdateProperties();

            Assert.That(obj?.EntityFake?.StringValue == "_string", "Bad meta wrapper");
            Assert.That(obj?.EntityFake?.IntValue == 1500, "Bad meta wrapper");
            Assert.That(obj?.EntityFake?.EnumValue == ActionPriorities.Low, "Bad meta wrapper");
        }

        [Test, Order(8)]
        public void DescendantPropertiesTest()
        {
            // Update properties

            var obj = SystemData.Scope.NewMetaWrapper<MetaWrapperFake>();
            obj.UpdateDetail(
                BdoData.NewConfig(
                    BdoData.NewScalar("testListA", "monthA"),
                    BdoData.NewObject("entityFake")
                        .With(
                            BdoData.NewScalar("stringValue", "_string"),
                            BdoData.NewScalar("intValue", 1500),
                            BdoData.NewScalar("enumValue", ActionPriorities.Low)
                        )));

            obj.UpdateProperties();

            Assert.That(obj?.SubEnumValue == ActionPriorities.Low, "Bad meta wrapper");

            // Update detail

            obj.SubEnumValue = ActionPriorities.High;

            obj.UpdateDetail();

            Assert.That(obj?.Detail["subEnumValue"].GetData<ActionPriorities>() == ActionPriorities.High, "Bad meta wrapper");
        }

        [Test, Order(9)]
        public void ObjectInCOnfigTest()
        {
            var obj = SystemData.Scope.NewMetaWrapper<MetaWrapperFake>();
            obj.UpdateDetail(
                BdoData.NewConfig(
                    BdoData.NewScalar("testList", "monthA"))
                .WithChildren(
                    BdoData.NewConfig("$entityFake",
                        BdoData.NewObject("node1").With(
                            BdoData.NewScalar("stringValue", "_string"),
                            BdoData.NewScalar("intValue", 1500),
                            BdoData.NewScalar("enumValue", ActionPriorities.Low)
                        )
                )));

            obj.UpdateProperties();

            Assert.That(obj?.ConfigEntityFake?.StringValue == "_string", "Bad meta wrapper");
            Assert.That(obj?.ConfigEntityFake?.IntValue == 1500, "Bad meta wrapper");
            Assert.That(obj?.ConfigEntityFake?.EnumValue == ActionPriorities.Low, "Bad meta wrapper");
        }
    }
}
