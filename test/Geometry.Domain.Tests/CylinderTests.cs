using Geometry.Domain.CylinderModel;

namespace Geometry.Domain.Tests;

public class CylinderTests
{
    [Fact]
    public void Constructor_WithValidParameters_ShouldCreateCylinder()
    {
        // Arrange
        var id = Guid.NewGuid();
        var radius = 3;
        var height = 10;

        // Act
        var cylinder = new Cylinder(id, radius, height);

        // Assert
        Assert.NotNull(cylinder);
        Assert.Equal(id, cylinder.Id);
        Assert.Equal(radius, cylinder.Radius);
        Assert.Equal(height, cylinder.Height);
    }

    [Fact]
    public void Constructor_WithZeroRadius_ShouldThrowArgumentException()
    {
        // Arrange
        var id = Guid.NewGuid();
        var radius = 0;
        var height = 10;

        // Act & Assert
        Assert.Throws<ArgumentException>(() => new Cylinder(id, radius, height));
    }

    [Fact]
    public void Constructor_WithNegativeRadius_ShouldThrowArgumentException()
    {
        // Arrange
        var id = Guid.NewGuid();
        var radius = -5;
        var height = 10;

        // Act & Assert
        var exception = Assert.Throws<ArgumentException>(() => new Cylinder(id, radius, height));
        Assert.Contains("Radius must be greater than 0", exception.Message);
    }

    [Fact]
    public void Constructor_WithZeroHeight_ShouldThrowArgumentException()
    {
        // Arrange
        var id = Guid.NewGuid();
        var radius = 3;
        var height = 0;

        // Act & Assert
        Assert.Throws<ArgumentException>(() => new Cylinder(id, radius, height));
    }

    [Fact]
    public void Constructor_WithNegativeHeight_ShouldThrowArgumentException()
    {
        // Arrange
        var id = Guid.NewGuid();
        var radius = 3;
        var height = -10;

        // Act & Assert
        var exception = Assert.Throws<ArgumentException>(() => new Cylinder(id, radius, height));
        Assert.Contains("Height must be greater than 0", exception.Message);
    }

    [Fact]
    public void Radius_SetValidValue_ShouldUpdateProperty()
    {
        // Arrange
        var id = Guid.NewGuid();
        var cylinder = new Cylinder(id, 3, 10);

        // Act
        cylinder.Radius = 6;

        // Assert
        Assert.Equal(6, cylinder.Radius);
    }

    [Fact]
    public void Radius_SetZero_ShouldThrowArgumentException()
    {
        // Arrange
        var id = Guid.NewGuid();
        var cylinder = new Cylinder(id, 3, 10);

        // Act & Assert
        var exception = Assert.Throws<ArgumentException>(() => cylinder.Radius = 0);
        Assert.Contains("Radius must be greater than 0", exception.Message);
        Assert.Equal("value", exception.ParamName);
    }

    [Fact]
    public void Radius_SetNegativeValue_ShouldThrowArgumentException()
    {
        // Arrange
        var id = Guid.NewGuid();
        var cylinder = new Cylinder(id, 3, 10);

        // Act & Assert
        var exception = Assert.Throws<ArgumentException>(() => cylinder.Radius = -5);
        Assert.Contains("Radius must be greater than 0", exception.Message);
        Assert.Equal("value", exception.ParamName);
    }

    [Fact]
    public void Height_SetValidValue_ShouldUpdateProperty()
    {
        // Arrange
        var id = Guid.NewGuid();
        var cylinder = new Cylinder(id, 3, 10);

        // Act
        cylinder.Height = 20;

        // Assert
        Assert.Equal(20, cylinder.Height);
    }

    [Fact]
    public void Height_SetZero_ShouldThrowArgumentException()
    {
        // Arrange
        var id = Guid.NewGuid();
        var cylinder = new Cylinder(id, 3, 10);

        // Act & Assert
        var exception = Assert.Throws<ArgumentException>(() => cylinder.Height = 0);
        Assert.Contains("Height must be greater than 0", exception.Message);
        Assert.Equal("value", exception.ParamName);
    }

    [Fact]
    public void Height_SetNegativeValue_ShouldThrowArgumentException()
    {
        // Arrange
        var id = Guid.NewGuid();
        var cylinder = new Cylinder(id, 3, 10);

        // Act & Assert
        var exception = Assert.Throws<ArgumentException>(() => cylinder.Height = -10);
        Assert.Contains("Height must be greater than 0", exception.Message);
        Assert.Equal("value", exception.ParamName);
    }

    [Fact]
    public void Radius_SetSameValue_ShouldNotThrowException()
    {
        // Arrange
        var id = Guid.NewGuid();
        var radius = 3;
        var cylinder = new Cylinder(id, radius, 10);

        // Act
        cylinder.Radius = radius;

        // Assert
        Assert.Equal(radius, cylinder.Radius);
    }

    [Fact]
    public void Height_SetSameValue_ShouldNotThrowException()
    {
        // Arrange
        var id = Guid.NewGuid();
        var height = 10;
        var cylinder = new Cylinder(id, 3, height);

        // Act
        cylinder.Height = height;

        // Assert
        Assert.Equal(height, cylinder.Height);
    }

    [Fact]
    public void Constructor_WithMinimumValidValues_ShouldCreateCylinder()
    {
        // Arrange
        var id = Guid.NewGuid();

        // Act
        var cylinder = new Cylinder(id, 1, 1);

        // Assert
        Assert.Equal(1, cylinder.Radius);
        Assert.Equal(1, cylinder.Height);
    }

    [Fact]
    public void Id_ShouldBeReadOnlyAfterConstruction()
    {
        // Arrange
        var id = Guid.NewGuid();
        var cylinder = new Cylinder(id, 3, 10);

        // Assert
        Assert.Equal(id, cylinder.Id);
    }
}
