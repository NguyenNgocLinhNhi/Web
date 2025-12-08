using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Lab2.Models;

namespace Lab2.Data
{
    public class Lab2Context : DbContext
    {
        public Lab2Context (DbContextOptions<Lab2Context> options)
            : base(options)
        {
        }

        public DbSet<Lab2.Models.Movie> Movie { get; set; } = default!;

        public DbSet<Customer> Customer { get; set; } = default!;
        public DbSet<Category> Category { get; set; } = default!;
        public DbSet<Ticket> Ticket { get; set; } = default!;
    }
}
