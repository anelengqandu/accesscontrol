using Plugin.Settings;
using Plugin.Settings.Abstractions;
using System;

namespace NMBM.Helpers
{
    public static class Settings
    {
        private static ISettings AppSettings
        {
            get
            {
                return CrossSettings.Current;
            }
        }
        public static string Username
        {
            get
            {
                // return AppSettings.GetValueOrDefault<string>("Username", "");
                return AppSettings.GetValueOrDefault(nameof(Username), String.Empty);
            }
            set
            {
                // AppSettings.AddOrUpdateValue<string>("Username", value);
                AppSettings.AddOrUpdateValue(nameof(Username), value);
            }
        }
        public static string Password
        {
            get
            {
                return AppSettings.GetValueOrDefault(nameof(Password), String.Empty);
            }
            set
            {
                AppSettings.AddOrUpdateValue(nameof(Password), value);
            }
        }
        public static string AccessToken
        {
            get
            {
                return AppSettings.GetValueOrDefault<string>("AccessToken", "");
            }
            set
            {
                AppSettings.AddOrUpdateValue<string>("AccessToken", value);
            }
        }

        public static DateTime AccessTokenExpirationDate
        {
            get
            {
                return AppSettings.GetValueOrDefault<DateTime>("AccessTokenExpirationDate", DateTime.UtcNow);
            }
            set
            {
                AppSettings.AddOrUpdateValue<DateTime>("AccessTokenExpirationDate", value);
            }
        }
    }
}
