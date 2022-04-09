using EldenRingArmorOptimizer.Engine.Calculators;
using EldenRingArmorOptimizer.Engine.Records;
using FluentAssertions;
using NUnit.Framework;

namespace EldenRingArmorOptimizer.Tests.Calculators;

[TestFixture]
public class EquipLoadRepositoryTests
{
    private static IEnumerable<TestCaseData> TestCases
    {
        get
        {
            const byte startingEndurance = 8;
            var zeroTalisman = new Talisman("Null", 0.0, 0.0, 0);
            var enduranceTalisman = new Talisman("Endurance", 0.0, 0.0, 5);
            var equipLoadTalismanA = new Talisman("Equip Load Talisman A", 0.0, 0.19, 0);
            var equipLoadTalismanB = new Talisman("Equip Load Talisman B", 0.0, 0.15, 0);

            yield return new TestCaseData(
                startingEndurance,
                zeroTalisman,
                zeroTalisman,
                zeroTalisman,
                zeroTalisman,
                45.0
            );

            yield return new TestCaseData(
                startingEndurance,
                enduranceTalisman,
                zeroTalisman,
                zeroTalisman,
                zeroTalisman,
                52.9
            );

            yield return new TestCaseData(
                startingEndurance,
                enduranceTalisman,
                enduranceTalisman,
                enduranceTalisman,
                enduranceTalisman,
                75.2
            );

            yield return new TestCaseData(
                startingEndurance,
                equipLoadTalismanA,
                zeroTalisman,
                zeroTalisman,
                zeroTalisman,
                53.55
            );

            yield return new TestCaseData(
                startingEndurance,
                equipLoadTalismanA,
                equipLoadTalismanB,
                zeroTalisman,
                zeroTalisman,
                60.3
            );

            yield return new TestCaseData(
                startingEndurance,
                enduranceTalisman,
                equipLoadTalismanA,
                equipLoadTalismanB,
                zeroTalisman,
                70.886
            );
        }
    }

    [Test]
    [TestCaseSource(nameof(TestCases))]
    public void EquipLoadRepository_generates_expected_equip_load(
        byte endurance,
        Talisman talisman1,
        Talisman talisman2,
        Talisman talisman3,
        Talisman talisman4,
        double expectedEquipLoad)
    {
        // arrange
        var equipLoadRepository = new EquipLoadCalculator();

        // act
        var equipLoad = equipLoadRepository.Get(endurance, talisman1, talisman2, talisman3, talisman4);

        // assert
        equipLoad.Should().Be(expectedEquipLoad);
    }
}
