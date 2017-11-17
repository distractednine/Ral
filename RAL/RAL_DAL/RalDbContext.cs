using System.Data.Entity;

namespace RAL_DAL
{
    public class RalDbContext : DbContext
    {
        public RalDbContext(string nameOrConnectionString) : base(nameOrConnectionString)
        {
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<RalDbContext>());
            //Database.Initialize(force: true);
        }

        public DbSet<Anime> anime { get; set; }
        public DbSet<Plan> plans { get; set; }
        public DbSet<User> users { get; set; }
        public DbSet<Watcheda> watcheda { get; set; }
    }
}