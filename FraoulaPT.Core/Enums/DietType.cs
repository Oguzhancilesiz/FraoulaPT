using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FraoulaPT.Core.Enums
{
    public enum DietType
    {
        [Description("Seçiniz")]
        None = 0,
        [Description("Diyet Yok")]
        Vegetarian = 1,
        [Description("Vegan")]
        Vegan = 2,
        [Description("Balık ve Deniz Ürünleri")]
        Pescatarian = 3,
        [Description("Akdeniz")]
        Mediterranean = 4,
        [Description("Ketojenik")]
        Ketogenic = 5,
        [Description("Paleo")]
        Paleo = 6,
        [Description("Düşük Karbonhidrat")]
        LowCarb = 7,
        [Description("Yüksek Protein")]
        HighProtein = 8,
        [Description("Glutensiz")]
        GlutenFree = 9,
        [Description("Süt Ürünleri İçermeyen")]
        DairyFree = 10,
        [Description("Dengeli")]
        Balanced = 11,
        [Description("Aralıklı Oruç")]
        IntermittentFasting = 12,
        [Description("Özel Diyet")]
        Custom = 13
    }
}
