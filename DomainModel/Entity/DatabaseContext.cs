using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Text;
using Utility;

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
            base.OnModelCreating(modelBuilder);
            //User Mod:
            modelBuilder.Entity<User>().HasIndex(u => u.UserName).IsUnique();
            modelBuilder.Entity<User>().Property(u => u.AvatarImage).HasDefaultValue(ImageConverter.ImageToByteArray("wwwroot/Images/Avatar/default.png"));
            //modelBuilder.Entity<User>().HasOne(u => u.UserRole).WithMany(r => r.Users).HasForeignKey(u => u.UserRoleId).OnDelete(DeleteBehavior.Restrict);
            //Thread Mod:
           // modelBuilder.Entity<Thread>().HasOne(t => t.Section).WithMany(s => s.Threads).HasForeignKey(t=>t.SectionId).OnDelete(DeleteBehavior.Restrict);
            //Post Mod:
            modelBuilder.Entity<Post>().Property("IsClosed").HasDefaultValue(false);
            //modelBuilder.Entity<Post>().HasOne(p => p.Thread).WithMany(t => t.Posts).HasForeignKey(p=>p.ThreadId).OnDelete(DeleteBehavior.Restrict);
            //Comment Mod:
            //modelBuilder.Entity<Comment>().HasKey(c => new { c.UserId,c.PostId});
            //modelBuilder.Entity<Comment>().HasKey(c => new { c.Id });
            modelBuilder.Entity<Comment>().HasOne(c => c.User).WithMany(u => u.Comments).HasForeignKey(c => c.UserId).OnDelete(DeleteBehavior.Restrict);
            //modelBuilder.Entity<Comment>().HasOne(c => c.Post).WithMany(p => p.Comments).HasForeignKey(c => c.PostId);//.OnDelete(DeleteBehavior.Restrict);
            //Data seed for test
            modelBuilder.Entity<UserRole>().HasData(
                new UserRole { Id = 1, RoleName = "Admin" },
                new UserRole { Id = 2, RoleName = "User" });
            modelBuilder.Entity<User>().HasData(
                new User() { Id = 1, UserName = "Admin", Password = "Admin", Email = "Admin@gmail.com", FirstName = "Hien", LastName = "Nguyen Minh", UserRoleId = 1 },
                new User() { Id = 2, UserName = "User1", Password = "User1", Email = "User1@gmail.com", FirstName = "Hien", LastName = "Nguyen Minh", UserRoleId = 2 });
            modelBuilder.Entity<Section>().HasData(
                new Section { Id = 1, SectionName = "Section 1" },
                new Section { Id = 2, SectionName = "Section 2" },
                new Section { Id = 3, SectionName = "Test Section" }
                );
            modelBuilder.Entity<Thread>().HasData(
                new Thread() { Id = 1, ThreadName = "Thread 1", SectionId = 1 },
                new Thread() { Id = 2, ThreadName = "Thread 2", SectionId = 2 },
                new Thread() { Id = 3, ThreadName = "Test Thread", SectionId = 3 }
                );
            modelBuilder.Entity<Post>().HasData(
                new Post() { Id=1, Title="test",Content = "test",PostDate=DateTimeOffset.UtcNow,ThreadId=3,UserId=1,IsClosed=false},
                new Post() { Id = 2, Title = "test", Content = "test", PostDate = DateTimeOffset.UtcNow, ThreadId = 3, UserId = 1, IsClosed = false }
            );
            modelBuilder.Entity<Comment>().HasData(
                new Comment() { Id = 1, Content = "Testssss", CommentDate = DateTimeOffset.UtcNow, PostId = 1, UserId = 1 },
                new Comment() { Id = 2, Content = "Testssss", CommentDate = DateTimeOffset.UtcNow, PostId = 1, UserId = 1 },
                new Comment() { Id = 3, Content = "Testssss", CommentDate = DateTimeOffset.UtcNow, PostId = 1, UserId = 2 },
                new Comment() { Id = 4, Content = "Testssss", CommentDate = DateTimeOffset.UtcNow, PostId = 1, UserId = 2 },
                new Comment() { Id = 5, Content = "Testssss", CommentDate = DateTimeOffset.UtcNow, PostId = 2, UserId = 1 },
                new Comment() { Id = 6, Content = "Testssss", CommentDate = DateTimeOffset.UtcNow, PostId = 2, UserId = 1 },
                new Comment() { Id = 7, Content = "Testssss", CommentDate = DateTimeOffset.UtcNow, PostId = 2, UserId = 2 },
                new Comment() { Id = 8, Content = "Testssss", CommentDate = DateTimeOffset.UtcNow, PostId = 2, UserId = 2 }
                );

        }
    }
}
