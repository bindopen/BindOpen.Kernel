using BindOpen.System.Data;
using BindOpen.System.Data.Meta;
using BindOpen.System.Scoping.Script;
using BindOpen.System.Tests;
using NUnit.Framework;

namespace BindOpen.System.Scoping.Functions
{
    [TestFixture, Order(300)]
    public class BdoFunctionTests
    {
        private readonly string _filePath = SystemData.WorkingFolder + "Function.xml";

        private dynamic _testData;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _testData = BdoFunctionFaker.Fake();
        }

        [Test, Order(1)]
        public void NewWordFromConfigTest()
        {
            var config =
                BdoData.NewConfig()
                .WithDefinition("bindopen.system.tests$testEqual")
                .With(
                    BdoData.NewMetaScalar("stringValue", _testData.stringValue as string),
                    BdoData.NewMetaScalar("intValue", _testData.intValue as int?));

            var word = BdoScript.NewWord(config);

            var result = SystemData.Scope?.CallFunction<bool?>(word);

            Assert.That(result == false, "Bad function result");
        }

        [Test, Order(2)]
        public void NewFunTest()
        {
            var word = BdoScript.Func("testEqual",
                    _testData.stringValue as string,
                    _testData.intValue as int?);

            var result = SystemData.Scope?.CallFunction<bool?>(word);

            Assert.That(result == false, "Bad function result");
        }
    }

}
