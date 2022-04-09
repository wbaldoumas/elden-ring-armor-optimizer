namespace EldenRingArmorOptimizer.Engine.DataTransfer;

public record TalismanDto(
    string Name,
    double Weight,
    double? EquipLoadModifier,
    byte? EnduranceModifier
);
