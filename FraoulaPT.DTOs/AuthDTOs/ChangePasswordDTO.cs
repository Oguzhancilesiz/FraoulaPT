using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FraoulaPT.DTOs.AuthDTOs
{
    public class ChangePasswordDTO
    {
        [Required, DataType(DataType.Password), Display(Name = "Mevcut Şifre")]
        public string CurrentPassword { get; set; } = "";

        [Required, DataType(DataType.Password), MinLength(6), Display(Name = "Yeni Şifre")]
        public string NewPassword { get; set; } = "";

        [Required, DataType(DataType.Password), Compare(nameof(NewPassword)), Display(Name = "Yeni Şifre (Tekrar)")]
        public string ConfirmPassword { get; set; } = "";
    }
}
