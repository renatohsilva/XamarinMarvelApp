using Newtonsoft.Json.Linq;
using System.Diagnostics;
using System.IO;
using System.Reflection;

namespace MarvelApp.Setup
{
    public class AppSettingsManager
    {
        private static AppSettingsManager instance;
        private readonly JObject secrets;

        private const string NAMESPACE = "MarvelApp";

        private readonly string fileName = GetExecutionAppSettings();

        private AppSettingsManager()
        {
            try
            {
                var assembly = IntrospectionExtensions.GetTypeInfo(typeof(AppSettingsManager)).Assembly;

                var stream = assembly.GetManifestResourceStream($"{NAMESPACE}.{fileName}");
                using (var reader = new StreamReader(stream))
                {
                    var json = reader.ReadToEnd();
                    secrets = JObject.Parse(json);
                }
            }
            catch
            {
                Debug.WriteLine("Unable to load secrets file");
            }
        }

        private static string GetExecutionAppSettings()
        {
            switch (App.PerfilDeExecucao)
            {
                case PerfilDeExecucao.Renato:
                    return "appSettings.renato.json";
            }
            return string.Empty;
        }

        public static AppSettingsManager Settings
        {
            get
            {
                if (instance == null)
                {
                    instance = new AppSettingsManager();
                }

                return instance;
            }
        }

        public string this[string name]
        {
            get
            {
                try
                {
                    var path = name.Split(':');

                    JToken node = secrets[path[0]];
                    for (int index = 1; index < path.Length; index++)
                    {
                        node = node[path[index]];
                    }

                    return node.ToString();
                }
                catch
                {
                    Debug.WriteLine($"Unable to retrieve secret '{name}'");
                    return string.Empty;
                }
            }
        }
    }
}
