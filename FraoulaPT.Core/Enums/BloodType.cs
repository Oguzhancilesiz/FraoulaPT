using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FraoulaPT.Core.Enums
{
    public enum BloodType
    {
        [Description("Seçiniz")]
        None = 0,
        [Description("A+")]
        APlus = 1,
        [Description("A-")]
        AMinus = 2,
        [Description("B+")]
        BPlus = 3,
        [Description("B-")]
        BMinus = 4,
        [Description("AB+")]
        ABPlus = 5,
        [Description("AB-")]
        ABMinus = 6,
        [Description("O+")]
        OPlus = 7,
        [Description("O-")]
        OMinus = 8
    }
}
