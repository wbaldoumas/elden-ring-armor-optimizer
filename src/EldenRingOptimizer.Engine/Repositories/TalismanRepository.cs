using EldenRingOptimizer.Engine.Records;
using System.Net.Http.Json;

namespace EldenRingOptimizer.Engine.Repositories;

public class TalismanRepository : ITalismanRepository
{
    private const string TalismanJsonPath = "/elden-ring-armor-optimizer/data/talismans.json";
    private static IList<Talisman>? _talismans;
    private readonly HttpClient _httpClient;

    public TalismanRepository(HttpClient httpClient) => _httpClient = httpClient;

    public async Task<IEnumerable<Talisman>> GetAll()
    {
        _talismans ??= await _httpClient.GetFromJsonAsync<IList<Talisman>>(TalismanJsonPath);

        return _talismans!;
    }
}
