using BindOpen.Extensions;
using BindOpen.Extensions.Modeling;
using BindOpen.MetaData;
using NUnit.Framework;

namespace BindOpen.Tests.Extensions
{
    [TestFixture, Order(300)]
    public class BdoCarrierTests
    {
        private CarrierFake _carrier = null;

        private readonly string _filePath = GlobalVariables.WorkingFolder + "Carrier.xml";

        private dynamic _testData;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _testData = BdoCarrierFaker.Fake();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static IBdoCarrier CreateCarrier(dynamic data)
        {
            IBdoCarrierConfiguration config =
                BdoExt.NewCarrierConfig("tests.core$testCarrier")
                .WithItems(
                    BdoMeta.NewScalar("boolValue", data.boolValue),
                    BdoMeta.NewScalar("enumValue", data.enumValue),
                    BdoMeta.NewScalar("intValue", data.intValue),
                    BdoMeta.NewScalar("stringValue", data.stringValue));

            return BdoExt.NewCarrier<CarrierFake>(config);
        }

        [Test, Order(1)]
        public void CreateCarrierNewObjectTest()
        {
            _carrier = new CarrierFake
            {
                BoolValue = _testData.boolValue,
                EnumValue = _testData.enumValue,
                IntValue = _testData.intValue,
                StringValue = _testData.stringValue,
            };

            BdoCarrierFaker.AssertFake(_carrier, _testData);
        }


        [Test, Order(2)]
        public void CreateCarrierFromScopeTest()
        {
            _carrier = CreateCarrier(_testData);

            BdoCarrierFaker.AssertFake(_carrier, _testData);
        }
    }

}
