namespace Geometry.Domain.CylinderModel;

/// <summary>
/// Repository interface for managing Cylinder entities.
/// Provides methods for retrieving and persisting Cylinder instances.
/// </summary>
public interface ICylinderRepository
{
    /// <summary>
    /// Retrieves a Cylinder by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the Cylinder to retrieve.</param>
    /// <returns>The cylinder with the specified identifier, or null if not found.</returns>
    Task<Cylinder?> ReadById(Guid id);

    /// <summary>
    /// Saves or updates a Cylinder entity in the repository.
    /// </summary>
    /// <param name="cylinder">The Cylinder entity to save or update.</param>
    /// <returns>The unique identifier of the saved Cylinder.</returns>
    Task<Guid> Insert(Cylinder cylinder);
}
