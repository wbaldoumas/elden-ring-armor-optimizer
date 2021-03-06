using EldenRingArmorOptimizer.Engine.Records;

namespace EldenRingArmorOptimizer.Engine.Repositories;

/// <summary>
///     A repository of <see cref="Talisman"/>.
/// </summary>
public interface ITalismanRepository
{
    /// <summary>
    ///     Retrieve all of the talismans.
    /// </summary>
    /// <returns>All of the talismans.</returns>
    Task<IEnumerable<Talisman>> GetAll();
}
