using GreenRoad.DataAccess;
using GreenRoad.Domain.Perfil;
using GreenRoad.Web.Models.Perfil;
using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace GreenRoad.Web.Controllers
{
    [Authorize]
    public class PerfilController : Controller
    {
        ApiClientHelper _clientHelper = new ApiClientHelper();

        BlobClienteHelper _blobHelper = new BlobClienteHelper();

        private string connectionString = CloudConfigurationManager.GetSetting("StorageConnectionString");

        // GET: Perfil
        public async Task<ActionResult> Index()
        {
            var model = await _clientHelper.GetPerfilsAsync();

            return View(model);
        }

        // GET: Perfil/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var model = await _clientHelper.GetPerfilAsync(id);

            return View(model);
        }

        // GET: Perfil/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Perfil/Create
        [HttpPost]
        public async Task<ActionResult> Create(PerfilBindingModel perfil)
        {
            var model = new PerfilDataModel()
            {
                Email = perfil.Email,
                Nome = perfil.Nome,
                Sobrenome = perfil.Sobrenome                
            };

            try
            {
                HttpFileCollectionBase files = Request.Files;

                int fileCount = files.Count;

                await _blobHelper.SetupCloudBlob(connectionString);

                var blob = _blobHelper._blobCointainer.GetBlockBlobReference(_blobHelper.GetRandomBlobName(files[0].FileName));

                await blob.UploadFromStreamAsync(files[0].InputStream);

                model.Foto = blob.StorageUri.PrimaryUri.ToString();

                await _clientHelper.PostPerfilAsync(model);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Perfil/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var model = await _clientHelper.GetPerfilAsync(id);

            return View(model);
        }

        // POST: Perfil/Edit/5
        [HttpPost]
        public async Task<ActionResult> Edit(int id, PerfilDataModel model)
        {
            try
            {
                HttpFileCollectionBase files = Request.Files;

                int fileCount = files.Count;

                await _blobHelper.SetupCloudBlob(connectionString);

                var blob = _blobHelper._blobCointainer.GetBlockBlobReference(_blobHelper.GetRandomBlobName(files[0].FileName));

                await blob.UploadFromStreamAsync(files[0].InputStream);

                model.Foto = blob.StorageUri.PrimaryUri.ToString();

                await _clientHelper.PutPerfilAsync(id, model);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Perfil/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            var model = await _clientHelper.GetPerfilAsync(id);

            return View(model);
        }

        // POST: Perfil/Delete/5
        [HttpPost]
        public async Task<ActionResult> Delete(int id, FormCollection collection)
        {
            try
            {
                await _clientHelper.DelPerfilAsync(id);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }        
    }
}
