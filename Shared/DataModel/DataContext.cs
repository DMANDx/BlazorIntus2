using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using static BlazorIntus2.Shared.DataModel.Data;

namespace BlazorIntus2.Shared.DataModel
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
        }		
		
        public DbSet<Order> Order { get; set; }
        public DbSet<OrderWindow> OrderWindow { get; set; }
        public DbSet<Window> Windows { get; set; }
        public DbSet<SubElement> SubElements { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>().ToTable("Order");
            modelBuilder.Entity<OrderWindow>().ToTable("OrderWindow");
            modelBuilder.Entity<Window>().ToTable("Window");
            modelBuilder.Entity<SubElement>().ToTable("SubElement");

           // modelBuilder.Entity<Window>()
           //.HasMany(w => w.SubElements)
           //.WithOne(se => se.Window)
           //.OnDelete(DeleteBehavior.Cascade);


            //modelBuilder.Entity<Order>().Property(e => e.Id).ValueGeneratedOnAdd();
            //modelBuilder.Entity<Window>().Property(e => e.Id).ValueGeneratedOnAdd();
            //modelBuilder.Entity<SubElement>().Property(e => e.Id).ValueGeneratedOnAdd();            

            //modelBuilder.Entity<Window>().HasData(
            //    new Window { Id = 1, Name = "A51", QuantityOfWindows = 2, TotalSubElements = 5 },
            //    new Window { Id = 2, Name = "GLB", QuantityOfWindows = 1, TotalSubElements = 3 }
            //);            

            //modelBuilder.Entity<Window>().HasKey(w => w.Id);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("DefaultConnection",
                    b => b.MigrationsAssembly("BlazorIntus2.Shared"));
            }
        }
    }
}