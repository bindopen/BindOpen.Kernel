namespace BindOpen.Data.Queries
{
    public interface IDbQueryUnionClause
    {
        DbQueryUnionKind Kind { get; set; }
        IDbSingleQuery Query { get; set; }
    }
}