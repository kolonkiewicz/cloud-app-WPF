using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cloud.MVVM.Model
{
    public class CloudFileContext : DbContext
    {
        public DbSet<CloudFile> CloudFiles { get; set; }
        public DbSet<User> Users { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Data source=cloud.sqlite");
        }
    }
}
