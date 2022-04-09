using EldenRingArmorOptimizer.Engine.Records;

namespace EldenRingArmorOptimizer.Engine.Calculators;

/// <summary>
///     Calculates a given armor set's score for the given player stat priorities.
/// </summary>
public interface IArmorSetScoreCalculator
{
    /// <summary>
    ///     Calculate the given armor set's score for the given player stat priorities.
    /// </summary>
    /// <param name="armorSet">The armor set to calculate the score of.</param>
    /// <param name="statPriorityLoadout">The player's stat priorities.</param>
    /// <returns>The calculated armor set's score.</returns>
    double Calculate(ArmorSet armorSet, StatPriorityLoadout statPriorityLoadout);
}
