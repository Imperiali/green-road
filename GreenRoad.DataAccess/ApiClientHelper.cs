using GreenRoad.Domain.Perfil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace GreenRoad.DataAccess
{
    public class ApiClientHelper
    {
        private readonly HttpClient _client;

        public ApiClientHelper()
        {
            _client = new HttpClient
            {
                BaseAddress = new Uri("http://localhost:55920/")
            };

            _client.DefaultRequestHeaders.Accept.Clear();

            var mediaType = new MediaTypeWithQualityHeaderValue("application/json");
            _client.DefaultRequestHeaders.Accept.Add(mediaType);
        }
        public async Task<HttpResponseMessage> PostPerfilAsync(PerfilDataModel model)
        {
            return await _client.PostAsJsonAsync("api/Perfil/Post", model);
        }

        public async Task<List<PerfilDataModel>> GetPerfilsAsync()
        {
            List<PerfilDataModel> EmpInfo = new List<PerfilDataModel>();

            var response = await _client.GetAsync("api/Perfil/");

            if (response.IsSuccessStatusCode)
            {
                EmpInfo = await response.Content.ReadAsAsync<List<PerfilDataModel>>();
            }

            return EmpInfo;
        }

        public async Task<PerfilDataModel> GetPerfilAsync(int id)
        {
            var response = await _client.GetAsync($"api/Perfil/{id}");

            return await response.Content.ReadAsAsync<PerfilDataModel>();
        }

        public async Task<HttpResponseMessage> PutPerfilAsync(int id, PerfilDataModel model)
        {
            return await _client.PutAsJsonAsync($"api/Perfil/{id}", model);
        }

        public async Task<HttpResponseMessage> DelPerfilAsync(int id)
        {
            return await _client.DeleteAsync($"api/Perfil/{id}");
        }
    }
}
