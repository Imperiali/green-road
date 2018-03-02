using GreenRoad.Web.Attributes;
using GreenRoad.Web.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace GreenRoad.Web.Controllers
{
    public class HomeController : Controller
    {

        private HttpClient _client;
        private TokenHelper _helper;

        public HomeController()
        {
            _client = new HttpClient();
            _helper = new TokenHelper();

            _client.BaseAddress = new Uri("http://localhost:55920/");
            _client.DefaultRequestHeaders.Accept.Clear();

            var mediaType = new MediaTypeWithQualityHeaderValue("application/json");
            _client.DefaultRequestHeaders.Accept.Add(mediaType);
        }
        // GET: Home
        [Authentication]
        public ActionResult Index()
        {
            return View();
        }
        [Authentication]
        public async Task<ActionResult> Value()
        {
            if (_helper.AccessToken != null)
            {
                _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", $"{_helper.AccessToken}");
                var response = await _client.GetAsync("api/Values");

                //ViewBag.Message = response.Content.ReadAsStringAsync().Result;

                return View();
            }
            else
            {
                return Redirect("Account/Login");
            }
            

        }
    }
}