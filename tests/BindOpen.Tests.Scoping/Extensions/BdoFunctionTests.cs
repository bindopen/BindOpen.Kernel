using BindOpen.Scoping.Data;
using BindOpen.Scoping.Data.Meta;
using BindOpen.Scoping.Extensions.Functions;
using BindOpen.Scoping.Script;
using NUnit.Framework;

namespace BindOpen.Tests.Scoping.Extensions
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
                .WithDefinition("bindopen.tests.kernel$testEqual")
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
