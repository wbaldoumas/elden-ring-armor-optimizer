namespace EldenRingArmorOptimizer.Engine.Records;

public readonly record struct Weapon(
    string Name,
    double Weight)
{
    public static Weapon None() => new("None", 0.0);
}
