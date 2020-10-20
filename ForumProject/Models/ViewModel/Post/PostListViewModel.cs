using DomainModel.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForumProject.Models.ViewModel.Post
{
    public class PostListViewModel
    {
        public int id { get; set; }
        public string ThreadName { get; set; }
        public IEnumerable<DomainModel.Entity.Post> Posts { get; set; }
    }
}
