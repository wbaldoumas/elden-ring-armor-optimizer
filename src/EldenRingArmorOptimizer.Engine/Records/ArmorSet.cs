namespace EldenRingArmorOptimizer.Engine.Records;

public record ArmorSet(ArmorPiece HeadArmor, ArmorPiece ChestArmor, ArmorPiece HandArmor, ArmorPiece LegArmor)
{
    public double Weight => HeadArmor.Weight + ChestArmor.Weight + HandArmor.Weight + LegArmor.Weight;

    public double AveragePhysical => (Physical + Strike + Slash + Pierce) / 4;

    public double Physical => HeadArmor.Physical + ChestArmor.Physical + HandArmor.Physical + LegArmor.Physical;

    public double Strike => HeadArmor.Strike + ChestArmor.Strike + HandArmor.Strike + LegArmor.Strike;

    public double Slash => HeadArmor.Slash + ChestArmor.Slash + HandArmor.Slash + LegArmor.Slash;

    public double Pierce => HeadArmor.Pierce + ChestArmor.Pierce + HandArmor.Pierce + LegArmor.Pierce;

    public double Magic => HeadArmor.Magic + ChestArmor.Magic + HandArmor.Magic + LegArmor.Magic;

    public double Fire => HeadArmor.Fire + ChestArmor.Fire + HandArmor.Fire + LegArmor.Fire;

    public double Lightning => HeadArmor.Lightning + ChestArmor.Lightning + HandArmor.Lightning + LegArmor.Lightning;

    public double Holy => HeadArmor.Holy + ChestArmor.Holy + HandArmor.Holy + LegArmor.Holy;

    public double Immunity => HeadArmor.Immunity + ChestArmor.Immunity + HandArmor.Immunity + LegArmor.Immunity;

    public double Robustness => HeadArmor.Robustness + ChestArmor.Robustness + HandArmor.Robustness + LegArmor.Robustness;

    public double Focus => HeadArmor.Focus + ChestArmor.Focus + HandArmor.Focus + LegArmor.Focus;

    public double Vitality => HeadArmor.Vitality + ChestArmor.Vitality + HandArmor.Vitality + LegArmor.Vitality;

    public double Poise => HeadArmor.Poise + ChestArmor.Poise + HandArmor.Poise + LegArmor.Poise;
}
