using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DiplomskiPokusaj1.Model;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace DiplomskiPokusaj1.Repository
{
    public class DBContext : IdentityDbContext<User>
    {
        public DbSet <Address> Addreses { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Library> Libraries { get; set; }
        public DbSet<Material> Materials { get; set; }
        public DbSet<MaterialCopy> MaterialCopies { get; set; }
        public DbSet<Publisher> Publishers { get; set; }
        public DbSet<Rent> Rents { get; set; }
        public DbSet<Reservation> Reservations { get; set; }

        public DBContext ([NotNullAttribute] DbContextOptions options) : base (options)
        {

        }
    }
}
