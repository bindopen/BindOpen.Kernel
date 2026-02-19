using BindOpen.Data.Meta;

namespace BindOpen.Scoping.Tests
{
    /// <summary>
    /// This class provides fake script function extensions used to test the 
    /// BindOpen script interpreter and extension resolution logic.
    /// </summary>
    public static class FunctionFake
    {
        /// <summary>
        /// A test function that checks if a string representation of an integer 
        /// matches a provided string value.
        /// </summary>
        /// <param name="stringValue">The string value to compare.</param>
        /// <param name="intValue">The integer value (defaults to 10).</param>
        /// <returns>Returns True if the string matches the integer's string representation.</returns>
        [BdoFunction(Name = "testEqual")]
        public static bool GetTest(
            [BdoParameter(Title = "String value")] string stringValue,
            [BdoParameter(Title = "Integer value")] int intValue = 10)
        {
            // The logic converts the int to string to perform a cross-type equality check
            return stringValue == intValue.ToString();
        }
    }
}