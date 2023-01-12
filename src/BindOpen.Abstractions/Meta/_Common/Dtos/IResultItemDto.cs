namespace BindOpen.Meta
{
    public interface IResultItemDto : IBdoDto
    {
        string Key { get; set; }

        ResourceStatus Status { get; set; }
    }
}