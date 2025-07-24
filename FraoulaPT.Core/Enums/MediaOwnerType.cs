using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FraoulaPT.Core.Enums
{
    public enum MediaOwnerType
    {
        [Display(Name = "Seçiniz")]
        None = 0,
        [Display(Name = "Kullanıcı")]
        Chat = 1,
        [Display(Name = "Kullanıcı Profili")]
        UserProfile = 2,
        [Display(Name = "Kullanıcı Grubu")]
        Group = 3,
        [Display(Name = "Kullanıcı Kanalı")]
        Channel = 4
    }
}
