using BindOpen.Meta.Elements;
using BindOpen.Meta.Items;
using BindOpen.Extensions.Processing;

namespace BindOpen.Meta.Specification
{
    /// <summary>
    /// 
    /// </summary>
    public interface IDataConstraintStatement : ITBdoItemSet<IBdoRoutineConfiguration>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        IBdoRoutineConfiguration GetConstraint(string name);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="constraintName"></param>
        /// <param name="parameterName"></param>
        /// <returns></returns>
        IBdoMetaElement GetConstraintParameter(string constraintName, string parameterName = null);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="constraintName"></param>
        /// <param name="parameterName"></param>
        /// <returns></returns>
        object GetConstraintParameterValue(string constraintName, string parameterName = null);
    }
}