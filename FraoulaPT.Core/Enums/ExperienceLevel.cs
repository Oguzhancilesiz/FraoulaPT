using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FraoulaPT.Core.Enums
{
    public enum ExperienceLevel
    {
        [Description("Seçiniz")]
        None = 0,
        [Description("Yeni Başlayan")]
        Beginner = 1,
        [Description("Orta Seviye")]
        Intermediate = 2,
        [Description("İleri Seviye")]
        Advanced = 3,
        [Description("Profesyonel")]
        Professional = 4,
        [Description("Uzman")]
        Elite = 5
    }
}
