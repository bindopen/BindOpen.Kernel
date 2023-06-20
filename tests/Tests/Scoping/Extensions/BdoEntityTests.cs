using BindOpen.System.Data;
using BindOpen.System.Data.Meta;
using BindOpen.System.Tests;
using NUnit.Framework;

namespace BindOpen.System.Scoping.Entities
{
    [TestFixture, Order(300)]
    public class BdoEntityTests
    {
        private EntityFake _entity = null;

        private readonly string _filePath = SystemData.WorkingFolder + "Entity.xml";

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
                BdoData.NewConfig()
                .WithDefinition("bindopen.system.tests$testEntity")
                .With(
                    BdoData.NewMetaScalar("boolValue", data.boolValue as bool?),
                    BdoData.NewMetaScalar("enumValue", data.enumValue as ActionPriorities?),
                    BdoData.NewMetaScalar("intValue", data.intValue as int?),
                    BdoData.NewMetaScalar("stringValue", data.stringValue as string));

            return SystemData.Scope.CreateEntity<EntityFake>(config);
        }

        [Test, Order(1)]
        public void CreateEntityNewObjectTest()
        {
            _entity = new EntityFake
            {
                BoolValue = Data.BdoData.NewMetaScalar<bool?>(_testData.boolValue as bool?),
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
