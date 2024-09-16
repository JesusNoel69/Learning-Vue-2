 using Learning_Vue_2.Models;
using Microsoft.EntityFrameworkCore;


namespace  Learning_Vue_2.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet<Users> Users { get; set; } 
        public DbSet<Tasks> Tasks { get; set; } 
    }
}
