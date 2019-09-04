using GreenRoad.DataAccess;
using GreenRoad.Domain.Galeria;
using GreenRoad.Web.Models.Galeria;
using Microsoft.Azure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace GreenRoad.Web.Controllers
{
    [Authorize]
    public class GaleriaController : Controller
    {
        ApiClientHelper _clientHelper = new ApiClientHelper();

        BlobClienteHelper _blobHelper = new BlobClienteHelper();

        private string connectionString = CloudConfigurationManager.GetSetting("StorageConnectionString");

        // GET: Galeria
        public async Task<ActionResult> Index()
        {
            var model = await _clientHelper.GetGaleriasAsync();

            foreach (var item in model)
            {
                var perfil = await _clientHelper.GetPerfilAsync(item.PerfilId);

                item.PerfilNome = perfil.Nome;
            }

            return View(model);
        }

        // GET: Galeria/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var galeria = await _clientHelper.GetGaleriaAsync(id);

            var model = new GaleriaDetalheModel()
            {
                Id = id,                
                PerfilId = galeria.PerfilId,
                FotoUrl = galeria.FotoUrl
            };

            var perfil = await _clientHelper.GetPerfilAsync(model.PerfilId);

            model.PerfilNome = perfil.Nome;

            return View(model);
        }

        // GET: Galeria/Create
        public async Task<ActionResult> Create()
        {
            var perfis = await _clientHelper.GetPerfilsAsync();

            var model = new GaleriaCreateModel();

            foreach (var item in perfis)
            {
                var SelectItem = new SelectListItem()
                {
                    Value = item.Id.ToString(),
                    Text = item.Nome
                };
                model.Perfis.Add(SelectItem);
            }

            return View(model);
        }

        // POST: Galeria/Create
        [HttpPost]
        public async Task<ActionResult> Create(GaleriaBindingModel galeria)
        {
            var model = new GaleriaDataModel()
            {
                PerfilId = galeria.PerfilId
            };

            try
            {
                HttpFileCollectionBase files = Request.Files;

                int fileCount = files.Count;

                await _blobHelper.SetupCloudBlob(connectionString);

                var blob = _blobHelper._blobCointainer.GetBlockBlobReference(_blobHelper.GetRandomBlobName(files[0].FileName));

                await blob.UploadFromStreamAsync(files[0].InputStream);

                model.FotoUrl = blob.StorageUri.PrimaryUri.ToString();

                await _clientHelper.PostGaleriaAsync(model);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Galeria/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var galeria = await _clientHelper.GetGaleriaAsync(id);

            var perfis = await _clientHelper.GetPerfilsAsync();

            var perfil = await _clientHelper.GetPerfilAsync(galeria.PerfilId);

            var model = new GaleriaCreateModel()
            {                
                FotoUrl = galeria.FotoUrl,
                Id = id,
                PerfilId = galeria.PerfilId,
                PerfilNome = perfil.Nome
            };

            foreach (var item in perfis)
            {
                var SelectItem = new SelectListItem()
                {
                    Value = item.Id.ToString(),
                    Text = item.Nome
                };
                model.Perfis.Add(SelectItem);
            }
            return View(model);
        }

        // POST: Galeria/Edit/5
        [HttpPost]
        public async Task<ActionResult> Edit(int id, GaleriaBindingModel galeria)
        {
            var model = new GaleriaDataModel()
            {
                PerfilId = galeria.PerfilId
            };

            try
            {
                HttpFileCollectionBase files = Request.Files;

                int fileCount = files.Count;

                await _blobHelper.SetupCloudBlob(connectionString);

                var blob = _blobHelper._blobCointainer.GetBlockBlobReference(_blobHelper.GetRandomBlobName(files[0].FileName));

                await blob.UploadFromStreamAsync(files[0].InputStream);

                model.FotoUrl = blob.StorageUri.PrimaryUri.ToString();

                await _clientHelper.PutGaleriaAsync(id, model);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Galeria/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            var galeria = await _clientHelper.GetGaleriaAsync(id);

            var model = new GaleriaDetalheModel()
            {
                Id = id,
                PerfilId = galeria.PerfilId,
                FotoUrl = galeria.FotoUrl
            };

            var perfil = await _clientHelper.GetPerfilAsync(model.PerfilId);

            model.PerfilNome = perfil.Nome;

            return View(model);
        }

        // POST: Galeria/Delete/5
        [HttpPost]
        public async Task<ActionResult> Delete(int id, FormCollection collection)
        {
            try
            {
                await _clientHelper.DelGaleriaAsync(id);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
