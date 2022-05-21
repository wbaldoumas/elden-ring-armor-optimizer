using EldenRingArmorOptimizer.Engine.Calculators;
using EldenRingArmorOptimizer.Engine.Configuration;
using EldenRingArmorOptimizer.Engine.Enums;
using EldenRingArmorOptimizer.Engine.Mappers;
using EldenRingArmorOptimizer.Engine.Records;
using EldenRingArmorOptimizer.Engine.Repositories;
using EldenRingArmorOptimizer.Engine.Services;
using System.Diagnostics;

var optimizer = new ArmorOptimizer(
    new ArmorPieceFromDiskRepository(
        new ArmorPieceMapper()
    ),
    new AvailableEquipLoadCalculator(
        new EquipLoadCalculator()
    ),
    new ArmorOptimizerWorker(new ArmorSetScoreCalculator()),
    new ArmorOptimizerConfiguration
    {
        MaxDegreesOfParallelism = 6,
        ArmorOptimizerWorkerSampleSize = 25
    }
);

var stopwatch = new Stopwatch();

stopwatch.Start();

var results = await optimizer.Optimize(
    new PlayerLoadout(
        20,
        new WeaponLoadout(Array.Empty<Weapon>()),
        new TalismanLoadout(Array.Empty<Talisman>()),
        new MinimumStatLoadout(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0),
        new StatPriorityLoadout(1, 1, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0),
        new ReservedArmorLoadout(null, null, null, null),
        RollType.Medium,
        5
    )
);

Console.WriteLine(stopwatch.ElapsedMilliseconds);

foreach (var armorSet in results)
{
    Console.Write($"{armorSet.ArmorPieces.First(armorPiece => armorPiece.IsOfArmorType(ArmorType.Head)).Name} | ");
    Console.Write($"{armorSet.ArmorPieces.First(armorPiece => armorPiece.IsOfArmorType(ArmorType.Chest)).Name} | ");
    Console.Write($"{armorSet.ArmorPieces.First(armorPiece => armorPiece.IsOfArmorType(ArmorType.Hands)).Name} | ");
    Console.Write($"{armorSet.ArmorPieces.First(armorPiece => armorPiece.IsOfArmorType(ArmorType.Legs)).Name}");
    Console.WriteLine();
}

Console.ReadLine();
