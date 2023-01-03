using BindOpen.Extensions.Processing;
using BindOpen.Data.Elements;
using BindOpen.Data.Items;

namespace BindOpen.Data.Specification
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
        IBdoElement GetConstraintParameter(string constraintName, string parameterName = null);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="constraintName"></param>
        /// <param name="parameterName"></param>
        /// <returns></returns>
        object GetConstraintParameterValue(string constraintName, string parameterName = null);
    }
}