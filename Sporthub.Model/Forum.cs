using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Sporthub.Model.Interfaces;

namespace Sporthub.Model
{
    public class Forum : IEntity
    {
        public int ID { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? CreatedUserID { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int? UpdatedUserID { get; set; }

        public string ForumName { get; set; }
        public int ParentID { get; set; }
        public int Sequence { get; set; }
        public string Description { get; set; }
        public bool IsAdmin { get; set; }
        public IList<Thread> Threads { get; set; }
        public Post LastPost { get; set; }
    }
}
