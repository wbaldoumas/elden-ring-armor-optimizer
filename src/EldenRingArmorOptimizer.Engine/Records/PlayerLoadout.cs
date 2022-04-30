using EldenRingArmorOptimizer.Engine.Enums;

namespace EldenRingArmorOptimizer.Engine.Records;

public readonly record struct PlayerLoadout(
    byte Endurance,
    WeaponLoadout WeaponLoadout,
    TalismanLoadout TalismanLoadout,
    MinimumStatLoadout MinimumStatLoadout,
    StatPriorityLoadout StatPriorityLoadout,
    ReservedArmorLoadout ReservedArmorLoadout,
    RollType TargetRollType,
    int NumberOfResults)
{
    public double Weight => WeaponLoadout.Weight + TalismanLoadout.Weight;
}
