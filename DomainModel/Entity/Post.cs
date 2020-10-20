using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DomainModel.Entity
{
    public class Post
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public DateTimeOffset PostDate { get; set; }
        public int ThreadId { get; set; }
        public Thread Thread { get; set; }

        public ICollection<Comment> Comments { get; set; }
    }
}
