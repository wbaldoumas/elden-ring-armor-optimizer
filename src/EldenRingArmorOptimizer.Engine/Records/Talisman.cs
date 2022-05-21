namespace EldenRingArmorOptimizer.Engine.Records;

public readonly record struct Talisman(
    string Name,
    double Weight,
    double EquipLoadModifier,
    byte EnduranceModifier)
{
    public static Talisman None() => new("None", 0.0, 0.0, 0);
}
