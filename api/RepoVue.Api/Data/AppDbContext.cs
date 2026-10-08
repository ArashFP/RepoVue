using Microsoft.EntityFrameworkCore;

namespace RepoVue.Api.Data;

/// <summary>
/// The RepoVue database. Tables (users, installations, repositories, plans, ...) are added here
/// as the features that need them are built.
/// </summary>
public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
}
