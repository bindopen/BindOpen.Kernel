namespace BindOpen.Framework.Databases.Data.Queries
{
    public interface IDbDataQueryUnionStatement
    {
        DbDataQueryUnionKind Type { get; set; }

        IAdvancedDbDataQuery Query { get; set; }
    }
}