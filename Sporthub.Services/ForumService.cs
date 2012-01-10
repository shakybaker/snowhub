using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Sporthub.Model;
using Sporthub.Repository;

namespace Sporthub.Services
{
    public class ForumService
    {
        private ForumRepository forumRepository;

        public ForumService(ForumRepository forumRepository)
        {
            this.forumRepository = forumRepository;
        }

        public IList<Forum> GetAll()
        {
            return this.forumRepository.AsQueryable().OrderBy(f => f.Sequence).ToList<Forum>();
        }

        public Forum Get(int id)
        {
            return forumRepository.AsQueryable().SingleOrDefault(t => t.ID == id);
        }
    }
}
