using EldenRingArmorOptimizer.Engine.Calculators;
using EldenRingArmorOptimizer.Engine.Enums;
using EldenRingArmorOptimizer.Engine.Records;
using FluentAssertions;
using NUnit.Framework;

namespace EldenRingArmorOptimizer.Tests.Calculators;

[TestFixture]
public sealed class ArmorSetScoreCalculatorTests
{
    private static IEnumerable<TestCaseData> TestCases
    {
        get
        {
            const double defaultStatValue = 10;

            var headArmor = new ArmorPiece(
                "Head",
                ArmorType.Head,
                defaultStatValue,
                defaultStatValue,
                defaultStatValue,
                defaultStatValue,
                defaultStatValue,
                defaultStatValue,
                defaultStatValue,
                defaultStatValue,
                defaultStatValue,
                defaultStatValue,
                defaultStatValue,
                defaultStatValue,
                defaultStatValue,
                defaultStatValue
            );

            var chestArmor = new ArmorPiece(
                "Chest",
                ArmorType.Chest,
                defaultStatValue,
                defaultStatValue,
                defaultStatValue,
                defaultStatValue,
                defaultStatValue,
                defaultStatValue,
                defaultStatValue,
                defaultStatValue,
                defaultStatValue,
                defaultStatValue,
                defaultStatValue,
                defaultStatValue,
                defaultStatValue,
                defaultStatValue
            );

            var handArmor = new ArmorPiece(
                "Hands",
                ArmorType.Hands,
                defaultStatValue,
                defaultStatValue,
                defaultStatValue,
                defaultStatValue,
                defaultStatValue,
                defaultStatValue,
                defaultStatValue,
                defaultStatValue,
                defaultStatValue,
                defaultStatValue,
                defaultStatValue,
                defaultStatValue,
                defaultStatValue,
                defaultStatValue
            );

            var legArmor = new ArmorPiece(
                "Legs",
                ArmorType.Legs,
                defaultStatValue,
                defaultStatValue,
                defaultStatValue,
                defaultStatValue,
                defaultStatValue,
                defaultStatValue,
                defaultStatValue,
                defaultStatValue,
                defaultStatValue,
                defaultStatValue,
                defaultStatValue,
                defaultStatValue,
                defaultStatValue,
                defaultStatValue
            );

            var armorSet = new ArmorSet(headArmor, chestArmor, handArmor, legArmor);

            yield return new TestCaseData(
                armorSet,
                new StatPriorityLoadout(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0),
                0
            );

            yield return new TestCaseData(
                armorSet,
                new StatPriorityLoadout(1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1),
                560
            );

            yield return new TestCaseData(
                armorSet,
                new StatPriorityLoadout(1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0),
                40
            );

            yield return new TestCaseData(
                armorSet,
                new StatPriorityLoadout(1, 2, 3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0),
                240
            );
        }
    }

    [Test]
    [TestCaseSource(nameof(TestCases))]
    public void ArmorSetScoreCalculator_calculates_expected_ArmorSet_score(
        ArmorSet armorSet,
        StatPriorityLoadout statPriorityLoadout,
        double expectedScore)
    {
        // arrange
        var calculator = new ArmorSetScoreCalculator();

        // act
        var score = calculator.Calculate(armorSet, statPriorityLoadout);

        // assert
        score.Should().Be(expectedScore);
    }
}
