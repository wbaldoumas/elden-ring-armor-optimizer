namespace EldenRingArmorOptimizer.Engine.Records;

public readonly record struct TalismanLoadout(params Talisman[] Talismans)
{
    public double Weight => Talismans.Sum(talisman => talisman.Weight);
}
