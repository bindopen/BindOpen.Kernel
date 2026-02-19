using BindOpen.Data;
using System;
using System.Linq;

namespace BindOpen.Tests;

/// <summary>
/// Represents a fake hierarchical node used for testing tree structures and recursive operations.
/// </summary>
/// <remarks>
/// This class implements <see cref="ITTreeNode{T}"/> to simulate complex parent-child 
/// relationships within the BindOpen Kernel.
/// </remarks>
public class TreeFake : INamed, ITTreeNode<TreeFake>
{
    private TreeFake _parent;

    /// <summary>
    /// Initializes a new instance of the <see cref="TreeFake"/> class.
    /// </summary>
    public TreeFake()
    {
    }

    /// <summary>
    /// Gets or sets the set of child nodes.
    /// </summary>
    public ITBdoSet<TreeFake> _Children { get; set; }

    /// <summary>
    /// Gets or sets the parent node of this instance.
    /// </summary>
    public TreeFake Parent { get => _parent; set { _parent = value; } }

    /// <summary>
    /// Gets or sets the name of the node.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Removes children from this node based on a filter, optionally performing a recursive cleanup.
    /// </summary>
    /// <param name="filter">The predicate used to filter which children should be removed.</param>
    /// <param name="isRecursive">True if the removal should be applied to all descendants; otherwise, false.</param>
    public void RemoveChildren(Predicate<TreeFake> filter = null, bool isRecursive = false)
    {
        _Children?.Remove(filter);

        if (isRecursive && _Children?.Any() == true)
        {
            foreach (var child in _Children)
            {
                child.RemoveChildren(filter, true);
            }
        }
    }

    /// <summary>
    /// Gets the unique key identifying this node, which corresponds to its name.
    /// </summary>
    /// <returns>The name of the node as a string.</returns>
    public string Key() => Name;

    /// <summary>
    /// Inserts a new child node of a specific type and updates its properties.
    /// </summary>
    /// <typeparam name="Q">The type of the child node, which must inherit from <see cref="TreeFake"/>.</typeparam>
    /// <param name="updater">An action to configure the newly created child node.</param>
    /// <returns>The newly created and inserted child node.</returns>
    public Q InsertChild<Q>(Action<Q> updater) where Q : TreeFake, new()
    {
        var child = new Q();
        updater?.Invoke(child);

        _Children ??= BdoData.NewItemSet<TreeFake>();
        _Children.Insert(child);

        return child;
    }
}