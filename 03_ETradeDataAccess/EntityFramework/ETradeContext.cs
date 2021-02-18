using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using AppCore.DataAccess.Configs;
using ETradeEntities.Entities;

namespace ETradeDataAccess.EntityFramework
{
    public class ETradeContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserDetail> UserDetails { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<City> Cities { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConnectionConfig.ConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Cascade olmasını istemediğimiz için kendimiz elle yazdık...

            modelBuilder.Entity<Category>()
                .HasMany(category => category.Products)
                .WithOne(product => product.Category)
                .HasForeignKey(product => product.CategoryId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Role>()
                .HasMany(role => role.Users)
                .WithOne(user => user.Role)
                .HasForeignKey(user => user.RoleId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Country>()
                .HasMany(country => country.UserDetails)
                .WithOne(userDetail => userDetail.Country)
                .HasForeignKey(userDetail => userDetail.CountryId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Country>()
                .HasMany(country => country.Cities)
                .WithOne(city => city.Country)
                .HasForeignKey(city => city.CountryId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<City>()
                .HasMany(city => city.UserDetails)
                .WithOne(userDetail => userDetail.City)
                .HasForeignKey(userDetail => userDetail.CountryId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<UserDetail>()
                .HasOne(userDetail => userDetail.User)
                .WithOne(user => user.UserDetail)
                // 1 to 1 ilişkikerde .HasForeingKey<User, UserDetail gibi> List Of dememiz gerekiyor sonrasında lambda expression kullanabiliyoruz...
                .HasForeignKey<User>(user=> user.UserDetailId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<UserDetail>()
                .HasIndex(userDetail => userDetail.Email)
                .IsUnique();

            modelBuilder.Entity<User>()
                .HasIndex(user => user.UserName)
                .IsUnique();

            modelBuilder.Entity<Role>()
                .HasIndex(role => role.RoleName )
                .IsUnique();
        }
    }
}