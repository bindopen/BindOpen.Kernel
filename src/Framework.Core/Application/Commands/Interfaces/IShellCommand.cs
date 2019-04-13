namespace BindOpen.Framework.Core.Application.Commands.Interfaces
{
    public interface IShellCommand : ICommand
    {
        string ArgumentString { get; set; }
        string FileName { get; set; }
        string WorkingDirectory { get; set; }
    }
}