using Geometry.Infrastructure.Persistence.EFCore;
using Microsoft.EntityFrameworkCore;

namespace Geometry.Infrastructure.Tests;

public class CylinderDbContextTests
{
    private GeometryDbContext CreateContext()
    {
        var options = new DbContextOptionsBuilder<GeometryDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        return new GeometryDbContext(options);
    }

    [Fact]
    public void Constructor_WithOptions_ShouldCreateInstance()
    {
        var options = new DbContextOptionsBuilder<GeometryDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        using var context = new GeometryDbContext(options);
        Assert.NotNull(context);
    }

    [Fact]
    public void Cylinders_ShouldBeInitialized()
    {
        using var context = CreateContext();
        Assert.NotNull(context.Cylinders);
    }

    [Fact]
    public async Task Cylinders_ShouldAllowAddingEntities()
    {
        using var context = CreateContext();
        var dbo = new CylinderDBO { Id = Guid.NewGuid(), Radius = 5, Height = 10 };
        await context.Cylinders.AddAsync(dbo);
        await context.SaveChangesAsync();

        var count = await context.Cylinders.CountAsync();
        Assert.Equal(1, count);
    }

    [Fact]
    public async Task Cylinders_ShouldAllowQueryingEntities()
    {
        using var context = CreateContext();
        var id = Guid.NewGuid();
        var dbo = new CylinderDBO { Id = id, Radius = 4, Height = 8 };
        await context.Cylinders.AddAsync(dbo);
        await context.SaveChangesAsync();

        var retrieved = await context.Cylinders.FirstOrDefaultAsync(c => c.Id == id);
        Assert.NotNull(retrieved);
        Assert.Equal(4, retrieved.Radius);
        Assert.Equal(8, retrieved.Height);
    }

    [Fact]
    public async Task ModelConfiguration_IdShouldBePrimaryKey()
    {
        using var context = CreateContext();
        var dbo = new CylinderDBO { Id = Guid.NewGuid(), Radius = 3, Height = 6 };
        await context.Cylinders.AddAsync(dbo);
        await context.SaveChangesAsync();

        var retrieved = await context.Cylinders.FindAsync(dbo.Id);
        Assert.NotNull(retrieved);
    }

    [Fact]
    public async Task Database_ShouldSupportMultipleEntities()
    {
        using var context = CreateContext();
        var c1 = new CylinderDBO { Id = Guid.NewGuid(), Radius = 1, Height = 2 };
        var c2 = new CylinderDBO { Id = Guid.NewGuid(), Radius = 5, Height = 10 };
        var c3 = new CylinderDBO { Id = Guid.NewGuid(), Radius = 8, Height = 16 };

        await context.Cylinders.AddRangeAsync(c1, c2, c3);
        await context.SaveChangesAsync();

        var count = await context.Cylinders.CountAsync();
        Assert.Equal(3, count);
    }

    [Fact]
    public async Task Database_ShouldSupportUpdatingEntities()
    {
        using var context = CreateContext();
        var id = Guid.NewGuid();
        var dbo = new CylinderDBO { Id = id, Radius = 3, Height = 6 };
        await context.Cylinders.AddAsync(dbo);
        await context.SaveChangesAsync();

        var retrieved = await context.Cylinders.FindAsync(id);
        retrieved!.Radius = 7;
        retrieved.Height = 14;
        context.Cylinders.Update(retrieved);
        await context.SaveChangesAsync();

        var updated = await context.Cylinders.FindAsync(id);
        Assert.Equal(7, updated!.Radius);
        Assert.Equal(14, updated.Height);
    }

    [Fact]
    public async Task Database_ShouldSupportDeletingEntities()
    {
        using var context = CreateContext();
        var dbo = new CylinderDBO { Id = Guid.NewGuid(), Radius = 2, Height = 4 };
        await context.Cylinders.AddAsync(dbo);
        await context.SaveChangesAsync();

        context.Cylinders.Remove(dbo);
        await context.SaveChangesAsync();

        var count = await context.Cylinders.CountAsync();
        Assert.Equal(0, count);
    }
}
