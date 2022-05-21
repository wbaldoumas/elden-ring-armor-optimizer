using System.Diagnostics.CodeAnalysis;

namespace EldenRingArmorOptimizer.Engine.Configuration;

[ExcludeFromCodeCoverage]
public sealed class ArmorOptimizerConfiguration
{
    public int MaxDegreesOfParallelism { get; set; }

    public int ArmorOptimizerWorkerSampleSize { get; set; }
}
