using BindOpen.Framework.Core.Data.Common;
using BindOpen.Framework.Core.Data.Elements;
using BindOpen.Framework.Core.Data.Elements.Sets;
using BindOpen.Framework.Core.Data.Items.Sets;
using BindOpen.Framework.Core.Extensions.Items.Routines;
using BindOpen.Framework.Core.System.Diagnostics;
using BindOpen.Framework.Core.System.Diagnostics.Events;

namespace BindOpen.Framework.Core.Data.Specification.Constraints
{
    public interface IDataConstraintStatement : IDataItemSet<RoutineConfiguration>
    {
        void AddConstraint(IRoutineConfiguration routine);

        IRoutineConfiguration AddConstraint(
            string name,
            string definitionUniqueId,
            IDataElementSet parameterDetail = null,
            //IDataItemSet<Command> commandSet = null,
            IDataItemSet<ConditionalEvent> outputEventSet = null);

        IDataElement AddConstraintParameter(string constraintName, string definitionUniqueId = null, string parameterName = null, DataValueType dataValueType = DataValueType.Any);
        IRoutineConfiguration GetConstraint(string name);
        IDataElement GetConstraintParameter(string constraintName, string parameterName = null);
        object GetConstraintParameterValue(string constraintName, string parameterName = null);
        bool SetConstraintParameterValue(string constraintName, string definitionUniqueId = null, string parameterName = null, object value = null, DataValueType dataValueType = DataValueType.Any);
        ILog CheckItem(
            object item,
            IDataElement dataElement,
            bool isDeepCheck = false);
    }
}