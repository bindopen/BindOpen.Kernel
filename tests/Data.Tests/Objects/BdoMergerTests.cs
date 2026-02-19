using BindOpen.Data.Helpers;
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
/// These tests verify that the merger correctly applies differential updates (additions and removals) 
/// to existing string collections.
/// </remarks>
[TestFixture, Order(210)]
public class BdoMergerTests
{
    private List<string> _addList = [];
    private List<string> _removeList = [];
    private List<string> _list = [];

    /// <summary>
    /// The merger instance currently being tested.
    /// </summary>
    public BdoMerger _merger = null;

    /// <summary>
    /// Initializes randomized string lists for testing merger operations.
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
    /// Asserts that two merger instances are equivalent, handling empty collections gracefully.
    /// </summary>
    /// <param name="merger1">The first merger to compare.</param>
    /// <param name="merger2">The second merger to compare.</param>
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
                // We exclude empty collections from the comparison to avoid false negatives
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
    /// Tests a merger initialized with both addition and removal lists in the constructor.
    /// </summary>
    [Test, Order(1)]
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
    /// Tests a merger using the fluent API to specify removals only.
    /// </summary>
    [Test, Order(2)]
    public void Create2Test()
    {
        var list = _list.Adding(_removeList?.ToArray());
        _merger = BdoData.NewMerger().Removing(_removeList?.ToArray());
        var newList = list.Merge(_merger);

        newList.Should().NotContain(_removeList, "The merged list should successfully filter out all strings in the removal list.");
    }

    /// <summary>
    /// Tests a merger using the fluent API for both additions and removals.
    /// </summary>
    [Test, Order(3)]
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