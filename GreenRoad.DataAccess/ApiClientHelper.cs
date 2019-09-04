using GreenRoad.Domain.Comentario;
using GreenRoad.Domain.Galeria;
using GreenRoad.Domain.Horta;
using GreenRoad.Domain.Perfil;
using GreenRoad.Domain.Post;
using GreenRoad.Domain.Produto;
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
                BaseAddress = new Uri("http://greenroadapi20180407014749.azurewebsites.net/")
            };

            _client.DefaultRequestHeaders.Accept.Clear();

            var mediaType = new MediaTypeWithQualityHeaderValue("application/json");
            _client.DefaultRequestHeaders.Accept.Add(mediaType);
        }
        // perfilHelper
        #region 
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
        #endregion

        // HortaHelper
        #region
        public async Task<HttpResponseMessage> PostHortaAsync(HortaDataModel model)
        {
            return await _client.PostAsJsonAsync("api/Horta/Post", model);
        }

        public async Task<List<HortaDataModel>> GetHortasAsync()
        {
            List<HortaDataModel> EmpInfo = new List<HortaDataModel>();

            var response = await _client.GetAsync("api/Horta/");

            if (response.IsSuccessStatusCode)
            {
                EmpInfo = await response.Content.ReadAsAsync<List<HortaDataModel>>();
            }

            return EmpInfo;
        }

        public async Task<HortaDataModel> GetHortaAsync(int id)
        {
            var response = await _client.GetAsync($"api/Horta/{id}");

            return await response.Content.ReadAsAsync<HortaDataModel>();
        }

        public async Task<HttpResponseMessage> PutHortaAsync(int id, HortaDataModel model)
        {
            return await _client.PutAsJsonAsync($"api/Horta/{id}", model);
        }

        public async Task<HttpResponseMessage> DelHortaAsync(int id)
        {
            return await _client.DeleteAsync($"api/Horta/{id}");
        }
        #endregion

        // GaleriaHelper
        #region
        public async Task<HttpResponseMessage> PostGaleriaAsync(GaleriaDataModel model)
        {
            return await _client.PostAsJsonAsync("api/Galeria/Post", model);
        }

        public async Task<List<GaleriaDataModel>> GetGaleriasAsync()
        {
            List<GaleriaDataModel> EmpInfo = new List<GaleriaDataModel>();

            var response = await _client.GetAsync("api/Galeria/");

            if (response.IsSuccessStatusCode)
            {
                EmpInfo = await response.Content.ReadAsAsync<List<GaleriaDataModel>>();
            }

            return EmpInfo;
        }

        public async Task<GaleriaDataModel> GetGaleriaAsync(int id)
        {
            var response = await _client.GetAsync($"api/Galeria/{id}");

            return await response.Content.ReadAsAsync<GaleriaDataModel>();
        }

        public async Task<HttpResponseMessage> PutGaleriaAsync(int id, GaleriaDataModel model)
        {
            return await _client.PutAsJsonAsync($"api/Galeria/{id}", model);
        }

        public async Task<HttpResponseMessage> DelGaleriaAsync(int id)
        {
            return await _client.DeleteAsync($"api/Galeria/{id}");
        }
        #endregion

        // PostHelper
        #region
        public async Task<HttpResponseMessage> PostPostAsync(PostDataModel model)
        {
            return await _client.PostAsJsonAsync("api/Post/Post", model);
        }

        public async Task<List<PostDataModel>> GetPostsAsync()
        {
            List<PostDataModel> EmpInfo = new List<PostDataModel>();

            var response = await _client.GetAsync("api/Post/");

            if (response.IsSuccessStatusCode)
            {
                EmpInfo = await response.Content.ReadAsAsync<List<PostDataModel>>();
            }

            return EmpInfo;
        }

        public async Task<PostDataModel> GetPostAsync(int id)
        {
            var response = await _client.GetAsync($"api/Post/{id}");

            return await response.Content.ReadAsAsync<PostDataModel>();
        }

        public async Task<HttpResponseMessage> PutPostAsync(int id, PostDataModel model)
        {
            return await _client.PutAsJsonAsync($"api/Post/{id}", model);
        }

        public async Task<HttpResponseMessage> DelPostAsync(int id)
        {
            return await _client.DeleteAsync($"api/Post/{id}");
        }
        #endregion

        // ComentarioHelper
        #region
        public async Task<HttpResponseMessage> PostComentarioAsync(ComentarioDataModel model)
        {
            return await _client.PostAsJsonAsync("api/Comentario/Post", model);
        }

        public async Task<List<ComentarioDataModel>> GetComentariosAsync()
        {
            List<ComentarioDataModel> EmpInfo = new List<ComentarioDataModel>();

            var response = await _client.GetAsync("api/Comentario/");

            if (response.IsSuccessStatusCode)
            {
                EmpInfo = await response.Content.ReadAsAsync<List<ComentarioDataModel>>();
            }

            return EmpInfo;
        }

        public async Task<ComentarioDataModel> GetComentarioAsync(int id)
        {
            var response = await _client.GetAsync($"api/Comentario/{id}");

            return await response.Content.ReadAsAsync<ComentarioDataModel>();
        }

        public async Task<HttpResponseMessage> PutComentarioAsync(int id, ComentarioDataModel model)
        {
            return await _client.PutAsJsonAsync($"api/Comentario/{id}", model);
        }

        public async Task<HttpResponseMessage> DelComentarioAsync(int id)
        {
            return await _client.DeleteAsync($"api/Comentario/{id}");
        }
        #endregion

        // ProdutoHelper
        #region
        public async Task<HttpResponseMessage> PostProdutoAsync(ProdutoDataModel model)
        {
            return await _client.PostAsJsonAsync("api/Produto/Post", model);
        }

        public async Task<List<ProdutoDataModel>> GetProdutosAsync()
        {
            List<ProdutoDataModel> EmpInfo = new List<ProdutoDataModel>();

            var response = await _client.GetAsync("api/Produto/");

            if (response.IsSuccessStatusCode)
            {
                EmpInfo = await response.Content.ReadAsAsync<List<ProdutoDataModel>>();
            }

            return EmpInfo;
        }

        public async Task<ProdutoDataModel> GetProdutoAsync(int id)
        {
            var response = await _client.GetAsync($"api/Produto/{id}");

            return await response.Content.ReadAsAsync<ProdutoDataModel>();
        }

        public async Task<HttpResponseMessage> PutProdutoAsync(int id, ProdutoDataModel model)
        {
            return await _client.PutAsJsonAsync($"api/Produto/{id}", model);
        }

        public async Task<HttpResponseMessage> DelProdutoAsync(int id)
        {
            return await _client.DeleteAsync($"api/Produto/{id}");
        }
        #endregion
    }
}
