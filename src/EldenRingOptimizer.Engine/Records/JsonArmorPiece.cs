using System.Text.Json.Serialization;

namespace EldenRingOptimizer.Engine.Records;

public record JsonArmorPiece(
    [property: JsonPropertyName("name")] string? Name,
    [property: JsonPropertyName("type")] string Type,
    [property: JsonPropertyName("weight")] double? Weight,
    [property: JsonPropertyName("physical")] double? Physical,
    [property: JsonPropertyName("strike")] double? Strike,
    [property: JsonPropertyName("slash")] double? Slash,
    [property: JsonPropertyName("pierce")] double? Pierce,
    [property: JsonPropertyName("magic")] double? Magic,
    [property: JsonPropertyName("fire")] double? Fire,
    [property: JsonPropertyName("lightning")] double? Lightning,
    [property: JsonPropertyName("holy")] double? Holy,
    [property: JsonPropertyName("immunity")] double? Immunity,
    [property: JsonPropertyName("robustness")] double? Robustness,
    [property: JsonPropertyName("focus")] double? Focus,
    [property: JsonPropertyName("vitality")] double? Vitality,
    [property: JsonPropertyName("poise")] double? Poise
);
