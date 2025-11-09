using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace JogoVelha.Api.Data;
public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<GameResult> GameResults => Set<GameResult>();
}