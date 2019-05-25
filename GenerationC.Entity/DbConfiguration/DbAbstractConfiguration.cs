using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenerationC.Entity.DbConfiguration
{
    public abstract class DbAbstractConfiguration<Table> :EntityTypeConfiguration<Table>
        where Table : class
    {
        public DbAbstractConfiguration()
        {
            ConfigurateTableName();
            ConfigurateFields();
            ConfiguratePK();
            ConfigurateFK();
        }

        protected abstract void ConfigurateFK();

        protected abstract void ConfiguratePK();

        protected abstract void ConfigurateFields();

        protected abstract void ConfigurateTableName();
        
    }
}
