using MarvelApp.Helper;
using MarvelApp.Models;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.Globalization;
using System.Net.Http;
using System.Threading.Tasks;

namespace MarvelApp.Service
{
    public class MarvelClient
    {
        private String BaseUrl = @"https://gateway.marvel.com/v1/public/";
        private Configuracoes Configuracao;

        #region Singleton

        private MarvelClient()
        {
            Configuracao = Configuracoes.Instance;
        }

        private static readonly Lazy<MarvelClient> lazy = new Lazy<MarvelClient>(() => new MarvelClient());

        public static MarvelClient Instance
        {
            get
            {
                return lazy.Value;
            }
        }

        #endregion

        #region Get Characteres

        public String GetUrlCharacteres(String filter, int limit, int offset)
        {
            string MarvelCredentials = $"?ts={Configuracao.ApiTimeStamp}&apikey={Configuracao.ApiPublicKey}&hash={Configuracao.ApiHash}";
            filter = GetFilter(filter, limit, offset);

            return $"{BaseUrl}characters{MarvelCredentials}{filter}";
        }


        private string GetFilter(String filter, int limit, int offset)
        {
            var querystring = string.Empty;

            if (!string.IsNullOrWhiteSpace(filter))
                querystring += $"&nameStartsWith={System.Net.WebUtility.UrlEncode(filter)}";
            if (limit > 0)
                querystring += $"&limit={limit.ToString()}";
            if (offset > 0)
                querystring += $"&offset={offset.ToString()}";

            return querystring;
        }

        #endregion


        public async Task<MarvelApiData<Personagens>> GetPersonagens(String filter = null, int limit = 30, int offset = 0)
        {
            var querystring = GetUrlCharacteres(filter, limit, offset);

            var result = await this.MakeHttpCall<MarvelApiResult<Personagens>>(querystring);
            return result.Data;
        }

        #region MakeHttpCall

        private async Task<TOutput> MakeHttpCall<TOutput>(string url)
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = new HttpResponseMessage();
            try
            {
                response = await client.GetAsync(url);

                string responseText = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    return JsonConvert.DeserializeObject<TOutput>(responseText);
                }
                else
                {
                    throw new Exception(string.Format("Response Statuscode for {0}: {1}", url, response.StatusCode));
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                throw ex;
            }
        }

        #endregion

    }
}
