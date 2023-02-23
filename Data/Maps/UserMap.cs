using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Maps
{
    public class UserMap : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("users");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("idusuario");
            builder.Property(x => x.Email).HasColumnName("email");
            builder.Property(x => x.Password).HasColumnName("senha");
            builder.Property(x => x.Document).HasColumnName("document");
            builder.Property(x => x.Name).HasColumnName("name");
            builder.Property(x => x.Phone).HasColumnName("telefone");

        }
    }
}
