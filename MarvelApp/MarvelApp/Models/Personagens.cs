using MarvelApp.Helper;
using Newtonsoft.Json;

namespace MarvelApp.Models
{
    public class Personagens : ObservableObject
    {
        private int id;
        [JsonProperty("id")]
        public int Id
        {
            get
            {
                return id;
            }
            private set
            {
                id = value;
                OnPropertyChanged();
            }
        }

        private string name;
        [JsonProperty("name")]
        public string Name 
        {
            get
            {
                return name;
            }
            private set
            {
                name = value;
                OnPropertyChanged();
            }
        }

        private string description;
        [JsonProperty("description")]
        public string Description 
        {
            get
            {
                return description;
            }
            private set
            {
                description = value;
                OnPropertyChanged();
            }
        }

        private ImageUrl thumbnail;
        [JsonProperty("thumbnail")]
        public ImageUrl Thumbnail
        {
            get
            {
                return thumbnail;
            }
            private set
            {
                thumbnail = value;
                OnPropertyChanged();
            }
        }

        public string Image
        {
            get
            {
                return Thumbnail == null ? "" : Thumbnail.ToString();
            }
        }

    }
}
