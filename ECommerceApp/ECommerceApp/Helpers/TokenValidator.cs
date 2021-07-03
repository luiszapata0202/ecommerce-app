using System;
using Xamarin.Essentials;

namespace ECommerceApp.Helpers
{
    public static class TokenValidator
    {
        public static bool CheckTokenValidity()
        {
            Preferences.Set("currentTime", DateTimeOffset.Now.ToUnixTimeSeconds());

            var expirationTime = Preferences.Get("tokenExpirationTime", 0);
            var currentTime = Preferences.Get("currentTime", long.MinValue);

            if (currentTime > expirationTime)
            {
                return false;
            }

            return true;
        }
    }
}
