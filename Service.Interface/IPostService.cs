using DomainModel.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service.Interface
{
    public interface IPostService
    {
        void CreatePost(Post post);
        void EditPost(Post post);
        void DeletePost(Post post);
        Post GetPostById(int id);
        IEnumerable<Post> PostList();
    }
}
