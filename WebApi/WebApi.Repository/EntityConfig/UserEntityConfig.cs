using System.Data.Entity.ModelConfiguration;
using WebApi.Domain.Entities;

namespace WebApi.Repository.Entities.EntityConfig
{
    public class UserEntityConfig : EntityTypeConfiguration<User>
    {

        public UserEntityConfig()
        {
            HasKey(c => c.UserID);

            Property(c => c.FirstName)
                .IsRequired()
                .HasMaxLength(150);

            Property(c => c.LastName)
                .IsRequired()
                .HasMaxLength(150);

            Property(c => c.Email)
               .IsRequired();
        }
    }
}