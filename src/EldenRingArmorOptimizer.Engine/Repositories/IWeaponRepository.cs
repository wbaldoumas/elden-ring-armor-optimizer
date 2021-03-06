using EldenRingArmorOptimizer.Engine.Records;

namespace EldenRingArmorOptimizer.Engine.Repositories;

/// <summary>
///     A repository of <see cref="Weapon"/>.
/// </summary>
public interface IWeaponRepository
{
    /// <summary>
    ///     Retrieve all of the weapons.
    /// </summary>
    /// <returns>All of the weapons.</returns>
    Task<IEnumerable<Weapon>> GetAll();
}
