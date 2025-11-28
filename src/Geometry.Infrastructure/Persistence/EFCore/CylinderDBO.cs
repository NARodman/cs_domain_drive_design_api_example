namespace Geometry.Infrastructure.Persistence.EFCore;

/// <summary>
/// Database object representation of a Cylinder entity for Entity Framework Core persistence.
/// This class maps to the database table storing cylinder information.
/// </summary>
public class CylinderDBO
{
    /// <summary>
    /// Gets or sets the unique identifier of the cylinder.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the radius of the cylinder's base.
    /// </summary>
    public double Radius { get; set; }

    /// <summary>
    /// Gets or sets the height of the cylinder.
    /// </summary>
    public double Height { get; set; }
}
