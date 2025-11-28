using Geometry.Domain.CylinderModel;
using Microsoft.EntityFrameworkCore;

namespace Geometry.Infrastructure.Persistence.EFCore;

/// <summary>
/// Entity Framework Core implementation of the ICylinderRepository interface.
/// Provides persistence operations for Cylinder entities using EF Core.
/// </summary>
public class CylinderRepository : ICylinderRepository
{
    private readonly GeometryDbContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="CylinderRepository"/> class.
    /// </summary>
    /// <param name="context">The database context to use for persistence operations.</param>
    /// <exception cref="ArgumentNullException">Thrown when context is null.</exception>
    public CylinderRepository(GeometryDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    /// <summary>
    /// Retrieves a Cylinder by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the Cylinder to retrieve.</param>
    /// <returns>
    /// A task that represents the asynchronous operation.
    /// The task result contains the cylinder with the specified identifier, or null if not found.
    /// </returns>
    public async Task<Cylinder?> ReadById(Guid id)
    {
        var cylinderDBO = await _context.Cylinders
            .FirstOrDefaultAsync(c => c.Id == id);

        return cylinderDBO == null ? null : CylinderMapper.ToDomain(cylinderDBO);
    }

    /// <summary>
    /// Saves or updates a Cylinder entity in the repository.
    /// </summary>
    /// <param name="cylinder">The Cylinder entity to save or update.</param>
    /// <returns>
    /// A task that represents the asynchronous operation.
    /// The task result contains the unique identifier of the saved or updated cylinder.
    /// </returns>
    /// <exception cref="ArgumentNullException">Thrown when cylinder is null.</exception>
    public async Task<Guid> Insert(Cylinder cylinder)
    {
        if (cylinder == null)
        {
            throw new ArgumentNullException(nameof(cylinder));
        }

        var existingCylinder = await _context.Cylinders
            .FirstOrDefaultAsync(c => c.Id == cylinder.Id);

        if (existingCylinder != null)
        {
            // Update existing entity
            existingCylinder.Radius = cylinder.Radius;
            existingCylinder.Height = cylinder.Height;
            _context.Cylinders.Update(existingCylinder);
        }
        else
        {
            // Insert new entity
            var cylinderDBO = CylinderMapper.ToDBO(cylinder);
            await _context.Cylinders.AddAsync(cylinderDBO);
        }

        await _context.SaveChangesAsync();

        return cylinder.Id;
    }
}
