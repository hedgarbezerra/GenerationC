using GenerationC.Database.models;
using GenerationC.Entity.DbConfiguration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenerationC.Entity
{
    class GenerationCDbDeviceConfiguration : DbAbstractConfiguration<Device>
    {
        protected override void ConfigurateFields()
        {
            Property(d => d.Id)
                .IsRequired()
                .HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity)
                .HasColumnName("id");

            Property(d => d.Name)
                .IsRequired()
                .HasColumnName("name")
                .HasMaxLength(20);

            Property(d => d.Type)
                .IsRequired()
                .HasColumnName("type")
                .HasMaxLength(15);

            Property(d => d.Gateway)
                .IsRequired()
                .HasColumnName("gateway")
                .HasMaxLength(20);


            Property(d => d.Created_at)
                .IsOptional() 
                .HasColumnName("created_at");

        }

        protected override void ConfigurateFK()
        {
           
        }

        protected override void ConfiguratePK()
        {
            HasKey(pk => pk.Id);
        }

        protected override void ConfigurateTableName()
        {
            ToTable("devices");
        }
    }
}
