using FraoulaPT.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FraoulaPT.Entity
{
    public class Media : BaseEntity
    {
        public string Url { get; set; }
        public string AltText { get; set; }
        public MediaType MediaType { get; set; }
        public string ThumbnailUrl { get; set; }

        public Guid? UserWeeklyFormId { get; set; }
        public virtual UserWeeklyForm UserWeeklyForm { get; set; }

    }

}
