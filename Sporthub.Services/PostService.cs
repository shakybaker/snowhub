using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Sporthub.Model;
using Sporthub.Repository;

namespace Sporthub.Services
{
    public class PostService
    {
        private PostRepository postRepository;

        public PostService(PostRepository postRepository)
        {
            this.postRepository = postRepository;
        }

        public IList<Post> GetAll()
        {
            return this.postRepository.AsQueryable().ToList<Post>();
        }

        public IList<Post> GetAll(int id)
        {
            return this.postRepository.AsQueryable().Where(p => p.ThreadID == id).OrderByDescending(p => p.UpdatedDate).ToList<Post>();
        }

        public IList<Post> GetAllOldestFirst(int id)
        {
            return this.postRepository.AsQueryable().Where(p => p.ThreadID == id).OrderBy(p => p.CreatedDate).ToList<Post>();
        }

        public IList<Post> GetAllForUser(int id)
        {
            return this.postRepository.AsQueryable().Where(p => p.UpdatedUserID == id).OrderByDescending(p => p.UpdatedDate).ToList<Post>();
        }

        public Post Get(int id)
        {
            return postRepository.AsQueryable().SingleOrDefault(p => p.ID == id);
        }

        public int Add(Post post)
        {
            return postRepository.Add(post);
        }

    }
}
