using EldenRingArmorOptimizer.Engine.Configuration;
using EldenRingArmorOptimizer.Engine.Records;
using EldenRingArmorOptimizer.Engine.Repositories;
using FluentAssertions;
using Microsoft.Extensions.Options;
using NSubstitute;
using NUnit.Framework;

namespace EldenRingArmorOptimizer.Tests.Repositories;

[TestFixture]
public sealed class WeaponRepositoryTests
{
    [Test]
    public async Task WeaponRepository_returns_expected_weapons()
    {
        // arrange
        const string response = "[{\"Name\":\"Hand Axe\",\"Weight\":3.5},{\"Name\":\"Forked Hatchet\",\"Weight\":2.5},{\"Name\":\"Battle Axe\",\"Weight\":4.5}]";

        IEnumerable<Weapon> expectedWeapons = new List<Weapon>
        {
            new("Hand Axe", 3.5),
            new("Forked Hatchet", 2.5),
            new("Battle Axe", 4.5)
        };

        var mockHttpClient = new HttpClient(new MockHttpMessageHandler(response));
        var mockOptions = Substitute.For<IOptions<RepositoryConfiguration>>();

        mockOptions.Value.Returns(new RepositoryConfiguration
        {
            BaseAddress = "https://wbaldoumas.github.io/elden-ring-armor-optimizer"
        });

        var weaponRepository = new WeaponRepository(mockHttpClient, mockOptions);

        // act
        var weapons = await weaponRepository.GetAll();

        // assert
        weapons.Should().BeEquivalentTo(expectedWeapons);
    }
}
