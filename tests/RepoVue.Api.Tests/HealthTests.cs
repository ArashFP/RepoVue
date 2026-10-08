using System.Net;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;

namespace RepoVue.Api.Tests;

public class HealthTests(RepoVueApiFactory api) : IClassFixture<RepoVueApiFactory>
{
    [Fact]
    public async Task Health_is_healthy_when_the_database_is_up()
    {
        var response = await api.CreateClient().GetAsync("/health");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.Equal("Healthy", await response.Content.ReadAsStringAsync());
    }

    [Fact]
    public async Task Health_is_unhealthy_when_the_database_cannot_be_reached()
    {
        // Same API, pointed at a port where no database is listening.
        await using var noDatabase = new WebApplicationFactory<Program>().WithWebHostBuilder(builder =>
        {
            builder.UseEnvironment("Development");
            builder.UseSetting("ConnectionStrings:RepoVue",
                "Host=127.0.0.1;Port=1;Database=none;Username=none;Password=none;Timeout=2");
        });

        var response = await noDatabase.CreateClient().GetAsync("/health");

        Assert.Equal(HttpStatusCode.ServiceUnavailable, response.StatusCode);
        Assert.Equal("Unhealthy", await response.Content.ReadAsStringAsync());
    }
}
