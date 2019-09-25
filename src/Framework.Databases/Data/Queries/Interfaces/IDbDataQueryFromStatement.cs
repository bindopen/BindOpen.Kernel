using System.Collections.Generic;

namespace BindOpen.Framework.Databases.Data.Queries
{
    /// <summary>
    /// 
    /// </summary>
    public interface IDbDataQueryFromStatement
    {
        /// <summary>
        /// 
        /// </summary>
        List<IDbDataQueryJointureStatement> JointureStatements { get; set; }

        /// <summary>
        /// 
        /// </summary>
        IDbDataQueryUnionStatement UnionStatement { get; set; }
    }
}