using EldenRingArmorOptimizer.Engine.Calculators;
using EldenRingArmorOptimizer.Engine.Enums;
using FluentAssertions;
using NUnit.Framework;

namespace EldenRingArmorOptimizer.Tests.Calculators;

[TestFixture]
public class AvailableEquipLoadCalculatorTests
{
    private static IEnumerable<TestCaseData> TestCases
    {
        get
        {
            yield return new TestCaseData(
                100,
                RollType.Light,
                30
            );

            yield return new TestCaseData(
                100,
                RollType.Medium,
                70
            );

            yield return new TestCaseData(
                100,
                RollType.Heavy,
                100
            );

            yield return new TestCaseData(
                100,
                RollType.Overloaded,
                double.MaxValue
            );
        }
    }

    [Test]
    [TestCaseSource(nameof(TestCases))]
    public void AvailableEquipLoadCalculator_calculates_expected_available_equip_load(
        double equipLoad,
        RollType rollType,
        double expectedAvailableEquipLoad)
    {
        // arrange
        var availableEquipLoadCalculator = new AvailableEquipLoadCalculator();

        // act
        var availableEquipLoad = availableEquipLoadCalculator.Calculate(equipLoad, rollType);

        // assert
        availableEquipLoad.Should().Be(expectedAvailableEquipLoad);
    }
}
