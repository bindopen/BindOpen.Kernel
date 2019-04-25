using System;
using System.Xml.Serialization;
using BindOpen.Framework.Core.Data.Common;
using BindOpen.Framework.Core.Data.Elements;
using BindOpen.Framework.Core.Data.Elements.Factories;
using BindOpen.Framework.Core.Data.Elements.Sets;
using BindOpen.Framework.Core.Data.Helpers.Objects;
using BindOpen.Framework.Core.Data.Items.Sets;
using BindOpen.Framework.Core.Extensions.Items.Routines;
using BindOpen.Framework.Core.System.Diagnostics;
using BindOpen.Framework.Core.System.Diagnostics.Events;

namespace BindOpen.Framework.Core.Data.Specification.Constraints
{
    /// <summary>
    /// This class represents the data constraint statement.
    /// </summary>
    [Serializable()]
    [XmlType("DataConstraintStatement", Namespace = "https://bindopen.org/xsd")]
    [XmlRoot(ElementName = "constraintStatement", Namespace = "https://bindopen.org/xsd", IsNullable = false)]
    public class DataConstraintStatement : DataItemSet<RoutineConfiguration>, IDataConstraintStatement
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
        public void AddConstraint(IRoutineConfiguration routine)
        {
            if (routine != null) Add(routine as RoutineConfiguration);
        }

        /// <summary>
        /// Adds the specified constraint.
        /// </summary>
        /// <param name="name">The name of constraint to create.</param>
        /// <param name="definitionUniqueId">The definition unique ID to create.</param>
        /// <param name="parameterDetail">The parameter detail to consider.</param>
        /// <param name="commandSet">The command set to consider.</param>
        /// <param name="outputEventSet">The output event set to consider.</param>
        /// <returns>Returns the item with the specified name.</returns>
        public IRoutineConfiguration AddConstraint(
            string name,
            string definitionUniqueId,
            IDataElementSet parameterDetail = null,
            //IDataItemSet<Command> commandSet = null,
            IDataItemSet<ConditionalEvent> outputEventSet = null)
        {
            IRoutineConfiguration routine = null; // new RoutineConfiguration(null, definitionUniqueId, commandSet, outputEventSet, parameterDetail?.Elements?.ToArray());
            Add(routine as RoutineConfiguration);

            return routine;
        }

        /// <summary>
        /// Sets the constraint parameter value.
        /// </summary>
        /// <param name="constraintName">The name of the constraint to return.</param>
        /// <param name="definitionUniqueId">The name of the definition to return.</param>
        /// <param name="parameterName">The name of the parameter to return.</param>
        /// <param name="dataValueType">The name of the parameter to return.</param>
        /// <returns>Returns the specified constrainst parameter.</returns>
        public IDataElement AddConstraintParameter(
            string constraintName,
            string definitionUniqueId = null,
            string parameterName = null,
            DataValueType dataValueType = DataValueType.Any)
        {
            IDataElement dataElement = null;

            IRoutineConfiguration routine = GetConstraint(constraintName);
            if ((routine == null) || (!routine.DefinitionUniqueId.KeyEquals(definitionUniqueId)))
                routine = AddConstraint(constraintName, definitionUniqueId);

            if (routine != null)
            {
                if (parameterName == null && routine.Count > 0)
                    dataElement = routine[0];
                else
                    dataElement = routine[parameterName];
                if (dataElement == null)
                {
                    routine.AddElement(dataElement = ElementFactory.CreateScalar(
                       parameterName,
                       dataValueType == DataValueType.Any ? dataValueType.GetValueType() : dataValueType));
                }
            }

            return dataElement;
        }

        /// <summary>
        /// Sets the constraint parameter value.
        /// </summary>
        /// <param name="constraintName">The name of the constraint to return.</param>
        /// <param name="definitionUniqueId">The name of the definition to return.</param>
        /// <param name="parameterName">The name of the parameter to return.</param>
        /// <param name="value">The value to consider.</param>
        /// <param name="dataValueType">The name of the parameter to return.</param>
        /// <returns>Returns the specified constrainst parameter.</returns>
        public bool SetConstraintParameterValue(
            string constraintName,
            string definitionUniqueId = null,
            string parameterName = null,
            object value = null,
            DataValueType dataValueType = DataValueType.Any)
        {
            IDataElement dataElement = null;

            IRoutineConfiguration routine = GetConstraint(constraintName);
            if (routine?.DefinitionUniqueId.KeyEquals(definitionUniqueId) != true)
                routine = AddConstraint(constraintName, definitionUniqueId);

            if (parameterName == null && routine.Count > 0)
                dataElement = routine[0];
            else
                dataElement = routine[parameterName];
            if (dataElement == null)
            {
                routine.AddElement(
                    dataElement = ElementFactory.CreateScalar(
                        parameterName,
                        dataValueType == DataValueType.Any ? value.GetValueType() : dataValueType,
                        value));
            }
            else
            {
                dataElement.SetItem(value);
            }

            return true;
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
        public IRoutineConfiguration GetConstraint(string name)
        {
            return GetItem(name) as RoutineConfiguration;
        }

        /// <summary>
        /// Returns the constraint parameter.
        /// </summary>
        /// <param name="constraintName">The name of the constraint to return.</param>
        /// <param name="parameterName">The name of the parameter to return.</param>
        /// <returns>Returns the specified constrainst parameter.</returns>
        public IDataElement GetConstraintParameter(
            string constraintName,
            string parameterName = null)
        {
            IRoutineConfiguration routine = GetConstraint(constraintName);
            return routine?[parameterName];
        }

        /// <summary>
        /// Returns the constraint parameter value.
        /// </summary>
        /// <param name="constraintName">The name of the constraint to return.</param>
        /// <param name="parameterName">The name of the parameter to return.</param>
        /// <returns>Returns the specified constrainst parameter.</returns>
        public object GetConstraintParameterValue(
            string constraintName,
            string parameterName = null)
        {
            IRoutineConfiguration routine = GetConstraint(constraintName);
            return routine?.GetElementObject(parameterName);
        }

        #endregion

        // --------------------------------------------------
        // CHECKING
        // --------------------------------------------------

        #region Checking

        /// <summary>
        /// Check value.
        /// </summary>
        /// <param name="item">The item to consider.</param>
        /// <param name="dataElement">The element to consider.</param>
        /// <param name="isDeepCheck">Indicates whether other rules than allowed and forbidden values are checked.</param>
        /// <param name="appScope">The application scope to consider.</param>
        /// <param name="scriptVariableSet">The script variable set to use.</param>
        /// <returns>The log of check log.</returns>
        public ILog CheckItem(
            object item,
            IDataElement dataElement,
            bool isDeepCheck = false)
        {
            ILog log = new Log();

            //if (appScope != null && appScope.AppExtension != null)
            //    if (!isDeepCheck)
            //    {
            //        Routine routine_AllowedValues =
            //            appScope.CreateItem<RoutineDefinition>(null, GetConstraint("AllowedValues"), null, log) as Routine;
            //        Routine routine_ForbiddenValues =
            //            appScope.CreateItem<RoutineDefinition>(null, GetConstraint("ForbiddenValues"), null, log) as Routine;

            //        if (routine_AllowedValues != null)
            //            log.AddEvents(routine_AllowedValues.Execute(appScope, scriptVariableSet, item, dataElement));
            //        if (routine_ForbiddenValues != null)
            //            log.AddEvents(routine_ForbiddenValues.Execute(appScope, scriptVariableSet, item, dataElement));
            //    }
            //    else
            //        foreach (IRoutineConfiguration config in Items)
            //        {
            //            Routine routine =
            //                appScope.CreateItem<RoutineDefinition>(null, config, null, log) as Routine;

            //            if (routine != null)
            //                log.AddEvents(routine.Execute(appScope, scriptVariableSet, item, dataElement));
            //        }

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
        public override object Clone()
        {
            DataConstraintStatement dataConstraintStatement = base.Clone() as DataConstraintStatement;
            return dataConstraintStatement;
        }

        #endregion
    }
}
