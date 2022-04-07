using EldenRingOptimizer.Engine.Enums;
using EldenRingOptimizer.Engine.Records;

namespace EldenRingOptimizer.Engine.Repositories;

/// <summary>
///     A repository of <see cref="ArmorPiece"/>.
/// </summary>
public interface IArmorPieceRepository
{
    /// <summary>
    ///     Retrieve the armor piece by its <paramref name="type"/>.
    /// </summary>
    /// <param name="type">The type of armor piece to retrieve.</param>
    /// <returns>All armor pieces with the given <paramref name="type"/>.</returns>
    Task<IEnumerable<ArmorPiece>> GetByTypeAsync(ArmorType type);
}
