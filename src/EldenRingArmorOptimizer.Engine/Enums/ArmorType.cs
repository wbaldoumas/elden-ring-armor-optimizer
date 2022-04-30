namespace EldenRingArmorOptimizer.Engine.Enums;

public enum ArmorType
{
    Head,
    Chest,
    Hands,
    Legs
}

public static class ArmorTypes
{
    public static IEnumerable<ArmorType> All() => new[]
    {
        ArmorType.Head,
        ArmorType.Chest,
        ArmorType.Hands,
        ArmorType.Legs
    };
}
