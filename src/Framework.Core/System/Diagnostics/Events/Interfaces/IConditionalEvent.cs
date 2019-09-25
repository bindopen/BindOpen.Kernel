namespace BindOpen.Framework.Core.System.Diagnostics.Events
{
    /// <summary>
    /// 
    /// </summary>
    public interface IConditionalEvent : IEvent
    {
        /// <summary>
        /// 
        /// </summary>
        string ConditionScript { get; set; }
    }
}