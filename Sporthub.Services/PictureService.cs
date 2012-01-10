using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Sporthub.Model;
using Sporthub.Repository;

namespace Sporthub.Services
{
    public class PictureService
    {
        private PictureRepository pictureRepository;

        public PictureService(PictureRepository pictureRepository)
        {
            this.pictureRepository = pictureRepository;
        }

        public IList<Picture> GetAll()
        {
            return this.pictureRepository.AsQueryable().ToList<Picture>();
        }

        public IList<Picture> GetAllForResort(int id)
        {
            return this.pictureRepository.AsQueryable().Where(p => p.ResortID == id).OrderByDescending(p => p.ID).ToList<Picture>();
        }

        public IList<Picture> GetAllForUser(int id)
        {
            return this.pictureRepository.AsQueryable().Where(p => p.CreatedUserID == id).OrderByDescending(p => p.ID).ToList<Picture>();
        }

        public Picture Get(int id)
        {
            return pictureRepository.AsQueryable().SingleOrDefault(t => t.ID == id);
        }
    }
}
