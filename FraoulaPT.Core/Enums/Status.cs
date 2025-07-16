using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FraoulaPT.Core.Enums
{
    public enum Status
    {
        [Description("None")]
        None = 0,
        [Description("Aktif")]
        Active = 1,
        [Description("Pasif")]
        DeActive = 2,
        [Description("Onaylı")]
        Approved = 6,
        [Description("Silindi")]
        Deleted = 4,
        [Description("Onay Bekliyor")]
        UnApproved = 3,
        [Description("İptal Edildi")]
        Cancel = 7,
        [Description("Beklemede")]
        Pending = 8,
        [Description("Okundu")]
        Read = 9,
        [Description("İşlemde")]
        Commit = 14


        
    }
}
