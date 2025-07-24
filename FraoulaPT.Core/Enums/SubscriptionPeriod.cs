using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FraoulaPT.Core.Enums
{
    public enum SubscriptionPeriod
    {
        [Display(Name = "Seçiniz")]
        None = 0,
        [Display(Name = "Günlük")]
        Daily = 1,
        [Display(Name = "Haftalık")]
        Weekly = 2,
        [Display(Name = "Aylık")]
        Monthly = 3,
        [Display(Name = "Yıllık")]
        Yearly = 4,
        [Display(Name = "Ömür Boyu")]
        Lifetime = 5,
        [Display(Name = "Deneme Süresi")]
        Trial = 6,
        [Display(Name = "Özel Süre")]
        Custom = 7
    }
}
