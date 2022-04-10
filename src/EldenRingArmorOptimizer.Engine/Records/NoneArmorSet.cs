using EldenRingArmorOptimizer.Engine.Enums;

namespace EldenRingArmorOptimizer.Engine.Records;

/// <summary>
///     Represents the complete lack of armor.
/// </summary>
public record NoneArmorSet() : ArmorSet(
    new NoneArmorPiece(ArmorType.Head),
    new NoneArmorPiece(ArmorType.Chest),
    new NoneArmorPiece(ArmorType.Hands),
    new NoneArmorPiece(ArmorType.Legs)
);
