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
                    "Test",
                    "head",
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
                ),
                new ArmorPiece(
                    "Test",
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
            ).SetName("Non-null values are mapped appropriately.");

            yield return new TestCaseData(
                new ArmorPieceDto(
                    null,
                    "head",
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
                    null,
                    null,
                    null
                ),
                new ArmorPiece(
                    string.Empty,
                    ArmorType.Head,
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
                    0.0,
                    0.0,
                    0.0
                )
            ).SetName("Null values are mapped appropriately.");
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
