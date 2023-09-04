using BindOpen.System.Data;
using BindOpen.System.Data.Meta;
using BindOpen.System.Tests;
using NUnit.Framework;

namespace BindOpen.System.Scoping
{
    [TestFixture, Order(300)]
    public class BdoEntityTests
    {
        private dynamic _testData;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _testData = BdoEntityFaker.Fake();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param key="data"></param>
        /// <returns></returns>
        public static IBdoMetaObject CreateMetaObject(dynamic data)
        {
            var meta =
                BdoData.NewObject()
                .WithDataType(BdoExtensionKinds.Entity, "bindopen.system.tests$testEntity")
                .With(
                    BdoData.NewScalar("boolValue", data.boolValue as bool?),
                    BdoData.NewScalar("enumValue", data.enumValue as ActionPriorities?),
                    BdoData.NewScalar("intValue", data.intValue as int?),
                    BdoData.NewScalar("stringValue", data.stringValue as string));

            return meta;
        }

        [Test, Order(1)]
        public void CreateEntityTest_FromMetaSet()
        {
            IBdoMetaObject meta = CreateMetaObject(_testData);
            var entity = SystemData.Scope.CreateEntity<EntityFake>(meta);

            BdoEntityFaker.AssertFake(entity, _testData);
        }

        [Test, Order(2)]
        public void CreateEntityTest_FromConfig()
        {
            IBdoMetaObject meta = CreateMetaObject(_testData);
            var entity = SystemData.Scope.CreateEntity(meta) as EntityFake;

            BdoEntityFaker.AssertFake(entity, _testData);
        }

        [Test, Order(3)]
        public void CreateEntityTest_FromObject()
        {
            var entity = new EntityFake
            {
                BoolValue = BdoData.NewScalar<bool?>(_testData.boolValue as bool?),
                EnumValue = (ActionPriorities)_testData.enumValue,
                IntValue = (int)_testData.intValue,
                StringValue = _testData.stringValue as string,
            };

            var config = entity.ToMeta(SystemData.Scope);
            entity = SystemData.Scope.CreateEntity(config) as EntityFake;

            BdoEntityFaker.AssertFake(entity, _testData);
        }
    }

}
