using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarvelApp.Models
{
    public class MarvelApiResult<TResult>
    {
        [JsonProperty("code")]
        public string Code { get; set; }
        [JsonProperty("status")]
        public string Status { get; set; }
        [JsonProperty("copyright")]
        public string Copyright { get; set; }
        [JsonProperty("attributionText")]
        public string AttributionText { get; set; }
        [JsonProperty("attributionHTML")]
        public string AttributionHTML { get; set; }
        [JsonProperty("etag")]
        public string Etag { get; set; }
        [JsonProperty("data")]
        public MarvelApiData<TResult> Data { get; set; }
    }
}
