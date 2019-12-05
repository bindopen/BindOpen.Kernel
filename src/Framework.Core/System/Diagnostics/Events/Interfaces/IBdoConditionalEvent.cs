namespace BindOpen.Framework.Core.System.Diagnostics.Events
{
    /// <summary>
    /// 
    /// </summary>
    public interface IBdoConditionalEvent : BdoIEvent
    {
        /// <summary>
        /// 
        /// </summary>
        string ConditionScript { get; set; }
    }
}