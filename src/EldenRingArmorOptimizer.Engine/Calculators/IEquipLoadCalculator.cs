using EldenRingArmorOptimizer.Engine.Records;

namespace EldenRingArmorOptimizer.Engine.Calculators;

/// <summary>
///     A calculator for calculating equip load values.
/// </summary>
public interface IEquipLoadCalculator
{
    /// <summary>
    ///     Calculate the equip load value for the given endurance and talismans.
    /// </summary>
    /// <param name="endurance">The player's endurance.</param>
    /// <param name="talisman1">The player's first talisman.</param>
    /// <param name="talisman2">The player's second talisman.</param>
    /// <param name="talisman3">The player's third talisman.</param>
    /// <param name="talisman4">The player's fourth talisman.</param>
    /// <returns>The equip load value for the given endurance and talismans.</returns>
    double Calculate(
        byte endurance,
        Talisman talisman1,
        Talisman talisman2,
        Talisman talisman3,
        Talisman talisman4
    );
}
