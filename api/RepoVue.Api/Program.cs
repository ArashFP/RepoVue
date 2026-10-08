using Microsoft.EntityFrameworkCore;
using RepoVue.Api.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("RepoVue")));

// /health answers "Healthy" only when the API can reach the database.
builder.Services.AddHealthChecks().AddDbContextCheck<AppDbContext>("database");

var app = builder.Build();

app.MapHealthChecks("/health");

app.Run();

// Lets the test project start the API in memory (WebApplicationFactory<Program>).
public partial class Program;
