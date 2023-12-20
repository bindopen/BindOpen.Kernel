using BindOpen.Data.Meta;
using System;
using System.Linq;

namespace BindOpen.Scoping.Script
{
    /// <summary>
    /// This class represents a script word definition fake.
    /// </summary>
    public static partial class FunctionStore
    {
        [BdoFunction(Name = "text")]
        public static string Text([BdoParameter(Title = "Object")] object obj1)
            => obj1?.ToString();

        [BdoFunction(Name = "concat")]
        public static string Concat([BdoParameter(Title = "Objects")] params object[] objs)
            => String.Concat(objs.Select(q => q.ToString()));
    }
}