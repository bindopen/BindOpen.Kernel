using BindOpen.Data;
using BindOpen.Extensions;
using BindOpen.Extensions.Modeling;
using NUnit.Framework;

namespace BindOpen.Tests.Extensions
{
    [TestFixture, Order(300)]
    public class BdoEntityTests
    {
        private EntityFake _entity = null;

        private readonly string _filePath = GlobalVariables.WorkingFolder + "Entity.xml";

        private dynamic _testData;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _testData = BdoEntityFaker.Fake();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static IBdoEntity CreateEntity(dynamic data)
        {
            IBdoEntityConfiguration config =
                BdoExt.NewEntityConfig("tests.core$testEntity")
                .WithItems(
                    BdoData.NewMetaScalar("boolValue", data.boolValue),
                    BdoData.NewMetaScalar("enumValue", data.enumValue),
                    BdoData.NewMetaScalar("intValue", data.intValue),
                    BdoData.NewMetaScalar("stringValue", data.stringValue));

            return BdoExt.NewEntity<EntityFake>(config);
        }

        [Test, Order(1)]
        public void CreateEntityNewObjectTest()
        {
            _entity = new EntityFake
            {
                BoolValue = _testData.boolValue,
                EnumValue = _testData.enumValue,
                IntValue = _testData.intValue,
                StringValue = _testData.stringValue,
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
