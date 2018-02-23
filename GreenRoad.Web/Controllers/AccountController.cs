using GreenRoad.Web.Models.Account;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using GreenRoad.Web.Helpers;

namespace GreenRoad.Web.Controllers
{
    public class AccountController : Controller
    {
        /// <summary>
        /// TO DO
        /// 
        /// Recovery Password : https://docs.microsoft.com/en-us/aspnet/identity/overview/features-api/account-confirmation-and-password-recovery-with-aspnet-identity
        /// </summary>


        private HttpClient _client;

        private TokenHelper _helper;

        public AccountController()
        {
            _client = new HttpClient();
            _helper = new TokenHelper();

            _client.BaseAddress = new Uri("http://localhost:55920/");
            _client.DefaultRequestHeaders.Accept.Clear();
            
            var mediaType = new MediaTypeWithQualityHeaderValue("application/json");
            _client.DefaultRequestHeaders.Accept.Add(mediaType);
        }
        // GET: Account
        public ActionResult Index()
        {
            return View();
        }

        // GET: Account/Register
        public ActionResult Register()
        {
            return View();
        }

        // POST: Account/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(LoginViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var response = await _client.PostAsJsonAsync("api/Account/Register", viewModel);

                if (response.IsSuccessStatusCode)
                {

                }
                else
                {

                }
            }
            return View(viewModel);
        }

        // GET: Account/Register
        public ActionResult Login()
        {
            return View();
        }

        // POST: Account/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var data = new Dictionary<string, string>
                {
                    { "grant_type", "password" },
                    { "username", viewModel.Email },
                    { "password", viewModel.Password },
                };

                using (var requestContent = new FormUrlEncodedContent(data))
                {
                    var response = await _client.PostAsync("/Token", requestContent);

                    if (response.IsSuccessStatusCode)
                    {
                        var responseContent = await response.Content.ReadAsStringAsync();
                        var tokenData = JObject.Parse(responseContent);
                        _helper.AccessToken = tokenData["access_token"];
                    }
                    else
                    {
                        ModelState.AddModelError("", "");
                    }
                }

            }
            return View(viewModel);
           
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && _client != null)
            {
                _client.Dispose();
                _client = null;
            }
            base.Dispose(disposing);
        }

    }
}