using BindOpen.Framework.Core.Data.References;

namespace BindOpen.Framework.Core.Application.Commands.Interfaces
{
    public interface IReferenceCommand : ICommand
    {
        DataReferenceConfiguration Reference { get; set; }
    }
}