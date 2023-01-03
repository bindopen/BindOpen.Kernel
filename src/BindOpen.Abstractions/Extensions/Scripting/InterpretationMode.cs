namespace BindOpen.Extensions.Scripting
{
    /// <summary>
    /// This enumerates the possible modes of runtime.
    /// </summary>
    public enum InterpretationMode
    {
        /// <summary>
        /// None. Never interpreted.
        /// </summary>
        None,

        /// <summary>
        /// Once. Interpreted once at startup.
        /// </summary>
        Once,

        /// <summary>
        /// EachTime. Each time on use.
        /// </summary>
        EachTime
    }
}