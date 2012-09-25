using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Sporthub.Model;
using Sporthub.Repository;
using Sporthub.Repository.DataAccess;
using System.Configuration;
using Sporthub.Mvc.Code.Extensions;

namespace Sporthub.Services
{
    public class UserService
    {
        private UserRepository userRepository;
        private SporthubDataContext db = new SporthubDataContext(ConfigurationManager.ConnectionStrings["SQL2005_615410_sporthubConnectionString"].ConnectionString);

        public UserService(UserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public IList<Sporthub.Model.User> GetAll()
        {
            return this.userRepository.AsQueryable().ToList<Sporthub.Model.User>();
        }

        public IList<Sporthub.Model.User> GetRecentVisitors()
        {
            
            return this.userRepository.AsQueryable().OrderByDescending(x => x.LastVisitDate).Take(6).ToList<Sporthub.Model.User>();
        }

        public IList<Sporthub.Model.User> GetNewMembers()
        {
            return this.userRepository.AsQueryable().OrderByDescending(x => x.CreatedDate).Take(6).ToList<Sporthub.Model.User>();
        }

        public IList<Sporthub.Model.User> GetUsersThatFavedResort(int resortId, int take)
        {

            int[] userIds = (from lru in db.LinkResortUsers
                            where (lru.ResortID == resortId && lru.IsFavourite == true)
                            select lru.UserID).ToArray();

            return this.userRepository.AsQueryableBasic().Where(x => userIds.Contains(x.ID)).Take(take).ToList<Sporthub.Model.User>();
        }

        public IList<Sporthub.Model.User> GetUsersThatVisitedResort(int resortId, int take)
        {

            int[] userIds = (from lru in db.LinkResortUsers
                             where (lru.ResortID == resortId && lru.HasVisited == true)
                             select lru.UserID).ToArray();

            return this.userRepository.AsQueryableBasic().Where(x => userIds.Contains(x.ID)).Take(take).ToList<Sporthub.Model.User>();
        }

        public IList<Sporthub.Model.User> GetUsersThatReviewedResort(int resortId, int take)
        {

            int[] userIds = (from lru in db.LinkResortUsers
                             where (lru.ResortID == resortId && lru.Score > 0)
                             select lru.UserID).ToArray();

            return this.userRepository.AsQueryable().Where(x => userIds.Contains(x.ID)).OrderByDescending(x => x.CreatedDate).Take(take).ToList<Sporthub.Model.User>();
        }

        public Sporthub.Model.User Get(int id)
        {
            return userRepository.AsQueryable().SingleOrDefault(u => u.ID == id);
        }

        public Sporthub.Model.User GetByFacebookId(string id)
        {
            return userRepository.AsQueryable().SingleOrDefault(u => u.FacebookUid == id);
        }

        public Sporthub.Model.User Get(string userName)
        {
            return userRepository.AsQueryable().SingleOrDefault(u => u.UserName == userName);
        }

        public Sporthub.Model.User GetByEmail(string email)
        {
            var user = new Sporthub.Model.User();
            try
            {
                user = userRepository.AsQueryable().SingleOrDefault(u => u.Email == email);
            }
            catch (Exception ex)
            {
                user = null;
            }
            return user;
        }

        public Sporthub.Model.User GetByUserName(string userName)
        {
            return userRepository.AsQueryable().SingleOrDefault(u => u.UserName == userName);
        }

        public int Add(Sporthub.Model.User user)
        {
            return userRepository.Add(user);
        }

        public void Update(Sporthub.Model.User user)
        {
            userRepository.Update(user);
        }

        public void Delete(Sporthub.Model.User user)
        {
            userRepository.Delete(user);
        }
    }
}
