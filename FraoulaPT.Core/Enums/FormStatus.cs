using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FraoulaPT.Core.Enums
{
    public enum FormStatus 
    {
        [Display(Name ="Seçiniz")]
        None = 0,
        [Display(Name = "Beklemede")]
        Pending = 1,
        [Display(Name = "Onaylandı")]
        Approved = 2,
        [Display(Name = "Reddedildi")]
        Rejected = 3,
        [Display(Name = "İnceleniyor")]
        InReview = 4,
        [Display(Name = "Tamamlandı")]
        Completed = 5,
        [Display(Name = "İnceleme Bekliyor")]
        PendingReview = 6,
        [Display(Name = "İptal Edildi")]
        Cancelled = 7,
        [Display(Name = "Güncellendi")]
        Updated = 8,
        [Display(Name = "Arşivlendi")]
        Archived = 9,
        [Display(Name = "Yeniden Değerlendiriliyor")]
        ReEvaluating = 10,
        [Display(Name = "İşlemde")]
        InProcess = 11,
        [Display(Name = "İşlem Tamamlandı")]
        Processed = 12,
        [Display(Name = "İşlem İptal Edildi")]
        ProcessCancelled = 13,
        [Display(Name = "İşlem Güncellendi")]
        ProcessUpdated = 14,
        [Display(Name = "İşlem Arşivlendi")]
        ProcessArchived = 15,

    }
}
