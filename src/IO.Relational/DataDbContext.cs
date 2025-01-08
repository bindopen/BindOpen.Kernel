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

    //public DbSet<AssemblyReferenceDb> AssemblyReferences { get; set; }

    public DbSet<ClassReferenceDb> ClassReferences { get; set; }

    // Conditions

    public DbSet<ConditionDb> Conditions { get; set; }

    // Meta

    public DbSet<MetaDataDb> MetaDatas { get; set; }

    public DbSet<MetaSetDb> MetaSets { get; set; }

    // Meta.Configuration

    public DbSet<ConfigurationDb> Configurations { get; set; }

    // Meta.Definition

    public DbSet<DefinitionDb> Definitions { get; set; }

    public DbSet<SpecDb> Specs { get; set; }

    public DbSet<SpecRuleDb> SpecRules { get; set; }

    public DbSet<SpecSetDb> SpecSets { get; set; }

    // Objects.Dictionary

    public DbSet<KeyValuePairDb> KeyValuePairs { get; set; }

    public DbSet<StringDictionaryDb> StringDictionaries { get; set; }

    // Objects.Expression

    public DbSet<ExpressionDb> Expressions { get; set; }

    // Objects.Mergers

    public DbSet<MergerDb> Mergers { get; set; }

    // Objects.Reference

    public DbSet<ReferenceDb> References { get; set; }

    // Scoping.Extensions

    //public DbSet<ExtensionDefinitionDb> ExtensionDefinitions { get; set; }

    public DbSet<ExtensionGroupDb> ExtensionGroups { get; set; }

    public DbSet<PackageDefinitionDb> PackageDefinitions { get; set; }

    // Scoping.Connectors

    public DbSet<ConnectorDefinitionDb> ConnectorDefinitions { get; set; }

    public DbSet<ConnectorDictionaryDb> ConnectorDictionaries { get; set; }

    //public DbSet<EntityDefinitionDb> EntityDefinitions { get; set; }

    //public DbSet<EntityDictionaryDb> EntityDictionaries { get; set; }

    //public DbSet<FunctionDefinitionDb> FunctionDefinitions { get; set; }

    //public DbSet<FunctionDictionaryDb> FunctionDictionaries { get; set; }

    public DbSet<ScriptwordDb> Scriptwords { get; set; }

    //public DbSet<TaskDefinitionDb> TaskDefinitions { get; set; }

    //public DbSet<TaskDictionaryDb> TaskDictionaries { get; set; }

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
            if (displayName.EndsWith("Db"))
            {
                displayName = displayName[0..^2];
            }

            entityType.SetSchema("bdo");
            entityType.SetTableName(displayName);
        }

        // multiple keys

        modelBuilder.Entity<KeyValuePairDb>()
            .HasKey(nameof(KeyValuePairDb.Key), nameof(KeyValuePairDb.StringDictionaryId));

        // delete cascade

        modelBuilder
            .Entity<StringDictionaryDb>()
            .HasMany(e => e.Values)
            .WithOne(e => e.StringDictionary)
            .OnDelete(DeleteBehavior.ClientCascade);

        modelBuilder
            .Entity<ExpressionDb>()
            .HasOne(e => e.Scriptword)
            .WithOne(e => e.Expression)
            .OnDelete(DeleteBehavior.ClientCascade);

        modelBuilder
            .Entity<ReferenceDb>()
            .HasOne(e => e.Expression)
            .WithOne(e => e.Reference)
            .OnDelete(DeleteBehavior.ClientCascade);

        modelBuilder
            .Entity<MetaDataDb>()
            .HasOne(e => e.ClassReference)
            .WithOne(e => e.MetaData)
            .OnDelete(DeleteBehavior.ClientCascade);

        // table relationships

        modelBuilder.Entity<MetaSetDb>()
            .HasMany(e => e.Items)
            .WithMany(e => e.Sets)
            .UsingEntity($"{nameof(MetaDataDb)}_{nameof(MetaSetDb)}".Replace("Db", ""),
                l => l.HasOne(typeof(MetaDataDb))
                    .WithMany()
                    .HasForeignKey($"{nameof(MetaDataDb)[0..^2]}Id")
                    .HasPrincipalKey(nameof(MetaDataDb.Identifier))
                    .OnDelete(DeleteBehavior.ClientCascade),
                r => r.HasOne(typeof(MetaSetDb))
                    .WithMany()
                    .HasForeignKey($"{nameof(MetaSetDb)[0..^2]}Id")
                    .HasPrincipalKey(nameof(MetaSetDb.Identifier))
                    .OnDelete(DeleteBehavior.ClientCascade),
                j => j.HasKey($"{nameof(MetaDataDb)[0..^2]}Id", $"{nameof(MetaSetDb)[0..^2]}Id"));

        modelBuilder.Entity<SpecDb>()
            .HasMany(e => e.Items)
            .WithMany(e => e.Supers)
            .UsingEntity($"{nameof(SpecDb)}_{nameof(SpecDb)}".Replace("Db", ""),
                l => l.HasOne(typeof(SpecDb))
                    .WithMany()
                    .HasForeignKey($"{nameof(SpecDb)[0..^2]}ItemId")
                    .HasPrincipalKey(nameof(SpecDb.Identifier)),
                r => r.HasOne(typeof(SpecDb))
                    .WithMany()
                    .HasForeignKey($"{nameof(SpecDb)[0..^2]}SetId")
                    .HasPrincipalKey(nameof(SpecDb.Identifier)),
                j => j.HasKey($"{nameof(SpecDb)[0..^2]}ItemId", $"{nameof(SpecDb)[0..^2]}SetId"));

        modelBuilder.Entity<ExtensionGroupDb>()
            .HasMany(e => e.SubGroups)
            .WithMany(e => e.Supers)
            .UsingEntity($"{nameof(ExtensionGroupDb)}_{nameof(ExtensionGroupDb)}".Replace("Db", ""),
                l => l.HasOne(typeof(ExtensionGroupDb))
                    .WithMany()
                    .HasForeignKey($"{nameof(ExtensionGroupDb)[0..^2]}SubId")
                    .HasPrincipalKey(nameof(ExtensionGroupDb.Identifier)),
                r => r.HasOne(typeof(ExtensionGroupDb))
                    .WithMany()
                    .HasForeignKey($"{nameof(ExtensionGroupDb)[0..^2]}SetId")
                    .HasPrincipalKey(nameof(ExtensionGroupDb.Identifier)),
                j => j.HasKey($"{nameof(ExtensionGroupDb)[0..^2]}SubId", $"{nameof(ExtensionGroupDb)[0..^2]}SetId"));

        //modelBuilder.Entity<ExtensionDefinitionDb>()
        //    .HasMany(e => e.Groups)
        //    .WithMany(e => e.Definitions)
        //    .UsingEntity($"{nameof(ExtensionDefinitionDb)}_{nameof(ExtensionGroupDb)}".Replace("Db", ""),
        //        l => l.HasOne(typeof(ExtensionDefinitionDb))
        //            .WithMany()
        //            .HasForeignKey($"{nameof(ExtensionDefinitionDb)[0..^2]}Id")
        //            .HasPrincipalKey(nameof(ExtensionDefinitionDb.Identifier)),
        //        r => r.HasOne(typeof(ExtensionGroupDb))
        //            .WithMany()
        //            .HasForeignKey($"{nameof(ExtensionGroupDb)[0..^2]}Id")
        //            .HasPrincipalKey(nameof(ExtensionGroupDb.Identifier)),
        //        j => j.HasKey($"{nameof(ExtensionDefinitionDb)[0..^2]}Id", $"{nameof(ExtensionGroupDb)[0..^2]}Id"));
    }
}
