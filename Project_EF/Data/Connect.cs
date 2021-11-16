using Microsoft.EntityFrameworkCore;
using Project_EF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project_EF.Data
{
    public class Connect:DbContext
    {
        public Connect(DbContextOptions<Connect> options) : base(options)
        {

        }
        public DbSet<ParentCate> ParentCate { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<User> User { get; set; }

    }
}
