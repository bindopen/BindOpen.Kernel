﻿using BindOpen.Data.Meta;
using System;
using System.Linq;

namespace BindOpen.Scoping.Script
{
    /// <summary>
    /// This class represents a script word definition fake.
    /// </summary>
    public static partial class FunctionStore
    {
        [BdoFunction(Name = "and")]
        public static bool And(
            [BdoParameter(Title = "Conditions")] params bool[] objs)
            => objs.All(q => q);

        [BdoFunction(Name = "or")]
        public static bool Or(
            [BdoParameter(Title = "Conditions")] params bool[] objs)
            => objs.Any(q => q);

        [BdoFunction(Name = "xor")]
        public static bool Xor(
            [BdoParameter(Title = "Conditions")] params bool[] objs)
            => !objs.Any(q => q);

        [BdoFunction(Name = "not")]
        public static bool Not(
            [BdoParameter(Title = "Condition")] bool obj1)
            => !obj1;

        [BdoFunction(Name = "eq")]
        public static bool Eq(
            [BdoParameter(Title = "First object")] object obj1,
            [BdoParameter(Title = "Second object")] object obj2)
            => (obj1 == null && obj2 == null) || obj1?.Equals(obj2) == true;

        [BdoFunction(Name = "ne")]
        public static bool Ne(
            [BdoParameter(Title = "First object")] object obj1,
            [BdoParameter(Title = "Second object")] object obj2)
            => (obj1 == null && obj2 != null) || obj1?.Equals(obj2) == false;

        [BdoFunction(Name = "inEnum")]
        public static bool InEnum(
            [BdoParameter(Title = "First object")] object obj1,
            [BdoParameter(Title = "Type name")] string fullyQualifiedName)
        {
            if (obj1 == null || string.IsNullOrEmpty(fullyQualifiedName)) return false;

            Type type = Type.GetType(fullyQualifiedName);

            if (type != null)
            {
                return Enum.GetNames(type).Any(q => q == obj1?.ToString());
            }

            return false;
        }
    }
}