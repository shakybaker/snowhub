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
    public class ActivityService
    {
        private ActivityRepository activityRepository;
        private SporthubDataContext db = new SporthubDataContext(ConfigurationManager.ConnectionStrings["SQL2005_615410_sporthubConnectionString"].ConnectionString);

        public ActivityService(ActivityRepository activityRepository)
        {
            this.activityRepository = activityRepository;
        }

        public IList<Sporthub.Model.Activity> GetAll()
        {
            return this.activityRepository.AsQueryable().OrderByDescending(x => x.CreatedDate).ToList<Sporthub.Model.Activity>();
        }

        public IList<Sporthub.Model.Activity> GetAll(int take)
        {
            return this.activityRepository.AsQueryable().OrderByDescending(x => x.CreatedDate).Take(take).ToList<Sporthub.Model.Activity>();
        }

        public Sporthub.Model.Activity Get(int id)
        {
            return activityRepository.AsQueryable().SingleOrDefault(u => u.ID == id);
        }

        public Sporthub.Model.Activity Get(string facebookUid)
        {
            return activityRepository.AsQueryable().SingleOrDefault(u => u.FacebookUid == facebookUid);
        }

        public IList<Sporthub.Model.Activity> GetAllByUserID(int id)
        {
            return this.activityRepository.AsQueryable().OrderByDescending(x => x.CreatedDate).ToList<Sporthub.Model.Activity>();
        }

        public IList<Sporthub.Model.Activity> GetAllByUserID(int id, int take)
        {
            return this.activityRepository.AsQueryable().Where(x => x.CreatedUserID == id).OrderByDescending(x => x.CreatedDate).Take(take).ToList<Sporthub.Model.Activity>();
        }

        public int Add(Sporthub.Model.Activity activity)
        {
            return activityRepository.Add(activity);
        }

        public void Update(Sporthub.Model.Activity activity)
        {
            activityRepository.Update(activity);
        }
    }
}
