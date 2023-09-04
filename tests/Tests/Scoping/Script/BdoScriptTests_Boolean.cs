using BindOpen.System.Data;
using BindOpen.System.Tests;
using NUnit.Framework;

namespace BindOpen.System.Scoping.Script
{
    /// <summary>
    /// 
    /// </summary>
    public class BdoScriptTests_Boolean
    {
        [Test, Order(201)]
        public void EqTest()
        {
            var word = BdoScript._Eq("MYTABLE", BdoScript._Text("MYTABLE"));

            var interpreter = SystemData.Scope.Interpreter;
            var result = interpreter.Evaluate<bool?>(word);

            Assert.That(result == true, "Bad script interpretation");
        }

        [Test, Order(202)]
        public void SubwordTest()
        {
            var word = BdoScript._Eq(BdoScript.Var("MYTABLE"), 135);

            var interpreter = SystemData.Scope.Interpreter;

            var varSet = BdoData.NewSet(
                ("MYTABLE", 135)
            );

            var result = interpreter.Evaluate<bool?>(word, varSet);

            Assert.That(result == true, "Bad script interpretation");
        }
    }
}
