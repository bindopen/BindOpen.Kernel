using BindOpen.Framework.Core.Application.Commands;
using BindOpen.Framework.Core.Application.Scopes;
using BindOpen.Framework.Core.Data.Common;
using BindOpen.Framework.Core.Data.Elements;
using BindOpen.Framework.Core.Data.Elements.Sets;
using BindOpen.Framework.Core.Data.Helpers.Objects;
using BindOpen.Framework.Core.Data.Items.Sets;
using BindOpen.Framework.Core.Extensions.Configuration.Routines;
using BindOpen.Framework.Core.Extensions.Definition.Routines;
using BindOpen.Framework.Core.Extensions.Runtime.Routines;
using BindOpen.Framework.Core.System.Diagnostics;
using BindOpen.Framework.Core.System.Diagnostics.Events;
using BindOpen.Framework.Core.System.Scripting;
using System;
using System.Xml.Serialization;

namespace BindOpen.Framework.Core.Data.Specification.Constraints
{

    /// <summary>
    /// This class represents the data constraint statement.
    /// </summary>
    [Serializable()]
    [XmlType("DataConstraintStatement", Namespace = "http://meltingsoft.com/bindopen/xsd")]
    [XmlRoot(ElementName = "constraintStatement", Namespace = "http://meltingsoft.com/bindopen/xsd", IsNullable = false)]
    public class DataConstraintStatement : DataItemSet<RoutineConfiguration>
    {

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the DataConstraintStatement class.
        /// </summary>
        public DataConstraintStatement()
        {
        }

        #endregion


        // ------------------------------------------
        // MUTATORS
        // ------------------------------------------

        #region Mutators

        /// <summary>
        /// Adds the specified constraint.
        /// </summary>
        /// <param name="routine">The constraint to add.</param>
        /// <returns>Returns the item with the specified name.</returns>
        public void AddConstraint(RoutineConfiguration routine)
        {
            if (routine != null)
                this.Add(routine);
        }

        /// <summary>
        /// Adds the specified constraint.
        /// </summary>
        /// <param name="name">The name of constraint to create.</param>
        /// <param name="definitionName">The definition name to create.</param>
        /// <param name="parameterDetail">The parameter detail to consider.</param>
        /// <param name="commandSet">The command set to consider.</param>
        /// <param name="outputEventSet">The output event set to consider.</param>
        /// <returns>Returns the item with the specified name.</returns>
        public RoutineConfiguration AddConstraint(
            String name,
            String definitionName,
            DataElementSet parameterDetail = null,
            DataItemSet<Command> commandSet = null,
            DataItemSet<ConditionalEvent> outputEventSet = null)
        {
            RoutineConfiguration routine = new RoutineConfiguration(name, definitionName, parameterDetail, commandSet, outputEventSet);
            this.Add(routine);

            return routine;
        }

        /// <summary>
        /// Sets the constraint parameter value.
        /// </summary>
        /// <param name="constraintName">The name of the constraint to return.</param>
        /// <param name="definitionName">The name of the definition to return.</param>
        /// <param name="parameterName">The name of the parameter to return.</param>
        /// <param name="dataValueType">The name of the parameter to return.</param>
        /// <returns>Returns the specified constrainst parameter.</returns>
        public DataElement AddConstraintParameter(
            String constraintName, String definitionName = null,
            String parameterName = null, DataValueType dataValueType = DataValueType.Any)
        {
            DataElement dataElement = null;

            RoutineConfiguration routine = this.GetConstraint(constraintName);
            if ((routine == null) || (!routine.DefinitionUniqueId.KeyEquals(definitionName)))
                routine = this.AddConstraint(constraintName, definitionName);

            if (routine != null && routine.ParameterDetail!=null)
            {
                if (parameterName == null && routine.ParameterDetail.Count > 0)
                    dataElement = routine.ParameterDetail[0];
                else
                    dataElement = routine.ParameterDetail[parameterName];
                if (dataElement == null)
                    routine.ParameterDetail.AddElement(dataElement = DataElement.Create((dataValueType == DataValueType.Any ? dataValueType.GetValueType() : dataValueType), parameterName));
            }

            return dataElement;
        }

        /// <summary>
        /// Sets the constraint parameter value.
        /// </summary>
        /// <param name="constraintName">The name of the constraint to return.</param>
        /// <param name="definitionName">The name of the definition to return.</param>
        /// <param name="parameterName">The name of the parameter to return.</param>
        /// <param name="value">The value to consider.</param>
        /// <param name="dataValueType">The name of the parameter to return.</param>
        /// <returns>Returns the specified constrainst parameter.</returns>
        public Boolean SetConstraintParameterValue(
            String constraintName, String definitionName = null,
            String parameterName = null, Object value = null, DataValueType dataValueType = DataValueType.Any)
        {
            DataElement dataElement = null;

            RoutineConfiguration routine = this.GetConstraint(constraintName);
            if ((routine == null) || (!routine.DefinitionUniqueId.KeyEquals(definitionName)))
                routine = this.AddConstraint(constraintName, definitionName);

            if (routine != null && routine.ParameterDetail!=null)
            {
                if (parameterName == null && routine.ParameterDetail.Count > 0)
                    dataElement = routine.ParameterDetail[0];
                else
                    dataElement = routine.ParameterDetail[parameterName];
                if (dataElement == null)
                    routine.ParameterDetail.AddElement(dataElement = DataElement.Create(value, (dataValueType == DataValueType.Any ? value.GetValueType() : dataValueType), parameterName));
                else
                    dataElement.SetItem(value);
                return true;
            }
            return false;
        }

        #endregion


        // ------------------------------------------
        // ACCESSORS
        // ------------------------------------------

        #region Accessors

        /// <summary>
        /// Returns the constraint with the specified name.
        /// </summary>
        /// <param name="name">The name of the item to return.</param>
        /// <returns>Returns the item with the specified name.</returns>
        public RoutineConfiguration GetConstraint(String name)
        {
            return this.GetItem(name) as RoutineConfiguration;
        }

        /// <summary>
        /// Returns the constraint parameter.
        /// </summary>
        /// <param name="constraintName">The name of the constraint to return.</param>
        /// <param name="parameterName">The name of the parameter to return.</param>
        /// <returns>Returns the specified constrainst parameter.</returns>
        public DataElement GetConstraintParameter(String constraintName, String parameterName = null)
        {
            RoutineConfiguration routine = this.GetConstraint(constraintName);
            return (routine != null && routine.ParameterDetail !=null? routine.ParameterDetail[parameterName] : null);
        }

        /// <summary>
        /// Returns the constraint parameter value.
        /// </summary>
        /// <param name="constraintName">The name of the constraint to return.</param>
        /// <param name="parameterName">The name of the parameter to return.</param>
        /// <returns>Returns the specified constrainst parameter.</returns>
        public Object GetConstraintParameterValue(String constraintName, String parameterName = null)
        {
            RoutineConfiguration routine = this.GetConstraint(constraintName);
            return (routine!=null && routine.ParameterDetail!=null ? routine.ParameterDetail.GetElementItemObject(parameterName) : null);
        }

        #endregion
        

        // ------------------------------------------
        // CHECK
        // ------------------------------------------

        #region Check

        /// <summary>
        /// Check value.
        /// </summary>
        /// <param name="item">The item to consider.</param>
        /// <param name="dataElement">The element to consider.</param>
        /// <param name="isDeepCheck">Indicates whether other rules than allowed and forbidden values are checked.</param>
        /// <param name="appScope">The application scope to consider.</param>
        /// <param name="scriptVariableSet">The script variable set to use.</param>
        /// <returns>The log of check log.</returns>
        public Log CheckItem(
            Object item,
            DataElement dataElement,
            Boolean isDeepCheck = false,
            IAppScope appScope = null,
            ScriptVariableSet scriptVariableSet =null)
        {
            Log log = new Log();

            if (appScope != null && appScope.AppExtension !=null)
                if (!isDeepCheck)
                {
                    Routine routine_AllowedValues =
                        appScope.CreateItem<RoutineDefinition>(null, this.GetConstraint("AllowedValues"), null, log) as Routine;
                    Routine routine_ForbiddenValues =
                        appScope.CreateItem<RoutineDefinition>(null, this.GetConstraint("ForbiddenValues"), null, log) as Routine;

                    if (routine_AllowedValues != null)
                        log.AddEvents(routine_AllowedValues.Execute(appScope, scriptVariableSet, item, dataElement));
                    if (routine_ForbiddenValues != null)
                        log.AddEvents(routine_ForbiddenValues.Execute(appScope, scriptVariableSet, item, dataElement));
                }
                else
                    foreach (RoutineConfiguration routineConfiguration in this.Items)
                    {
                        Routine routine =
                            appScope.CreateItem<RoutineDefinition>(null, routineConfiguration, null, log) as Routine;

                        if (routine != null)
                            log.AddEvents(routine.Execute(appScope, scriptVariableSet, item, dataElement));
                    }

            return log;

        }

        #endregion


        // --------------------------------------------------
        // CLONING
        // --------------------------------------------------

        #region Cloning

        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <returns>Returns a cloned instance.</returns>
        public override Object Clone()
        {
            DataConstraintStatement dataConstraintStatement = base.Clone() as DataConstraintStatement;
            return dataConstraintStatement;
        }

        #endregion

    }
}
