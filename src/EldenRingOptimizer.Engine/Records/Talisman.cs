namespace EldenRingOptimizer.Engine.Records;

public record Talisman(
    string Name,
    double Weight,
    double? EquipLoadModifier,
    double? EnduranceModifier
);
