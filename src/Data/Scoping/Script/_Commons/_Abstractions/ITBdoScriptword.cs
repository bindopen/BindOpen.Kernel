namespace BindOpen.Scoping.Script;

/// <summary>
/// Defines a strongly-typed scriptword.
/// </summary>
/// <typeparam name="TItem">The type of the item or value associated with this scriptword.</typeparam>
/// <remarks>
/// This generic interface allows for compile-time type checking and cleaner 
/// integration when scriptwords are used to manipulate specific business 
/// objects or data structures.
/// </remarks>
public interface ITBdoScriptword<TItem> : IBdoScriptword
{
}