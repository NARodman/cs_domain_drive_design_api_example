using Geometry.Domain.CylinderModel;
using Geometry.Infrastructure.Persistence.EFCore;

namespace Geometry.Infrastructure.Tests;

public class CylinderMapperTests
{
    [Fact]
    public void ToDBO_WithValidCylinder_ShouldMapCorrectly()
    {
        // Arrange
        var id = Guid.NewGuid();
        var radius = 4;
        var height = 10;
        var cylinder = new Cylinder(id, radius, height);

        // Act
        var dbo = CylinderMapper.ToDBO(cylinder);

        // Assert
        Assert.NotNull(dbo);
        Assert.Equal(id, dbo.Id);
        Assert.Equal(radius, dbo.Radius);
        Assert.Equal(height, dbo.Height);
    }

    [Fact]
    public void ToDBO_WithNullCylinder_ShouldThrowArgumentNullException()
    {
        // Arrange
        Cylinder cylinder = null!;

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => CylinderMapper.ToDBO(cylinder));
    }

    [Fact]
    public void ToDomain_WithValidDBO_ShouldMapCorrectly()
    {
        // Arrange
        var id = Guid.NewGuid();
        var radius = 7;
        var height = 20;

        var dbo = new CylinderDBO
        {
            Id = id,
            Radius = radius,
            Height = height
        };

        // Act
        var cylinder = CylinderMapper.ToDomain(dbo);

        // Assert
        Assert.NotNull(cylinder);
        Assert.Equal(id, cylinder.Id);
        Assert.Equal(radius, cylinder.Radius);
        Assert.Equal(height, cylinder.Height);
    }

    [Fact]
    public void ToDomain_WithNullDBO_ShouldThrowArgumentNullException()
    {
        // Arrange
        CylinderDBO dbo = null!;

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => CylinderMapper.ToDomain(dbo));
    }

    [Fact]
    public void ToDBO_ThenToDomain_ShouldRoundTripCorrectly()
    {
        // Arrange
        var id = Guid.NewGuid();
        var radius = 3;
        var height = 11;
        var original = new Cylinder(id, radius, height);

        // Act
        var dbo = CylinderMapper.ToDBO(original);
        var roundTripped = CylinderMapper.ToDomain(dbo);

        // Assert
        Assert.Equal(original.Id, roundTripped.Id);
        Assert.Equal(original.Radius, roundTripped.Radius);
        Assert.Equal(original.Height, roundTripped.Height);
    }

    [Fact]
    public void ToDomain_ThenToDBO_ShouldRoundTripCorrectly()
    {
        // Arrange
        var id = Guid.NewGuid();
        var radius = 8;
        var height = 25;

        var original = new CylinderDBO
        {
            Id = id,
            Radius = radius,
            Height = height
        };

        // Act
        var cylinder = CylinderMapper.ToDomain(original);
        var dbo = CylinderMapper.ToDBO(cylinder);

        // Assert
        Assert.Equal(original.Id, dbo.Id);
        Assert.Equal(original.Radius, dbo.Radius);
        Assert.Equal(original.Height, dbo.Height);
    }

    [Fact]
    public void ToDBO_WithVariousValues_ShouldMapCorrectly()
    {
        // Arrange
        var id = Guid.NewGuid();
        var values = new (int radius, int height)[]
        {
            (1, 1),
            (5, 10),
            (10, 50),
            (100, 200)
        };

        foreach (var (radius, height) in values)
        {
            var cylinder = new Cylinder(id, radius, height);

            // Act
            var dbo = CylinderMapper.ToDBO(cylinder);

            // Assert
            Assert.Equal(radius, dbo.Radius);
            Assert.Equal(height, dbo.Height);
        }
    }
}
