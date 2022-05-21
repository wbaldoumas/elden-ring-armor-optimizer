namespace EldenRingArmorOptimizer.Engine.Records;

public readonly record struct WeaponLoadout(params Weapon[] Weapons)
{
    public double Weight => Weapons.Sum(weapon => weapon.Weight);
}
