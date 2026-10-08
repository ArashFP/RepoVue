using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Testcontainers.PostgreSql;

namespace RepoVue.Api.Tests;

/// <summary>
/// Runs the real API in memory against a throwaway PostgreSQL container.
/// One container per test class (xUnit class fixture); each class gets a clean database.
/// </summary>
public class RepoVueApiFactory : WebApplicationFactory<Program>, IAsyncLifetime
{
    private readonly PostgreSqlContainer _db = new PostgreSqlBuilder("postgres:17").Build();

    public async Task InitializeAsync()
    {
        await _db.StartAsync();
        _ = Server;
    }

    async Task IAsyncLifetime.DisposeAsync()
    {
        await base.DisposeAsync();
        await _db.DisposeAsync();
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("Development");
        builder.UseSetting("ConnectionStrings:RepoVue", _db.GetConnectionString());
    }
}
