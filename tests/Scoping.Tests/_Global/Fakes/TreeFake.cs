using BindOpen.Data;
using System;
using System.Linq;

namespace BindOpen.Scoping.Tests;

/// <summary>
/// This class represents a fake tree node used for testing hierarchical navigation, 
/// recursive operations, and set management within the BindOpen framework.
/// </summary>
public class TreeFake : INamed, ITTreeNode<TreeFake>
{
    private TreeFake _parent;

    // ------------------------------------------
    // CONSTRUCTORS
    // ------------------------------------------

    #region Constructors

    /// <summary>
    /// Instantiates a new instance of the TreeFake class.
    /// </summary>
    public TreeFake()
    {
    }

    #endregion

    // ------------------------------------------
    // PROPERTIES
    // ------------------------------------------

    #region Properties

    /// <summary>
    /// Gets or sets the set of child nodes.
    /// </summary>
    public ITBdoSet<TreeFake> _Children { get; set; }

    /// <summary>
    /// Gets or sets the parent node of this instance.
    /// </summary>
    public TreeFake Parent { get => _parent; set => _parent = value; }

    /// <summary>
    /// Gets or sets the name of the node.
    /// </summary>
    public string Name { get; set; }

    #endregion

    // ------------------------------------------
    // METHODS
    // ------------------------------------------

    #region Methods

    /// <summary>
    /// Removes children based on a filter, optionally processing the tree recursively.
    /// </summary>
    /// <param name="filter">The predicate to determine which nodes to remove.</param>
    /// <param name="isRecursive">Indicates whether to apply the removal to all descendants.</param>
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
    /// Returns the unique key for this node, required by the INamed interface.
    /// </summary>
    public string Key() => Name;

    /// <summary>
    /// Inserts a new child node of a specific type and initializes it.
    /// </summary>
    /// <typeparam name="Q">The type of the child node (must inherit from TreeFake).</typeparam>
    /// <param name="updater">The action to configure the new child node.</param>
    /// <returns>The newly created child node.</returns>
    public Q InsertChild<Q>(Action<Q> updater) where Q : TreeFake, new()
    {
        var child = new Q();
        updater?.Invoke(child);

        // Using BdoData.NewItemSet ensures the collection is compatible with BindOpen's set logic
        _Children ??= BdoData.NewItemSet<TreeFake>();
        _Children.Insert(child);

        // Automatically establish the back-reference to the parent
        child.Parent = this;

        return child;
    }

    #endregion
}