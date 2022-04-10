using EldenRingArmorOptimizer.Engine.Configuration;
using EldenRingArmorOptimizer.Engine.Enums;
using EldenRingArmorOptimizer.Engine.Mappers;
using EldenRingArmorOptimizer.Engine.Records;
using EldenRingArmorOptimizer.Engine.Repositories;
using FluentAssertions;
using Microsoft.Extensions.Options;
using NSubstitute;
using NUnit.Framework;

namespace EldenRingArmorOptimizer.Tests.Repositories;

[TestFixture]
public class ArmorPieceRepositoryTests
{
    [Test]
    public async Task ArmorPieceRepository_returns_expected_armor_pieces()
    {
        // arrange
        const string response = "[{\"name\":\"Alberich's Bracers\",\"type\":\"hands\",\"weight\":1.4,\"physical\":1.3,\"strike\":1,\"slash\":1.3,\"pierce\":1.3,\"magic\":3.2,\"fire\":2.9,\"lightning\":3.1,\"holy\":3.2,\"immunity\":13,\"robustness\":8,\"focus\":22,\"vitality\":24,\"poise\":0},{\"name\":\"Alberich's Pointed Hat (Altered)\",\"type\":\"head\",\"weight\":1,\"physical\":0.9,\"strike\":0.2,\"slash\":0.9,\"pierce\":0.9,\"magic\":4.4,\"fire\":3.8,\"lightning\":4,\"holy\":4.4,\"immunity\":12,\"robustness\":7,\"focus\":null,\"vitality\":24,\"poise\":null},{\"name\":\"Alberich's Pointed Hat\",\"type\":\"head\",\"weight\":1.7,\"physical\":1.8,\"strike\":1.4,\"slash\":1.8,\"pierce\":1.8,\"magic\":4.6,\"fire\":4.2,\"lightning\":4.4,\"holy\":4.6,\"immunity\":16,\"robustness\":10,\"focus\":29,\"vitality\":31,\"poise\":0},{\"name\":\"Alberich's Robe (Altered)\",\"type\":\"chest\",\"weight\":3.2,\"physical\":4.2,\"strike\":2.7,\"slash\":4.2,\"pierce\":4.2,\"magic\":12.6,\"fire\":11.4,\"lightning\":11.9,\"holy\":12.6,\"immunity\":32,\"robustness\":19,\"focus\":null,\"vitality\":61,\"poise\":null},{\"name\":\"Alberich's Robe\",\"type\":\"chest\",\"weight\":4.1,\"physical\":5.3,\"strike\":4.2,\"slash\":5.3,\"pierce\":5.3,\"magic\":12.8,\"fire\":11.9,\"lightning\":12.4,\"holy\":12.8,\"immunity\":38,\"robustness\":23,\"focus\":67,\"vitality\":71,\"poise\":1},{\"name\":\"Alberich's Trousers\",\"type\":\"legs\",\"weight\":2.5,\"physical\":3,\"strike\":2.3,\"slash\":3,\"pierce\":3,\"magic\":7.3,\"fire\":6.8,\"lightning\":7.2,\"holy\":7.3,\"immunity\":26,\"robustness\":14,\"focus\":41,\"vitality\":44,\"poise\":1}]";

        IEnumerable<ArmorPiece> expectedArmorPieces = new List<ArmorPiece>
        {
            new("Alberich's Pointed Hat (Altered)", ArmorType.Head, 1, 0.9, 0.2, 0.9, 0.9, 4.4, 3.8, 4.0, 4.4, 12, 7, 0, 24, 0),
            new("Alberich's Pointed Hat", ArmorType.Head, 1.7, 1.8, 1.4, 1.8, 1.8, 4.6, 4.2, 4.4, 4.6, 16, 10, 29, 31, 0)
        };

        var mockHttpClient = new HttpClient(new MockHttpMessageHandler(response));
        var mockOptions = Substitute.For<IOptions<RepositoryConfiguration>>();

        mockOptions.Value.Returns(new RepositoryConfiguration
        {
            BaseAddress = "https://wbaldoumas.github.io/elden-ring-armor-optimizer"
        });

        var armorPieceRepository = new ArmorPieceRepository(mockHttpClient, new ArmorPieceMapper(), mockOptions);

        // act
        var armorPieces = await armorPieceRepository.GetByTypeAsync(ArmorType.Head);

        // assert
        armorPieces.Should().BeEquivalentTo(expectedArmorPieces);
    }
}
