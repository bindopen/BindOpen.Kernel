using BindOpen.Framework.Core.Application.Commands;
using BindOpen.Framework.Core.Data.Common;
using BindOpen.Framework.Core.Data.Elements;
using BindOpen.Framework.Core.Data.Elements.Sets;
using BindOpen.Framework.Core.Data.Items.Sets;
using BindOpen.Framework.Core.Extensions.Configuration.Routines;
using BindOpen.Framework.Core.System.Diagnostics.Events;

namespace BindOpen.Framework.Core.Data.Specification.Constraints
{
    public interface IDataConstraintStatement : IDataItemSet<IRoutineConfiguration>
    {
        void AddConstraint(IRoutineConfiguration routine);
        IRoutineConfiguration AddConstraint(string name, string definitionName, IDataElementSet parameterDetail = null, IDataItemSet<ICommand> commandSet = null, IDataItemSet<IConditionalEvent> outputEventSet = null);
        IDataElement AddConstraintParameter(string constraintName, string definitionName = null, string parameterName = null, DataValueType dataValueType = DataValueType.Any);
        IRoutineConfiguration GetConstraint(string name);
        IDataElement GetConstraintParameter(string constraintName, string parameterName = null);
        object GetConstraintParameterValue(string constraintName, string parameterName = null);
        bool SetConstraintParameterValue(string constraintName, string definitionName = null, string parameterName = null, object value = null, DataValueType dataValueType = DataValueType.Any);
    }
}