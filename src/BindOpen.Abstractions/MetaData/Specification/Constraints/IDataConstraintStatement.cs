using BindOpen.MetaData.Elements;
using BindOpen.MetaData.Items;
using BindOpen.Extensions.Processing;

namespace BindOpen.MetaData.Specification
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