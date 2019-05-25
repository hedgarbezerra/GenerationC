using GenerationC.Database.models;
using GenerationC.Entity;
using System.Data.Entity;
using System.Data.SqlClient;

namespace GenerationC.Database
{
    public class GenerationCDbContextC : DbContext
    {
       

        //Classes virando tabela
        public  DbSet<User> Users { get; set; }
        public  DbSet<Device> Devices { get; set; }

        public GenerationCDbContextC()
        {
            Configuration.LazyLoadingEnabled = false;
            Configuration.ProxyCreationEnabled = false;
        }

        //Relacionamento das classes
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new GenerationCDbDeviceConfiguration());

            modelBuilder.Configurations.Add(new GenerationCDbUserConfiguration());
        }
    }
}