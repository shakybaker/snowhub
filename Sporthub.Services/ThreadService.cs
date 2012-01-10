using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Sporthub.Model;
using Sporthub.Repository;

namespace Sporthub.Services
{
    public class ThreadService
    {
        private ThreadRepository threadRepository;

        public ThreadService(ThreadRepository threadRepository)
        {
            this.threadRepository = threadRepository;
        }

        public IList<Thread> GetAll()
        {
            return this.threadRepository.AsQueryable().ToList<Thread>();
        }

        public IList<Thread> GetAll(int id)
        {
            return this.threadRepository.AsQueryable().Where(t => t.ForumID == id).OrderByDescending(t => t.UpdatedDate).ToList<Thread>();
        }

        public IList<Thread> GetAllForResort(int id)
        {
            return this.threadRepository.AsQueryable().Where(t => t.ResortID == id).OrderByDescending(t => t.UpdatedDate).ToList<Thread>();
        }

        public IList<Thread> GetAllForUser(int id)
        {
            return this.threadRepository.AsQueryable().Where(t => t.UpdatedUserID == id).OrderByDescending(t => t.UpdatedDate).ToList<Thread>();
        }

        public Thread Get(int id)
        {
            return threadRepository.AsQueryable().SingleOrDefault(t => t.ID == id);
        }

        public int Add(Thread thread)
        {
            return threadRepository.Add(thread);
        }
    }
}
