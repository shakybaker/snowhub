using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Sporthub.Model;
using Sporthub.Repository;

namespace Sporthub.Services
{
    public class ConfigSportTypeService
    {
        private ConfigSportTypeRepository configSportTypeRepository;

        public ConfigSportTypeService(ConfigSportTypeRepository configSportTypeRepository)
        {
            this.configSportTypeRepository = configSportTypeRepository;
        }

        public IList<ConfigSportType> GetAll()
        {
            return this.configSportTypeRepository.AsQueryable().ToList<ConfigSportType>();
        }

        public ConfigSportType Get(int id)
        {
            return configSportTypeRepository.AsQueryable().SingleOrDefault(cst => cst.ID == id);
        }
    }
}
