using EldenRingArmorOptimizer.Engine.Calculators;
using EldenRingArmorOptimizer.Engine.Enums;
using EldenRingArmorOptimizer.Engine.Records;
using EldenRingArmorOptimizer.Engine.Services;
using FluentAssertions;
using NUnit.Framework;

namespace EldenRingArmorOptimizer.Tests.Services;

[TestFixture]
public sealed class ArmorOptimizerWorkerTests
{
    private static readonly ArmorPiece PhysicalHead = new("Physical Head", ArmorType.Head, 10, 20, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);

    private static readonly ArmorPiece PhysicalLightningHead = new("Physical-Lightning Head", ArmorType.Head, 10, 10, 0, 0, 0, 0, 0, 10, 0, 0, 0, 0, 0, 0);

    private static readonly ArmorPiece HeavyHead = new("Heavy Head", ArmorType.Head, 100, 200, 0, 0, 0, 0, 0, 200, 0, 0, 0, 0, 0, 0);

    private static readonly ArmorPiece PhysicalChest = new("Physical Chest", ArmorType.Chest, 10, 20, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);

    private static readonly ArmorPiece PhysicalLightningChest = new("Physical-Lightning Chest", ArmorType.Chest, 10, 10, 0, 0, 0, 0, 0, 20, 0, 0, 0, 0, 0, 0);

    private static readonly ArmorPiece HeavyChest = new("Heavy Chest", ArmorType.Chest, 100, 200, 0, 0, 0, 0, 0, 200, 0, 0, 0, 0, 0, 0);

    private static readonly ArmorPiece PhysicalHands = new("Physical Hands", ArmorType.Hands, 10, 20, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);

    private static readonly ArmorPiece PhysicalLightningHands = new("Physical-Lightning Hands", ArmorType.Hands, 10, 10, 0, 0, 0, 0, 0, 5, 0, 0, 0, 0, 0, 0);

    private static readonly ArmorPiece HeavyHands = new("Heavy Hands", ArmorType.Hands, 100, 200, 0, 0, 0, 0, 0, 200, 0, 0, 0, 0, 0, 0);

    private static readonly ArmorPiece PhysicalLegs = new("Physical Legs", ArmorType.Legs, 10, 20, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);

    private static readonly ArmorPiece PhysicalLightningLegs = new("Physical-Lightning Legs", ArmorType.Legs, 10, 10, 0, 0, 0, 0, 0, 15, 0, 0, 0, 0, 0, 0);

    private static readonly ArmorPiece HeavyLegs = new("Heavy Legs", ArmorType.Legs, 100, 200, 0, 0, 0, 0, 0, 200, 0, 0, 0, 0, 0, 0);

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

    private static readonly Weapon NoneWeapon = Weapon.None();

    private static readonly WeaponLoadout NoneWeaponLoadout = new(
        NoneWeapon,
        NoneWeapon,
        NoneWeapon,
        NoneWeapon,
        NoneWeapon,
        NoneWeapon
    );

    private static readonly Talisman NoneTalisman = Talisman.None();

    private static readonly TalismanLoadout NoneTalismanLoadout = new(
        NoneTalisman,
        NoneTalisman,
        NoneTalisman,
        NoneTalisman
    );

    private readonly IArmorSetScoreCalculator _armorSetScoreCalculator = new ArmorSetScoreCalculator();

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
            ).SetName("Player loadout prioritizing physical defense with no minimum stat requirements.");

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
            ).SetName("Player loadout prioritizing physical defense with minimum lightning stat.");

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
    public async Task ArmorOptimizerWorker_generates_expected_armor_sets(
        PlayerLoadout playerLoadout,
        IList<ArmorSet> expectedArmorSets)
    {
        // arrange
        const double availableEquipLoad = 50.0;

        var optimizer = new ArmorOptimizerWorker(_armorSetScoreCalculator);

        // act
        var armorSets = (await optimizer.Optimize(
                    playerLoadout,
                    availableEquipLoad,
                    3,
                    ArmorPiecesByType[ArmorType.Head].ToList(),
                    ArmorPiecesByType[ArmorType.Chest].ToList(),
                    ArmorPiecesByType[ArmorType.Hands].ToList(),
                    ArmorPiecesByType[ArmorType.Legs].ToList()
                )
            ).ToList();

        // assert
        var orderedExpectedArmorSets = GetOrderedArmorSets(expectedArmorSets);
        var orderedArmorSets = GetOrderedArmorSets(armorSets);

        for (var i = 0; i < armorSets.Count; i++)
        {
            var armorSet = orderedArmorSets[i];
            var expectedArmorSet = orderedExpectedArmorSets[i];

            foreach (var type in ArmorTypes.All())
            {
                var armorPiece = armorSet.ArmorPieces.First(armorPiece => armorPiece.IsOfArmorType(type));
                var expectedArmorPiece = expectedArmorSet.ArmorPieces.First(expectedArmorPiece => expectedArmorPiece.IsOfArmorType(type));

                armorPiece.Should().Be(expectedArmorPiece);
            }
        }
    }

    private static IList<ArmorSet> GetOrderedArmorSets(IEnumerable<ArmorSet> armorSets) =>
        armorSets
            .OrderBy(armorSet => armorSet.ArmorPieces.First(armorPiece => armorPiece.IsOfArmorType(ArmorType.Head)).Name)
            .ThenBy(armorSet => armorSet.ArmorPieces.First(armorPiece => armorPiece.IsOfArmorType(ArmorType.Chest)).Name)
            .ThenBy(armorSet => armorSet.ArmorPieces.First(armorPiece => armorPiece.IsOfArmorType(ArmorType.Hands)).Name)
            .ThenBy(armorSet => armorSet.ArmorPieces.First(armorPiece => armorPiece.IsOfArmorType(ArmorType.Legs)).Name)
            .ToList();
}
