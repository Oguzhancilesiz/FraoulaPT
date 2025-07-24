using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FraoulaPT.Core.Enums
{
    public enum BloodType
    {
        [Display(Name = "Seçiniz")]
        None = 0,
        [Display(Name = "A+")]
        APlus = 1,
        [Display(Name = "A-")]
        AMinus = 2,
        [Display(Name = "B+")]
        BPlus = 3,
        [Display(Name = "B-")]
        BMinus = 4,
        [Display(Name = "AB+")]
        ABPlus = 5,
        [Display(Name = "AB-")]
        ABMinus = 6,
        [Display(Name = "O+")]
        OPlus = 7,
        [Display(Name = "O-")]
        OMinus = 8
    }
}
