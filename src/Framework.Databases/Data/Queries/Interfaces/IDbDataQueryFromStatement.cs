using System.Collections.Generic;

namespace BindOpen.Framework.Databases.Data.Queries
{
    /// <summary>
    /// 
    /// </summary>
    public interface IDbQueryFromStatement
    {
        /// <summary>
        /// 
        /// </summary>
        List<IDbQueryJointureStatement> JointureStatements { get; set; }

        /// <summary>
        /// 
        /// </summary>
        IDbQueryUnionStatement UnionStatement { get; set; }
    }
}