namespace BindOpen.Data.Dtos
{
    public interface IResultItemDto
    {
        string Key { get; set; }
        ResourceStatus Status { get; set; }
    }
}