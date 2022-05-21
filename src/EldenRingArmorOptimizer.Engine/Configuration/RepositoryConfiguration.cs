using System.Diagnostics.CodeAnalysis;

namespace EldenRingArmorOptimizer.Engine.Configuration;

[ExcludeFromCodeCoverage]
public sealed class RepositoryConfiguration
{
    public const string Key = "Repository";

    public string BaseAddress { get; set; } = string.Empty;
}
