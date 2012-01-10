using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Sporthub.Model;
using Sporthub.Repository;

namespace Sporthub.Services
{
    public class ReviewUsefulnessService
    {
        private ReviewUsefulnessRepository reviewUsefulnessRepository;

        public ReviewUsefulnessService(ReviewUsefulnessRepository reviewUsefulnessRepository)
        {
            this.reviewUsefulnessRepository = reviewUsefulnessRepository;
        }

        public int Add(ReviewUsefulness ru)
        {
            return reviewUsefulnessRepository.Add(ru);
        }

        public void Update(ReviewUsefulness ru)
        {
            reviewUsefulnessRepository.Update(ru);
        }
    }
}
