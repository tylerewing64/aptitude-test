using System.Text.Json;
using AptitudeTestServer.Models;
using Microsoft.EntityFrameworkCore;

namespace AptitudeTestServer.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Quote> Quotes { get; init; }
    public DbSet<State>States { get; init; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder
            .UseInMemoryDatabase("AptitudeTest")
            .UseSeeding((context, _) =>
            {
                var seedData = ReadSeedData();

                if (seedData?.Quotes is not null)
                    context.Set<Quote>().AddRange(seedData.Quotes);
                
                if(seedData?.States is not null)
                    context.Set<State>().AddRange(seedData.States);

                context.SaveChanges();
            })
            .UseAsyncSeeding(async (context, _, cancellationToken) =>
            {
                var seedData = ReadSeedData();

                if (seedData?.Quotes is not null)
                    await context.Set<Quote>().AddRangeAsync(seedData.Quotes, cancellationToken);
                
                if(seedData?.States is not null)
                    await context.Set<State>().AddRangeAsync(seedData.States, cancellationToken);

                await context.SaveChangesAsync(cancellationToken);
            });

    private SeedData ReadSeedData()
    {
        var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "seed.json");
        var jsonData = File.ReadAllText(filePath);
        var seedData = JsonSerializer.Deserialize<SeedData>(jsonData);

        if (seedData is null)
            throw new Exception("Seed data is null");

        return seedData;
    }
}