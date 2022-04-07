namespace EldenRingOptimizer.Engine.Mappers;

/// <summary>
///     A mapper that can map from <typeparamref name="TSource"/> to <typeparamref name="TTarget"/>.
/// </summary>
/// <typeparam name="TSource">The source item type.</typeparam>
/// <typeparam name="TTarget">The target item type.</typeparam>
public interface IMapper<in TSource, out TTarget>
{
    /// <summary>
    ///     Map from a single <typeparamref name="TSource"/> to a single <typeparamref name="TTarget"/>.
    /// </summary>
    /// <param name="item">The source item to map from.</param>
    /// <returns>A <typeparamref name="TTarget"/> mapped from the <typeparamref name="TSource"/>.</returns>
    TTarget Map(TSource item);

    /// <summary>
    ///     Map from a collection of <typeparamref name="TSource"/> to a collection of <typeparamref name="TTarget"/>.
    /// </summary>
    /// <param name="items">The source items to map from.</param>
    /// <returns>A collection of <typeparamref name="TTarget"/> mapped from the collection of <typeparamref name="TSource"/>.</returns>
    IEnumerable<TTarget> Map(IEnumerable<TSource> items);
}
