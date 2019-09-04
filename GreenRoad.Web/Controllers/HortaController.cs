using GreenRoad.DataAccess;
using GreenRoad.Domain.Horta;
using GreenRoad.Web.Models.Horta;
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
    public class HortaController : Controller
    {
        ApiClientHelper _clientHelper = new ApiClientHelper();

        BlobClienteHelper _blobHelper = new BlobClienteHelper();

        private string connectionString = CloudConfigurationManager.GetSetting("StorageConnectionString");

        // GET: Horta
        public async Task<ActionResult> Index()
        {
            var model = await _clientHelper.GetHortasAsync();

            foreach (var item in model)
            {
                var perfil = await _clientHelper.GetPerfilAsync(item.PerfilId);

                item.PerfilNome = perfil.Nome;
            }

            return View(model);
        }

        // GET: Horta/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var horta = await _clientHelper.GetHortaAsync(id);

            var model = new HortaDetalheModel()
            {
                Id = id,
                Titulo = horta.Titulo,
                Descricao = horta.Descricao,
                PerfilId = horta.PerfilId,
                Foto = horta.Foto
            };

            var perfil = await _clientHelper.GetPerfilAsync(model.PerfilId);

            model.PerfilNome = perfil.Nome;

            return View(model);
        }

        // GET: Horta/Create
        public async Task<ActionResult> Create()
        {
            var perfis = await _clientHelper.GetPerfilsAsync();

            var model = new HortaBindingModel();

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

        // POST: Horta/Create
        [HttpPost]
        public async Task<ActionResult> Create(HortaBindingModel horta)
        {
            var model = new HortaDataModel()
            {
                Titulo = horta.Titulo,
                Descricao = horta.Descricao,
                PerfilId = horta.PerfilId
            };

            try
            {
                HttpFileCollectionBase files = Request.Files;

                int fileCount = files.Count;

                await _blobHelper.SetupCloudBlob(connectionString);

                var blob = _blobHelper._blobCointainer.GetBlockBlobReference(_blobHelper.GetRandomBlobName(files[0].FileName));

                await blob.UploadFromStreamAsync(files[0].InputStream);

                model.Foto = blob.StorageUri.PrimaryUri.ToString();

                await _clientHelper.PostHortaAsync(model);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Horta/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var horta = await _clientHelper.GetHortaAsync(id);

            var perfis = await _clientHelper.GetPerfilsAsync();

            var perfil = await _clientHelper.GetPerfilAsync(horta.PerfilId);

            var model = new HortaBindingModel()
            {
                Titulo = horta.Titulo,
                Descricao = horta.Descricao,
                Foto = horta.Foto,
                Id = id,
                PerfilId = horta.PerfilId,
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

        // POST: Horta/Edit/5
        [HttpPost]
        public async Task<ActionResult> Edit(int id, HortaBindingModel horta)
        {
            var model = new HortaDataModel()
            {
                Titulo = horta.Titulo,
                Descricao = horta.Descricao,
                PerfilId = horta.PerfilId
            };

            try
            {
                HttpFileCollectionBase files = Request.Files;

                int fileCount = files.Count;

                await _blobHelper.SetupCloudBlob(connectionString);

                var blob = _blobHelper._blobCointainer.GetBlockBlobReference(_blobHelper.GetRandomBlobName(files[0].FileName));

                await blob.UploadFromStreamAsync(files[0].InputStream);

                model.Foto = blob.StorageUri.PrimaryUri.ToString();

                await _clientHelper.PutHortaAsync(id, model);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Horta/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            var horta = await _clientHelper.GetHortaAsync(id);

            var model = new HortaDetalheModel()
            {
                Titulo = horta.Titulo,
                Descricao = horta.Descricao,
                Foto = horta.Foto,
                Id = id,
                PerfilId = horta.PerfilId
            };

            var perfil = await _clientHelper.GetPerfilAsync(model.PerfilId);

            model.PerfilNome = perfil.Nome;

            return View(model);
        }

        // POST: Horta/Delete/5
        [HttpPost]
        public async Task<ActionResult> Delete(int id, FormCollection collection)
        {
            try
            {
                await _clientHelper.DelHortaAsync(id);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
