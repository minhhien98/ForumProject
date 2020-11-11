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
            //Post Mod:
            modelBuilder.Entity<Post>().Property("IsClosed").HasDefaultValue(false);
            //Comment Mod:
            modelBuilder.Entity<Comment>().HasOne(c => c.User).WithMany(u => u.Comments).HasForeignKey(c => c.UserId).OnDelete(DeleteBehavior.Restrict);
            //Data seed for test
            modelBuilder.Entity<UserRole>().HasData(
                new UserRole { Id = 1, RoleName = "Admin" },
                new UserRole { Id = 2, RoleName = "User" });
            modelBuilder.Entity<User>().HasData(
                new User() { Id = 1, UserName = "Admin", Password = PasswordHelper.Sha256("Admin","Admin"), Email = "Admin@gmail.com", FirstName = "Hien", LastName = "Nguyen Minh", UserRoleId = 1 },
                new User() { Id = 2, UserName = "User1", Password = PasswordHelper.Sha256("User1", "User1"), Email = "User1@gmail.com", FirstName = "Hien", LastName = "Nguyen Minh", UserRoleId = 2 });
            modelBuilder.Entity<Section>().HasData(
                new Section { Id = 1, SectionName = "Section 1" },
                new Section { Id = 2, SectionName = "Section 2" }
                );
            modelBuilder.Entity<Thread>().HasData(
                new Thread() { Id = 1, ThreadName = "Thread 1", SectionId = 1 },
                new Thread() { Id = 2, ThreadName = "Thread 2", SectionId = 2 }
                );
            modelBuilder.Entity<Post>().HasData(
                new Post() { Id = 1, Title = "Post1", Content = "Post Content", PostDate = DateTimeOffset.UtcNow, ThreadId = 1, UserId = 1, IsClosed = false },
                new Post() { Id = 2, Title = "Post2", Content = "Post Content", PostDate = DateTimeOffset.UtcNow, ThreadId = 1, UserId = 1, IsClosed = false },
                new Post() { Id = 3, Title = "Post3", Content = "Post Content", PostDate = DateTimeOffset.UtcNow, ThreadId = 1, UserId = 2, IsClosed = false },
                new Post() { Id = 4, Title = "Post4", Content = "Post Content", PostDate = DateTimeOffset.UtcNow, ThreadId = 1, UserId = 1, IsClosed = false },
                new Post() { Id = 5, Title = "Post5", Content = "Post Content", PostDate = DateTimeOffset.UtcNow, ThreadId = 1, UserId = 1, IsClosed = false },
                new Post() { Id = 6, Title = "Post6", Content = "Post Content", PostDate = DateTimeOffset.UtcNow, ThreadId = 1, UserId = 1, IsClosed = false },
                new Post() { Id = 7, Title = "Post7", Content = "Post Content", PostDate = DateTimeOffset.UtcNow, ThreadId = 1, UserId = 2, IsClosed = false },
                new Post() { Id = 8, Title = "Post8", Content = "Post Content", PostDate = DateTimeOffset.UtcNow, ThreadId = 1, UserId = 1, IsClosed = false },
                new Post() { Id = 9, Title = "Post9", Content = "Post Content", PostDate = DateTimeOffset.UtcNow, ThreadId = 1, UserId = 1, IsClosed = false },
                new Post() { Id = 10, Title = "Post10", Content = "Post Content", PostDate = DateTimeOffset.UtcNow, ThreadId = 1, UserId = 1, IsClosed = false },
                new Post() { Id = 11, Title = "Post11", Content = "Post Content", PostDate = DateTimeOffset.UtcNow, ThreadId = 1, UserId = 1, IsClosed = false },
                new Post() { Id = 12, Title = "Post12", Content = "Post Content", PostDate = DateTimeOffset.UtcNow, ThreadId = 1, UserId = 2, IsClosed = false },
                new Post() { Id = 13, Title = "Post1", Content = "Post Content", PostDate = DateTimeOffset.UtcNow, ThreadId = 2, UserId = 1, IsClosed = false },
                new Post() { Id = 14, Title = "Post2", Content = "Post Content", PostDate = DateTimeOffset.UtcNow, ThreadId = 2, UserId = 2, IsClosed = false }
            );
            modelBuilder.Entity<Comment>().HasData(
                new Comment() { Id = 1, Content = "Comment 1", CommentDate = DateTimeOffset.UtcNow, PostId = 1, UserId = 1 },
                new Comment() { Id = 2, Content = "Comment 2", CommentDate = DateTimeOffset.UtcNow, PostId = 1, UserId = 1 },
                new Comment() { Id = 3, Content = "Comment 3", CommentDate = DateTimeOffset.UtcNow, PostId = 1, UserId = 2 },
                new Comment() { Id = 4, Content = "Comment 4", CommentDate = DateTimeOffset.UtcNow, PostId = 1, UserId = 1 },
                new Comment() { Id = 5, Content = "Comment 5", CommentDate = DateTimeOffset.UtcNow, PostId = 1, UserId = 2 },
                new Comment() { Id = 6, Content = "Comment 6", CommentDate = DateTimeOffset.UtcNow, PostId = 1, UserId = 1 },
                new Comment() { Id = 7, Content = "Comment 7", CommentDate = DateTimeOffset.UtcNow, PostId = 1, UserId = 1 },
                new Comment() { Id = 8, Content = "Comment 8", CommentDate = DateTimeOffset.UtcNow, PostId = 1, UserId = 2 },
                new Comment() { Id = 9, Content = "Comment 9", CommentDate = DateTimeOffset.UtcNow, PostId = 1, UserId = 1 },
                new Comment() { Id = 10, Content = "Comment 10", CommentDate = DateTimeOffset.UtcNow, PostId = 1, UserId = 1 },
                new Comment() { Id = 11, Content = "Comment 11", CommentDate = DateTimeOffset.UtcNow, PostId = 1, UserId = 2 },
                new Comment() { Id = 12, Content = "Comment 12", CommentDate = DateTimeOffset.UtcNow, PostId = 1, UserId = 1 },
                new Comment() { Id = 13, Content = "Comment 1", CommentDate = DateTimeOffset.UtcNow, PostId = 2, UserId = 1 },
                new Comment() { Id = 14, Content = "Comment 2", CommentDate = DateTimeOffset.UtcNow, PostId = 2, UserId = 2 }
                );
        }
    }
}
