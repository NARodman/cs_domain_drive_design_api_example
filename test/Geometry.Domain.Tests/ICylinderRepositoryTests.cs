using Geometry.Domain.CylinderModel;

namespace Geometry.Domain.Tests;

/// <summary>
/// In-memory implementation of ICylinderRepository for testing.
/// </summary>
public class InMemoryCylinderRepository : ICylinderRepository
{
    private readonly Dictionary<Guid, Cylinder> _cylinders = new();

    public Task<Cylinder?> ReadById(Guid id)
    {
        _cylinders.TryGetValue(id, out var cylinder);
        return Task.FromResult<Cylinder?>(cylinder);
    }

    public Task<Guid> Insert(Cylinder cylinder)
    {
        if (cylinder == null)
        {
            throw new ArgumentNullException(nameof(cylinder));
        }

        _cylinders[cylinder.Id] = cylinder;
        return Task.FromResult(cylinder.Id);
    }

    public void Clear()
    {
        _cylinders.Clear();
    }

    public int Count => _cylinders.Count;
}

public class ICylinderRepositoryTests
{
    [Fact]
    public async Task ReadById_WithExistingCylinder_ShouldReturnCylinder()
    {
        // Arrange
        var repository = new InMemoryCylinderRepository();
        var id = Guid.NewGuid();
        var cylinder = new Cylinder(id, 3, 10);
        await repository.Insert(cylinder);

        // Act
        var result = await repository.ReadById(id);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(id, result.Id);
        Assert.Equal(3, result.Radius);
        Assert.Equal(10, result.Height);
    }

    [Fact]
    public async Task ReadById_WithNonExistentId_ShouldReturnNull()
    {
        // Arrange
        var repository = new InMemoryCylinderRepository();
        var nonExistentId = Guid.NewGuid();

        // Act
        var result = await repository.ReadById(nonExistentId);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task Insert_WithValidCylinder_ShouldReturnCylinderId()
    {
        // Arrange
        var repository = new InMemoryCylinderRepository();
        var id = Guid.NewGuid();
        var cylinder = new Cylinder(id, 5, 20);

        // Act
        var result = await repository.Insert(cylinder);

        // Assert
        Assert.Equal(id, result);
    }

    [Fact]
    public async Task Insert_WithValidCylinder_ShouldStoreCylinder()
    {
        // Arrange
        var repository = new InMemoryCylinderRepository();
        var id = Guid.NewGuid();
        var cylinder = new Cylinder(id, 5, 20);

        // Act
        await repository.Insert(cylinder);

        // Assert
        var retrieved = await repository.ReadById(id);
        Assert.NotNull(retrieved);
        Assert.Equal(5, retrieved.Radius);
        Assert.Equal(20, retrieved.Height);
    }

    [Fact]
    public async Task Insert_WithNullCylinder_ShouldThrowArgumentNullException()
    {
        // Arrange
        var repository = new InMemoryCylinderRepository();

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentNullException>(() => repository.Insert(null!));
    }

    [Fact]
    public async Task Insert_WithSameId_ShouldUpdateExistingCylinder()
    {
        // Arrange
        var repository = new InMemoryCylinderRepository();
        var id = Guid.NewGuid();
        var cylinder1 = new Cylinder(id, 3, 10);
        var cylinder2 = new Cylinder(id, 6, 25);

        // Act
        await repository.Insert(cylinder1);
        await repository.Insert(cylinder2);

        // Assert
        var result = await repository.ReadById(id);
        Assert.NotNull(result);
        Assert.Equal(6, result.Radius);
        Assert.Equal(25, result.Height);
    }

    [Fact]
    public async Task ReadById_WithEmptyRepository_ShouldReturnNull()
    {
        // Arrange
        var repository = new InMemoryCylinderRepository();

        // Act
        var result = await repository.ReadById(Guid.NewGuid());

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task Insert_MultipleCylinders_ShouldStoreAll()
    {
        // Arrange
        var repository = new InMemoryCylinderRepository();
        var id1 = Guid.NewGuid();
        var id2 = Guid.NewGuid();
        var c1 = new Cylinder(id1, 3, 8);
        var c2 = new Cylinder(id2, 6, 15);

        // Act
        await repository.Insert(c1);
        await repository.Insert(c2);

        // Assert
        var r1 = await repository.ReadById(id1);
        var r2 = await repository.ReadById(id2);

        Assert.NotNull(r1);
        Assert.NotNull(r2);
        Assert.Equal(3, r1.Radius);
        Assert.Equal(8, r1.Height);
        Assert.Equal(6, r2.Radius);
        Assert.Equal(15, r2.Height);
    }
}
