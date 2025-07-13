using FraoulaPT.Core.Tokens;
using FraoulaPT.DTOs.PackageDTO;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FraoulaPT.Services.Concrete
{
    public class PayTRPaymentService
    {
        private readonly PayTROptions _options;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public PayTRPaymentService(IOptions<PayTROptions> options, IHttpContextAccessor httpContextAccessor)
        {
            _options = options.Value;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<string> CreatePaymentToken(PackageListDTO package, string userEmail, string userName, string userPhone, Guid appUserId)
        {
            // Kullanıcı IP'sini al
            var userIp = _httpContextAccessor.HttpContext.Connection.RemoteIpAddress?.ToString() ?? "127.0.0.1";
            var paymentAmount = (int)(package.Price * 100); // Kuruş
            var merchantOid = Guid.NewGuid().ToString();    // Sipariş no gibi benzersiz id

            // Hash için sıralı değerleri hazırla
            string userBasketJson = "[[\"" + package.Name + "\", \"" + package.Price.ToString("N2") + "\", \"1\"]]";
            string paymentType = "card"; // "card" veya "eft"
            string currency = "TL";
            string lang = "tr";
            string testMode = "1"; // Test için 1, canlıda 0
            string debugOn = "1";

            // GÜVENLİK: Hash hesapla (PayTR dökümanda örneği var)
            var hashStr = $"{_options.MerchantId}{userIp}{merchantOid}{userEmail}{paymentAmount}{userBasketJson}{testMode}{_options.MerchantSalt}";
            string paytrToken;
            using (var sha256 = System.Security.Cryptography.SHA256.Create())
            {
                var bytes = Encoding.UTF8.GetBytes(hashStr + _options.MerchantKey);
                var hash = sha256.ComputeHash(bytes);
                paytrToken = Convert.ToBase64String(hash);
            }

            // Kart saklama aktifleştirildi!
            var postData = new Dictionary<string, string>
        {
            { "merchant_id", _options.MerchantId },
            { "user_ip", userIp },
            { "merchant_oid", merchantOid },
            { "email", userEmail },
            { "payment_amount", paymentAmount.ToString() },
            { "paytr_token", paytrToken },
            { "user_name", userName },
            { "user_address", "Adres Girilecek" },
            { "user_phone", userPhone },
            { "user_basket", userBasketJson },
            { "currency", currency },
            { "test_mode", testMode },
            { "lang", lang },
            { "debug_on", debugOn },
            { "no_installment", "0" },  // Taksit kapalıysa 1
            { "max_installment", "12" },
            { "card_type", paymentType },
            { "payment_type", "card" }, // "card" / "eft"
            { "card_storage_enabled", "1" }, // KART SAKLAMA (en önemli parametre!)
            // Ek güvenlik: kullanıcı id ile eşle
            { "user_id", appUserId.ToString() }
        };

            using var client = new HttpClient();
            var content = new FormUrlEncodedContent(postData);
            var response = await client.PostAsync("https://www.paytr.com/odeme/api/get-token", content);
            var respStr = await response.Content.ReadAsStringAsync();
            var result = Newtonsoft.Json.JsonConvert.DeserializeObject<PayTRTokenResult>(respStr);
            if (result.status != "success")
                throw new Exception("Ödeme başlatılamadı: " + result.reason);

            return result.token;
        }
    }
}
