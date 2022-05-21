using EldenRingArmorOptimizer.Engine.Enums;

namespace EldenRingArmorOptimizer.Engine.Records;

public readonly record struct ArmorSet
{
    public ArmorSet(params ArmorPiece[] armorPieces)
    {
        ArmorPieces = armorPieces;
    }

    public IReadOnlyCollection<ArmorPiece> ArmorPieces { get; }

    public double Weight => ArmorPieces.Sum(armorPiece => armorPiece.Weight);

    public double Physical => ArmorPieces.Sum(armorPiece => armorPiece.Physical);

    public double Strike => ArmorPieces.Sum(armorPiece => armorPiece.Strike);

    public double Slash => ArmorPieces.Sum(armorPiece => armorPiece.Slash);

    public double Pierce => ArmorPieces.Sum(armorPiece => armorPiece.Pierce);

    public double AveragePhysical => (Physical + Strike + Slash + Pierce) / 4;

    public double Magic => ArmorPieces.Sum(armorPiece => armorPiece.Magic);

    public double Fire => ArmorPieces.Sum(armorPiece => armorPiece.Fire);

    public double Lightning => ArmorPieces.Sum(armorPiece => armorPiece.Lightning);

    public double Holy => ArmorPieces.Sum(armorPiece => armorPiece.Holy);

    public double Immunity => ArmorPieces.Sum(armorPiece => armorPiece.Immunity);

    public double Robustness => ArmorPieces.Sum(armorPiece => armorPiece.Robustness);

    public double Focus => ArmorPieces.Sum(armorPiece => armorPiece.Focus);

    public double Vitality => ArmorPieces.Sum(armorPiece => armorPiece.Vitality);

    public double Poise => ArmorPieces.Sum(armorPiece => armorPiece.Poise);

    public static ArmorSet None() => new(
        ArmorPiece.None(ArmorType.Head),
        ArmorPiece.None(ArmorType.Chest),
        ArmorPiece.None(ArmorType.Hands),
        ArmorPiece.None(ArmorType.Legs)
    );
}
