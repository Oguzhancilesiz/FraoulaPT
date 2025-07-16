using System.Security.Claims;

namespace FraoulaPT.WebUI.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static Guid GetUserId(this ClaimsPrincipal user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            var idString = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (Guid.TryParse(idString, out var id))
                return id;

            throw new Exception("Kullanıcı ID'si bulunamadı!");
        }
    }
}
