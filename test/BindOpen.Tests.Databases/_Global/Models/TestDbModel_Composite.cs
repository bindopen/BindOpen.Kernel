using BindOpen.Data.Common;
using BindOpen.Data.Elements;
using BindOpen.Data.Models;
using BindOpen.Data.Queries;
using BindOpen.Tests.Databases.Dtos;
using BindOpen.Tests.Databases.Entities;

namespace BindOpen.Tests.Databases.Models
{
    /// <summary>
    /// This class represents a database model.
    /// </summary>
    public partial class TestDbModel : BdoDbModel
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        internal IDbQuery UpsertEmployee(EmployeeDto employee)
        {
            return DbFluent.Upsert(Table("Employee"))
                .WithQueries(
                    DbFluent.UpdateQuery(Table("Employee"))
                    .WithFields(q => new[]
                    {
                        DbFluent.FieldAsParameter(nameof(DbEmployee.Code), q.UseParameter("code", DataValueType.Text)),
                        DbFluent.FieldAsParameter(nameof(DbEmployee.ContactEmail), q.UseParameter("contactEmail", DataValueType.Text)),
                        DbFluent.FieldAsParameter(nameof(DbEmployee.FisrtName), q.UseParameter("fisrtName", DataValueType.Text)),
                        DbFluent.FieldAsParameter(nameof(DbEmployee.FisrtName), q.UseParameter("fisrtName", DataValueType.Text)),
                        DbFluent.FieldAsParameter(nameof(DbEmployee.LastName), q.UseParameter("lastName", DataValueType.Text)),
                        DbFluent.FieldAsParameter(nameof(DbEmployee.StaffNumber), q.UseParameter("staffNumber", DataValueType.Text))
                    }),
                    DbFluent.InsertQuery(Table("Employee"))
                        .WithFields(q => new[]
                        {
                            DbFluent.FieldAsParameter(nameof(DbEmployee.Code), q.UseParameter("code", DataValueType.Text)),
                            DbFluent.FieldAsParameter(nameof(DbEmployee.ContactEmail), q.UseParameter("contactEmail", DataValueType.Text)),
                            DbFluent.FieldAsParameter(nameof(DbEmployee.FisrtName), q.UseParameter("fisrtName", DataValueType.Text)),
                            DbFluent.FieldAsParameter(nameof(DbEmployee.LastName), q.UseParameter("lastName", DataValueType.Text)),
                            DbFluent.FieldAsParameter(nameof(DbEmployee.StaffNumber), q.UseParameter("staffNumber", DataValueType.Text))
                        })
                        .WithParameters(
                            ElementFactory.Create("code", employee.Code),
                            ElementFactory.Create("contactEmail", employee.ContactEmail),
                            ElementFactory.Create("fisrtName", employee.FisrtName),
                            ElementFactory.Create("lastName", employee.LastName),
                            ElementFactory.Create("staffNumber", employee.StaffNumber)))
                .WithCTE(
                    DbFluent.Table(
                        DbFluent.SelectQuery(null)
                            .WithFields(q => new[]
                            {
                                DbFluent.FieldAsParameter(nameof(DbEmployee.Code), q.UseParameter("code", DataValueType.Text))
                            }))
                    .WithAlias("T"))
                .WithParameters(
                    ElementFactory.Create("code", employee.Code),
                    ElementFactory.Create("contactEmail", employee.ContactEmail),
                    ElementFactory.Create("fisrtName", employee.FisrtName),
                    ElementFactory.Create("lastName", employee.LastName),
                    ElementFactory.Create("staffNumber", employee.StaffNumber));
        }
    }
}
