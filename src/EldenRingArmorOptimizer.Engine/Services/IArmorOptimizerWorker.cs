using EldenRingArmorOptimizer.Engine.Records;

namespace EldenRingArmorOptimizer.Engine.Services;

/// <summary>
///     Calculates the best armor sets for the player's given loadout.
/// </summary>
public interface IArmorOptimizerWorker
{
    /// <summary>
    ///     Calculate the best armor sets for the player's given loadout.
    /// </summary>
    /// <param name="playerLoadout">The player's loadout.</param>
    /// <param name="availableEquipLoad">The player's available equip load.</param>
    /// <param name="numberOfResults">The number of armor sets to return.</param>
    /// <param name="headArmors">The head armor pieces to work with.</param>
    /// <param name="chestArmors">The chest armor pieces to work with.</param>
    /// <param name="handArmors">The hand armor pieces to work with.</param>
    /// <param name="legArmors">The leg armor pieces to work with.</param>
    /// <returns>The best armor sets for the player's loadout.</returns>
    Task<IEnumerable<ArmorSet>> Optimize(
        PlayerLoadout playerLoadout,
        double availableEquipLoad,
        int numberOfResults,
        IReadOnlyCollection<ArmorPiece> headArmors,
        IReadOnlyCollection<ArmorPiece> chestArmors,
        IReadOnlyCollection<ArmorPiece> handArmors,
        IReadOnlyCollection<ArmorPiece> legArmors
    );
}
