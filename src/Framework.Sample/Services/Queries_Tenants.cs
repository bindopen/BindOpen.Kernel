using BindOpen.Framework.Databases.Data.Queries;
using BindOpen.Framework.Databases.Extensions.Carriers;

namespace BindOpen.Framework.Sample.Services
{
    public static class Queries_Tenants
    {
        public static AdvancedDbDataQuery GetOrganizations(
             string tenantName, string dataModuleName = "Kernel")
             => new AdvancedDbDataQuery(DbDataQueryKind.Select, dataModuleName, null, null)
             {
                 Name = "SelectOrganizations",
                 IsDistinct = true,
                 Fields =
                 {
                        new DbField("CreationDate", "organization"),
                        new DbField("Description", "organization"),
                        new DbField("DisplayName", "organization"),
                        new DbField("LastModificationDate", "organization"),
                        new DbField("Name", "organization"),
                        new DbField("ParentId", "organization"),
                        new DbField("OrganizationId", "organization"),
                        new DbField("rowguid", "organization"),
                        new DbField("TenantId", "organization"),

                        new DbField("TenantId", "tenant").WithAlias("tenant.TenantId"),
                        new DbField("Name", "tenant").WithAlias("tenant.Name"),

                        new DbField("OrganizationId", "parent").WithAlias("parent.OrganizationId"),
                        new DbField("Name", "parent").WithAlias("parent.Name"),
                 },
                 FromClauses =
                 {
                            new DbDataQueryFromStatement()
                            {
                                JointureStatements=
                                {
                                    new DbDataQueryJointureStatement(
                                        DbDataQueryJointureKind.None,
                                        new DbTable("Organization", "Iam", null).WithAlias("organization" )),

                                    new DbDataQueryJointureStatement(
                                        DbDataQueryJointureKind.Left,
                                        new DbTable("Organization", "Iam", null).WithAlias("parent"),
                                        new DbField("OrganizationId", "parent"),
                                        new DbField("ParentId", "organization")),

                                    new DbDataQueryJointureStatement(
                                        DbDataQueryJointureKind.Left,
                                        new DbTable("TTenant", "Iam", null).WithAlias("tenant"),
                                        new DbField("TTenant.TenantId", "tenant"),
                                        new DbField("TenantId", "organization")),
                                }
                            }
                 }
             };
    }
}
