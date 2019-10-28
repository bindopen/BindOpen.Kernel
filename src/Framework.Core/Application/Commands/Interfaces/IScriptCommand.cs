namespace BindOpen.Framework.Core.Application.Commands.Interfaces
{
    public interface IScriptCommand : ICommand
    {
        string Script { get; set; }
    }
}