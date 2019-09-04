using GreenRoad.DataAccess;
using GreenRoad.Domain.Post;
using GreenRoad.Web.Models.Post;
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
    public class PostController : Controller
    {
        ApiClientHelper _clientHelper = new ApiClientHelper();

        BlobClienteHelper _blobHelper = new BlobClienteHelper();

        private string connectionString = CloudConfigurationManager.GetSetting("StorageConnectionString");

        // GET: Post
        public async Task<ActionResult> Index()
        {
            var model = await _clientHelper.GetPostsAsync();

            foreach (var item in model)
            {
                var perfil = await _clientHelper.GetPerfilAsync(item.PerfilId);

                item.PerfilNome = perfil.Nome;
            }

            return View(model);            
        }

        // GET: Post/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var post = await _clientHelper.GetPostAsync(id);

            var model = new PostDetalheModel()
            {
                Id = id,
                Titulo = post.Titulo,
                Descricao = post.Descricao,
                PerfilId = post.PerfilId,
                Foto = post.Foto                
            };

            var perfil = await _clientHelper.GetPerfilAsync(model.PerfilId);

            model.PerfilNome = perfil.Nome;

            return View(model);
        }

        // GET: Post/Create
        public async Task<ActionResult> Create()
        {
            var perfis = await _clientHelper.GetPerfilsAsync();

            var model = new PostBindingModel();

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

        // POST: Post/Create
        [HttpPost]
        public async Task<ActionResult> Create(PostBindingModel post)
        {
            var model = new PostDataModel()
            {
                Titulo = post.Titulo,
                Descricao = post.Descricao,
                PerfilId = post.PerfilId                
            };

            try
            {
                HttpFileCollectionBase files = Request.Files;

                int fileCount = files.Count;

                await _blobHelper.SetupCloudBlob(connectionString);

                var blob = _blobHelper._blobCointainer.GetBlockBlobReference(_blobHelper.GetRandomBlobName(files[0].FileName));

                await blob.UploadFromStreamAsync(files[0].InputStream);

                model.Foto = blob.StorageUri.PrimaryUri.ToString();

                await _clientHelper.PostPostAsync(model);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Post/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var post = await _clientHelper.GetPostAsync(id);

            var perfis = await _clientHelper.GetPerfilsAsync();

            var perfil = await _clientHelper.GetPerfilAsync(post.PerfilId);

            var model = new PostBindingModel()
            {
                Titulo = post.Titulo,
                Descricao = post.Descricao,
                Foto = post.Foto,
                Id = id,
                PerfilId = post.PerfilId,
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

        // POST: Post/Edit/5
        [HttpPost]
        public async Task<ActionResult> Edit(int id, PostBindingModel post)
        {
            var model = new PostDataModel()
            {
                Titulo = post.Titulo,
                Descricao = post.Descricao,
                PerfilId = post.PerfilId                
            };

            try
            {
                HttpFileCollectionBase files = Request.Files;

                int fileCount = files.Count;

                await _blobHelper.SetupCloudBlob(connectionString);

                var blob = _blobHelper._blobCointainer.GetBlockBlobReference(_blobHelper.GetRandomBlobName(files[0].FileName));

                await blob.UploadFromStreamAsync(files[0].InputStream);

                model.Foto = blob.StorageUri.PrimaryUri.ToString();

                await _clientHelper.PutPostAsync(id, model);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Post/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            var post = await _clientHelper.GetPostAsync(id);

            var model = new PostDetalheModel()
            {
                Titulo = post.Titulo,
                Descricao = post.Descricao,
                Foto = post.Foto,
                Id = id,
                PerfilId = post.PerfilId
            };

            var perfil = await _clientHelper.GetPerfilAsync(model.PerfilId);

            model.PerfilNome = perfil.Nome;

            return View(model);
        }

        // POST: Post/Delete/5
        [HttpPost]
        public async Task<ActionResult> Delete(int id, FormCollection collection)
        {
            try
            {
                await _clientHelper.DelPostAsync(id);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
