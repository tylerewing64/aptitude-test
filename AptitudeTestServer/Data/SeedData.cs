using AptitudeTestServer.Models;

namespace AptitudeTestServer.Data;

public class SeedData
{
    public IEnumerable<Quote> Quotes { get; init; }
    public IEnumerable<State> States { get; init; }
}