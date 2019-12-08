using BindOpen.Framework.Core.Data.Common;
using BindOpen.Framework.Core.Data.Elements;
using BindOpen.Framework.Core.Data.Items;
using BindOpen.Framework.Core.Extensions.Runtime.Items;
using BindOpen.Framework.Core.System.Diagnostics;
using BindOpen.Framework.Core.System.Diagnostics.Events;

namespace BindOpen.Framework.Core.Data.Specification
{
    /// <summary>
    /// 
    /// </summary>
    public interface IDataConstraintStatement : IDataItemSet<BdoRoutineConfiguration>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="routine"></param>
        void AddConstraint(IBdoRoutineConfiguration routine);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="definitionUniqueId"></param>
        /// <param name="parameterDetail"></param>
        /// <param name="outputEventSet"></param>
        /// <returns></returns>
        IBdoRoutineConfiguration AddConstraint(
            string name,
            string definitionUniqueId,
            IDataElementSet parameterDetail = null,
            IDataItemSet<BdoConditionalEvent> outputEventSet = null);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="constraintName"></param>
        /// <param name="definitionUniqueId"></param>
        /// <param name="parameterName"></param>
        /// <param name="dataValueType"></param>
        /// <returns></returns>
        IDataElement AddConstraintParameter(string constraintName, string definitionUniqueId = null, string parameterName = null, DataValueType dataValueType = DataValueType.Any);

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
        IDataElement GetConstraintParameter(string constraintName, string parameterName = null);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="constraintName"></param>
        /// <param name="parameterName"></param>
        /// <returns></returns>
        object GetConstraintParameterValue(string constraintName, string parameterName = null);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="constraintName"></param>
        /// <param name="definitionUniqueId"></param>
        /// <param name="parameterName"></param>
        /// <param name="value"></param>
        /// <param name="dataValueType"></param>
        /// <returns></returns>
        bool SetConstraintParameterValue(string constraintName, string definitionUniqueId = null, string parameterName = null, object value = null, DataValueType dataValueType = DataValueType.Any);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <param name="dataElement"></param>
        /// <param name="isDeepCheck"></param>
        /// <returns></returns>
        IBdoLog CheckItem(
            object item,
            IDataElement dataElement,
            bool isDeepCheck = false);
    }
}