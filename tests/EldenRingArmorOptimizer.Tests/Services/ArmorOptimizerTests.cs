using EldenRingArmorOptimizer.Engine.Calculators;
using EldenRingArmorOptimizer.Engine.Configuration;
using EldenRingArmorOptimizer.Engine.Enums;
using EldenRingArmorOptimizer.Engine.Records;
using EldenRingArmorOptimizer.Engine.Repositories;
using EldenRingArmorOptimizer.Engine.Services;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;

namespace EldenRingArmorOptimizer.Tests.Services;

[TestFixture]
public class ArmorOptimizerTests
{
    private IArmorPieceRepository? _mockArmorPieceRepository;
    private IAvailableEquipLoadCalculator? _mockAvailableEquipLoadCalculator;
    private IArmorOptimizerWorker? _mockArmorOptimizerWorker;

    [SetUp]
    public void SetUp()
    {
        _mockArmorPieceRepository = Substitute.For<IArmorPieceRepository>();
        _mockAvailableEquipLoadCalculator = Substitute.For<IAvailableEquipLoadCalculator>();
        _mockArmorOptimizerWorker = Substitute.For<IArmorOptimizerWorker>();
    }

    [Test]
    public async Task When_optimizer_is_invoked_expected_components_are_invoked()
    {
        // arrange
        var configuration = new ArmorOptimizerConfiguration
        {
            MaxDegreesOfParallelism = 3,
            ArmorOptimizerWorkerSampleSize = 10
        };

        var playerLoadout = new PlayerLoadout(
            40,
            new WeaponLoadout(Array.Empty<Weapon>()),
            new TalismanLoadout(Array.Empty<Talisman>()),
            new MinimumStatLoadout(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0),
            new StatPriorityLoadout(1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1),
            new ReservedArmorLoadout(null, null, null, null),
            RollType.Medium,
            20
        );

        var mockHeadArmorPieces = GenerateArmorPieces(ArmorType.Head, 10);
        var mockChestArmorPieces = GenerateArmorPieces(ArmorType.Chest, 10);
        var mockHandArmorPieces = GenerateArmorPieces(ArmorType.Hands, 10);
        var mockLegArmorPieces = GenerateArmorPieces(ArmorType.Legs, 10);

        var armorSets = (
            from mockHeadArmorPiece in mockHeadArmorPieces
            from mockChestArmorPiece in mockChestArmorPieces
            from mockHandArmorPiece in mockHandArmorPieces
            from mockLegArmorPiece in mockLegArmorPieces
            select new ArmorSet(mockHeadArmorPiece, mockChestArmorPiece, mockHandArmorPiece, mockLegArmorPiece)
        ).ToList();

        const double availableEquipLoad = 100.00;

        _mockAvailableEquipLoadCalculator!
            .Calculate(playerLoadout)
            .Returns(availableEquipLoad);

        _mockArmorPieceRepository!
            .GetByTypeAsync(ArmorType.Head)
            .Returns(mockHeadArmorPieces);

        _mockArmorPieceRepository
            .GetByTypeAsync(ArmorType.Chest)
            .Returns(mockChestArmorPieces);

        _mockArmorPieceRepository
            .GetByTypeAsync(ArmorType.Hands)
            .Returns(mockHandArmorPieces);

        _mockArmorPieceRepository
            .GetByTypeAsync(ArmorType.Legs)
            .Returns(mockLegArmorPieces);

        _mockArmorOptimizerWorker!
            .Optimize(
                playerLoadout,
                availableEquipLoad,
                configuration.ArmorOptimizerWorkerSampleSize,
                Arg.Any<IReadOnlyCollection<ArmorPiece>>(),
                Arg.Any<IReadOnlyCollection<ArmorPiece>>(),
                Arg.Any<IReadOnlyCollection<ArmorPiece>>(),
                Arg.Any<IReadOnlyCollection<ArmorPiece>>()
            ).Returns(
                armorSets.OrderBy(_ => Guid.NewGuid()).Take(configuration.ArmorOptimizerWorkerSampleSize).ToList(),
                armorSets.OrderBy(_ => Guid.NewGuid()).Take(configuration.ArmorOptimizerWorkerSampleSize).ToList(),
                armorSets.OrderBy(_ => Guid.NewGuid()).Take(configuration.ArmorOptimizerWorkerSampleSize).ToList()
            );

        var expectedArmorSets = armorSets.OrderBy(_ => Guid.NewGuid()).Take(playerLoadout.NumberOfResults).ToList();

        _mockArmorOptimizerWorker
            .Optimize(
                playerLoadout,
                availableEquipLoad,
                playerLoadout.NumberOfResults,
                Arg.Any<IReadOnlyCollection<ArmorPiece>>(),
                Arg.Any<IReadOnlyCollection<ArmorPiece>>(),
                Arg.Any<IReadOnlyCollection<ArmorPiece>>(),
                Arg.Any<IReadOnlyCollection<ArmorPiece>>()
            ).Returns(expectedArmorSets);

        var optimizer = new ArmorOptimizer(
            _mockArmorPieceRepository,
            _mockAvailableEquipLoadCalculator,
            _mockArmorOptimizerWorker,
            configuration
        );

        // act
        var results = await optimizer.Optimize(playerLoadout);

        // assert
        results.Should().BeEquivalentTo(expectedArmorSets);

        _mockAvailableEquipLoadCalculator
            .Received(1)
            .Calculate(playerLoadout);

        await _mockArmorPieceRepository
            .Received(1)
            .GetByTypeAsync(ArmorType.Head);

        await _mockArmorPieceRepository
            .Received(1)
            .GetByTypeAsync(ArmorType.Chest);

        await _mockArmorPieceRepository
            .Received(1)
            .GetByTypeAsync(ArmorType.Hands);

        await _mockArmorPieceRepository
            .Received(1)
            .GetByTypeAsync(ArmorType.Legs);

        await _mockArmorOptimizerWorker
            .Received(configuration.MaxDegreesOfParallelism)
            .Optimize(
                playerLoadout,
                availableEquipLoad,
                configuration.ArmorOptimizerWorkerSampleSize,
                Arg.Any<IReadOnlyCollection<ArmorPiece>>(),
                Arg.Any<IReadOnlyCollection<ArmorPiece>>(),
                Arg.Any<IReadOnlyCollection<ArmorPiece>>(),
                Arg.Any<IReadOnlyCollection<ArmorPiece>>()
            );

        await _mockArmorOptimizerWorker
            .Received(1)
            .Optimize(
                playerLoadout,
                availableEquipLoad,
                playerLoadout.NumberOfResults,
                Arg.Any<IReadOnlyCollection<ArmorPiece>>(),
                Arg.Any<IReadOnlyCollection<ArmorPiece>>(),
                Arg.Any<IReadOnlyCollection<ArmorPiece>>(),
                Arg.Any<IReadOnlyCollection<ArmorPiece>>()
            );
    }

    private static IList<ArmorPiece> GenerateArmorPieces(ArmorType type, int count) =>
        Enumerable
            .Range(1, count)
            .Select(i =>
                new ArmorPiece($"Armor Piece {i}", type, i, i, i, i, i, i, i, i, i, i, i, i, i, i)
            )
            .OrderBy(_ => Guid.NewGuid())
            .ToList();
}
