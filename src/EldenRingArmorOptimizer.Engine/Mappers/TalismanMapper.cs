using EldenRingArmorOptimizer.Engine.DataTransfer;
using EldenRingArmorOptimizer.Engine.Records;

namespace EldenRingArmorOptimizer.Engine.Mappers;

public class TalismanMapper : BaseMapper<TalismanDto, Talisman>
{
    public override Talisman Map(TalismanDto item) => new(
        item.Name,
        item.Weight,
        item.EquipLoadModifier ?? 0.0,
        item.EnduranceModifier ?? 0
    );
}
