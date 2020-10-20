using DomainModel.Entity;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Collections.Generic;

namespace ForumProject.Models.ViewModel.Comment
{
    public class CommentViewModel
    {
        public CommentViewModel()
        {
            comments = new List<DomainModel.Entity.Comment>();
        }
        public DomainModel.Entity.Post Post { get; set; }
        public DomainModel.Entity.Comment Comment { get; set; }
        public IList<DomainModel.Entity.Comment> comments { get; set; }
    }
}
