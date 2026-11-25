using BindOpen.Tests;
using NUnit.Framework;
using System.Linq;

namespace BindOpen.Data;

/// <summary>
/// Provides unit tests for hierarchical tree structures and traversal algorithms.
/// </summary>
/// <remarks>
/// These tests verify the ability of the Kernel to navigate parent-child relationships,
/// perform recursive descendant queries, and filter nodes across multiple hierarchy levels.
/// </remarks>
[TestFixture]
public class TreeTests
{
    private TreeFake _tree;

    /// <summary>
    /// Initializes a two-level test tree with 10 direct children, each containing 10 sub-children
    /// (111 nodes total including the root), before any tests in this fixture are executed.
    /// </summary>
    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        _tree = new TreeFake();
        for (var i = 0; i < 10; i++)
        {
            var child = _tree.InsertChild<TreeFake>(q => q.WithName("child" + i));

            for (var j = 0; j < 10; j++)
            {
                child.InsertChild<TreeFake>(q => q.WithName("subChild" + j));
            }
        }
    }

    /// <summary>
    /// Verifies the retrieval and counting of direct and recursive descendant nodes across hierarchy levels.
    /// </summary>
    [Test]
    public void ChildrenTest()
    {
        // 1. Verify first level child name
        var name = _tree.Children()?.FirstOrDefault()?.Name;
        Assert.That(name, Is.EqualTo("child0"), "Direct child name resolution failed.");

        // 2. Verify direct children count (level 1)
        var count = _tree.Children()?.Count();
        Assert.That(count, Is.EqualTo(10), "Direct children count must be 10.");

        // 3. Verify recursive children count (level 1 + level 2)
        // 10 children + (10 * 10) sub-children = 110 descendants
        count = _tree.Children(true)?.Count();
        Assert.That(count, Is.EqualTo(110), "Recursive children count must be 110.");
    }

    /// <summary>
    /// Verifies node existence checks using predicates with and without recursive traversal enabled.
    /// </summary>
    [Test]
    public void HasChildTest()
    {
        // 1. Check if any child exists
        Assert.That(_tree.HasChild(), Is.True);

        // 2. Check for a specific level 1 child
        Assert.That(_tree.HasChild(q => q.Name == "child9"), Is.True);

        // 3. Check for a level 2 child WITHOUT recursion (should be false)
        Assert.That(_tree.HasChild(q => q.Name == "subChild9"), Is.False);

        // 4. Check for a level 2 child WITH recursion (should be true)
        Assert.That(_tree.HasChild(q => q.Name == "subChild9", true), Is.True);
    }

    /// <summary>
    /// Verifies the retrieval of specific child nodes via predicates at both direct and nested levels.
    /// </summary>
    [Test]
    public void ChildTest()
    {
        // 1. Get the first available child
        var child = _tree.FirstChild();
        Assert.That(child, Is.Not.Null);

        // 2. Get a specific child by name at level 1
        child = _tree.Child(q => q.Name == "child9");
        Assert.That(child, Is.Not.Null);

        // 3. Attempt to get a level 2 child without recursion
        child = _tree.Child(q => q.Name == "subChild9");
        Assert.That(child, Is.Null);

        // 4. Get a level 2 child with recursion enabled
        child = _tree.Child(q => q.Name == "subChild9", true);
        Assert.That(child, Is.Not.Null);
    }
}