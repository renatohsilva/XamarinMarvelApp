using MarvelApp.Helper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarvelApp.Models
{
    public class ImageUrl : ObservableObject
    {
        private string extension;
        [JsonProperty("extension")]
        public string Extension
        {
            get
            {
                return extension;
            }
            private set
            {
                extension = value;
                OnPropertyChanged();
            }
        }

        private string path;
        [JsonProperty("path")]
        public string Path
        {
            get
            {
                return path;
            }
            private set
            {
                path = value;
                OnPropertyChanged();
            }
        }

        public override string ToString()
        {
            return $"{Path}.{Extension}";
        }
    }
}
