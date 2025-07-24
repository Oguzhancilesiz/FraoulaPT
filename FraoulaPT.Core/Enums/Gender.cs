using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FraoulaPT.Core.Enums
{
    public enum Gender
    {
        [Display(Name = "Seçiniz")]
        Other = 0,
        [Display(Name = "Kadın")]
        Female = 1,
        [Display(Name = "Erkek")]
        Male = 2
    }
}
