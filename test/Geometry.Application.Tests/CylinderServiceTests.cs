using Geometry.Domain.CylinderModel;

namespace Geometry.Application.Tests;

/// <summary>
/// Mock implementation of ICylinderRepository for testing purposes.
/// </summary>
public class MockCylinderRepository : ICylinderRepository
{
    private readonly Dictionary<Guid, Cylinder> _cylinders = new();
    public int ReadByIdCallCount { get; private set; }
    public int InsertCallCount { get; private set; }
    public Guid? LastReadByIdParameter { get; private set; }
    public Cylinder? LastInsertParameter { get; private set; }

    public Task<Cylinder?> ReadById(Guid id)
    {
        ReadByIdCallCount++;
        LastReadByIdParameter = id;
        _cylinders.TryGetValue(id, out var cylinder);
        return Task.FromResult<Cylinder?>(cylinder);
    }

    public Task<Guid> Insert(Cylinder cylinder)
    {
        InsertCallCount++;
        LastInsertParameter = cylinder;

        if (cylinder != null)
        {
            _cylinders[cylinder.Id] = cylinder;
            return Task.FromResult(cylinder.Id);
        }

        throw new ArgumentNullException(nameof(cylinder));
    }

    public void Reset()
    {
        _cylinders.Clear();
        ReadByIdCallCount = 0;
        InsertCallCount = 0;
        LastReadByIdParameter = null;
        LastInsertParameter = null;
    }
}

public class CylinderServiceTests
{
    [Fact]
    public void Constructor_WithNullRepository_ShouldCreateInstance()
    {
        // Arrange
        ICylinderRepository repository = null!;

        // Act
        var service = new CylinderService(repository);

        // Assert
        Assert.NotNull(service);
    }

    [Fact]
    public async Task Insert_WithNullRepository_ShouldThrowNullReferenceException()
    {
        // Arrange
        ICylinderRepository repository = null!;
        var service = new CylinderService(repository);
        var cylinder = new Cylinder(Guid.NewGuid(), 3, 10);

        // Act & Assert
        await Assert.ThrowsAsync<NullReferenceException>(() => service.Insert(cylinder));
    }

    [Fact]
    public async Task ReadById_WithNullRepository_ShouldThrowNullReferenceException()
    {
        // Arrange
        ICylinderRepository repository = null!;
        var service = new CylinderService(repository);
        var id = Guid.NewGuid();

        // Act & Assert
        await Assert.ThrowsAsync<NullReferenceException>(() => service.ReadById(id));
    }

    [Fact]
    public void Constructor_WithValidRepository_ShouldCreateInstance()
    {
        // Arrange
        var repository = new MockCylinderRepository();

        // Act
        var service = new CylinderService(repository);

        // Assert
        Assert.NotNull(service);
    }

    [Fact]
    public async Task Insert_WithValidCylinder_ShouldDelegateToRepository()
    {
        // Arrange
        var repository = new MockCylinderRepository();
        var service = new CylinderService(repository);
        var id = Guid.NewGuid();
        var cylinder = new Cylinder(id, 3, 10);

        // Act
        var result = await service.Insert(cylinder);

        // Assert
        Assert.Equal(id, result);
        Assert.Equal(1, repository.InsertCallCount);
        Assert.Equal(cylinder, repository.LastInsertParameter);
    }

    [Fact]
    public async Task Insert_WithValidCylinder_ShouldReturnCylinderId()
    {
        // Arrange
        var repository = new MockCylinderRepository();
        var service = new CylinderService(repository);
        var id = Guid.NewGuid();
        var cylinder = new Cylinder(id, 5, 20);

        // Act
        var result = await service.Insert(cylinder);

        // Assert
        Assert.Equal(id, result);
    }

    [Fact]
    public async Task Insert_WithNullCylinder_ShouldPropagateException()
    {
        // Arrange
        var repository = new MockCylinderRepository();
        var service = new CylinderService(repository);
        Cylinder cylinder = null!;

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentNullException>(() => service.Insert(cylinder));
    }

    [Fact]
    public async Task ReadById_WithExistingCylinder_ShouldDelegateToRepository()
    {
        // Arrange
        var repository = new MockCylinderRepository();
        var service = new CylinderService(repository);
        var id = Guid.NewGuid();
        var cylinder = new Cylinder(id, 4, 12);

        await repository.Insert(cylinder);

        // Act
        var result = await service.ReadById(id);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(id, result.Id);
        Assert.Equal(4, result.Radius);
        Assert.Equal(12, result.Height);
        Assert.Equal(1, repository.ReadByIdCallCount);
        Assert.Equal(id, repository.LastReadByIdParameter);
    }

    [Fact]
    public async Task ReadById_WithNonExistentId_ShouldReturnNull()
    {
        // Arrange
        var repository = new MockCylinderRepository();
        var service = new CylinderService(repository);
        var nonExistentId = Guid.NewGuid();

        // Act
        var result = await service.ReadById(nonExistentId);

        // Assert
        Assert.Null(result);
        Assert.Equal(1, repository.ReadByIdCallCount);
        Assert.Equal(nonExistentId, repository.LastReadByIdParameter);
    }

    [Fact]
    public async Task Insert_ThenReadById_ShouldReturnInsertedCylinder()
    {
        // Arrange
        var repository = new MockCylinderRepository();
        var service = new CylinderService(repository);
        var id = Guid.NewGuid();
        var cylinder = new Cylinder(id, 7, 30);

        await service.Insert(cylinder);
        var result = await service.ReadById(id);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(id, result.Id);
        Assert.Equal(7, result.Radius);
        Assert.Equal(30, result.Height);
    }

    [Fact]
    public async Task MultipleInserts_ShouldDelegateToRepositoryEachTime()
    {
        // Arrange
        var repository = new MockCylinderRepository();
        var service = new CylinderService(repository);
        var c1 = new Cylinder(Guid.NewGuid(), 2, 5);
        var c2 = new Cylinder(Guid.NewGuid(), 4, 10);
        var c3 = new Cylinder(Guid.NewGuid(), 6, 15);

        await service.Insert(c1);
        await service.Insert(c2);
        await service.Insert(c3);

        // Assert
        Assert.Equal(3, repository.InsertCallCount);
        Assert.Equal(c3, repository.LastInsertParameter);
    }

    [Fact]
    public async Task MultipleReads_ShouldDelegateToRepositoryEachTime()
    {
        // Arrange
        var repository = new MockCylinderRepository();
        var service = new CylinderService(repository);
        var id1 = Guid.NewGuid();
        var id2 = Guid.NewGuid();
        var c1 = new Cylinder(id1, 2, 10);
        var c2 = new Cylinder(id2, 5, 20);

        await repository.Insert(c1);
        await repository.Insert(c2);

        var result1 = await service.ReadById(id1);
        var result2 = await service.ReadById(id2);
        var result3 = await service.ReadById(Guid.NewGuid());

        // Assert
        Assert.NotNull(result1);
        Assert.NotNull(result2);
        Assert.Null(result3);
        Assert.Equal(3, repository.ReadByIdCallCount);
    }

    [Fact]
    public async Task Insert_WithDifferentDimensions_ShouldWorkCorrectly()
    {
        // Arrange
        var repository = new MockCylinderRepository();
        var service = new CylinderService(repository);

        var dimensions = new (int Radius, int Height)[]
        {
            (1, 5),
            (3, 10),
            (5, 20),
            (7, 30),
            (10, 50)
        };

        foreach (var d in dimensions)
        {
            var id = Guid.NewGuid();
            var cylinder = new Cylinder(id, d.Radius, d.Height);

            var result = await service.Insert(cylinder);

            Assert.Equal(id, result);
            var retrieved = await service.ReadById(id);
            Assert.NotNull(retrieved);
            Assert.Equal(d.Radius, retrieved.Radius);
            Assert.Equal(d.Height, retrieved.Height);
        }
    }

    [Fact]
    public async Task Service_ShouldMaintainRepositoryReference()
    {
        // Arrange
        var repository = new MockCylinderRepository();
        var service = new CylinderService(repository);
        var cylinder = new Cylinder(Guid.NewGuid(), 3, 10);

        await service.Insert(cylinder);
        var retrieved = await service.ReadById(cylinder.Id);

        // Assert
        Assert.NotNull(retrieved);
        Assert.Equal(1, repository.InsertCallCount);
        Assert.Equal(1, repository.ReadByIdCallCount);
    }
}
