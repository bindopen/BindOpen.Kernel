namespace BindOpen.System.Scripting
{
    /// <summary>
    /// This static class provides methods to create data elements.
    /// </summary>
    public static partial class ScriptingFactory
    {
        // Static creators -------------------------

        /// <summary>
        /// Creates a data element with specified items.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="items">The items to consider.</param>
        public static IScriptVariableSet CreateVariableSet(
            params (string name, object value)[] items)
        {
            var variableSet = new ScriptVariableSet();
            foreach (var (name, value) in items)
            {
                variableSet.SetValue(name, value);
            }

            return variableSet;
        }
    }
}
