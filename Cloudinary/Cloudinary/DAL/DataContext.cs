using Cloudinary.Models;
using Microsoft.EntityFrameworkCore;

namespace Cloudinary.DAL
{
    public class DataContext:DbContext
    {
        public DataContext(DbContextOptions<DataContext>options):base(options)
        {
            
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Photo> Photos { get; set; }
        public DbSet<City> Cities { get; set; }
    }
}