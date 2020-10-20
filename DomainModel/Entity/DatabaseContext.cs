using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DomainModel.Entity
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) {}       
        public DbSet<User> Users { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<Section> Sections { get; set; }
        public DbSet<Thread> Threads { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //base.OnModelCreating(modelBuilder);
            //User Mod:
            modelBuilder.Entity<User>().HasIndex(u => u.UserName).IsUnique();
            //Comment Mod:
            modelBuilder.Entity<Comment>().HasKey(c => new { c.Id});
            modelBuilder.Entity<Comment>().HasOne(c => c.User).WithMany(u => u.Comments).HasForeignKey(c => c.UserId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Comment>().HasOne(c => c.Post).WithMany(p => p.Comments).HasForeignKey(c => c.PostId).OnDelete(DeleteBehavior.Restrict);
            //Data seed for test
            modelBuilder.Entity<UserRole>().HasData(
                new UserRole { Id = 1, RoleName = "Admin" },
                new UserRole { Id = 2, RoleName = "User" });
            modelBuilder.Entity<User>().HasData(
                new User() { Id = 1, UserName = "Admin", Password = "Admin", Email = "Admin@gmail.com", FirstName = "Hien", LastName = "Nguyen Minh", UserRoleId = 1 },
                new User() { Id = 2, UserName = "User1", Password = "User1", Email = "User1@gmail.com", FirstName = "Hien", LastName = "Nguyen Minh", UserRoleId = 2 });
            modelBuilder.Entity<Section>().HasData(
                new Section { Id = 1, SectionName = "Section 1" },
                new Section { Id = 2, SectionName = "Section 2" });
            modelBuilder.Entity<Thread>().HasData(
                new Thread() { Id = 1, ThreadName = "Thread 1", SectionId = 1 },
                new Thread() { Id = 2, ThreadName = "Thread 2", SectionId = 2 });
        }
    }
}
