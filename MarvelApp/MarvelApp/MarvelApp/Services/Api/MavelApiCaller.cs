using Flurl;
using Flurl.Http;
using MarvelApp.ApiContracts;
using MarvelApp.Helpers;
using MarvelApp.Model;
using MarvelApp.Setup;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;

namespace MarvelApp.Business.Api
{
    public class MavelApiCaller : IMavelApiCaller
    {
        private readonly IApiManager apiManager;

        public MavelApiCaller(IApiManager apiManager)
        {
            this.apiManager = apiManager;
        }

        public async Task<ApiDataContainer<Character>> GetPersonagens(PageData pageData, string filter = null)
        {
            try
            {
                var url = Startup.BaseAddress()
                .AppendPathSegments("v1", "public", "characters")
                .AddMarvelAuthenticationParameters(Startup.ApiPublicKey(), Startup.ApiPrivateKey())
                .AddMarvelPageParameters(pageData);

                if (!string.IsNullOrWhiteSpace(filter))
                {
                    url.SetQueryParam("nameStartsWith", filter);
                }
                
                var requestResponse = await apiManager.MakeRequest(url.GetAsync());
                var response = await requestResponse.Content.ReadAsStringAsync();
                if (requestResponse.IsSuccessStatusCode)
                {
                    var results = JsonHandler.FromJson<ApiDataWrapper<ApiDataContainer<Character>>>(response);
                    return results.Data;
                }
                throw new Exception(response);
            }
            catch (Exception ex)
            {
                throw new Exception("Não foi possível consultar o personagem.", ex);
            }
        }

        private async Task<ApiDataContainer<Character>> MakeHttpCall(string url)
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = new HttpResponseMessage();
            try
            {
                response = await client.GetAsync(url);

                string responseText = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    return JsonConvert.DeserializeObject<ApiDataContainer<Character>>(responseText);
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
    }
}
