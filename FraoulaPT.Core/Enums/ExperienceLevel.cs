using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FraoulaPT.Core.Enums
{
    public enum ExperienceLevel
    {
        [Display(Name = "Seçiniz")]
        None = 0,
        [Display(Name = "Yeni Başlayan")]
        Beginner = 1,
        [Display(Name = "Orta Seviye")]
        Intermediate = 2,
        [Display(Name = "İleri Seviye")]
        Advanced = 3,
        [Display(Name = "Profesyonel")]
        Professional = 4,
        [Display(Name = "Uzman")]
        Elite = 5
    }
}
