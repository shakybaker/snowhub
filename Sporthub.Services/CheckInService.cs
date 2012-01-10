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
    public class CheckInService
    {
        private CheckInRepository checkInRepository;
        private SporthubDataContext db = new SporthubDataContext(ConfigurationManager.ConnectionStrings["SQL2005_615410_sporthubConnectionString"].ConnectionString);

        public CheckInService(CheckInRepository checkInRepository)
        {
            this.checkInRepository = checkInRepository;
        }

        public int Add(Sporthub.Model.CheckIn checkIn)
        {
            return checkInRepository.Add(checkIn);
        }

        public void UpdateAllToInactiveForUser(int userID)
        {
            checkInRepository.UpdateAllToInactiveForUser(userID);
        }

        public void Update(Sporthub.Model.CheckIn checkIn)
        {
            checkInRepository.Update(checkIn);
        }
    }
}
