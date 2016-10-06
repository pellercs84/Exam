using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SqlProviderServices = System.Data.Entity.SqlServer.SqlProviderServices;

namespace Exam.DataAccessLayer
{
    internal class DataContext:DbContext
    {
        public DbSet<Manufacturer> Manufacturers { get; set; }
        public DbSet<Product> Products { get; set; }
    }
}
