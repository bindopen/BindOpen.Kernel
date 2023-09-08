namespace BindOpen.Kernel.Scoping.Script
{
    /// <summary>
    /// This static class provides methods to create script elements.
    /// </summary>
    public static partial class BdoScript
    {
        public static IBdoScriptInterpreter NewInterpreter(IBdoScope scope)
        {
            return new BdoScriptInterpreter(scope);
        }
    }
}
