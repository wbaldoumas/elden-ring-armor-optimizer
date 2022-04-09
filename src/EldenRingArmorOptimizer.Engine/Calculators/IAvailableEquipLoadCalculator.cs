using EldenRingArmorOptimizer.Engine.Enums;

namespace EldenRingArmorOptimizer.Engine.Calculators;

/// <summary>
///     Calculates the available equip load to remain within the target roll type.
/// </summary>
public interface IAvailableEquipLoadCalculator
{
    /// <summary>
    ///     Calculate the available equip load to remain within the target roll type.
    /// </summary>
    /// <param name="equipLoad">The player's current equip load.</param>
    /// <param name="rollType">The target roll type.</param>
    /// <returns>The available equip load to remain within the target roll type.</returns>
    double Calculate(double equipLoad, RollType rollType);
}
