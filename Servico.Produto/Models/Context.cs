using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Servico.Produto.Models
{
    public class Context : DbContext
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //var booking = modelBuilder.Entity<Booking>();

            //Setando duas keys na tabela de Status, Currency, Meal
            //modelBuilder.Entity<Status>().HasKey(c => new { c.idStatus, c.language });
            //modelBuilder.Entity<Currency>().HasKey(c => new { c.idCurrency, c.language });
            //modelBuilder.Entity<Meal>().HasKey(c => new { c.idMeal, c.language });

            base.OnModelCreating(modelBuilder);
        }

        public Context(DbContextOptions<Context> options) : base(options)
        {

        }

        public DbSet<Produto> Produto { get; set; }

        public static class ContextFactory
        {
            public static Context Create(string connectionString)
            {
                var optionsBuilder = new DbContextOptionsBuilder<Context>();
                optionsBuilder.UseMySql(connectionString);

                //Ensure database creation
                var context = new Context(optionsBuilder.Options);
                context.Database.EnsureCreated();

                return context;
            }
        }
    }
}
