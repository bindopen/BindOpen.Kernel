using BindOpen.Data.Helpers;
using BindOpen.Data.Objects;
using Bogus;
using FluentAssertions;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace BindOpen.Data;

/// <summary>
/// Contains unit tests for the <see cref="BdoMerger"/> class, focusing on collection merging logic.
/// </summary>
/// <remarks>
/// These tests verify that <see cref="IBdoMerger"/> instances properly apply differential updates
/// (additions and removals) to string collections via both factory constructors and fluent builders.
/// </remarks>
[TestFixture]
public class BdoMergerTests
{
    private List<string> _addList = [];
    private List<string> _removeList = [];
    private List<string> _list = [];

    /// <summary>
    /// Gets or sets the <see cref="BdoMerger"/> instance currently under test.
    /// </summary>
    public BdoMerger _merger = null;

    /// <summary>
    /// Initializes randomized string collections before any tests in this fixture are executed.
    /// </summary>
    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        var f = new Faker();
        _addList = f.Random.WordsArray(5, 10)?.ToList();
        _removeList = f.Random.WordsArray(5, 10)?.ToList();
        _list = f.Random.WordsArray(5, 10)?.ToList();
    }

    /// <summary>
    /// Asserts that two <see cref="IBdoMerger"/> instances are structurally equivalent,
    /// gracefully ignoring empty <see cref="IBdoMerger.AddedValues"/> or <see cref="IBdoMerger.RemovedValues"/> collections.
    /// </summary>
    /// <param name="merger1">The first merger instance to compare.</param>
    /// <param name="merger2">The second merger instance to compare.</param>
    /// <exception cref="AssertionException">
    /// Thrown when only one of the mergers is <see langword="null"/> or when their active rules do not match.
    /// </exception>
    public static void AssertEquals(
        IBdoMerger merger1,
        IBdoMerger merger2)
    {
        if ((merger1 != null && merger2 == null) || (merger1 == null && merger2 != null))
        {
            Assert.Fail("One of the merger objects is null while the other is not.");
        }

        merger1.Should().BeEquivalentTo(
            merger2,
            options =>
            {
                // Exclude empty collections from the comparison to avoid false negatives
                if (merger1?.AddedValues?.Any() != true && merger2?.AddedValues?.Any() != true)
                {
                    options = options.Excluding(x => x.AddedValues);
                }
                if (merger1?.RemovedValues?.Any() != true && merger2?.RemovedValues?.Any() != true)
                {
                    options = options.Excluding(x => x.RemovedValues);
                }

                return options;
            });
    }

    /// <summary>
    /// Verifies that a merger initialized with both addition and removal collections via <see cref="BdoData.NewMerger"/>
    /// accurately inserts new items and strips removed ones.
    /// </summary>
    [Test]
    public void Create1Test()
    {
        var list = _list.Adding(_removeList?.ToArray());
        _merger = BdoData.NewMerger(_addList, _removeList);
        var newList = list.Merge(_merger);

        // All added values should be present (unless they were also in the removal list)
        _addList.All(q => newList?.Contains(q) == true || _removeList?.Contains(q) == true).Should().BeTrue();

        // No removed values should be present
        newList.Should().NotContain(_removeList, "The merged list must not contain any strings specified in the removal list.");
    }

    /// <summary>
    /// Verifies that a merger configured fluently with <c>Removing</c> filters out all specified items from the target list.
    /// </summary>
    [Test]
    public void Create2Test()
    {
        var list = _list.Adding(_removeList?.ToArray());
        _merger = BdoData.NewMerger().Removing(_removeList?.ToArray());
        var newList = list.Merge(_merger);

        newList.Should().NotContain(_removeList, "The merged list should successfully filter out all strings in the removal list.");
    }

    /// <summary>
    /// Verifies that a merger configured fluently with both <c>Adding</c> and <c>Removing</c>
    /// produces the expected combined collection state.
    /// </summary>
    [Test]
    public void Create3Test()
    {
        var list = _list.Adding(_removeList?.ToArray());
        _merger = BdoData.NewMerger()
            .Adding(_addList?.ToArray())
            .Removing(_removeList?.ToArray());
        var newList = list.Merge(_merger);

        _addList.All(q => newList?.Contains(q) == true || _removeList?.Contains(q) == true).Should().BeTrue();
        newList.Should().NotContain(_removeList);
    }
}