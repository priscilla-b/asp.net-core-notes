using Microsoft.EntityFrameworkCore;
using WebApp.Models;

namespace WebApp.Data
{
    public class ApplicationDbContext : DbContext
     // ApplicationDbContext is inheriting from the base class DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base (options)
        // DbContextOptions<ApplicationDbContext> is named options and passes down the options defined in the
        // ApplicationDbcontext to the base DbContext class
        {

        }
        public DbSet<Category> Categories { get; set; }
        // this creates a Category table with the name Categories based on the
        // model definition in the Category class

    }
}
