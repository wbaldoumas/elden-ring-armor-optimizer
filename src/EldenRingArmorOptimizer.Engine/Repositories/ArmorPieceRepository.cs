using EldenRingArmorOptimizer.Engine.DataTransfer;
using EldenRingArmorOptimizer.Engine.Enums;
using EldenRingArmorOptimizer.Engine.Mappers;
using EldenRingArmorOptimizer.Engine.Records;
using System.Net.Http.Json;

namespace EldenRingArmorOptimizer.Engine.Repositories;

public class ArmorPieceRepository : IArmorPieceRepository
{
    private const string ArmorPiecesPath = "/data/armor.json";
    private static IList<ArmorPiece>? _armorPieces;
    private readonly HttpClient _httpClient;
    private readonly IMapper<ArmorPieceDto, ArmorPiece> _mapper;

    public ArmorPieceRepository(HttpClient httpClient, IMapper<ArmorPieceDto, ArmorPiece> mapper)
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
            var jsonArmorPieces = await _httpClient.GetFromJsonAsync<IEnumerable<ArmorPieceDto>>(ArmorPiecesPath);

            _armorPieces = _mapper.Map(jsonArmorPieces!).ToList();
        }
    }
}
