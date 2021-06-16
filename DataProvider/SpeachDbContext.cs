using Microsoft.EntityFrameworkCore;

namespace DataProvider
{
    [Keyless]
    public class Speach
    {
        public string Content { get; set; }

        public string Party { get; set; }
    }

    public class SpeachDbContext : DbContext
    {
        public DbSet<Speach> Speaches { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=SpeachDb2");
    }
}
