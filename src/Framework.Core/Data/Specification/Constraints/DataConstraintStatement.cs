using System;
using System.Xml.Serialization;
using BindOpen.Framework.Core.Application.Commands;
using BindOpen.Framework.Core.Data.Common;
using BindOpen.Framework.Core.Data.Elements;
using BindOpen.Framework.Core.Data.Elements.Sets;
using BindOpen.Framework.Core.Data.Helpers.Objects;
using BindOpen.Framework.Core.Data.Items.Sets;
using BindOpen.Framework.Core.Extensions.Configuration.Routines;
using BindOpen.Framework.Core.System.Diagnostics.Events;

namespace BindOpen.Framework.Core.Data.Specification.Constraints
{
    /// <summary>
    /// This class represents the data constraint statement.
    /// </summary>
    [Serializable()]
    [XmlType("DataConstraintStatement", Namespace = "http://meltingsoft.com/bindopen/xsd")]
    [XmlRoot(ElementName = "constraintStatement", Namespace = "http://meltingsoft.com/bindopen/xsd", IsNullable = false)]
    public class DataConstraintStatement : DataItemSet<IRoutineConfiguration>, IDataConstraintStatement
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
            if (routine != null) this.Add(routine);
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
        public IRoutineConfiguration AddConstraint(
            string name,
            string definitionName,
            IDataElementSet parameterDetail = null,
            IDataItemSet<ICommand> commandSet = null,
            IDataItemSet<IConditionalEvent> outputEventSet = null)
        {
            IRoutineConfiguration routine = new RoutineConfiguration(name, definitionName, parameterDetail, commandSet, outputEventSet);
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
        public IDataElement AddConstraintParameter(
            string constraintName, string definitionName = null,
            string parameterName = null, DataValueType dataValueType = DataValueType.Any)
        {
            IDataElement dataElement = null;

            IRoutineConfiguration routine = this.GetConstraint(constraintName);
            if ((routine == null) || (!routine.DefinitionUniqueId.KeyEquals(definitionName)))
                routine = this.AddConstraint(constraintName, definitionName);

            if (routine?.ParameterDetail != null)
            {
                if (parameterName == null && routine.ParameterDetail.Count > 0)
                    dataElement = routine.ParameterDetail[0];
                else
                    dataElement = routine.ParameterDetail[parameterName];
                if (dataElement == null)
                {
                    routine.ParameterDetail.AddElement(dataElement = DataElement.Create(
                       dataValueType == DataValueType.Any ?
                       dataValueType.GetValueType() :
                       dataValueType, parameterName));
                }
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
        public bool SetConstraintParameterValue(
            string constraintName, string definitionName = null,
            string parameterName = null, object value = null, DataValueType dataValueType = DataValueType.Any)
        {
            IDataElement dataElement = null;

            IRoutineConfiguration routine = this.GetConstraint(constraintName);
            if ((routine == null) || (!routine.DefinitionUniqueId.KeyEquals(definitionName)))
                routine = this.AddConstraint(constraintName, definitionName);

            if (routine?.ParameterDetail != null)
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
        public IRoutineConfiguration GetConstraint(string name)
        {
            return this.GetItem(name) as RoutineConfiguration;
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
            IRoutineConfiguration routine = this.GetConstraint(constraintName);
            return routine?.ParameterDetail != null ? routine.ParameterDetail[parameterName] : null;
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
            IRoutineConfiguration routine = this.GetConstraint(constraintName);
            return routine?.ParameterDetail != null ? routine.ParameterDetail.GetElementItemObject(parameterName) : null;
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
