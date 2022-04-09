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
    /// <param name="talismanLoadout">The player's talismans.</param>
    /// <returns>The equip load value for the given endurance and talismans.</returns>
    double Calculate(byte endurance, TalismanLoadout talismanLoadout);
}
