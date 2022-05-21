namespace EldenRingArmorOptimizer.Engine.Records;

public readonly record struct MinimumStatLoadout(
    double MinimumAveragePhysical,
    double MinimumPhysical,
    double MinimumStrike,
    double MinimumSlash,
    double MinimumPierce,
    double MinimumMagic,
    double MinimumFire,
    double MinimumLightning,
    double MinimumHoly,
    double MinimumImmunity,
    double MinimumRobustness,
    double MinimumFocus,
    double MinimumVitality,
    double MinimumPoise
);
