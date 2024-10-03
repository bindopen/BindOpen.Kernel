using BindOpen.Data.Assemblies;
using BindOpen.Data.Conditions;
using BindOpen.Data.Meta;
using BindOpen.Scoping;
using BindOpen.Scoping.Connectors;
using BindOpen.Scoping.Script;
using Microsoft.EntityFrameworkCore;

namespace BindOpen.Data;

public partial class DataDbContext : DbContext
{
    // Assemblies

    public DbSet<AssemblyReferenceDto> AssemblyReferences { get; set; }

    public DbSet<ClassReferenceDto> ClassReferences { get; set; }

    // Conditions

    public DbSet<BasicConditionDto> BasicConditions { get; set; }

    public DbSet<CompositeConditionDto> CompositeConditions { get; set; }

    //public DbSet<ConditionDto> Conditions { get; set; }

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

    // Scoping.Extensions

    //public DbSet<ExtensionDefinitionDto> ExtensionDefinitions { get; set; }

    public DbSet<ExtensionGroupDto> ExtensionGroups { get; set; }

    public DbSet<PackageDefinitionDto> PackageDefinitions { get; set; }

    // Scoping.Connectors

    public DbSet<ConnectorDefinitionDto> ConnectorDefinitions { get; set; }

    public DbSet<ConnectorDictionaryDto> ConnectorDictionaries { get; set; }

    //public DbSet<EntityDefinitionDto> EntityDefinitions { get; set; }

    //public DbSet<EntityDictionaryDto> EntityDictionaries { get; set; }

    //public DbSet<FunctionDefinitionDto> FunctionDefinitions { get; set; }

    //public DbSet<FunctionDictionaryDto> FunctionDictionaries { get; set; }

    public DbSet<ScriptwordDto> Scriptwords { get; set; }

    //public DbSet<TaskDefinitionDto> TaskDefinitions { get; set; }

    //public DbSet<TaskDictionaryDto> TaskDictionaries { get; set; }

    public DataDbContext(DbContextOptions<DataDbContext> options) : base(options)
    {
        ChangeTracker.LazyLoadingEnabled = false;
        ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
    }

    private string _connectionString;

    public DataDbContext(string connectionString) : base()
    {
        _connectionString = connectionString;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    => optionsBuilder
        .AddInterceptors(new DataDbContextInterceptor());

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // table names

        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            var displayName = entityType.DisplayName();
            if (displayName.EndsWith("Dto"))
            {
                displayName = displayName[0..^3];
            }

            entityType.SetSchema("bdo");
            entityType.SetTableName(displayName);
        }

        // multiple keys

        modelBuilder.Entity<KeyValuePairDto>()
            .HasKey(nameof(KeyValuePairDto.Key), nameof(KeyValuePairDto.StringDictionaryId));

        // delete cascade

        modelBuilder
            .Entity<StringDictionaryDto>()
            .HasMany(e => e.Values)
            .WithOne(e => e.StringDictionary)
            .OnDelete(DeleteBehavior.ClientCascade);

        modelBuilder
            .Entity<ExpressionDto>()
            .HasOne(e => e.Scriptword)
            .WithOne(e => e.Expression)
            .OnDelete(DeleteBehavior.ClientCascade);

        modelBuilder
            .Entity<ReferenceDto>()
            .HasOne(e => e.Expression)
            .WithOne(e => e.Reference)
            .OnDelete(DeleteBehavior.ClientCascade);

        // table relationships

        modelBuilder.Entity<MetaSetDto>()
            .HasMany(e => e.Items)
            .WithMany(e => e.Supers)
            .UsingEntity($"{nameof(MetaDataDto)}_{nameof(MetaSetDto)}".Replace("Dto", ""),
                l => l.HasOne(typeof(MetaDataDto))
                    .WithMany()
                    .HasForeignKey($"{nameof(MetaDataDto)[0..^3]}Id")
                    .HasPrincipalKey(nameof(MetaDataDto.Identifier)),
                r => r.HasOne(typeof(MetaSetDto))
                    .WithMany()
                    .HasForeignKey($"{nameof(MetaSetDto)[0..^3]}Id")
                    .HasPrincipalKey(nameof(MetaSetDto.Identifier)),
                j => j.HasKey($"{nameof(MetaDataDto)[0..^3]}Id", $"{nameof(MetaSetDto)[0..^3]}Id"));

        modelBuilder.Entity<SpecDto>()
            .HasMany(e => e.Items)
            .WithMany(e => e.Supers)
            .UsingEntity($"{nameof(SpecDto)}_{nameof(SpecDto)}".Replace("Dto", ""),
                l => l.HasOne(typeof(SpecDto))
                    .WithMany()
                    .HasForeignKey($"{nameof(SpecDto)[0..^3]}ItemId")
                    .HasPrincipalKey(nameof(SpecDto.Identifier)),
                r => r.HasOne(typeof(SpecDto))
                    .WithMany()
                    .HasForeignKey($"{nameof(SpecDto)[0..^3]}SetId")
                    .HasPrincipalKey(nameof(SpecDto.Identifier)),
                j => j.HasKey($"{nameof(SpecDto)[0..^3]}ItemId", $"{nameof(SpecDto)[0..^3]}SetId"));

        modelBuilder.Entity<ExtensionGroupDto>()
            .HasMany(e => e.SubGroups)
            .WithMany(e => e.Supers)
            .UsingEntity($"{nameof(ExtensionGroupDto)}_{nameof(ExtensionGroupDto)}".Replace("Dto", ""),
                l => l.HasOne(typeof(ExtensionGroupDto))
                    .WithMany()
                    .HasForeignKey($"{nameof(ExtensionGroupDto)[0..^3]}SubId")
                    .HasPrincipalKey(nameof(ExtensionGroupDto.Identifier)),
                r => r.HasOne(typeof(ExtensionGroupDto))
                    .WithMany()
                    .HasForeignKey($"{nameof(ExtensionGroupDto)[0..^3]}SetId")
                    .HasPrincipalKey(nameof(ExtensionGroupDto.Identifier)),
                j => j.HasKey($"{nameof(ExtensionGroupDto)[0..^3]}SubId", $"{nameof(ExtensionGroupDto)[0..^3]}SetId"));

        //modelBuilder.Entity<ExtensionDefinitionDto>()
        //    .HasMany(e => e.Groups)
        //    .WithMany(e => e.Definitions)
        //    .UsingEntity($"{nameof(ExtensionDefinitionDto)}_{nameof(ExtensionGroupDto)}".Replace("Dto", ""),
        //        l => l.HasOne(typeof(ExtensionDefinitionDto))
        //            .WithMany()
        //            .HasForeignKey($"{nameof(ExtensionDefinitionDto)[0..^3]}Id")
        //            .HasPrincipalKey(nameof(ExtensionDefinitionDto.Identifier)),
        //        r => r.HasOne(typeof(ExtensionGroupDto))
        //            .WithMany()
        //            .HasForeignKey($"{nameof(ExtensionGroupDto)[0..^3]}Id")
        //            .HasPrincipalKey(nameof(ExtensionGroupDto.Identifier)),
        //        j => j.HasKey($"{nameof(ExtensionDefinitionDto)[0..^3]}Id", $"{nameof(ExtensionGroupDto)[0..^3]}Id"));
    }
}
