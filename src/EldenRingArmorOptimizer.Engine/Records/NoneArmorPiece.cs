using EldenRingArmorOptimizer.Engine.Enums;

namespace EldenRingArmorOptimizer.Engine.Records;

/// <summary>
///     Represents the complete lack of armor for the specific armor <paramref name="Type"/>.
/// </summary>
/// <param name="Type">The specific armor type.</param>
public record NoneArmorPiece(ArmorType Type) : ArmorPiece("Naked", Type, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
