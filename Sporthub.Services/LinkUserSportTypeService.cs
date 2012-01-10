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
    public class LinkUserSportTypeService
    {
        private LinkUserSportTypeRepository linkUserSportTypeRepository;
        private SporthubDataContext db = new SporthubDataContext(ConfigurationManager.ConnectionStrings["SQL2005_615410_sporthubConnectionString"].ConnectionString);

        public LinkUserSportTypeService(LinkUserSportTypeRepository linkUserSportTypeRepository)
        {
            this.linkUserSportTypeRepository = linkUserSportTypeRepository;
        }

        public int Add(Sporthub.Model.LinkUserSportType lust)
        {
            return linkUserSportTypeRepository.Add(lust);
        }

        public void Update(Sporthub.Model.LinkUserSportType lust)
        {
            linkUserSportTypeRepository.Update(lust);
        }

        public void Delete(Sporthub.Model.LinkUserSportType lust)
        {
            linkUserSportTypeRepository.Delete(lust);
        }

        public void DeleteList(IList<Sporthub.Model.LinkUserSportType> lustList)
        {
            linkUserSportTypeRepository.DeleteList(lustList);
        }
    }
}

