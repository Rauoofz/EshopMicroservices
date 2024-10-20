namespace Catalog.API.SeedData;

public class CatalogSeedData : IInitialData
{
    public async Task Populate(IDocumentStore store, CancellationToken cancellation)
    {
        using var session = store.LightweightSession();

        if (await session.Query<Product>().AnyAsync())
            return;

        session.Store<Product>(GetProductSeedData());
        await session.SaveChangesAsync();
    }

    public static IEnumerable<Product> GetProductSeedData() => new List<Product>()
    {
        new Product
        {
            Id = Guid.NewGuid(),
            Name = "Iphone xs",
            Description = "Smart phone",
            ImageFile = "Iphone.png",
            Category = new List<string> { "Smart phone" },
            Price = 900
        },
        new Product
        {
            Id = Guid.NewGuid(),
            Name = "Samsung note",
            Description = "Smart phone",
            ImageFile = "Samsung.png",
            Category = new List<string> { "Smart phone" },
            Price = 700
        }
    };

}

