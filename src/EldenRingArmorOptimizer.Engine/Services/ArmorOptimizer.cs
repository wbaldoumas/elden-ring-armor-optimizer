using EldenRingArmorOptimizer.Engine.Calculators;
using EldenRingArmorOptimizer.Engine.Enums;
using EldenRingArmorOptimizer.Engine.Records;
using EldenRingArmorOptimizer.Engine.Repositories;

namespace EldenRingArmorOptimizer.Engine.Services;

public class ArmorOptimizer : IArmorOptimizer
{
    private const double DefaultArmorSetScore = -1.0;
    private static readonly NoneArmorSet NoneArmorSet = new();

    private readonly IArmorPieceRepository _armorPieceRepository;
    private readonly IArmorSetScoreCalculator _armorSetScoreCalculator;
    private readonly IAvailableEquipLoadCalculator _availableEquipLoadCalculator;

    public ArmorOptimizer(
        IArmorPieceRepository armorPieceRepository,
        IArmorSetScoreCalculator armorSetScoreCalculator,
        IAvailableEquipLoadCalculator availableEquipLoadCalculator)
    {
        _armorPieceRepository = armorPieceRepository;
        _armorSetScoreCalculator = armorSetScoreCalculator;
        _availableEquipLoadCalculator = availableEquipLoadCalculator;
    }

    public async Task<IEnumerable<ArmorSet>> Optimize(PlayerLoadout playerLoadout)
    {
        var (headArmors, chestArmors, handArmors, legArmors) = await (
            GetArmorPiecesByType(playerLoadout.ReservedArmorLoadout.HeadArmor, ArmorType.Head),
            GetArmorPiecesByType(playerLoadout.ReservedArmorLoadout.ChestArmor, ArmorType.Chest),
            GetArmorPiecesByType(playerLoadout.ReservedArmorLoadout.HandArmor, ArmorType.Hands),
            GetArmorPiecesByType(playerLoadout.ReservedArmorLoadout.LegArmor, ArmorType.Legs)
        );

        var availableEquipLoad = _availableEquipLoadCalculator.Calculate(playerLoadout);
        var armorSetQueue = InitializeArmorSetQueue(playerLoadout.NumberOfResults);

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

                        var armorSetScore = _armorSetScoreCalculator.Calculate(armorSet, playerLoadout.StatPriorityLoadout);

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

        return armorSets;
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

    private async Task<IList<ArmorPiece>> GetArmorPiecesByType(ArmorPiece? reservedArmor, ArmorType armorType)
    {
        return reservedArmor is not null
            ? new List<ArmorPiece> { reservedArmor }
            : (await _armorPieceRepository.GetByTypeAsync(armorType)).ToList();
    }
}
