using DomainModel.Entity;
using Repository.Interface;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service
{
    public class PostService : IPostService
    {
        private IPostRepository _postRepository;
        public PostService(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }

        public void CreatePost(Post post)
        {
            _postRepository.CreatePost(post);
        }

        public void DeletePost(Post post)
        {
            _postRepository.DeletePost(post);
        }

        public void EditPost(Post post)
        {
            _postRepository.EditPost(post);
        }

        public Post GetPostById(int id)
        {
            return _postRepository.GetPostById(id);
        }

        public IEnumerable<Post> PostList()
        {
            return _postRepository.PostList();
        }
    }
}
