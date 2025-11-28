using Geometry.Infrastructure.Persistence.EFCore;

namespace Geometry.Infrastructure.Tests;

public class CylinderDBOTests
{
    [Fact]
    public void Id_ShouldBeSetAndRetrieved()
    {
        // Arrange
        var dbo = new CylinderDBO();
        var id = Guid.NewGuid();

        // Act
        dbo.Id = id;

        // Assert
        Assert.Equal(id, dbo.Id);
    }

    [Fact]
    public void Radius_ShouldBeSetAndRetrieved()
    {
        // Arrange
        var dbo = new CylinderDBO();
        var radius = 5;

        // Act
        dbo.Radius = radius;

        // Assert
        Assert.Equal(radius, dbo.Radius);
    }

    [Fact]
    public void Height_ShouldBeSetAndRetrieved()
    {
        // Arrange
        var dbo = new CylinderDBO();
        var height = 12;

        // Act
        dbo.Height = height;

        // Assert
        Assert.Equal(height, dbo.Height);
    }

    [Fact]
    public void Constructor_ShouldCreateInstanceWithDefaultValues()
    {
        // Arrange & Act
        var dbo = new CylinderDBO();

        // Assert
        Assert.NotNull(dbo);
        Assert.Equal(Guid.Empty, dbo.Id);
        Assert.Equal(0, dbo.Radius);
        Assert.Equal(0, dbo.Height);
    }

    [Fact]
    public void Properties_ShouldBeMutable()
    {
        // Arrange
        var dbo = new CylinderDBO()
        {
            Id = Guid.NewGuid(),
            Radius = 5,
            Height = 10
        };

        // Act
        var newId = Guid.NewGuid();
        dbo.Id = newId;
        dbo.Radius = 15;
        dbo.Height = 30;

        // Assert
        Assert.Equal(newId, dbo.Id);
        Assert.Equal(15, dbo.Radius);
        Assert.Equal(30, dbo.Height);
    }
}
