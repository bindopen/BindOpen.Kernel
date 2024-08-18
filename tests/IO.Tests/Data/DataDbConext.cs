using BindOpen.Data.Assemblies;
using BindOpen.Data.Conditions;
using BindOpen.Data.Meta;
using BindOpen.Scoping.Connectors;
using Microsoft.EntityFrameworkCore;

namespace BindOpen.Data;

public class DataDbConext : DbContext
{
    // Assemblies

    public DbSet<AssemblyReferenceDto> AssemblyReferences { get; set; }

    public DbSet<ClassReferenceDto> ClassReferences { get; set; }

    // Conditions

    public DbSet<BasicConditionDto> BasicConditions { get; set; }

    public DbSet<CompositeConditionDto> CompositeConditions { get; set; }

    public DbSet<ConditionDto> Conditions { get; set; }

    public DbSet<ExpressionConditionDto> ExpressionConditions { get; set; }

    // Meta

    public DbSet<MetaDataDto> MetaDatas { get; set; }

    public DbSet<MetaNodeDto> MetaNodes { get; set; }

    public DbSet<MetaObjectDto> MetaObjects { get; set; }

    public DbSet<MetaScalarDto> MetaScalars { get; set; }

    public DbSet<MetaSetDto> MetaSets { get; set; }

    // Meta.Configuration

    public DbSet<ConfigurationDto> Configurations { get; set; }

    // Meta.Definition

    public DbSet<DefinitionDto> Definitions { get; set; }

    public DbSet<SpecDto> Specs { get; set; }

    public DbSet<SpecRuleDto> SpecRules { get; set; }

    public DbSet<SpecSetDto> SpecSets { get; set; }

    // Objects.Dictionary

    public DbSet<KeyValuePairDto> KeyValuePairs { get; set; }

    public DbSet<StringDictionaryDto> StringDictionaries { get; set; }

    // Objects.Expression

    public DbSet<ExpressionDto> Expressions { get; set; }

    // Objects.Mergers

    public DbSet<MergerDto> Mergers { get; set; }

    // Objects.Reference

    public DbSet<ReferenceDto> References { get; set; }

    // Scoping.Connectors

    public DbSet<ConnectorDefinitionDto> ConnectorDefinitions { get; set; }

    public DbSet<ConnectorDictionaryDto> ConnectorDictionaries { get; set; }

    public DataDbConext(DbContextOptions<DataDbConext> options) : base(options)
    {
        ChangeTracker.LazyLoadingEnabled = false;
        ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
    }
}
