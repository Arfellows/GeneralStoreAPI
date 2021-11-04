﻿using GeneralStore.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralStore.Data
{
    class GeneralStoreDbContext : DbContext
    {
        public GeneralStoreDbContext() : base("DefaultConnection")
        {

        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
    }
}
