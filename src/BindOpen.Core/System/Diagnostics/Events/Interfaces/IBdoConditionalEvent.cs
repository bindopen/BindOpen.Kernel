namespace BindOpen.System.Diagnostics.Events
{
    /// <summary>
    /// 
    /// </summary>
    public interface IBdoConditionalEvent : IBdoEvent
    {
        /// <summary>
        /// 
        /// </summary>
        string ConditionScript { get; set; }
    }
}