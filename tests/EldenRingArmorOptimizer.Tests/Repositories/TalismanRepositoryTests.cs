using EldenRingArmorOptimizer.Engine.Configuration;
using EldenRingArmorOptimizer.Engine.Mappers;
using EldenRingArmorOptimizer.Engine.Records;
using EldenRingArmorOptimizer.Engine.Repositories;
using FluentAssertions;
using Microsoft.Extensions.Options;
using NSubstitute;
using NUnit.Framework;

namespace EldenRingArmorOptimizer.Tests.Repositories;

[TestFixture]
public sealed class TalismanRepositoryTests
{
    [Test]
    public async Task TalismanRepository_returns_expected_talismans()
    {
        // arrange
        const string response = "[{\"Name\":\"Crepus\\u2019s Vial\",\"Weight\":0.7,\"EquipLoadModifier\":null,\"EnduranceModifier\":null},{\"Name\":\"Crimson Amber Medallion\",\"Weight\":0.3,\"EquipLoadModifier\":null,\"EnduranceModifier\":null},{\"Name\":\"Crimson Amber Medallion \\u002B1\",\"Weight\":0.3,\"EquipLoadModifier\":null,\"EnduranceModifier\":null}]";

        IEnumerable<Talisman> expectedTalismans = new List<Talisman>
        {
            new("Crepus’s Vial", 0.7, 0.0, 0),
            new("Crimson Amber Medallion", 0.3, 0.0, 0),
            new("Crimson Amber Medallion +1", 0.3, 0.0, 0)
        };

        var mockHttpClient = new HttpClient(new MockHttpMessageHandler(response));
        var mockOptions = Substitute.For<IOptions<RepositoryConfiguration>>();

        mockOptions.Value.Returns(new RepositoryConfiguration
        {
            BaseAddress = "https://wbaldoumas.github.io/elden-ring-armor-optimizer"
        });

        var talismanRepository = new TalismanRepository(mockHttpClient, new TalismanMapper(), mockOptions);

        // act
        var talismans = await talismanRepository.GetAll();

        // assert
        talismans.Should().BeEquivalentTo(expectedTalismans);
    }
}
