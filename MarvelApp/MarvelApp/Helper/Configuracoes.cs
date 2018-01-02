using Plugin.Settings;
using Plugin.Settings.Abstractions;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace MarvelApp.Helper
{
    public class Configuracoes : ObservableObject
    {
        private Configuracoes()
        {
            ApiPublicKey = "8f67073e16726d6caed8144a1b5fb18e";
            ApiPrivateKey = "492aa97a0b2e4b7e163834da9b66650c78a2d1ec";
            ApiTimeStamp = Util.UnixTimeNow().ToString(CultureInfo.InvariantCulture);
            ApiHash = Util.CalculateMd5Hash(ApiTimeStamp + ApiPrivateKey + ApiPublicKey).ToLower();
        }

        private static readonly Lazy<Configuracoes> lazy = new Lazy<Configuracoes>(() => new Configuracoes());

        public static Configuracoes Instance
        {
            get
            {
                return lazy.Value;
            }
        }

        private static ISettings AppSettings { get { return CrossSettings.Current; } }

        public string ApiTimeStamp
        {
            get
            {
                return AppSettings.GetValueOrDefault(nameof(ApiTimeStamp), string.Empty);
            }
            private set
            {
                AppSettings.AddOrUpdateValue(nameof(ApiTimeStamp), value);
                OnPropertyChanged();
            }
        }

        public string ApiHash
        {
            get
            {
                return AppSettings.GetValueOrDefault(nameof(ApiHash), string.Empty);
            }
            private set
            {
                AppSettings.AddOrUpdateValue(nameof(ApiHash), value);
                OnPropertyChanged();
            }
        }

        public string ApiPublicKey
        {
            get
            {
                return AppSettings.GetValueOrDefault(nameof(ApiPublicKey), string.Empty);
            }
            private set
            {
                AppSettings.AddOrUpdateValue(nameof(ApiPublicKey), value);
                OnPropertyChanged();
            }
        }

        public string ApiPrivateKey
        {
            get
            {
                return AppSettings.GetValueOrDefault(nameof(ApiPrivateKey), string.Empty);
            }
            private set
            {
                AppSettings.AddOrUpdateValue(nameof(ApiPrivateKey), value);
                OnPropertyChanged();
            }
        }
    }
}
