using Geometry.Domain.CylinderModel;
using Geometry.Infrastructure.Persistence.EFCore;
using Microsoft.EntityFrameworkCore;

namespace Geometry.Infrastructure.Tests;

public class CylinderRepositoryTests
{
    private GeometryDbContext CreateContext()
    {
        var options = new DbContextOptionsBuilder<GeometryDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        return new GeometryDbContext(options);
    }

    [Fact]
    public void Constructor_WithNullContext_ShouldThrowArgumentNullException()
    {
        GeometryDbContext context = null!;
        Assert.Throws<ArgumentNullException>(() => new CylinderRepository(context));
    }

    [Fact]
    public void Constructor_WithValidContext_ShouldCreateInstance()
    {
        using var context = CreateContext();
        var repository = new CylinderRepository(context);
        Assert.NotNull(repository);
    }

    [Fact]
    public async Task ReadById_WithExistingCylinder_ShouldReturnCylinder()
    {
        using var context = CreateContext();
        var repository = new CylinderRepository(context);
        var id = Guid.NewGuid();
        var cylinder = new Cylinder(id, 5, 10);

        await repository.Insert(cylinder);

        var result = await repository.ReadById(id);

        Assert.NotNull(result);
        Assert.Equal(id, result.Id);
        Assert.Equal(5, result.Radius);
        Assert.Equal(10, result.Height);
    }

    [Fact]
    public async Task ReadById_WithNonExistentId_ShouldReturnNull()
    {
        using var context = CreateContext();
        var repository = new CylinderRepository(context);
        var result = await repository.ReadById(Guid.NewGuid());
        Assert.Null(result);
    }

    [Fact]
    public async Task Insert_WithNewCylinder_ShouldSaveAndReturnId()
    {
        using var context = CreateContext();
        var repository = new CylinderRepository(context);
        var id = Guid.NewGuid();
        var cylinder = new Cylinder(id, 7, 14);

        var result = await repository.Insert(cylinder);

        Assert.Equal(id, result);

        var retrieved = await repository.ReadById(id);
        Assert.NotNull(retrieved);
        Assert.Equal(7, retrieved.Radius);
        Assert.Equal(14, retrieved.Height);
    }

    [Fact]
    public async Task Insert_WithNullCylinder_ShouldThrowArgumentNullException()
    {
        using var context = CreateContext();
        var repository = new CylinderRepository(context);
        Cylinder cylinder = null!;
        await Assert.ThrowsAsync<ArgumentNullException>(() => repository.Insert(cylinder));
    }

    [Fact]
    public async Task Insert_WithExistingId_ShouldUpdateExistingCylinder()
    {
        using var context = CreateContext();
        var repository = new CylinderRepository(context);
        var id = Guid.NewGuid();
        var cylinder1 = new Cylinder(id, 3, 6);
        var cylinder2 = new Cylinder(id, 8, 16);

        await repository.Insert(cylinder1);
        await repository.Insert(cylinder2);

        var retrieved = await repository.ReadById(id);
        Assert.NotNull(retrieved);
        Assert.Equal(8, retrieved.Radius);
        Assert.Equal(16, retrieved.Height);
    }

    [Fact]
    public async Task Insert_MultipleCylinders_ShouldSaveAll()
    {
        using var context = CreateContext();
        var repository = new CylinderRepository(context);

        var c1 = new Cylinder(Guid.NewGuid(), 2, 4);
        var c2 = new Cylinder(Guid.NewGuid(), 5, 10);

        await repository.Insert(c1);
        await repository.Insert(c2);

        var r1 = await repository.ReadById(c1.Id);
        var r2 = await repository.ReadById(c2.Id);

        Assert.NotNull(r1);
        Assert.NotNull(r2);
        Assert.Equal(2, r1.Radius);
        Assert.Equal(10, r2.Height);
    }
}
