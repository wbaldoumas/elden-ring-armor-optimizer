using EldenRingArmorOptimizer.Engine.Enums;

namespace EldenRingArmorOptimizer.Engine.Records;

public record PlayerLoadout(
    byte Endurance,
    WeaponLoadout WeaponLoadout,
    TalismanLoadout TalismanLoadout,
    MinimumStatLoadout MinimumStatLoadout,
    StatPriorityLoadout StatPriorityLoadout,
    RollType TargetRollType)
{
    public double Weight => WeaponLoadout.Weight + TalismanLoadout.Weight;
}
