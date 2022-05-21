using EldenRingArmorOptimizer.Engine.Enums;

namespace EldenRingArmorOptimizer.Engine.Records;

public readonly record struct ArmorPiece(
    string Name,
    ArmorType Type,
    double Weight,
    double Physical,
    double Strike,
    double Slash,
    double Pierce,
    double Magic,
    double Fire,
    double Lightning,
    double Holy,
    double Immunity,
    double Robustness,
    double Focus,
    double Vitality,
    double Poise)
{
    public static ArmorPiece None(ArmorType armorType) => new(
        "None",
        armorType,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0
    );

    public bool IsOfArmorType(ArmorType type) => Type == type;
}
