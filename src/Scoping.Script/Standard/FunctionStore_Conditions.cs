using BindOpen.Kernel.Data.Meta;
using BindOpen.Kernel.Scoping;
using System;
using System.Linq;

namespace BindOpen.Kernel.Scoping.Script
{
    /// <summary>
    /// This class represents a script word definition fake.
    /// </summary>
    public static partial class FunctionStore
    {
        [BdoFunction(Name = "and")]
        public static bool And(
            [BdoParameter(Title = "Conditions")]
            params bool[] objs)
            => objs.All(q => q);

        [BdoFunction(Name = "or")]
        public static bool Or(
            [BdoParameter(Title = "Conditions")]
            params bool[] objs)
            => objs.Any(q => q);

        [BdoFunction(Name = "xor")]
        public static bool Xor(
            [BdoParameter(Title = "Conditions")]
            params bool[] objs)
            => !objs.Any(q => q);

        [BdoFunction(Name = "not")]
        public static bool Not(
            [BdoParameter(Title = "Condition")]
            bool obj1)
            => !obj1;

        [BdoFunction(Name = "eq")]
        public static bool Eq(
            [BdoParameter(Title = "First object")]
            object obj1,
            [BdoParameter(Title = "Second object")]
            object obj2)
            => obj1?.ToString().Equals(obj2?.ToString(), StringComparison.OrdinalIgnoreCase) == true;

        [BdoFunction(Name = "ne")]
        public static bool Ne(
            [BdoParameter(Title = "First object")]
            object obj1,
            [BdoParameter(Title = "Second object")]
            object obj2)
            => !obj1?.ToString().Equals(obj2?.ToString(), StringComparison.OrdinalIgnoreCase) == true;
    }
}