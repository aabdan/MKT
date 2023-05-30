using Microsoft.EntityFrameworkCore;
using MKT.Website.Models;

namespace MKT.Website.Data
{
    public class ApplicationDbContext :DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) :base(options) 
        {

        }

        public  DbSet<Company> Companies { get; set; }
        public DbSet<Person> Persons { get; set; }

    }
}
