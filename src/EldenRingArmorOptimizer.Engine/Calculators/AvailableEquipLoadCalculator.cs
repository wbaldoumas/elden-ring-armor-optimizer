using EldenRingArmorOptimizer.Engine.Enums;
using EldenRingArmorOptimizer.Engine.Records;

namespace EldenRingArmorOptimizer.Engine.Calculators;

/// <inheritdoc cref="IAvailableEquipLoadCalculator"/>
public sealed class AvailableEquipLoadCalculator : IAvailableEquipLoadCalculator
{
    private static readonly IDictionary<RollType, double> RollTypePercentages = new Dictionary<RollType, double>
    {
        { RollType.Light, 0.30 },
        { RollType.Medium, 0.70 },
        { RollType.Heavy, 1 }
    };

    private readonly IEquipLoadCalculator _equipLoadCalculator;

    public AvailableEquipLoadCalculator(IEquipLoadCalculator equipLoadCalculator) =>
        _equipLoadCalculator = equipLoadCalculator;

    public double Calculate(PlayerLoadout playerLoadout)
    {
        if (playerLoadout.TargetRollType == RollType.Overloaded)
        {
            return double.MaxValue;
        }

        var equipLoad = _equipLoadCalculator.Calculate(playerLoadout.Endurance, playerLoadout.TalismanLoadout);

        return (equipLoad * RollTypePercentages[playerLoadout.TargetRollType]) - playerLoadout.Weight;
    }
}
