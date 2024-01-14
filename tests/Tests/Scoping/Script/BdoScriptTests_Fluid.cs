using BindOpen.Data;
using BindOpen.Data.Meta;
using BindOpen.Kernel.Tests;
using NUnit.Framework;

namespace BindOpen.Scoping.Script
{
    /// <summary>
    /// 
    /// </summary>
    public class BdoScriptTests_Fluid
    {
        [Test, Order(201)]
        public void InterpreteStringThisTest()
        {
            var exp = BdoData.NewExp("$(this).(parent).value()");

            var interpreter = SystemData.Scope.Interpreter;

            var meta = BdoData.NewMeta(123).WithParent(BdoData.NewMeta(125));

            var metaSet = BdoData.NewSet((BdoData.__VarName_This, meta));

            var result = interpreter.Evaluate<int?>(exp, metaSet);

            Assert.That(result == 125, "Bad script interpretation");
        }

        [Test, Order(201)]
        public void InterpreteWordThisTest()
        {
            var exp = BdoScript.This<IBdoMetaData>()._Parent()._Descendant("titi")._Value();

            var interpreter = SystemData.Scope.Interpreter;

            var set = BdoData.NewNode(("toto", 123), ("titi", 125));
            var meta = set[0];

            var metaSet = BdoData.NewSet((BdoData.__VarName_This, meta));

            var result = interpreter.Evaluate<int?>(exp, metaSet);

            Assert.That(result == 125, "Bad script interpretation");
        }

        [Test, Order(201)]
        public void InterpreteWordParentTest()
        {
            var exp = BdoScript._Parent<IBdoMetaData>()._Descendant("titi")._Value();

            var interpreter = SystemData.Scope.Interpreter;

            var set = BdoData.NewNode(("toto", 123), ("titi", 125));
            var meta = set[0];

            var varSet = BdoData.NewSet((BdoData.__VarName_This, meta));

            var result = interpreter.Evaluate<int?>(exp, varSet);
            Assert.That(result == 125, "Bad script interpretation");

            // Parent null

            BdoMetaData meta1 = ("toto", 123);
            varSet.Add(BdoData.__VarName_This, meta1);

            result = interpreter.Evaluate<int?>(exp, varSet);
            Assert.That(result == null, "Bad script interpretation");
        }
    }
}
