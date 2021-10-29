using EFCoreAttachBugTest002.ModelConfigurations;
using EFCoreAttachBugTest002.Models;
using Microsoft.EntityFrameworkCore;

namespace EFCoreAttachBugTest002
{
    public class TestDbContext : DbContext
    {
        // Migration created with dotnet ef migrations add TestDatabase-v1.0.0 --project EFCoreAttachBugTest002

        public virtual DbSet<Holder> Holders { get; set; }
        public virtual DbSet<Example> Examples { get; set; }
        public virtual DbSet<MysteryType> MysteryTypes { get; set; }

        public TestDbContext(DbContextOptions<TestDbContext> options)
            :base(options)
        { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlite("Data Source=TestDatabase.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration<Holder>(new HolderConfiguration());
            modelBuilder.ApplyConfiguration<MysteryType>(new MysteryTypeConfiguration());
            modelBuilder.ApplyConfiguration<Example>(new ExampleConfiguration());
        }
    }
}
