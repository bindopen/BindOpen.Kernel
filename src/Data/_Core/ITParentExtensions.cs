using System.Linq;

namespace BindOpen.Data
{
    /// <summary>
    /// This interface represents an described data item.
    /// </summary>
    public static class ITParentExtensions
    {
        public static TParent WithChildren<TParent, TChild>(
            this TParent parent,
            params TChild[] children)
            where TParent : ITParent<TChild>
            where TChild : IReferenced
        {
            if (parent != null)
            {
                parent._Children = BdoData.NewItemSet(children?.Any() == true ? children : null);
            }

            return parent;
        }

        public static TParent AddChildren<TParent, TChild>(
            this TParent parent,
            params TChild[] children)
            where TParent : ITParent<TChild>
            where TChild : IReferenced
        {
            if (parent != null)
            {
                parent._Children ??= BdoData.NewItemSet<TChild>();
                foreach (var child in children)
                {
                    parent._Children.Insert(child);
                }
            }

            return parent;
        }

    }
}
