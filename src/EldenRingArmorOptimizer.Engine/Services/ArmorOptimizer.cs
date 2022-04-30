using EldenRingArmorOptimizer.Engine.Calculators;
using EldenRingArmorOptimizer.Engine.Enums;
using EldenRingArmorOptimizer.Engine.Records;
using EldenRingArmorOptimizer.Engine.Repositories;

namespace EldenRingArmorOptimizer.Engine.Services;

/// <inheritdoc cref="IArmorOptimizer"/>
public sealed class ArmorOptimizer : IArmorOptimizer
{
    private const int MaxDegreesOfParallelism = 6;
    private const int ArmorOptimizerWorkerSampleSize = 25;

    private readonly IArmorPieceRepository _armorPieceRepository;
    private readonly IAvailableEquipLoadCalculator _availableEquipLoadCalculator;
    private readonly IArmorOptimizerWorker _armorOptimizerWorker;

    public ArmorOptimizer(
        IArmorPieceRepository armorPieceRepository,
        IAvailableEquipLoadCalculator availableEquipLoadCalculator,
        IArmorOptimizerWorker armorOptimizerWorker)
    {
        _armorPieceRepository = armorPieceRepository;
        _availableEquipLoadCalculator = availableEquipLoadCalculator;
        _armorOptimizerWorker = armorOptimizerWorker;
    }

    public async Task<IEnumerable<ArmorSet>> Optimize(PlayerLoadout playerLoadout)
    {
        var (headArmorPieces, chestArmorPieces, handArmorPieces, legArmorPieces) = await (
            GetArmorPiecesByType(playerLoadout.ReservedArmorLoadout.HeadArmor, ArmorType.Head),
            GetArmorPiecesByType(playerLoadout.ReservedArmorLoadout.ChestArmor, ArmorType.Chest),
            GetArmorPiecesByType(playerLoadout.ReservedArmorLoadout.HandArmor, ArmorType.Hands),
            GetArmorPiecesByType(playerLoadout.ReservedArmorLoadout.LegArmor, ArmorType.Legs)
        );

        var headArmorPiecesOffset = 0;
        var chestArmorPiecesOffset = 0;
        var handArmorPiecesOffset = 0;
        var legArmorPiecesOffset = 0;

        var headArmorPiecesSliceSize = GetArmorPiecesSliceSize(headArmorPieces.Count, MaxDegreesOfParallelism);
        var chestArmorPiecesSliceSize = GetArmorPiecesSliceSize(chestArmorPieces.Count, MaxDegreesOfParallelism);
        var handArmorPiecesSliceSize = GetArmorPiecesSliceSize(handArmorPieces.Count, MaxDegreesOfParallelism);
        var legArmorPiecesSliceSize = GetArmorPiecesSliceSize(legArmorPieces.Count, MaxDegreesOfParallelism);

        var availableEquipLoad = _availableEquipLoadCalculator.Calculate(playerLoadout);

        var optimizerWorkerTasks = new List<Task<IEnumerable<ArmorSet>>>();

        for (var i = 0; i < MaxDegreesOfParallelism; ++i)
        {
            var headArmorPiecesSlice = GetArmorPiecesSlice(
                headArmorPieces,
                playerLoadout.ReservedArmorLoadout.HeadArmor,
                headArmorPiecesOffset,
                headArmorPiecesSliceSize
            );

            var chestArmorPiecesSlice = GetArmorPiecesSlice(
                chestArmorPieces,
                playerLoadout.ReservedArmorLoadout.ChestArmor,
                chestArmorPiecesOffset,
                chestArmorPiecesSliceSize
            );

            var handArmorPiecesSlice = GetArmorPiecesSlice(
                handArmorPieces,
                playerLoadout.ReservedArmorLoadout.HandArmor,
                handArmorPiecesOffset,
                handArmorPiecesSliceSize
            );

            var legArmorPiecesSlice = GetArmorPiecesSlice(
                legArmorPieces,
                playerLoadout.ReservedArmorLoadout.LegArmor,
                legArmorPiecesOffset,
                legArmorPiecesSliceSize
            );

            optimizerWorkerTasks.Add(
                _armorOptimizerWorker.Optimize(
                    playerLoadout,
                    availableEquipLoad,
                    ArmorOptimizerWorkerSampleSize,
                    headArmorPiecesSlice,
                    chestArmorPiecesSlice,
                    handArmorPiecesSlice,
                    legArmorPiecesSlice
                )
            );

            headArmorPiecesOffset += headArmorPiecesSliceSize;
            chestArmorPiecesOffset += chestArmorPiecesSliceSize;
            handArmorPiecesOffset += handArmorPiecesSliceSize;
            legArmorPiecesOffset += legArmorPiecesSliceSize;
        }

        var initialOptimalArmorSets = (await Task.WhenAll(optimizerWorkerTasks))
            .SelectMany(armorSet => armorSet)
            .ToList();

        var candidateHeadArmorPieces = new HashSet<ArmorPiece>();
        var candidateChestArmorPieces = new HashSet<ArmorPiece>();
        var candidateHandArmorPieces = new HashSet<ArmorPiece>();
        var candidateLegArmorPieces = new HashSet<ArmorPiece>();

        foreach (var armorSet in initialOptimalArmorSets)
        {
            candidateHeadArmorPieces.Add(armorSet.ArmorPieces.First(armorPiece => armorPiece.IsOfArmorType(ArmorType.Head)));
            candidateChestArmorPieces.Add(armorSet.ArmorPieces.First(armorPiece => armorPiece.IsOfArmorType(ArmorType.Chest)));
            candidateHandArmorPieces.Add(armorSet.ArmorPieces.First(armorPiece => armorPiece.IsOfArmorType(ArmorType.Hands)));
            candidateLegArmorPieces.Add(armorSet.ArmorPieces.First(armorPiece => armorPiece.IsOfArmorType(ArmorType.Legs)));
        }

        var finalArmorSets = await _armorOptimizerWorker.Optimize(
            playerLoadout,
            availableEquipLoad,
            playerLoadout.NumberOfResults,
            candidateHeadArmorPieces,
            candidateChestArmorPieces,
            candidateHandArmorPieces,
            candidateLegArmorPieces
        );

        return finalArmorSets;
    }

    private static int GetArmorPiecesSliceSize(int armorPieceCount, double numberOfBuckets) =>
        (int)Math.Ceiling(armorPieceCount / numberOfBuckets);

    private static IReadOnlyCollection<ArmorPiece> GetArmorPiecesSlice(
        IEnumerable<ArmorPiece> armorPieces,
        ArmorPiece? reservedArmorPiece,
        int offset,
        int count
    ) => reservedArmorPiece is not null
        ? new List<ArmorPiece> { (ArmorPiece)reservedArmorPiece }
        : armorPieces.Skip(offset).Take(count).ToList();

    private async Task<IReadOnlyCollection<ArmorPiece>> GetArmorPiecesByType(
        ArmorPiece? reservedArmorPiece,
        ArmorType armorType)
    {
        var armorPieces = reservedArmorPiece is not null
            ? new List<ArmorPiece> { (ArmorPiece)reservedArmorPiece }
            : await _armorPieceRepository.GetByTypeAsync(armorType);

        return armorPieces.ToList();
    }
}
