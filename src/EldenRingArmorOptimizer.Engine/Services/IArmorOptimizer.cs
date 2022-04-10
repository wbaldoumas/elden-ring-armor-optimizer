using EldenRingArmorOptimizer.Engine.Records;

namespace EldenRingArmorOptimizer.Engine.Services;

/// <summary>
///     Calculates the best armor sets for the player's given loadout.
/// </summary>
public interface IArmorOptimizer
{
    /// <summary>
    ///     Calculate the best armor sets for the player's given loadout.
    /// </summary>
    /// <param name="playerLoadout">The player's loadout.</param>
    /// <returns>The best armor sets for the player's loadout.</returns>
    Task<IEnumerable<ArmorSet>> Optimize(PlayerLoadout playerLoadout);
}
