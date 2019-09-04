using GreenRoad.DataAccess;
using GreenRoad.Domain.Comentario;
using GreenRoad.Web.Models.Comentario;
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
    public class ComentarioController : Controller
    {
        ApiClientHelper _clientHelper = new ApiClientHelper();        

        private string connectionString = CloudConfigurationManager.GetSetting("StorageConnectionString");

        // GET: Comentario
        public async Task<ActionResult> Index()
        {
            var model = await _clientHelper.GetComentariosAsync();

            foreach (var item in model)
            {
                var perfil = await _clientHelper.GetPerfilAsync(item.PerfilId);

                var post = await _clientHelper.GetPostAsync(item.PostId);

                item.PerfilNome = perfil.Nome;

                item.PostNome = post.Titulo;
            }

            return View(model);

        }

        // GET: Comentario/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var comentario = await _clientHelper.GetComentarioAsync(id);

            var model = new ComentarioDetalheModel()
            {
                Id = id,                                
                Texto = comentario.Texto,
                PerfilId = comentario.PerfilId,
                PostId = comentario.PostId
            };

            var perfil = await _clientHelper.GetPerfilAsync(model.PerfilId);

            var post = await _clientHelper.GetPostAsync(model.PostId);

            model.PerfilNome = perfil.Nome;

            model.PostNome = post.Titulo;

            return View(model);
        }

        // GET: Comentario/Create
        public async Task<ActionResult> Create()
        {
            var perfis = await _clientHelper.GetPerfilsAsync();

            var posts = await _clientHelper.GetPostsAsync();

            var model = new ComentarioBindingModel();

            foreach (var item in posts)
            {
                var SelectItem = new SelectListItem()
                {
                    Value = item.Id.ToString(),
                    Text = item.Titulo
                };
                model.Posts.Add(SelectItem);
            }

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

        // POST: Comentario/Create
        [HttpPost]
        public async Task<ActionResult> Create(ComentarioBindingModel comentario)
        {
            var model = new ComentarioDataModel()
            {                
                Texto = comentario.Texto,
                PostId = comentario.PostId,
                PerfilId = comentario.PerfilId
            };

            try
            {
                await _clientHelper.PostComentarioAsync(model);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Comentario/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var comentario = await _clientHelper.GetComentarioAsync(id);

            var perfis = await _clientHelper.GetPerfilsAsync();

            var perfil = await _clientHelper.GetPerfilAsync(comentario.PerfilId);

            var posts = await _clientHelper.GetPostsAsync();

            var post = await _clientHelper.GetPostAsync(comentario.PostId);

            var model = new ComentarioBindingModel()
            {
                Id = id,
                Texto = comentario.Texto,
                PostId = comentario.PostId,
                PostNome = post.Titulo,
                PerfilId = comentario.PerfilId,
                PerfilNome = perfil.Nome
            };

            foreach (var item in posts)
            {
                var SelectItem = new SelectListItem()
                {
                    Value = item.Id.ToString(),
                    Text = item.Titulo
                };
                model.Posts.Add(SelectItem);
            }

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

        // POST: Comentario/Edit/5
        [HttpPost]
        public async Task<ActionResult> Edit(int id, ComentarioBindingModel comentario)
        {
            var model = new ComentarioDataModel()
            {
                Texto = comentario.Texto,
                PostId = comentario.PostId,
                PerfilId = comentario.PerfilId
            };

            try
            {
                await _clientHelper.PutComentarioAsync(id, model);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Comentario/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            var comentario = await _clientHelper.GetComentarioAsync(id);

            var model = new ComentarioDetalheModel()
            {
                Id = id,
                Texto = comentario.Texto,
                PerfilId = comentario.PerfilId,
                PostId = comentario.PostId
            };

            var perfil = await _clientHelper.GetPerfilAsync(model.PerfilId);

            var post = await _clientHelper.GetPostAsync(model.PostId);

            model.PerfilNome = perfil.Nome;

            model.PostNome = post.Titulo;

            return View(model);
        }

        // POST: Comentario/Delete/5
        [HttpPost]
        public async Task<ActionResult> Delete(int id, FormCollection collection)
        {
            try
            {
                await _clientHelper.DelComentarioAsync(id);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
