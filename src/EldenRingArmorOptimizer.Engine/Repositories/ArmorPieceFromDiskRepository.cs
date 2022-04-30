using EldenRingArmorOptimizer.Engine.DataTransfer;
using EldenRingArmorOptimizer.Engine.Enums;
using EldenRingArmorOptimizer.Engine.Mappers;
using EldenRingArmorOptimizer.Engine.Records;
using System.Reflection;
using System.Text.Json;

namespace EldenRingArmorOptimizer.Engine.Repositories;

/// <inheritdoc cref="IArmorPieceRepository"/>
public class ArmorPieceFromDiskRepository : IArmorPieceRepository
{
    private const string ArmorPiecesPath = "/data/armor.json";
    private static IList<ArmorPiece>? _armorPieces;
    private readonly string _armorPiecesPath;
    private readonly IMapper<ArmorPieceDto, ArmorPiece> _mapper;

    public ArmorPieceFromDiskRepository(IMapper<ArmorPieceDto, ArmorPiece> mapper)
    {
        _mapper = mapper;
        _armorPiecesPath = $"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}{ArmorPiecesPath}";
    }

    public async Task<IEnumerable<ArmorPiece>> GetByTypeAsync(ArmorType type)
    {
        await Initialize();

        return _armorPieces!.Where(armorPiece => armorPiece.IsOfArmorType(type));
    }

    private async Task Initialize()
    {
        if (_armorPieces is null)
        {
            var armorPiecesJson = await File.ReadAllTextAsync(_armorPiecesPath);
            var jsonArmorPieces = JsonSerializer.Deserialize<IEnumerable<ArmorPieceDto>>(armorPiecesJson);

            _armorPieces = _mapper.Map(jsonArmorPieces!).ToList();
        }
    }
}
