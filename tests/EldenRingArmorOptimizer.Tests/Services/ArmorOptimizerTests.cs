using EldenRingArmorOptimizer.Engine.Calculators;
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
    private static readonly ArmorPiece PhysicalHead = new("Physical Head", ArmorType.Head, 10, 20, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
    private static readonly ArmorPiece PhysicalLightningHead = new("Physical-Lightning Head", ArmorType.Head, 10, 10, 0, 0, 0, 0, 0, 10, 0, 0, 0, 0, 0, 0);
    private static readonly ArmorPiece HeavyHead = new("Heavy Head", ArmorType.Head, 100, 200, 0, 0, 0, 0, 0, 200, 0, 0, 0, 0, 0, 0);
    private static readonly ArmorPiece ReservedHead = new("Reserved Head", ArmorType.Head, 10, 0, 0, 0, 0, 0, 0, 0, 20, 0, 0, 0, 0, 0);
    private static readonly ArmorPiece PhysicalChest = new("Physical Chest", ArmorType.Chest, 10, 20, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
    private static readonly ArmorPiece PhysicalLightningChest = new("Physical-Lightning Chest", ArmorType.Chest, 10, 10, 0, 0, 0, 0, 0, 20, 0, 0, 0, 0, 0, 0);
    private static readonly ArmorPiece HeavyChest = new("Heavy Chest", ArmorType.Chest, 100, 200, 0, 0, 0, 0, 0, 200, 0, 0, 0, 0, 0, 0);
    private static readonly ArmorPiece ReservedChest = new("Reserved Chest", ArmorType.Chest, 10, 0, 0, 0, 0, 0, 0, 0, 20, 0, 0, 0, 0, 0);
    private static readonly ArmorPiece PhysicalHands = new("Physical Hands", ArmorType.Hands, 10, 20, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
    private static readonly ArmorPiece PhysicalLightningHands = new("Physical-Lightning Hands", ArmorType.Hands, 10, 10, 0, 0, 0, 0, 0, 5, 0, 0, 0, 0, 0, 0);
    private static readonly ArmorPiece HeavyHands = new("Heavy Hands", ArmorType.Hands, 100, 200, 0, 0, 0, 0, 0, 200, 0, 0, 0, 0, 0, 0);
    private static readonly ArmorPiece ReservedHands = new("Reserved Hands", ArmorType.Hands, 10, 0, 0, 0, 0, 0, 0, 0, 20, 0, 0, 0, 0, 0);
    private static readonly ArmorPiece PhysicalLegs = new("Physical Legs", ArmorType.Legs, 10, 20, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
    private static readonly ArmorPiece PhysicalLightningLegs = new("Physical-Lightning Legs", ArmorType.Legs, 10, 10, 0, 0, 0, 0, 0, 15, 0, 0, 0, 0, 0, 0);
    private static readonly ArmorPiece HeavyLegs = new("Heavy Legs", ArmorType.Legs, 100, 200, 0, 0, 0, 0, 0, 200, 0, 0, 0, 0, 0, 0);
    private static readonly ArmorPiece ReservedLegs = new("Reserved Legs", ArmorType.Legs, 10, 0, 0, 0, 0, 0, 0, 0, 20, 0, 0, 0, 0, 0);

    private static readonly IDictionary<ArmorType, IEnumerable<ArmorPiece>> ArmorPiecesByType =
        new Dictionary<ArmorType, IEnumerable<ArmorPiece>>
        {
            {
                ArmorType.Head,
                new List<ArmorPiece>
                {
                    PhysicalHead,
                    PhysicalLightningHead,
                    HeavyHead
                }
            },
            {
                ArmorType.Chest,
                new List<ArmorPiece>
                {
                    PhysicalChest,
                    PhysicalLightningChest,
                    HeavyChest
                }
            },
            {
                ArmorType.Hands,
                new List<ArmorPiece>
                {
                    PhysicalHands,
                    PhysicalLightningHands,
                    HeavyHands
                }
            },
            {
                ArmorType.Legs,
                new List<ArmorPiece>
                {
                    PhysicalLegs,
                    PhysicalLightningLegs,
                    HeavyLegs
                }
            }
        };

    private static readonly NoneWeapon NoneWeapon = new();

    private static readonly WeaponLoadout NoneWeaponLoadout = new(
        NoneWeapon,
        NoneWeapon,
        NoneWeapon,
        NoneWeapon,
        NoneWeapon,
        NoneWeapon
    );

    private static readonly NoneTalisman NoneTalisman = new();

    private static readonly TalismanLoadout NoneTalismanLoadout = new(
        NoneTalisman,
        NoneTalisman,
        NoneTalisman,
        NoneTalisman
    );

    private readonly IArmorPieceRepository _mockArmorPieceRepository = Substitute.For<IArmorPieceRepository>();
    private readonly IArmorSetScoreCalculator _armorSetScoreCalculator = new ArmorSetScoreCalculator();
    private readonly IAvailableEquipLoadCalculator _mockAvailableEquipLoadCalculator = Substitute.For<IAvailableEquipLoadCalculator>();

    private static IEnumerable<TestCaseData> TestCases
    {
        get
        {
            yield return new TestCaseData(
                new PlayerLoadout(
                    40,
                    NoneWeaponLoadout,
                    NoneTalismanLoadout,
                    new MinimumStatLoadout(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0),
                    new StatPriorityLoadout(0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0),
                    new ReservedArmorLoadout(null, null, null, null),
                    RollType.Medium,
                    3
                ),
                new List<ArmorSet>
                {
                    new(PhysicalHead, PhysicalChest, PhysicalLightningHands, PhysicalLegs),
                    new(PhysicalHead, PhysicalChest, PhysicalHands, PhysicalLightningLegs),
                    new(PhysicalHead, PhysicalChest, PhysicalHands, PhysicalLegs)
                }
            ).SetName("Player loadout prioritizing physical defense with no minimum stat requirements or reserved armor.");

            yield return new TestCaseData(
                new PlayerLoadout(
                    40,
                    NoneWeaponLoadout,
                    NoneTalismanLoadout,
                    new MinimumStatLoadout(0, 0, 0, 0, 0, 0, 0, 30, 0, 0, 0, 0, 0, 0),
                    new StatPriorityLoadout(0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0),
                    new ReservedArmorLoadout(null, null, null, null),
                    RollType.Medium,
                    3
                ),
                new List<ArmorSet>
                {
                    new(PhysicalHead, PhysicalLightningChest, PhysicalLightningHands, PhysicalLightningLegs),
                    new(PhysicalLightningHead, PhysicalLightningChest, PhysicalHands, PhysicalLegs),
                    new(PhysicalHead, PhysicalLightningChest, PhysicalHands, PhysicalLightningLegs)
                }
            ).SetName("Player loadout prioritizing physical defense with minimum lightning stat and no reserved armor.");

            yield return new TestCaseData(
                new PlayerLoadout(
                    40,
                    NoneWeaponLoadout,
                    NoneTalismanLoadout,
                    new MinimumStatLoadout(0, 0, 0, 0, 0, 0, 0, 20, 0, 0, 0, 0, 0, 0),
                    new StatPriorityLoadout(0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0),
                    new ReservedArmorLoadout(ReservedHead, null, null, null),
                    RollType.Medium,
                    3
                ),
                new List<ArmorSet>
                {
                    new(ReservedHead, PhysicalLightningChest, PhysicalHands, PhysicalLightningLegs),
                    new(ReservedHead, PhysicalChest, PhysicalLightningHands, PhysicalLightningLegs),
                    new(ReservedHead, PhysicalLightningChest, PhysicalHands, PhysicalLegs)
                }
            ).SetName("Player loadout prioritizing physical defense with minimum lightning stat and reserved armor.");

            yield return new TestCaseData(
                new PlayerLoadout(
                    40,
                    NoneWeaponLoadout,
                    NoneTalismanLoadout,
                    new MinimumStatLoadout(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0),
                    new StatPriorityLoadout(0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0),
                    new ReservedArmorLoadout(ReservedHead, ReservedChest, ReservedHands, ReservedLegs),
                    RollType.Medium,
                    3
                ),
                new List<ArmorSet>
                {
                    new(ReservedHead, ReservedChest, ReservedHands, ReservedLegs)
                }
            ).SetName("Player loadout with all reserved slots generates fully reserved armor set.");

            yield return new TestCaseData(
                new PlayerLoadout(
                    40,
                    NoneWeaponLoadout,
                    NoneTalismanLoadout,
                    new MinimumStatLoadout(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 500, 0, 0),
                    new StatPriorityLoadout(0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0),
                    new ReservedArmorLoadout(null, null, null, null),
                    RollType.Medium,
                    3
                ),
                new List<ArmorSet>()
            ).SetName("Player loadout prioritizing physical defense with minimum focus stat generates empty list.");
        }
    }

    [Test]
    [TestCaseSource(nameof(TestCases))]
    public async Task ArmorOptimizer_generates_expected_armor_sets(
        PlayerLoadout playerLoadout,
        IEnumerable<ArmorSet> expectedArmorSets)
    {
        // arrange
        foreach (var (armorType, armorPieces) in ArmorPiecesByType)
        {
            _mockArmorPieceRepository
                .GetByTypeAsync(armorType)
                .Returns(armorPieces);
        }

        const double availableEquipLoad = 50.0;

        _mockAvailableEquipLoadCalculator
            .Calculate(playerLoadout)
            .Returns(availableEquipLoad);

        var optimizer = new ArmorOptimizer(
            _mockArmorPieceRepository,
            _armorSetScoreCalculator,
            _mockAvailableEquipLoadCalculator
        );

        // act
        var armorSets = (await optimizer.Optimize(playerLoadout)).ToList();

        // assert
        armorSets.Should().BeEquivalentTo(expectedArmorSets);
    }
}
