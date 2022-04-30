namespace EldenRingArmorOptimizer.Engine.DataTransfer;

public sealed record TalismanDto(
    string Name,
    double Weight,
    double? EquipLoadModifier,
    byte? EnduranceModifier
);
