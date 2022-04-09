using EldenRingArmorOptimizer.Engine.Records;

namespace EldenRingArmorOptimizer.Engine.Calculators;

/// <summary>
///     Calculates the available equip load for the player's loadout.
/// </summary>
public interface IAvailableEquipLoadCalculator
{
    /// <summary>
    ///     Calculate the available equip load for the player's loadout.
    /// </summary>
    /// <param name="playerLoadout">The player's loadout.</param>
    /// <returns>The available equip load for the player's loadout.</returns>
    double Calculate(PlayerLoadout playerLoadout);
}
