using BindOpen.Scoping.Data;
using BindOpen.Scoping.Data.Meta;
using BindOpen.Scoping.Extensions.Entities;
using NUnit.Framework;

namespace BindOpen.Tests.Scoping.Extensions
{
    [TestFixture, Order(300)]
    public class BdoEntityTests
    {
        private EntityFake _entity = null;

        private readonly string _filePath = Tests.WorkingFolder + "Entity.xml";

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
        public static IBdoEntity CreateEntity(dynamic data)
        {
            var config =
                BdoMeta.NewConfig()
                .WithDefinition("bindopen.tests.kernel$testEntity")
                .With(
                    BdoMeta.NewScalar("boolValue", data.boolValue as bool?),
                    BdoMeta.NewScalar("enumValue", data.enumValue as ActionPriorities?),
                    BdoMeta.NewScalar("intValue", data.intValue as int?),
                    BdoMeta.NewScalar("stringValue", data.stringValue as string));

            return ScopingTests.Scope.CreateEntity<EntityFake>(config);
        }

        [Test, Order(1)]
        public void CreateEntityNewObjectTest()
        {
            _entity = new EntityFake
            {
                BoolValue = BdoMeta.NewScalar<bool?>(_testData.boolValue as bool?),
                EnumValue = (ActionPriorities)_testData.enumValue,
                IntValue = (int)_testData.intValue,
                StringValue = _testData.stringValue as string,
            };

            BdoEntityFaker.AssertFake(_entity, _testData);
        }


        [Test, Order(2)]
        public void CreateEntityFromScopeTest()
        {
            _entity = CreateEntity(_testData);

            BdoEntityFaker.AssertFake(_entity, _testData);
        }
    }

}
