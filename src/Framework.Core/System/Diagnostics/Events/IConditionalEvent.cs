namespace BindOpen.Framework.Core.System.Diagnostics.Events
{
    public interface IConditionalEvent : IEvent
    {
        string ConditionScript { get; set; }
    }
}