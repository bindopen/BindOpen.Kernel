using BindOpen.Data;
using BindOpen.Kernel.Tests;
using NUnit.Framework;

namespace BindOpen.Scoping.Script
{
    /// <summary>
    /// 
    /// </summary>
    public class BdoScriptTests_Boolean
    {
        [Test, Order(201)]
        public void EqTest()
        {
            var word = BdoScript.Eq("MYTABLE", BdoScript.Text("MYTABLE"));

            var interpreter = SystemData.Scope.Interpreter;
            var result = interpreter.Evaluate<bool?>(word);

            Assert.That(result == true, "Bad script interpretation");
        }

        [Test, Order(202)]
        public void SubwordTest()
        {
            var word = BdoScript.Eq(BdoScript.Var("MYTABLE"), 135);

            var interpreter = SystemData.Scope.Interpreter;

            var metaSet = BdoData.NewSet(
                ("MYTABLE", 135)
            );

            var result = interpreter.Evaluate<bool?>(word, metaSet);

            Assert.That(result == true, "Bad script interpretation");
        }
    }
}
