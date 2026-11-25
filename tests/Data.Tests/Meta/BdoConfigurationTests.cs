using BindOpen.Data.Meta;
using Bogus;
using FluentAssertions;
using NUnit.Framework;
using System.Linq;
using static BindOpen.Data.BdoData;

namespace BindOpen.Data.Meta;

/// <summary>
/// Provides comprehensive unit tests for <see cref="IBdoConfiguration"/> management.
/// </summary>
/// <remarks>
/// These tests verify configuration creation, hierarchical navigation (descendants),
/// deep cloning, and merging/updating behaviors across nested configurations.
/// </remarks>
[TestFixture]
public class BdoConfigurationTests
{
    private readonly string _configName10 = "Child1";
    private readonly string _configName10a = "Child1a";
    private readonly string _configName20 = "Child2";

    /// <summary>
    /// Gets or sets the primary parent configuration used across test cases.
    /// </summary>
    public IBdoConfiguration _config10 = null;

    /// <summary>
    /// Gets or sets the child configuration nested under <see cref="_config10"/>.
    /// </summary>
    public IBdoConfiguration _config10a = null;

    /// <summary>
    /// Gets or sets the secondary parent configuration used for merge and update tests.
    /// </summary>
    public IBdoConfiguration _config20 = null;

    /// <summary>
    /// Gets or sets the child configuration nested under <see cref="_config20"/>.
    /// </summary>
    public IBdoConfiguration _config20a = null;

    /// <summary>
    /// Initializes complex configuration hierarchies with randomized test data before executing tests.
    /// </summary>
    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        var f = new Faker();

        // Sub-configuration 1a
        _config10a = NewConfig(
            _configName10a,
            NewScalar("float1", DataValueTypes.Number, f.Random.Float()))
            .Using(_configName10, _configName20);

        // Parent configuration 10
        _config10 = NewConfig(_configName10)
            .WithTitle("myConfiguration")
            .WithDescription(("en", "Sample of description"))
            .WithChildren(_config10a)
            .Add(
                ("float1", f.Random.Float()),
                ("text1", f.Lorem.Words(10)),
                ("integer1", Enumerable.Range(0, 10).Select(p => f.Random.Int(5000))),
                ("byteArray1", Enumerable.Range(1, 2).Select(p => f.Random.Bytes(5000)).ToArray()),
                NewSet("$node1")
                    .Add(
                        ("textB1", f.Lorem.Words(10)),
                        (object)("textB2", f.Random.Int(5000)))
            );

        // Sub-configuration 20a
        _config20a = NewConfig(
            _configName10a,
            NewScalar("float1", f.Random.Float()),
            NewScalar("text1", f.Lorem.Words(10)))
            .Using(_configName10, _configName20);

        // Parent configuration 20
        _config20 = NewConfig(
            _configName20,
            NewScalar("float2", DataValueTypes.Number, 1.1, 1.2, 1.3),
            NewScalar("float1", DataValueTypes.Number, f.Random.Float()),
            NewSet(
                "$node1",
                NewScalar("textB1", DataValueTypes.Text, f.Lorem.Words(10)),
                NewScalar("coolB1", DataValueTypes.Boolean, f.Random.Bool())))
            .WithChildren(_config20a);
    }

    /// <summary>
    /// Asserts that two <see cref="IBdoConfiguration"/> instances are structurally equivalent,
    /// excluding their parent references.
    /// </summary>
    /// <param name="config1">The first configuration to compare.</param>
    /// <param name="config2">The second configuration to compare.</param>
    /// <exception cref="AssertionException">
    /// Thrown when only one of the configurations is <see langword="null"/> or when their properties do not match.
    /// </exception>
    public static void AssertEquals(IBdoConfiguration config1, IBdoConfiguration config2)
    {
        if ((config1 != null && config2 == null) || (config1 == null && config2 != null))
        {
            Assert.Fail("One configuration is null while the other is not.");
        }

        config1.Should().BeEquivalentTo(config2, options => options.Excluding(x => x.Parent));
    }

    /// <summary>
    /// Verifies basic typed data retrieval from a configuration by key.
    /// </summary>
    [Test]
    public void GetConfigurationItem1()
    {
        var text = _config10.GetData<string>("text1");
        text.Should().NotBeNullOrEmpty("The configuration should contain a valid string under the 'text1' key.");
    }

    /// <summary>
    /// Verifies descendant metadata resolution using empty, path-based, index-based, and invalid navigation paths.
    /// </summary>
    [Test]
    public void DescendantTest()
    {
        // 1. Null check
        var meta = _config10.Descendant<IBdoMetaData>();
        meta.Should().BeNull("Calling Descendant without arguments should return null.");

        // 2. Path-based resolution (using '^' for child configurations)
        meta = _config10.Descendant<IBdoMetaData>("^" + _configName10a, "float1");
        meta?.GetData().Should().Be(_config10a["float1"]?.GetData(), "Path-based resolution should match child config data.");

        // 3. Index-based resolution
        meta = _config10.Descendant<IBdoMetaData>("^:0", "float1");
        meta?.GetData().Should().Be(_config10a["float1"]?.GetData(), "Index-based resolution should match child config data.");

        // 4. Non-existing path
        var subConfig = _config10.Descendant<IBdoConfiguration>("^" + _configName10a + "x", "float1");
        subConfig.Should().BeNull("Resolution of an invalid path should return null.");
    }

    /// <summary>
    /// Verifies that deep cloning produces an independent configuration structurally equivalent to the source.
    /// </summary>
    [Test]
    public void CloneTest()
    {
        var meta = _config10.Clone<IBdoConfiguration>();
        AssertEquals(meta, _config10);
    }

    /// <summary>
    /// Verifies that updating a configuration properly overwrites scalars and merges nested sets and sub-configurations.
    /// </summary>
    [Test]
    public void UpdateTest()
    {
        var meta = _config10.Clone<IBdoConfiguration>();
        meta.Update(_config20);

        // Scalar update check
        meta.Descendant<IBdoMetaScalar>("float1")?.GetData().Should().Be(_config20["float1"]?.GetData());

        // Node merging check ($node1 should now have 3 sub-items: textB1, textB2 from config10 + coolB1 from config20)
        meta.Descendant<IBdoMetaSet>("$node1")?.Count.Should().Be(3, "The node should contain merged scalars from both configurations.");

        // Sub-configuration update check
        meta.Descendant<IBdoConfiguration>("^" + _configName10a)?.Count.Should().Be(2);
        meta.Descendant<IBdoMetaScalar>("^" + _configName10a, "float1")?.GetData().Should().Be(_config20a["float1"]?.GetData());
    }
}