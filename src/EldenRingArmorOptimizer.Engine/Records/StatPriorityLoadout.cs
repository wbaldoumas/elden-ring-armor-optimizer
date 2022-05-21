namespace EldenRingArmorOptimizer.Engine.Records;

public readonly record struct StatPriorityLoadout(
    int AveragePhysicalPriority,
    int PhysicalPriority,
    int StrikePriority,
    int SlashPriority,
    int PiercePriority,
    int MagicPriority,
    int FirePriority,
    int LightningPriority,
    int HolyPriority,
    int ImmunityPriority,
    int RobustnessPriority,
    int FocusPriority,
    int VitalityPriority,
    int PoisePriority
);
