using EldenRingArmorOptimizer.Engine.DataTransfer;
using EldenRingArmorOptimizer.Engine.Mappers;
using EldenRingArmorOptimizer.Engine.Records;
using System.Net.Http.Json;

namespace EldenRingArmorOptimizer.Engine.Repositories;

public class TalismanRepository : ITalismanRepository
{
    private const string TalismansPath = "/data/talismans.json";
    private static IList<Talisman>? _talismans;
    private readonly HttpClient _httpClient;
    private readonly IMapper<TalismanDto, Talisman> _mapper;

    public TalismanRepository(HttpClient httpClient, IMapper<TalismanDto, Talisman> mapper)
    {
        _httpClient = httpClient;
        _mapper = mapper;
    }

    public async Task<IEnumerable<Talisman>> GetAll()
    {
        await Initialize();

        return _talismans!;
    }

    private async Task Initialize()
    {
        if (_talismans is null)
        {
            var talismans = await _httpClient.GetFromJsonAsync<IList<TalismanDto>>(TalismansPath);

            _talismans = _mapper.Map(talismans!).ToList();
        }
    }
}
