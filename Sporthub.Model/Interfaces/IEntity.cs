using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sporthub.Model.Interfaces
{
    public interface IEntity
    {
        int ID { get; set; }
        DateTime? CreatedDate { get; set; }
        int? CreatedUserID { get; set; }
        DateTime? UpdatedDate { get; set; }
        int? UpdatedUserID { get; set; }
    }
}
