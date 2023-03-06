using BindOpen.Extensions.Modeling;

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
        [BdoFunction(Name = "equal")]
        public static bool GetTest(
            this string st,
            int int1 = 10)
            => st == int1.ToString();
    }
}
