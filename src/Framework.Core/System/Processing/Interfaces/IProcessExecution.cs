using BindOpen.Framework.Core.Data.Elements.Sets;
using BindOpen.Framework.Core.Data.Items;

namespace BindOpen.Framework.Core.System.Processing
{
    public interface IProcessExecution : IDataItem
    {
        string CustomStatus { get; set; }
        DataElementSet Detail { get; set; }
        string Duration { get; set; }
        string EndDate { get; set; }
        string Location { get; set; }
        float ProgressIndex { get; set; }
        float ProgressMax { get; set; }
        string RestartDate { get; set; }
        int ResultLevel { get; set; }
        string StartDate { get; set; }
        ProcessExecutionState State { get; set; }
        ProcessExecutionStatus Status { get; set; }

        void AddDetail(string name, object value);
        void End(ProcessExecutionStatus status = ProcessExecutionStatus.Completed);
        void Restart();
        void Resume();
        void Start();
    }
}