using EldenRingArmorOptimizer.Engine.Configuration;
using EldenRingArmorOptimizer.Engine.Records;
using Microsoft.Extensions.Options;
using System.Net.Http.Json;

namespace EldenRingArmorOptimizer.Engine.Repositories;

public sealed class WeaponRepository : IWeaponRepository
{
    private const string WeaponsPath = "/data/weapons.json";
    private static IEnumerable<Weapon>? _weapons;
    private readonly HttpClient _httpClient;
    private readonly RepositoryConfiguration _configuration;

    public WeaponRepository(HttpClient httpClient, IOptions<RepositoryConfiguration> configurationOptions)
    {
        _httpClient = httpClient;
        _configuration = configurationOptions.Value;
    }

    public async Task<IEnumerable<Weapon>> GetAll()
    {
        _weapons ??= await _httpClient.GetFromJsonAsync<IEnumerable<Weapon>>(
            $"{_configuration.BaseAddress}{WeaponsPath}"
        );

        return _weapons!;
    }
}
