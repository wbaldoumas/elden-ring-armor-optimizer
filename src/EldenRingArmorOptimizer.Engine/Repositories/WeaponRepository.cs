using EldenRingArmorOptimizer.Engine.Records;
using System.Net.Http.Json;

namespace EldenRingArmorOptimizer.Engine.Repositories;

public class WeaponRepository : IWeaponRepository
{
    private const string WeaponsPath = "/data/weapons.json";
    private static IList<Weapon>? _weapons;
    private readonly HttpClient _httpClient;

    public WeaponRepository(HttpClient httpClient) => _httpClient = httpClient;

    public async Task<IEnumerable<Weapon>> GetAll()
    {
        _weapons ??= await _httpClient.GetFromJsonAsync<IList<Weapon>>(WeaponsPath);

        return _weapons!;
    }
}
