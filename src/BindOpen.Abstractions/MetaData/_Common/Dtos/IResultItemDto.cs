namespace BindOpen.MetaData
{
    public interface IResultItemDto : IBdoDto
    {
        string Key { get; set; }

        ResourceStatus Status { get; set; }
    }
}