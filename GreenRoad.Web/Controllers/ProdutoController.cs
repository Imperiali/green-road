using GreenRoad.DataAccess;
using GreenRoad.Domain.Produto;
using GreenRoad.Web.Models.Produto;
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
    public class ProdutoController : Controller
    {
        ApiClientHelper _clientHelper = new ApiClientHelper();

        BlobClienteHelper _blobHelper = new BlobClienteHelper();

        private string connectionString = CloudConfigurationManager.GetSetting("StorageConnectionString");

        // GET: Produto
        public async Task<ActionResult> Index()
        {
            var model = await _clientHelper.GetProdutosAsync();

            foreach (var item in model)
            {
                var horta = await _clientHelper.GetHortaAsync(item.HortaId);

                item.HortaTitulo = horta.Titulo;
            }

            return View(model);
        }

        // GET: Produto/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var produto = await _clientHelper.GetProdutoAsync(id);

            var horta = await _clientHelper.GetHortaAsync(produto.HortaId);

            var model = new ProdutoDetalheModel()
            {
                Id = id,
                HortaTitulo = horta.Titulo,
                Nome = produto.Nome,
                HortaId = produto.HortaId,
                Preco = produto.Preco,
                Quantidade = produto.Quantidade,
                Foto = produto.Foto
            };            

            return View(model);
        }

        // GET: Produto/Create
        public async Task<ActionResult> Create()
        {
            var hortas = await _clientHelper.GetHortasAsync();

            var model = new ProdutoBindingModel();

            foreach (var item in hortas)
            {
                var SelectItem = new SelectListItem()
                {
                    Value = item.Id.ToString(),
                    Text = item.Titulo
                };
                model.Hortas.Add(SelectItem);
            }

            return View(model);
        }

        // POST: Produto/Create
        [HttpPost]
        public async Task<ActionResult> Create(ProdutoBindingModel produto)
        {
            var model = new ProdutoDataModel()
            {
                Nome = produto.Nome,
                Quantidade = produto.Quantidade,
                Preco = produto.Preco,
                HortaId = produto.HortaId
            };

            try
            {
                HttpFileCollectionBase files = Request.Files;

                int fileCount = files.Count;

                await _blobHelper.SetupCloudBlob(connectionString);

                var blob = _blobHelper._blobCointainer.GetBlockBlobReference(_blobHelper.GetRandomBlobName(files[0].FileName));

                await blob.UploadFromStreamAsync(files[0].InputStream);

                model.Foto = blob.StorageUri.PrimaryUri.ToString();

                await _clientHelper.PostProdutoAsync(model);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Produto/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var produto = await _clientHelper.GetProdutoAsync(id);
            
            var horta = await _clientHelper.GetHortaAsync(produto.HortaId);

            var hortas = await _clientHelper.GetHortasAsync();

            var model = new ProdutoBindingModel()
            {
                Id = id,
                HortaTitulo = horta.Titulo,
                Nome = produto.Nome,
                HortaId = produto.HortaId,
                Preco = produto.Preco,
                Quantidade = produto.Quantidade,
                Foto = produto.Foto
            };

            foreach (var item in hortas)
            {
                var SelectItem = new SelectListItem()
                {
                    Value = item.Id.ToString(),
                    Text = item.Titulo
                };
                model.Hortas.Add(SelectItem);
            }

            return View(model);
        }

        // POST: Produto/Edit/5
        [HttpPost]
        public async Task<ActionResult> Edit(int id, ProdutoBindingModel produto)
        {
            var model = new ProdutoDataModel()
            {
                Nome = produto.Nome,
                Quantidade = produto.Quantidade,
                Preco = produto.Preco,
                HortaId = produto.HortaId
            };

            try
            {
                HttpFileCollectionBase files = Request.Files;

                int fileCount = files.Count;

                await _blobHelper.SetupCloudBlob(connectionString);

                var blob = _blobHelper._blobCointainer.GetBlockBlobReference(_blobHelper.GetRandomBlobName(files[0].FileName));

                await blob.UploadFromStreamAsync(files[0].InputStream);

                model.Foto = blob.StorageUri.PrimaryUri.ToString();

                await _clientHelper.PutProdutoAsync(id, model);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Produto/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            var produto = await _clientHelper.GetProdutoAsync(id);

            var horta = await _clientHelper.GetHortaAsync(produto.HortaId);

            var model = new ProdutoDetalheModel()
            {
                Id = id,
                HortaTitulo = horta.Titulo,
                Nome = produto.Nome,
                HortaId = produto.HortaId,
                Preco = produto.Preco,
                Quantidade = produto.Quantidade,
                Foto = produto.Foto
            };

            return View(model);
        }

        // POST: Produto/Delete/5
        [HttpPost]
        public async Task<ActionResult> Delete(int id, FormCollection collection)
        {
            try
            {
                await _clientHelper.DelProdutoAsync(id);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
