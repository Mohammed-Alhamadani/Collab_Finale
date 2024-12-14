using Collab.Models;
using Microsoft.EntityFrameworkCore;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }
    public DbSet<Group> Groups { get; set; }
    public DbSet<Event> Events { get; set; }
    public DbSet<YouTubeCategory> YouTubeCategories { get; set; }
    public DbSet<YouTubeVideo> YouTubeVideos { get; set; }
}
