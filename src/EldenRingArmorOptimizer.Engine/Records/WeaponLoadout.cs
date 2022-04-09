namespace EldenRingArmorOptimizer.Engine.Records;

public record WeaponLoadout(
    Weapon Weapon1,
    Weapon Weapon2,
    Weapon Weapon3,
    Weapon Weapon4,
    Weapon Weapon5,
    Weapon Weapon6)
{
    public double Weight => Weapon1.Weight +
                            Weapon2.Weight +
                            Weapon3.Weight +
                            Weapon4.Weight +
                            Weapon5.Weight +
                            Weapon6.Weight;
}
