namespace EldenRingArmorOptimizer.Engine.Records;

public record TalismanLoadout(Talisman Talisman1, Talisman Talisman2, Talisman Talisman3, Talisman Talisman4)
{
    public double Weight => Talisman1.Weight + Talisman2.Weight + Talisman3.Weight + Talisman4.Weight;
}
