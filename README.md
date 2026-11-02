# FraoulaPT

Online kişisel antrenörlük platformunun ilk sürümü: koç ve öğrenci panelleri, antrenman programları, paket satışı ve gerçek zamanlı koç–öğrenci sohbeti.

> Platformun güncel sürümü (.NET 10 API + Next.js + Expo mobil) canlıda: **[ptfraoula.com](https://ptfraoula.com)**. Kaynak kodu özel repoda tutuluyor; bu repo ürünün ilk ASP.NET Core MVC sürümüdür.

## Özellikler

- **Koç paneli:** öğrenci yönetimi, egzersiz kategorileri ve egzersiz kütüphanesi, antrenman programı oluşturma, haftalık form takibi, paket ve ek paket tanımları, bildirimler
- **Öğrenci paneli:** PayTR ile paket satın alma, profil, haftalık form, destek talepleri
- **Gerçek zamanlı sohbet:** SignalR ile koç–öğrenci mesajlaşması
- **Kimlik ve yetki:** ASP.NET Core Identity, rol yönetimi

## Teknolojiler

- ASP.NET Core MVC (.NET 9), Areas (Admin / Koç / Öğrenci)
- Entity Framework Core + SQL Server
- ASP.NET Core Identity, SignalR
- Mapster

## Mimari

Katmanlı yapı, her katman ayrı proje:

```
FraoulaPT.Entity    → domain varlıkları
FraoulaPT.Mapping   → EF Core Fluent API konfigürasyonları
FraoulaPT.DAL       → DbContext, repository, unit of work
FraoulaPT.DTOs      → veri transfer nesneleri
FraoulaPT.Services  → iş kuralları
FraoulaPT.Core      → ortak altyapı (hub'lar, yardımcılar)
FraoulaPT.WebUI     → MVC arayüzü
```

## Çalıştırma

```bash
git clone https://github.com/Oguzhancilesiz/FraoulaPT.git
cd FraoulaPT
dotnet user-secrets --project FraoulaPT.WebUI set "SmtpSettings:Password" "<smtp-sifresi>"
dotnet ef database update --project FraoulaPT.DAL --startup-project FraoulaPT.WebUI
dotnet run --project FraoulaPT.WebUI
```

Hassas ayarlar (`SmtpSettings:Password`, `PayTR:MerchantKey`, `PayTR:MerchantSalt`, `JwtSettings:Secret`) repoda tutulmaz. `appsettings.json` içindeki `REDACTED` değerlerini user-secrets ya da ortam değişkenleriyle verin.
