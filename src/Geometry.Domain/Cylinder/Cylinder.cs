

namespace Geometry.Domain.CylinderModel;

/// <summary>
/// Represents a cylinder geometric entity with a specified radius and height.
/// </summary>
public class Cylinder : Entity
{
    private double _radius;
    private double _height;

    /// <summary>
    /// The radius of the cylinder's base.
    /// </summary>
    public double Radius
    {
        get => _radius;
        set
        {
            if (value <= 0)
            {
                throw new ArgumentException("Radius must be greater than 0.", nameof(value));
            }
            _radius = value;
        }
    }

    /// <summary>
    /// The height of the cylinder.
    /// </summary>
    public double Height
    {
        get => _height;
        set
        {
            if (value <= 0)
            {
                throw new ArgumentException("Height must be greater than 0.", nameof(value));
            }
            _height = value;
        }
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Cylinder"/> class.
    /// </summary>
    /// <param name="id">The unique identifier for the cylinder.</param>
    /// <param name="radius">The radius of the cylinder. Must be greater than 0.</param>
    /// <param name="height">The height of the cylinder. Must be greater than 0.</param>
    /// <exception cref="ArgumentException">
    /// Thrown when radius or height is less than or equal to 0.
    /// </exception>
    public Cylinder(Guid id, double radius, double height) : base(id)
    {
        Radius = radius;
        Height = height;
    }
}
