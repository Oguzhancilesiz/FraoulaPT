using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FraoulaPT.Core.Enums
{
    public enum FormStatus { 
    
        None = 0,
        Pending = 1,
        Approved = 2,
        Rejected = 3,
        InReview = 4,
        Completed = 5,
        PendingReview = 6
    }
}
