using BindOpen.System.Data.Meta;
using BindOpen.System.Tests;
using NUnit.Framework;

namespace BindOpen.System.Data
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
                ("test1", "ABC"));

            var value1 = obj.Test1;
            Assert.That(value1 == "ABC", "Bad dynamic object");

            obj.SetProperty(q => q.Test1, "123");
            obj.SetProperty(q => q.Test2, 1234);

            value1 = obj.GetData<string>("test1");
            Assert.That(value1 == obj.Test1, "Bad dynamic object");

            var value2 = obj.GetData<int?>("test2");
            Assert.That(value2 == obj.Test2, "Bad dynamic object");

            obj.Test2 = 150;
            obj.UpdateDetail();
            Assert.That(obj?.Detail?.GetData<int?>("test2") == 150, "Bad dynamic object");

            obj.Detail["test1"]?.SetData("TEST1");
            obj.UpdateProperties();
            Assert.That(obj?.Test1 == "TEST1", "Bad dynamic object");

            // null

            IBdoMetaSet metaSet = null;
            var obj1 = SystemData.Scope.NewMetaWrapper<MetaWrapperFake>(metaSet);
            Assert.That(obj?.Test1 == "TEST1", "Bad dynamic object");
        }

        [Test, Order(1)]
        public void DescendantTest()
        {
            var obj = SystemData.Scope.NewMetaWrapper<MetaWrapperFake>(
                BdoData.NewConfig(
                    BdoData.NewMetaScalar("test1", "ABC"))
                    .WithChildren(BdoData.NewConfig(
                        "config1A",
                        BdoData.NewMetaScalar("test1A", "1_ABC"))));

            var meta = obj.Detail.Descendant<IBdoMetaData>("/config1A", 0);
            Assert.That(meta?.GetData<string>() == "1_ABC", "Bad dynamic object");

            meta = obj.Detail.Descendant<IBdoMetaData>("/0", "test1A");
            Assert.That(meta?.GetData<string>() == "1_ABC", "Bad dynamic object");
        }

        [Test, Order(1)]
        public void ListTest()
        {
            var values = new[] { "daily", "month" };

            var obj = SystemData.Scope.NewMetaWrapper<MetaWrapperFake>(
                BdoData.NewConfig(
                    BdoData.NewMetaScalar("testList", values)));

            Assert.That(obj?.List?.Count == 2, "Bad dynamic object");
        }

        [Test, Order(1)]
        public void DictionaryTest()
        {
            var obj = SystemData.Scope.NewMetaWrapper<MetaWrapperFake>(
                BdoData.NewConfig(
                    BdoData.NewMetaNode("testDico")
                        .With(
                            BdoData.NewMetaScalar("day", 120),
                            BdoData.NewMetaScalar("month", 130))));

            Assert.That(obj?.Dico?.Count == 2, "Bad dynamic object");
        }
    }
}
