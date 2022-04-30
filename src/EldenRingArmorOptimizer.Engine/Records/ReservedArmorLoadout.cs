namespace EldenRingArmorOptimizer.Engine.Records;

public readonly record struct ReservedArmorLoadout(
    ArmorPiece? HeadArmor,
    ArmorPiece? ChestArmor,
    ArmorPiece? HandArmor,
    ArmorPiece? LegArmor
);
