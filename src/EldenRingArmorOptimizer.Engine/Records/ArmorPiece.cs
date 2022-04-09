using EldenRingArmorOptimizer.Engine.Enums;

namespace EldenRingArmorOptimizer.Engine.Records;

public record ArmorPiece(
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
    double Poise
);
