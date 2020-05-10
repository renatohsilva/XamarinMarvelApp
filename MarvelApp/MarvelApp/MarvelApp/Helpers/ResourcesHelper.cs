using Xamarin.Forms;

namespace MarvelApp.Helpers
{
    public static class ResourcesHelper
    {
        public static TResource SingleOrDefault<TResource>(string key, TResource defaultValue = default)
        {
            var resources = Application.Current.Resources;

            if (resources.ContainsKey(key))
            {
                return (TResource)resources[key];
            }

            return defaultValue;
        }
    }
}
