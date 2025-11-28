using Geometry.Domain.CylinderModel;
using Geometry.Presentation.CubeApi.DTOs;

namespace Geometry.Presentation.CubeApi.Mappers;

/// <summary>
/// Mapper class for converting between Cylinder domain entities and DTOs.
/// Provides bidirectional mapping between domain objects and data transfer objects.
/// </summary>
public static class CylinderDtoMapper
{
    /// <summary>
    /// Converts a Cylinder domain entity to a CylinderResponse DTO.
    /// </summary>
    /// <param name="cylinder">The Cylinder domain entity to convert. Cannot be null.</param>
    /// <returns>A CylinderResponse DTO containing the cylinder's Id, Radius, and Height.</returns>
    /// <exception cref="ArgumentNullException">Thrown when cylinder is null.</exception>
    public static CylinderResponse ToDto(Cylinder cylinder)
    {
        if (cylinder == null)
        {
            throw new ArgumentNullException(nameof(cylinder));
        }

        return new CylinderResponse
        {
            Id = cylinder.Id,
            Radius = (int)cylinder.Radius,
            Height = (int)cylinder.Height
        };
    }

    /// <summary>
    /// Converts a CreateCylinderRequest DTO to a Cylinder domain entity.
    /// Generates a new GUID for the cylinder's Id.
    /// </summary>
    /// <param name="request">The CreateCylinderRequest DTO to convert. Cannot be null.</param>
    /// <returns>A new Cylinder domain entity with the specified radius and height.</returns>
    /// <exception cref="ArgumentNullException">Thrown when request is null.</exception>
    public static Cylinder ToDomain(CreateCylinderRequest request)
    {
        if (request == null)
        {
            throw new ArgumentNullException(nameof(request));
        }

        return new Cylinder(
            Guid.NewGuid(),
            request.Radius,
            request.Height
        );
    }
}
