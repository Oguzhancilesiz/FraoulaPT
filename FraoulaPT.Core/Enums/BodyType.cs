using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FraoulaPT.Core.Enums
{
    public enum BodyType
    {
        [Display(Name = "Seçiniz")]
        None = 0,
        [Display(Name = "Endomorf")]
        Endomorph,
        [Display(Name = "Mezomorf")]
        Ectomorph,
        [Display(Name = "Ektomorf")]
        Mesomorph
    }
}
