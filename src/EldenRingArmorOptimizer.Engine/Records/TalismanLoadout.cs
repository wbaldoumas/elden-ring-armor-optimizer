namespace EldenRingArmorOptimizer.Engine.Records;

public readonly record struct TalismanLoadout(params Talisman[] Talismans)
{
    public double Weight => Talismans.Sum(talisman => talisman.Weight);

    public double EnduranceModifier => Talismans.Sum(talisman => talisman.EnduranceModifier);

    public double EquipLoadModifier => Talismans.Sum(talisman => talisman.EquipLoadModifier);
}
