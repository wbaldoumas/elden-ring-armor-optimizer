using EldenRingArmorOptimizer.Engine.Calculators;
using EldenRingArmorOptimizer.Engine.Enums;
using EldenRingArmorOptimizer.Engine.Records;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;

namespace EldenRingArmorOptimizer.Tests.Calculators;

[TestFixture]
public class AvailableEquipLoadCalculatorTests
{
    private const double TestEquipLoad = 100.0;
    private IEquipLoadCalculator? _mockEquipLoadCalculator;

    private static IEnumerable<TestCaseData> TestCases
    {
        get
        {
            var minimumStatLoadout = new MinimumStatLoadout(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
            var statPriorityLoadout = new StatPriorityLoadout(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);

            var nullWeapon = new Weapon("null", 0.0);
            var nullTalisman = new Talisman("null", 0.0, 0.0, 0);

            var weightedWeapon = new Weapon("weighted", 5.0);
            var weightedTalisman = new Talisman("weighted", 5.0, 0.0, 0);

            yield return new TestCaseData(
                new PlayerLoadout(
                    40,
                    new WeaponLoadout(nullWeapon, nullWeapon, nullWeapon, nullWeapon, nullWeapon, nullWeapon),
                    new TalismanLoadout(nullTalisman, nullTalisman, nullTalisman, nullTalisman),
                    minimumStatLoadout,
                    statPriorityLoadout,
                    RollType.Overloaded,
                    10
                ),
                double.MaxValue
            );

            yield return new TestCaseData(
                new PlayerLoadout(
                    40,
                    new WeaponLoadout(nullWeapon, nullWeapon, nullWeapon, nullWeapon, nullWeapon, nullWeapon),
                    new TalismanLoadout(nullTalisman, nullTalisman, nullTalisman, nullTalisman),
                    minimumStatLoadout,
                    statPriorityLoadout,
                    RollType.Light,
                    10
                ),
                30
            );

            yield return new TestCaseData(
                new PlayerLoadout(
                    40,
                    new WeaponLoadout(weightedWeapon, nullWeapon, nullWeapon, nullWeapon, nullWeapon, nullWeapon),
                    new TalismanLoadout(nullTalisman, nullTalisman, nullTalisman, nullTalisman),
                    minimumStatLoadout,
                    statPriorityLoadout,
                    RollType.Light,
                    10
                ),
                25
            );

            yield return new TestCaseData(
                new PlayerLoadout(
                    40,
                    new WeaponLoadout(weightedWeapon, nullWeapon, nullWeapon, nullWeapon, nullWeapon, nullWeapon),
                    new TalismanLoadout(weightedTalisman, nullTalisman, nullTalisman, nullTalisman),
                    minimumStatLoadout,
                    statPriorityLoadout,
                    RollType.Light,
                    10
                ),
                20
            );

            yield return new TestCaseData(
                new PlayerLoadout(
                    40,
                    new WeaponLoadout(weightedWeapon, weightedWeapon, weightedWeapon, weightedWeapon, weightedWeapon, weightedWeapon),
                    new TalismanLoadout(weightedTalisman, weightedTalisman, weightedTalisman, weightedTalisman),
                    minimumStatLoadout,
                    statPriorityLoadout,
                    RollType.Medium,
                    10
                ),
                20
            );

            yield return new TestCaseData(
                new PlayerLoadout(
                    40,
                    new WeaponLoadout(weightedWeapon, weightedWeapon, weightedWeapon, weightedWeapon, weightedWeapon, weightedWeapon),
                    new TalismanLoadout(weightedTalisman, weightedTalisman, weightedTalisman, weightedTalisman),
                    minimumStatLoadout,
                    statPriorityLoadout,
                    RollType.Heavy,
                    10
                ),
                50
            );
        }
    }

    [SetUp]
    public void SetUp()
    {
        _mockEquipLoadCalculator = Substitute.For<IEquipLoadCalculator>();
    }

    [Test]
    [TestCaseSource(nameof(TestCases))]
    public void AvailableEquipLoadCalculator_calculates_expected_available_equip_load(
        PlayerLoadout playerLoadout,
        double expectedAvailableEquipLoad)
    {
        // arrange
        _mockEquipLoadCalculator!
            .Calculate(Arg.Any<byte>(), Arg.Any<TalismanLoadout>())
            .Returns(TestEquipLoad);

        var availableEquipLoadCalculator = new AvailableEquipLoadCalculator(_mockEquipLoadCalculator);

        // act
        var availableEquipLoad = availableEquipLoadCalculator.Calculate(playerLoadout);

        // assert
        availableEquipLoad.Should().Be(expectedAvailableEquipLoad);
    }
}
