using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FraoulaPT.Core.Enums
{
    public enum PackageType
    {
        [Display(Name = "Seçiniz")]
        None = 0,
        [Display(Name = "Temel Paket")]
        Basic = 1,
        [Display(Name = "Klasik Paket")]
        Classic = 2,
        [Display(Name = "Altın Paket")]
        Gold = 3,
        [Display(Name = "Pro Paket")]
        Pro = 4,
        [Display(Name = "Özel Paket")]
        Premium = 5,
        [Display(Name = "Platin Paket")]
        Platinum = 6
    }

}
