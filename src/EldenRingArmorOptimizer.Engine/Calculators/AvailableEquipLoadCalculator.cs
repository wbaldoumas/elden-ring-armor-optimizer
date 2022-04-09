using EldenRingArmorOptimizer.Engine.Enums;

namespace EldenRingArmorOptimizer.Engine.Calculators;

public class AvailableEquipLoadCalculator : IAvailableEquipLoadCalculator
{
    private static readonly IDictionary<RollType, double> RollTypePercentages = new Dictionary<RollType, double>
    {
        { RollType.Light, 0.30 },
        { RollType.Medium, 0.70 },
        { RollType.Heavy, 1 }
    };

    public double Calculate(double equipLoad, RollType rollType)
    {
        if (rollType == RollType.Overloaded)
        {
            return double.MaxValue;
        }

        return equipLoad * RollTypePercentages[rollType];
    }
}
