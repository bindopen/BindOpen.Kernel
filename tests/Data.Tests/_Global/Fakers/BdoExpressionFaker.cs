using Bogus;
using System.Dynamic;

namespace BindOpen.Tests;

public static class BdoExpressionFaker
{
    public static dynamic GetValueSet()
    {
        var f = new Faker();
        dynamic obj = new ExpandoObject();
        obj.Script = "$(var1)";
        obj.Literal = f.Random.Word();
        obj.ScriptwordName = "func1";

        return obj;
    }
}
