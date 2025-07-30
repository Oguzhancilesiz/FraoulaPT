using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Net;

namespace FraoulaPT.WebUI.Infrastructure.Auth
{
    public class CustomRedirectHandler : CookieAuthenticationEvents
    {
        public override Task RedirectToLogin(RedirectContext<CookieAuthenticationOptions> context)
        {
            if (context.Request.Path.StartsWithSegments("/ExtraPackage", StringComparison.OrdinalIgnoreCase))
            {
                string alert = "Ek paket satın almak için giriş yapmalısınız.";
                string encodedAlert = WebUtility.UrlEncode(alert); // Türkçe karakterleri encode eder
                string returnUrl = WebUtility.UrlEncode(context.Request.Path); // opsiyonel: dönüş adresi

                var redirectUrl = $"/Auth/Login?alert={encodedAlert}&returnUrl={returnUrl}";
                context.Response.Redirect(redirectUrl);
            }
            else
            {
                context.Response.Redirect(context.RedirectUri);
            }

            return Task.CompletedTask;
        }
    }
}
