using FraoulaPT.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FraoulaPT.DTOs.UserDTOs
{
    public class ProfileEditDTO
    {
        // Anahtar
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Cinsiyet seçimi zorunludur")]
        public Gender? Gender { get; set; }

        [Display(Name = "Doğum Tarihi")]
        [DataType(DataType.Date)]
        public DateTime? BirthDate { get; set; }

        [Display(Name = "Boy (cm)")]
        [Range(50, 250, ErrorMessage = "Boy 50-250 cm arasında olmalıdır")]
        public double? HeightCm { get; set; }

        [Display(Name = "Kilo (kg)")]
        [Range(20, 300, ErrorMessage = "Kilo 20-300 kg arasında olmalıdır")]
        public double? WeightKg { get; set; }

        [Display(Name = "Vücut Tipi")]
        public BodyType? BodyType { get; set; }

        [Display(Name = "Kan Grubu")]
        [StringLength(10)]
        public string BloodType { get; set; }

        [Display(Name = "Telefon")]
        [StringLength(20)]
        public string PhoneNumber { get; set; }

        [Display(Name = "Adres")]
        [StringLength(250)]
        public string Address { get; set; }

        [Display(Name = "Acil Durum Kişisi")]
        public string EmergencyContactName { get; set; }

        [Display(Name = "Acil Durum Telefonu")]
        public string EmergencyContactPhone { get; set; }

        [Display(Name = "Geçmiş Hastalıklar")]
        public string MedicalHistory { get; set; }

        [Display(Name = "Kronik Hastalıklar")]
        public string ChronicDiseases { get; set; }

        [Display(Name = "Düzenli Kullanılan İlaçlar")]
        public string CurrentMedications { get; set; }

        [Display(Name = "Alerjiler")]
        public string Allergies { get; set; }

        [Display(Name = "Geçmiş Sakatlıklar")]
        public string PastInjuries { get; set; }

        [Display(Name = "Mevcut Ağrılar")]
        public string CurrentPain { get; set; }

        [Display(Name = "Hamilelik Durumu")]
        public bool? PregnancyStatus { get; set; }

        [Display(Name = "Son Sağlık Taraması")]
        public string LastCheckResults { get; set; }

        [Display(Name = "Sigara / Alkol Kullanımı")]
        public string SmokingAlcohol { get; set; }

        [Display(Name = "Meslek")]
        public string Occupation { get; set; }

        [Display(Name = "Egzersiz/Deneyim Seviyesi")]
        public string ExperienceLevel { get; set; }

        [Display(Name = "En Sevdiği Sporlar")]
        public string FavoriteSports { get; set; }

        [Display(Name = "Ek Notlar")]
        public string Notes { get; set; }

        [Display(Name = "Beslenme Tipi")]
        public string DietType { get; set; }
    }
}
