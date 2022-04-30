using EldenRingArmorOptimizer.Engine.Configuration;
using EldenRingArmorOptimizer.Engine.DataTransfer;
using EldenRingArmorOptimizer.Engine.Mappers;
using EldenRingArmorOptimizer.Engine.Records;
using Microsoft.Extensions.Options;
using System.Net.Http.Json;

namespace EldenRingArmorOptimizer.Engine.Repositories;

public sealed class TalismanRepository : ITalismanRepository
{
    private const string TalismansPath = "/data/talismans.json";
    private static IList<Talisman>? _talismans;
    private readonly HttpClient _httpClient;
    private readonly IMapper<TalismanDto, Talisman> _mapper;
    private readonly RepositoryConfiguration _configuration;

    public TalismanRepository(
        HttpClient httpClient,
        IMapper<TalismanDto, Talisman> mapper,
        IOptions<RepositoryConfiguration> configurationOptions)
    {
        _httpClient = httpClient;
        _mapper = mapper;
        _configuration = configurationOptions.Value;
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
            var talismans = await _httpClient.GetFromJsonAsync<IEnumerable<TalismanDto>>(
                $"{_configuration.BaseAddress}{TalismansPath}"
            );

            _talismans = _mapper.Map(talismans!).ToList();
        }
    }
}
