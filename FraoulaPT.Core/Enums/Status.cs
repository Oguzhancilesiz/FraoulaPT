using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FraoulaPT.Core.Enums
{
    public enum Status
    {
        [Display(Name = "Seçiniz")]
        None = 0,
        [Display(Name = "Aktif")]
        Active = 1,
        [Display(Name = "Pasif")]
        DeActive = 2,
        [Display(Name = "Reddedildi")]
        Approved = 6,
        [Display(Name = "Silindi")]
        Deleted = 4,
        [Display(Name = "Onay Bekliyor")]
        UnApproved = 3,
        [Display(Name = "İptal Edildi")]
        Cancel = 7,
        [Display(Name = "Beklemede")]
        Pending = 8,
        [Display(Name = "Okundu")]
        Read = 9,
        [Display(Name = "Güncellendi")]
        Updated,
        [Display(Name = "İşlemde")]
        Commit = 14



    }
}
