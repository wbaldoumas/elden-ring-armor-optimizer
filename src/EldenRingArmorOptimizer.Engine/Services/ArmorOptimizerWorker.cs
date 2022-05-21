using EldenRingArmorOptimizer.Engine.Calculators;
using EldenRingArmorOptimizer.Engine.Records;

namespace EldenRingArmorOptimizer.Engine.Services;

/// <inheritdoc cref="IArmorOptimizerWorker"/>
public sealed class ArmorOptimizerWorker : IArmorOptimizerWorker
{
    private const double DefaultArmorSetScore = -1.0;
    private static readonly ArmorSet NoneArmorSet = ArmorSet.None();
    private readonly IArmorSetScoreCalculator _armorSetScoreCalculator;

    public ArmorOptimizerWorker(IArmorSetScoreCalculator armorSetScoreCalculator) => _armorSetScoreCalculator = armorSetScoreCalculator;

    public Task<IEnumerable<ArmorSet>> Optimize(
        PlayerLoadout playerLoadout,
        double availableEquipLoad,
        int numberOfResults,
        IReadOnlyCollection<ArmorPiece> headArmors,
        IReadOnlyCollection<ArmorPiece> chestArmors,
        IReadOnlyCollection<ArmorPiece> handArmors,
        IReadOnlyCollection<ArmorPiece> legArmors)
    {
        return Task.Run(() =>
        {
            var armorSetQueue = InitializeArmorSetQueue(numberOfResults);

            foreach (var headArmor in headArmors)
            {
                foreach (var chestArmor in chestArmors)
                {
                    foreach (var handArmor in handArmors)
                    {
                        foreach (var legArmor in legArmors)
                        {
                            var armorSet = new ArmorSet(headArmor, chestArmor, handArmor, legArmor);

                            if (armorSet.Weight > availableEquipLoad)
                            {
                                continue;
                            }

                            if (!DoesArmorMeetMinimumStatRequirements(armorSet, playerLoadout.MinimumStatLoadout))
                            {
                                continue;
                            }

                            var armorSetScore = _armorSetScoreCalculator.Calculate(
                                armorSet,
                                playerLoadout.StatPriorityLoadout
                            );

                            armorSetQueue.EnqueueDequeue(armorSet, armorSetScore);
                        }
                    }
                }
            }

            var armorSets = new List<ArmorSet>();

            while (armorSetQueue.Count > 0)
            {
                var armorSet = armorSetQueue.Dequeue();

                if (armorSet != NoneArmorSet)
                {
                    armorSets.Add(armorSet);
                }
            }

            armorSets.Reverse();

            return armorSets as IEnumerable<ArmorSet>;
        });
    }

    private static PriorityQueue<ArmorSet, double> InitializeArmorSetQueue(int numberOfResults)
    {
        var queue = new PriorityQueue<ArmorSet, double>();

        for (var i = 0; i < numberOfResults; ++i)
        {
            queue.Enqueue(NoneArmorSet, DefaultArmorSetScore);
        }

        return queue;
    }

    private static bool DoesArmorMeetMinimumStatRequirements(
        ArmorSet armorSet,
        MinimumStatLoadout minimumStatLoadout)
    {
        return armorSet.AveragePhysical >= minimumStatLoadout.MinimumAveragePhysical &&
               armorSet.Physical >= minimumStatLoadout.MinimumPhysical &&
               armorSet.Strike >= minimumStatLoadout.MinimumStrike &&
               armorSet.Slash >= minimumStatLoadout.MinimumSlash &&
               armorSet.Pierce >= minimumStatLoadout.MinimumPierce &&
               armorSet.Magic >= minimumStatLoadout.MinimumMagic &&
               armorSet.Fire >= minimumStatLoadout.MinimumFire &&
               armorSet.Lightning >= minimumStatLoadout.MinimumLightning &&
               armorSet.Holy >= minimumStatLoadout.MinimumHoly &&
               armorSet.Immunity >= minimumStatLoadout.MinimumImmunity &&
               armorSet.Robustness >= minimumStatLoadout.MinimumRobustness &&
               armorSet.Focus >= minimumStatLoadout.MinimumFocus &&
               armorSet.Vitality >= minimumStatLoadout.MinimumVitality &&
               armorSet.Poise >= minimumStatLoadout.MinimumPoise;
    }
}
