using EldenRingArmorOptimizer.Engine.DataTransfer;
using EldenRingArmorOptimizer.Engine.Enums;
using EldenRingArmorOptimizer.Engine.Mappers;
using EldenRingArmorOptimizer.Engine.Records;
using FluentAssertions;
using NUnit.Framework;

namespace EldenRingArmorOptimizer.Tests.Mappers;

[TestFixture]
public sealed class ArmorPieceMapperTests
{
    private static IEnumerable<TestCaseData> TestCases
    {
        get
        {
            yield return new TestCaseData(
                new ArmorPieceDto(
                    "test",
                    "head",
                    1.0,
                    1.0,
                    null,
                    null,
                    null,
                    null,
                    null,
                    null,
                    null,
                    null,
                    null,
                    null,
                    null,
                    1.0
                ),
                new ArmorPiece(
                    "test",
                    ArmorType.Head,
                    1.0,
                    1.0,
                    0.0,
                    0.0,
                    0.0,
                    0.0,
                    0.0,
                    0.0,
                    0.0,
                    0.0,
                    0.0,
                    0.0,
                    0.0,
                    1.0
                )
            );
        }
    }

    [Test]
    [TestCaseSource(nameof(TestCases))]
    public void ArmorPieceMapper_generates_expected_ArmorPiece(
        ArmorPieceDto armorPieceDto,
        ArmorPiece expectedArmorPiece)
    {
        // arrange
        var mapper = new ArmorPieceMapper();

        // act
        var armorPiece = mapper.Map(armorPieceDto);

        // assert
        armorPiece.Should().Be(expectedArmorPiece);
    }
}
