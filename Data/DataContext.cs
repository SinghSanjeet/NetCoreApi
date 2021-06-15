using Microsoft.EntityFrameworkCore;
using NetCoreApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreApi.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<Characters> Characters { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
