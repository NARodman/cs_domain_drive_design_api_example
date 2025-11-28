namespace Geometry.Presentation.CubeApi.DTOs;

/// <summary>
/// Data Transfer Object for creating a new cylinder.
/// Represents the request payload for the POST /cylinder endpoint.
/// </summary>
public class CreateCylinderRequest
{
    /// <summary>
    /// Gets or sets the radius of the cylinder.
    /// Must be greater than 0.
    /// </summary>
    /// <example>3</example>
    public int Radius { get; set; }

    /// <summary>
    /// Gets or sets the height of the cylinder.
    /// Must be greater than 0.
    /// </summary>
    /// <example>10</example>
    public int Height { get; set; }
}
