using GreenRoad.DataAccess;
using GreenRoad.Domain.Perfil;
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
    public class PerfilController : Controller
    {
        ApiClientHelper _clientHelper = new ApiClientHelper();

        private CloudBlobClient _blobClient;
        private CloudBlobContainer _blobCointainer;

        private const string _blobContainerName = "igor-imperiali";

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
        public async Task<ActionResult> Create(PerfilDataModel model)
        {
            try
            {
                HttpFileCollectionBase files = Request.Files;

                int fileCount = files.Count;

                await SetupCloudBlob();

                var blob = _blobCointainer.GetBlockBlobReference(GetRandomBlobName(files[0].FileName));

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
        private async Task SetupCloudBlob()
        {
            var connectionString = CloudConfigurationManager.GetSetting("StorageConnectionString");
            var storageAccount = CloudStorageAccount.Parse(connectionString);

            _blobClient = storageAccount.CreateCloudBlobClient();
            _blobCointainer = _blobClient.GetContainerReference(_blobContainerName);

            await _blobCointainer.CreateIfNotExistsAsync();

            var permission = new BlobContainerPermissions
            {
                PublicAccess = BlobContainerPublicAccessType.Blob
            };

            await _blobCointainer.SetPermissionsAsync(permission);
        }

        private string GetRandomBlobName(string filename)
        {
            string ext = Path.GetExtension(filename);
            return string.Format("{0:10}_{1}{2}", DateTime.Now.Ticks, Guid.NewGuid(), ext);
        }
    }
}
