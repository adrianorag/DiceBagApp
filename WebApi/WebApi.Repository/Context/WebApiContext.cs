

namespace WebApi.Repository.Entities.Context
{

    using System;
    using System.Data.Entity.ModelConfiguration.Conventions;
    using System.Data.Entity;
    using WebApi.Repository.Entities.EntityConfig;
    using WebApi.Domain.Entities;

    public partial class WebApiContext : DbContext
    {
        public WebApiContext()
            : base("name=WebApiContext")
        {
        }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();


            modelBuilder.Properties().Where(p => p.Name == p.ReflectedType.Name + "Id")
                .Configure(p => p.IsKey());

            modelBuilder.Properties<string>()
                .Configure(p => p.HasColumnType("varchar"));

            modelBuilder.Properties<string>()
                .Configure(p => p.HasMaxLength(100));

            modelBuilder.Configurations.Add(new UserEntityConfig());
        }

        public override int SaveChanges()
        {
            foreach (var entry in ChangeTracker.Entries())
            {
                if (entry.Entity.GetType().GetProperty("DateRegister") != null)
                {

                    if (entry.State == EntityState.Added)
                    {
                        entry.Property("DateRegister").CurrentValue = DateTime.Now;
                    }

                    if (entry.State == EntityState.Modified)
                    {
                        entry.Property("DateRegister").IsModified = false;
                    }
                }

                if (entry.Entity.GetType().GetProperty("DateRegisterLastUpdate") != null)
                {
                    entry.Property("DateRegisterLastUpdate").CurrentValue = DateTime.Now;
                }

            }
            return base.SaveChanges();
        }
    }
}
