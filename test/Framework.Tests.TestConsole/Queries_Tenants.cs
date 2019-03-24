using BindOpen.Framework.Core.Data.Common;
using BindOpen.Framework.Core.Data.Expression;
using BindOpen.Framework.Databases.Data.Queries;
using BindOpen.Framework.Databases.Extensions.Carriers;

namespace BindOpen.Framework.Labs.Platform.Api.Dal.Database.Queries.Iam
{
    public static class Queries_Tenants
    {
        public static AdvancedDbDataQuery GetOrganizations(
             string tenantName, string dataModuleName = "Sphere.Kernel")
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

                            new DbField("TenantId", "tenant") { Alias = "tenant.TenantId" },
                            new DbField("Name", "tenant") { Alias = "tenant.Name" },

                            new DbField("OrganizationId", "parent") { Alias = "parent.OrganizationId" },
                            new DbField("Name", "parent") { Alias = "parent.Name" },
                 },
                 FromClauses =
                 {
                            new DbDataQueryFromStatement()
                            {
                                JointureStatements=
                                {
                                    new DbDataQueryJointureStatement(
                                        DbDataQueryJointureKind.None,
                                        new DbTable("Organization", "Iam", null) { Alias="organization" }),

                                    new DbDataQueryJointureStatement(
                                        DbDataQueryJointureKind.Left,
                                        new DbTable("Organization", "Iam", null) { Alias="parent" },
                                        new DbField("OrganizationId", "parent"),
                                        new DbField("ParentId", "organization")),

                                    new DbDataQueryJointureStatement(
                                        DbDataQueryJointureKind.Left,
                                        new DbTable("TTenant", "Iam", null) { Alias="tenant" },
                                        new DbField("TTenant.TenantId", "tenant"),
                                        new DbField("TenantId", "organization")),
                                }
                            }
                 }
             };

        public static DbDataQuery InsertOrganization(string tenantName, string dataModuleName = "Sphere.Kernel")
            => new BasicDbDataQuery(DbDataQueryKind.Insert, dataModuleName, null, "DbOrganization")
            {
                Name = "InsertOrganization",
                Fields =
                {
                            new DbField("DbOrganization.CreationDate", new DataExpression("$(sqlGetCurrentDate)")),
                            new DbField("DbOrganization.DisplayName", new DataExpression("title"), DataValueType.Text),
                            new DbField("DbOrganization.Description", new DataExpression("description"), DataValueType.Text),
                            new DbField("DbOrganization.LastModificationDate", new DataExpression("$(sqlGetCurrentDate)")),
                            new DbField("DbOrganization.Name", new DataExpression("name"), DataValueType.Text),
                            new DbField("DbOrganization.ParentId",
                                new BasicDbDataQuery(DbDataQueryKind.Select,null, "Iam","Organization")
                                {
                                    Top = 1,
                                    Fields=
                                    {
                                        new DbField("DbOrganization.OrganizationId")
                                    },
                                    IdFields=
                                    {
                                        new DbField("DbOrganization.Name", new DataExpression("ParentName"), DataValueType.Text)
                                    }
                                }),
                            new DbField("Organization.rowguid", new DataExpression("$(sqlnewguid)")),
                            new DbField("Organization.TenantId",
                                new BasicDbDataQuery(DbDataQueryKind.Select,null, "Iam","DbTTenant")
                                {
                                    Top = 1,
                                    Fields=
                                    {
                                        new DbField("DbTTenant.TenantId")
                                    },
                                    IdFields=
                                    {
                                        new DbField("DbTTenant.Name", new DataExpression("tenantName"), DataValueType.Text)
                                    }
                                })
                }
            };

        public static AdvancedDbDataQuery GetTenants(string dataModuleName = "platform.db")
        => new AdvancedDbDataQuery(DbDataQueryKind.Select, dataModuleName, null, null)
        {
            Name = "SelectTenants",
            IsDistinct = true,
            Fields =
            {
                new DbField("tenant")
                {
                    IsAll=true
                },
                new DbField("Name", "status"),
                new DbField("Name", "visibility")
            },
            FromClauses =
            {
                new DbDataQueryFromStatement()
                {
                    JointureStatements=
                    {
                        new DbDataQueryJointureStatement(
                            DbDataQueryJointureKind.None,
                            new DbTable("Tenant", "Iam", null) { Alias="tenant" }),

                        new DbDataQueryJointureStatement(
                            DbDataQueryJointureKind.Left,
                            new DbTable("ReferenceItem", "Mdm", null) { Alias="status" },
                            new DbField("ReferenceItemID", "status"),
                            new DbField("StatusReferenceID", "tenant")),

                        new DbDataQueryJointureStatement(
                            DbDataQueryJointureKind.Left,
                            new DbTable("ReferenceItem", "Mdm", null) { Alias="visibility" },
                            new DbField("ReferenceItemID", "visibility"),
                            new DbField("VisibilityReferenceID", "tenant"))
                    }
                }
            },
        };

        public static DbDataQuery GetTenant(string name, string dataModuleName = "platform.db")
        => new BasicDbDataQuery(DbDataQueryKind.Select, dataModuleName, null, "Tenant")
        {
            Name = "SelectTenants",
            IsDistinct = true,
            Fields =
            {
                new DbField("tenant")
                {
                    IsAll=true
                },
                new DbField("Name", "status"),
                new DbField("Name", "visibility")
            },
            FromClauses =
            {
                new DbDataQueryFromStatement()
                {
                    JointureStatements=
                    {
                        new DbDataQueryJointureStatement(
                            DbDataQueryJointureKind.None,
                            new DbTable("Tenant", "Iam", null) { Alias="tenant" }),

                        new DbDataQueryJointureStatement(
                            DbDataQueryJointureKind.Left,
                            new DbTable("ReferenceItem", "Mdm", null) { Alias="status" })
                        {
                            Condition=new DataExpression(
                                "$SqlEq($SqlTable('status').SqlField('ReferenceItemID'), $SqlTable('tenant').SqlField('StatusReferenceID'))"
                            )
                        },

                        new DbDataQueryJointureStatement(
                            DbDataQueryJointureKind.Left,
                            new DbTable("ReferenceItem", "Mdm", null) { Alias="visibility" })
                        {
                            Condition=new DataExpression(
                                "$SqlEq($SqlTable('visibility').SqlField('ReferenceItemID'), $SqlTable('tenant').SqlField('VisibilityReferenceID'))"
                            )
                        }
                    }
                }
            },
            IdFields =
            {
                new DbField("Name", "tenant", DataValueType.Text, name)
            }
        };

        public static DbDataQuery DeleteTenant(string name, string dataModuleName = "platform.db")
        => new BasicDbDataQuery(DbDataQueryKind.Delete, dataModuleName, "Iam", "Tenant")
        {
            Name = "DeleteTenant",
            IdFields =
            {
                new DbField("Name", DataValueType.Text, name)
            }
        };

        public static DbDataQuery UpdateTenant(string name, string dataModuleName = "platform.db")
        => new BasicDbDataQuery(DbDataQueryKind.Update, dataModuleName, "Iam", "Tenant")
        {
            Name = "UpdateTenant",
            Fields =
            {
                new DbField(
                    "VisibilityReferenceID",
                    new DataExpression("$SqlTable('visibility').SqlField('ReferenceItemID')"))
            },
            FromClauses =
            {
                new DbDataQueryFromStatement()
                {
                    JointureStatements=
                    {
                        new DbDataQueryJointureStatement(
                            DbDataQueryJointureKind.None,
                            new DbTable("Tenant", "Iam", null) { Alias="tenant" }),

                        new DbDataQueryJointureStatement(
                            DbDataQueryJointureKind.Left,
                            new DbTable("ReferenceItem", "Mdm", null) { Alias="visibility" })
                        {
                            Condition=new DataExpression(
                                "$SqlEq($SqlTable('visibility').SqlField('ReferenceItemID'), $SqlTable('tenant').SqlField('VisibilityReferenceID'))"
                            )
                        }
                    }
                }
            },
            IdFields =
            {
                new DbField("Name", "tenant", DataValueType.Text, name)
            }
        };

        public static DbDataQuery InsertTenant(string name, string dataModuleName = "platform.db")
        => new BasicDbDataQuery(DbDataQueryKind.Insert, dataModuleName, null, "Tenant")
        {
            Name = "InsertTenant",
            Fields =
            {
                new DbField("Name",
                    new BasicDbDataQuery(DbDataQueryKind.Select, "platform.db", null, "Tenant") {
                        Top=1,
                        IdFields =
                            {
                                new DbField("Name", DataValueType.Text, name)
                            }
                        }
                    ),
            }
        };
    }
}
