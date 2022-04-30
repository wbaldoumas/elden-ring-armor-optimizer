using EldenRingArmorOptimizer.Engine.Records;

namespace EldenRingArmorOptimizer.Engine.Calculators;

/// <inheritdoc cref="IArmorSetScoreCalculator"/>
public sealed class ArmorSetScoreCalculator : IArmorSetScoreCalculator
{
    public double Calculate(ArmorSet armorSet, StatPriorityLoadout statPriorityLoadout)
    {
        var armorScore = 0.0;

        // This is a bit gross, but conditionally performing these calculations based on whether
        // the user actually cares about the stat or not actually improves performance by ~20%,
        // depending on how many stats they've set priorities for.
        if (statPriorityLoadout.AveragePhysicalPriority > 0)
        {
            armorScore += armorSet.AveragePhysical * statPriorityLoadout.AveragePhysicalPriority;
        }

        if (statPriorityLoadout.PhysicalPriority > 0)
        {
            armorScore += armorSet.Physical * statPriorityLoadout.PhysicalPriority;
        }

        if (statPriorityLoadout.StrikePriority > 0)
        {
            armorScore += armorSet.Strike * statPriorityLoadout.StrikePriority;
        }

        if (statPriorityLoadout.SlashPriority > 0)
        {
            armorScore += armorSet.Slash * statPriorityLoadout.SlashPriority;
        }

        if (statPriorityLoadout.PiercePriority > 0)
        {
            armorScore += armorSet.Pierce * statPriorityLoadout.PiercePriority;
        }

        if (statPriorityLoadout.MagicPriority > 0)
        {
            armorScore += armorSet.Magic * statPriorityLoadout.MagicPriority;
        }

        if (statPriorityLoadout.FirePriority > 0)
        {
            armorScore += armorSet.Fire * statPriorityLoadout.FirePriority;
        }

        if (statPriorityLoadout.LightningPriority > 0)
        {
            armorScore += armorSet.Lightning * statPriorityLoadout.LightningPriority;
        }

        if (statPriorityLoadout.HolyPriority > 0)
        {
            armorScore += armorSet.Holy * statPriorityLoadout.HolyPriority;
        }

        if (statPriorityLoadout.ImmunityPriority > 0)
        {
            armorScore += armorSet.Immunity * statPriorityLoadout.ImmunityPriority;
        }

        if (statPriorityLoadout.RobustnessPriority > 0)
        {
            armorScore += armorSet.Robustness * statPriorityLoadout.RobustnessPriority;
        }

        if (statPriorityLoadout.FocusPriority > 0)
        {
            armorScore += armorSet.Focus * statPriorityLoadout.FocusPriority;
        }

        if (statPriorityLoadout.VitalityPriority > 0)
        {
            armorScore += armorSet.Vitality * statPriorityLoadout.VitalityPriority;
        }

        if (statPriorityLoadout.PiercePriority > 0)
        {
            armorScore += armorSet.Poise * statPriorityLoadout.PoisePriority;
        }

        return armorScore;
    }
}
