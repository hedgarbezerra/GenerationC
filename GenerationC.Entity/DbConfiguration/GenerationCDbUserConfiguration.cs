using GenerationC.Database.models;
using GenerationC.Entity.DbConfiguration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenerationC.Entity
{
    class GenerationCDbUserConfiguration : DbAbstractConfiguration<User>
    {
        protected override void ConfigurateFields()
        {
            Property(u => u.Id)
                .IsRequired()
                .HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity)
                .HasColumnName("id");

            Property(u => u.Name)
                .IsRequired()
                .HasColumnName("name")
                .HasMaxLength(60);

            Property(u => u.Username)
                .IsRequired()
                .HasColumnName("username")
                .HasMaxLength(15);
            Property(u => u.Password)
                .IsRequired()
                .HasColumnName("password")
                .HasMaxLength(150);

            Property(u => u.Email)
                .IsRequired()
                .HasColumnName("email")
                .HasMaxLength(60);

            Property(u => u.Created_at)
                .IsOptional()
                .HasColumnName("created_at");

        }

        protected override void ConfigurateFK()
        {
            HasMany(u => u.Devices)
           .WithRequired(u => u.User)
           .HasForeignKey(u => u.User_Id)
           .WillCascadeOnDelete(true);
        }

        protected override void ConfiguratePK()
        {
            HasKey(pk => pk.Id);
        }

        protected override void ConfigurateTableName()
        {
            ToTable("users");
        }
    }
}
