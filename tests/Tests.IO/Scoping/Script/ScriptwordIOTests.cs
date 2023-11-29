using BindOpen.Kernel.Data;
using BindOpen.Kernel.Tests;
using NUnit.Framework;
using System;
using System.IO;

namespace BindOpen.Kernel.Scoping.Script
{
    [TestFixture, Order(210)]
    public class ScriptwordIOTests
    {
        private readonly string _filePath_xml = SystemData.WorkingFolder + "Scriptword.xml";
        private readonly string _filePath_json = SystemData.WorkingFolder + "Scriptword.json";
        private IBdoScriptword _scriptword = null;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
        }

        public static bool Equals(
            IBdoScriptword exp1,
            IBdoScriptword exp2)
        {
            var st1 = (string)(exp1 as BdoScriptword);
            var st2 = (string)(exp2 as BdoScriptword);

            var b = exp1 != null && exp2 != null
                && st1.Equals(st2, StringComparison.OrdinalIgnoreCase);
            return b;
        }

        [Test, Order(1)]
        public void CreateTest()
        {
            _scriptword = BdoScript.Eq(BdoScript.Var("var1").Func("func1", 1, "lk", 1.2), BdoScript.Text("text1"));
        }

        // Xml

        [Test, Order(3)]
        public void SaveXmlTest()
        {
            if (_scriptword == null)
            {
                CreateTest();
            }

            var isSaved = _scriptword.ToDto().SaveXml(_filePath_xml);
            Assert.That(isSaved, "Scriptword saving failed");
        }

        [Test, Order(4)]
        public void LoadXmlTest()
        {
            if (_scriptword == null || !File.Exists(_filePath_xml))
            {
                SaveXmlTest();
            }

            var exp = XmlHelper.LoadXml<ScriptwordDto>(_filePath_xml).ToPoco();
            Assert.That(Equals(exp, _scriptword), "Error while loading");
        }

        // Json

        [Test, Order(5)]
        public void SaveJsonTest()
        {
            if (_scriptword == null)
            {
                CreateTest();
            }

            var isSaved = _scriptword.ToDto().SaveJson(_filePath_json);
            Assert.That(isSaved, "Scriptword saving failed");
        }

        [Test, Order(6)]
        public void LoadJsonTest()
        {
            if (_scriptword == null || !File.Exists(_filePath_json))
            {
                SaveJsonTest();
            }

            var exp = JsonHelper.LoadJson<ScriptwordDto>(_filePath_json).ToPoco();
            Assert.That(Equals(exp, _scriptword), "Error while loading");
        }
    }
}
