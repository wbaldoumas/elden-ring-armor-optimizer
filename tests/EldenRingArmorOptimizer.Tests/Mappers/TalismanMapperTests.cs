using EldenRingArmorOptimizer.Engine.DataTransfer;
using EldenRingArmorOptimizer.Engine.Mappers;
using EldenRingArmorOptimizer.Engine.Records;
using FluentAssertions;
using NUnit.Framework;

namespace EldenRingArmorOptimizer.Tests.Mappers;

[TestFixture]
public sealed class TalismanMapperTests
{
    private static IEnumerable<TestCaseData> TestCases
    {
        get
        {
            yield return new TestCaseData(
                new TalismanDto(
                    "test",
                    1.0,
                    null,
                    null
                ),
                new Talisman(
                    "test",
                    1.0,
                    0.0,
                    0
                )
            );

            yield return new TestCaseData(
                new TalismanDto(
                    "test",
                    1.0,
                    0.19,
                    5
                ),
                new Talisman(
                    "test",
                    1.0,
                    0.19,
                    5
                )
            );
        }
    }

    [Test]
    [TestCaseSource(nameof(TestCases))]
    public void TalismanMapper_maps_to_expected_Talisman(TalismanDto talismanDto, Talisman expectedTalisman)
    {
        // arrange
        var mapper = new TalismanMapper();

        // act
        var talisman = mapper.Map(talismanDto);

        // assert
        talisman.Should().Be(expectedTalisman);
    }
}
