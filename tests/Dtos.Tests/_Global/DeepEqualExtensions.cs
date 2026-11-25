using DeepEqual;
using DeepEqual.Syntax;
using System.Collections;
using System.Linq;

namespace BindOpen.Tests;

/// <summary>
/// Provides custom comparison helpers for DeepEqual assertions in unit tests.
/// </summary>
public static class DeepEqualExtensions
{
    /// <summary>
    /// Compares two objects for deep equality while treating <c>null</c> and empty collections as equivalent.
    /// </summary>
    public static bool IsDeepEqualWithEmptyCollections<TActual, TExpected>(
        this TActual actual,
        TExpected expected)
    {
        return actual.WithDeepEqual(expected)
            .WithCustomComparison(new NullOrEmptyCollectionComparison())
            .Compare();
    }

    private sealed class NullOrEmptyCollectionComparison : IComparison
    {
        public bool CanCompare(System.Type type1, System.Type type2)
        {
            return type1 != typeof(string)
                && type2 != typeof(string)
                && typeof(IEnumerable).IsAssignableFrom(type1)
                && typeof(IEnumerable).IsAssignableFrom(type2);
        }

        public (ComparisonResult result, IComparisonContext context) Compare(
            IComparisonContext context,
            object value1,
            object value2)
        {
            var list1 = (value1 as IEnumerable)?.Cast<object>().ToList();
            var list2 = (value2 as IEnumerable)?.Cast<object>().ToList();

            bool isEmpty1 = list1 == null || list1.Count == 0;
            bool isEmpty2 = list2 == null || list2.Count == 0;

            if (isEmpty1 && isEmpty2)
            {
                return (ComparisonResult.Pass, context);
            }

            return (ComparisonResult.Inconclusive, context);
        }
    }
}