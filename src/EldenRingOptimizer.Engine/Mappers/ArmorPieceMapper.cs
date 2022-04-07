using EldenRingOptimizer.Engine.Records;

namespace EldenRingOptimizer.Engine.Mappers;

public class ArmorPieceMapper : BaseMapper<JsonArmorPiece, ArmorPiece>
{
    public override ArmorPiece Map(JsonArmorPiece item) => new ArmorPiece(
        item.Name ?? string.Empty,
        item.Type
    );
}
