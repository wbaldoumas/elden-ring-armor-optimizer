namespace EldenRingOptimizer.Engine.Mappers;

public abstract class BaseMapper<TSource, TTarget> : IMapper<TSource, TTarget>
{
    public abstract TTarget Map(TSource item);

    public IEnumerable<TTarget> Map(IEnumerable<TSource> items) => items.Select(Map);
}
