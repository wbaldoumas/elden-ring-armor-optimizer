using EldenRingOptimizer.Engine.Enums;
using EldenRingOptimizer.Engine.Mappers;
using EldenRingOptimizer.Engine.Records;
using System.Net.Http.Json;

namespace EldenRingOptimizer.Engine.Repositories;

public class ArmorPieceRepository : IArmorPieceRepository
{
    private const string ArmorJsonPath = "/elden-ring-armor-optimizer/data/armor.json";
    private static IList<ArmorPiece>? _armorPieces;
    private readonly HttpClient _httpClient;
    private readonly IMapper<JsonArmorPiece, ArmorPiece> _mapper;

    public ArmorPieceRepository(HttpClient httpClient, IMapper<JsonArmorPiece, ArmorPiece> mapper)
    {
        _httpClient = httpClient;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ArmorPiece>> GetByTypeAsync(ArmorType type)
    {
        await Initialize();

        return _armorPieces!.Where(armorPiece => armorPiece.Type == type);
    }

    private async Task Initialize()
    {
        if (_armorPieces is null)
        {
            var jsonArmorPieces = await _httpClient.GetFromJsonAsync<IEnumerable<JsonArmorPiece>>(ArmorJsonPath);

            _armorPieces = _mapper.Map(jsonArmorPieces!).ToList();
        }
    }
}
