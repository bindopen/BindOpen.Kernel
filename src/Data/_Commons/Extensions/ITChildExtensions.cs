using BindOpen.Data.Helpers;

namespace BindOpen.Data;

/// <summary>
/// This static class provides extension methods for the ITChild interface.
/// </summary>
public static class ITChildExtensions
{
    /// <summary>
    /// Returns the root of the specified child by climbing up the parent chain.
    /// </summary>
    /// <typeparam name="T">The type of the referenced object.</typeparam>
    /// <param name="child">The child to consider.</param>
    /// <param name="levelMax">The maximum recursion level to prevent infinite loops.</param>
    /// <returns>Returns the highest parent in the tree, or the child itself if no parent exists.</returns>
    public static T Root<T>(
        this ITChild<T> child,
        int levelMax = 50)
        where T : IReferenced
    {
        if (child != null && levelMax > 0)
        {
            if (child.Parent == null)
            {
                return child.As<T>();
            }
            else if (child.Parent is ITChild<T> tree)
            {
                return tree.Root(--levelMax);
            }
        }

        return default;
    }

    /// <summary>
    /// Sets the parent of the specified child and synchronizes the relationship if possible.
    /// </summary>
    /// <typeparam name="TChild">The type of the child item.</typeparam>
    /// <typeparam name="TParent">The type of the parent item.</typeparam>
    /// <param name="item">The child item.</param>
    /// <param name="parent">The parent item to assign.</param>
    /// <returns>Returns the child item with its parent set.</returns>
    public static TChild WithParent<TChild, TParent>(this TChild item, TParent parent)
        where TChild : ITChild<TParent>
        where TParent : IReferenced
    {
        if (item != null)
        {
            item.Parent = parent;

            // Auto-registration: if the parent supports a single child, we update it
            if (parent is ITSingleChildParent<TChild> singleChildParent)
            {
                singleChildParent.WithChild(item);
            }
        }

        return item;
    }

    /// <summary>
    /// Calculates the depth level of the specified child within its tree.
    /// </summary>
    /// <typeparam name="T">The type of the referenced object.</typeparam>
    /// <param name="child">The child to analyze.</param>
    /// <returns>Returns 0 if the child is at the root level, otherwise returns the distance from the root.</returns>
    public static int Level<T>(this ITChild<T> child)
        where T : IReferenced
    {
        if (child != null)
        {
            // If the parent is a child node, add 1 to its level
            if (child.Parent is ITChild<T> parentChild)
            {
                return parentChild.Level() + 1;
            }

            // If parent exists but is the root (not a ITChild), level is 1
            return 1;
        }

        return 0;
    }
}