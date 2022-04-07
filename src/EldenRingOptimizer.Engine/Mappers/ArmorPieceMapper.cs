using EldenRingOptimizer.Engine.Enums;
using EldenRingOptimizer.Engine.Records;

namespace EldenRingOptimizer.Engine.Mappers;

public class ArmorPieceMapper : BaseMapper<JsonArmorPiece, ArmorPiece>
{
    public override ArmorPiece Map(JsonArmorPiece item) => new(
        item.Name ?? string.Empty,
        Enum.Parse<ArmorType>(string.Concat(item.Type[0].ToString().ToUpper(), item.Type.AsSpan(1))),
        item.Weight ?? 0.0,
        item.Physical ?? 0.0,
        item.Strike ?? 0.0,
        item.Slash ?? 0.0,
        item.Pierce ?? 0.0,
        item.Magic ?? 0.0,
        item.Fire ?? 0.0,
        item.Lightning ?? 0.0,
        item.Holy ?? 0.0,
        item.Immunity ?? 0.0,
        item.Robustness ?? 0.0,
        item.Focus ?? 0.0,
        item.Vitality ?? 0.0,
        item.Poise ?? 0.0
    );
}
