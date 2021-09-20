using LaunchDarkly.Sdk;
using LaunchDarkly.Sdk.Server.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;

namespace LaunchDarkly_Project.Pages
{
    public class IndexModel : PageModel
    {
        private ILdClient _ldClient;
        public bool _myFirstFeatureFlag;

        public IndexModel(ILdClient ldClient)
        {
            _ldClient = ldClient;
        }

        public void OnGet()
        {
            _myFirstFeatureFlag = _ldClient.BoolVariation("my-first-feature-flag", GetUser(), false);
        }

        private User GetUser()
        {
            // Simple User object creation
            // return LaunchDarkly.Sdk.User.WithKey(Guid.NewGuid().ToString());

            // Consistent User object creation
            return LaunchDarkly.Sdk.User.WithKey(GetUserKey());
        }

        private string GetUserKey()
        {
            string ldUserCookie = "_ldUserCookie";
            string guid;

            if (!Request.Cookies.ContainsKey(ldUserCookie))
            {
                guid = Guid.NewGuid().ToString();
                var cookieOptions = new CookieOptions()
                {
                    Path = "/",
                    Expires = DateTimeOffset.UtcNow.AddDays(365),
                    IsEssential = true,
                    HttpOnly = false,
                    Secure = false,
                };

                Response.Cookies.Append(ldUserCookie, guid, cookieOptions);
            }
            else
            {
                Request.Cookies.TryGetValue(ldUserCookie, out guid);
            }

            return guid;
        }
    }
}
