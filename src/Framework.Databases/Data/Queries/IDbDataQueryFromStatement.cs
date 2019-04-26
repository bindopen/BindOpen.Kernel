using System.Collections.Generic;

namespace BindOpen.Framework.Databases.Data.Queries
{
    public interface IDbDataQueryFromStatement
    {
        List<IDbDataQueryJointureStatement> JointureStatements { get; set; }
        IDbDataQueryUnionStatement UnionStatement { get; set; }
    }
}