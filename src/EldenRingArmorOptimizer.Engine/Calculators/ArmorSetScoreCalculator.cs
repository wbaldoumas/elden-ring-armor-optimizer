using EldenRingArmorOptimizer.Engine.Records;

namespace EldenRingArmorOptimizer.Engine.Calculators;

public class ArmorSetScoreCalculator : IArmorSetScoreCalculator
{
    private const double BaselineArmorScore = 0.0;

    public double Calculate(ArmorSet armorSet, StatPriorityLoadout statPriorityLoadout)
    {
        return BaselineArmorScore +
               (armorSet.AveragePhysical * statPriorityLoadout.AveragePhysicalPriority) +
               (armorSet.Physical * statPriorityLoadout.PhysicalPriority) +
               (armorSet.Strike * statPriorityLoadout.StrikePriority) +
               (armorSet.Slash * statPriorityLoadout.SlashPriority) +
               (armorSet.Pierce * statPriorityLoadout.PiercePriority) +
               (armorSet.Magic * statPriorityLoadout.MagicPriority) +
               (armorSet.Fire * statPriorityLoadout.FirePriority) +
               (armorSet.Lightning * statPriorityLoadout.LightningPriority) +
               (armorSet.Holy * statPriorityLoadout.HolyPriority) +
               (armorSet.Immunity * statPriorityLoadout.ImmunityPriority) +
               (armorSet.Robustness * statPriorityLoadout.RobustnessPriority) +
               (armorSet.Focus * statPriorityLoadout.FocusPriority) +
               (armorSet.Vitality * statPriorityLoadout.VitalityPriority) +
               (armorSet.Poise * statPriorityLoadout.PoisePriority);
    }
}
