﻿using BindOpen.Data.Meta;
using BindOpen.Extensions.Functions;

namespace BindOpen.Tests
{
    /// <summary>
    /// This class represents a database data field.
    /// </summary>
    public static class FunctionFake
    {
        /// <summary>
        /// The boolean value of this instance.
        /// </summary>
        [BdoFunction(Name = "testEqual")]
        public static bool GetTest(
            [BdoParameter(Title = "String value")]
            this string stringValue,
            [BdoParameter(Title = "Integer value")]
            int intValue = 10)
            => stringValue == intValue.ToString();
    }
}
