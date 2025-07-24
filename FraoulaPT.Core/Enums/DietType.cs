using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FraoulaPT.Core.Enums
{
    public enum DietType
    {
        [Display(Name = "Seçiniz")]
        None = 0,
        [Display(Name = "Vejetaryen")]
        Vegetarian = 1,
        [Display(Name = "Vegan")]
        Vegan = 2,
        [Display(Name = "Balık ve Deniz Ürünleri")]
        Pescatarian = 3,
        [Display(Name = "Akdeniz")]
        Mediterranean = 4,
        [Display(Name = "Ketojenik")]
        Ketogenic = 5,
        [Display(Name = "Paleo")]
        Paleo = 6,
        [Display(Name = "Düşük Karbonhidrat")]
        LowCarb = 7,
        [Display(Name = "Yüksek Protein")]
        HighProtein = 8,
        [Display(Name = "Glutensiz")]
        GlutenFree = 9,
        [Display(Name = "Süt Ürünleri İçermeyen")]
        DairyFree = 10,
        [Display(Name = "Dengeli")]
        Balanced = 11,
        [Display(Name = "Aralıklı Oruç")]
        IntermittentFasting = 12,
        [Display(Name = "Özel Diyet")]
        Custom = 13
    }
}
