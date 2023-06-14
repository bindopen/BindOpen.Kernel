using BindOpen.System.Data;
using BindOpen.System.Data.Meta;
using BindOpen.System.Scoping.Functions;
using BindOpen.System.Scoping.Script;
using NUnit.Framework;

namespace BindOpen.System.Tests.Scoping.Functions
{
    [TestFixture, Order(300)]
    public class BdoFunctionTests
    {
        private readonly string _filePath = Tests.WorkingFolder + "Function.xml";

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
                BdoMeta.NewConfig()
                .WithDefinition("bindopen.system.tests.scoping$testEqual")
                .With(
                    BdoMeta.NewScalar("stringValue", _testData.stringValue as string),
                    BdoMeta.NewScalar("intValue", _testData.intValue as int?));

            var word = BdoScript.NewWord(config);

            var result = ScopingTests.Scope?.CallFunction<bool?>(word);

            Assert.That(result == false, "Bad function result");
        }

        [Test, Order(2)]
        public void NewFunTest()
        {
            var word = BdoScript.Func("testEqual",
                    _testData.stringValue as string,
                    _testData.intValue as int?);

            var result = ScopingTests.Scope?.CallFunction<bool?>(word);

            Assert.That(result == false, "Bad function result");
        }
    }

}
