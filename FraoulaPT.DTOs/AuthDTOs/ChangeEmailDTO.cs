using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FraoulaPT.DTOs.AuthDTOs
{
    public class ChangeEmailDTO
    {
        [Required, EmailAddress, Display(Name = "Yeni E-posta")]
        public string NewEmail { get; set; } = "";
    }
}
