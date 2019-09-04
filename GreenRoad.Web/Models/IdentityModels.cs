using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace GreenRoad.Web.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public System.Data.Entity.DbSet<GreenRoad.Domain.Perfil.PerfilDataModel> PerfilDataModels { get; set; }

        public System.Data.Entity.DbSet<GreenRoad.Web.Models.Horta.HortaBindingModel> HortaBindingModels { get; set; }

        public System.Data.Entity.DbSet<GreenRoad.Web.Models.Horta.HortaListModel> HortaListModels { get; set; }

        public System.Data.Entity.DbSet<GreenRoad.Web.Models.Horta.HortaDetalheModel> HortaDetalheModels { get; set; }

        public System.Data.Entity.DbSet<GreenRoad.Domain.Galeria.GaleriaDataModel> GaleriaDataModels { get; set; }

        public System.Data.Entity.DbSet<GreenRoad.Web.Models.Galeria.GaleriaDetalheModel> GaleriaDetalheModels { get; set; }

        public System.Data.Entity.DbSet<GreenRoad.Web.Models.Galeria.GaleriaCreateModel> GaleriaCreateModels { get; set; }

        public System.Data.Entity.DbSet<GreenRoad.Domain.Post.PostDataModel> PostDataModels { get; set; }

        public System.Data.Entity.DbSet<GreenRoad.Web.Models.Post.PostDetalheModel> PostDetalheModels { get; set; }

        public System.Data.Entity.DbSet<GreenRoad.Web.Models.Post.PostBindingModel> PostBindingModels { get; set; }

        public System.Data.Entity.DbSet<GreenRoad.Domain.Comentario.ComentarioDataModel> ComentarioDataModels { get; set; }

        public System.Data.Entity.DbSet<GreenRoad.Web.Models.Comentario.ComentarioDetalheModel> ComentarioDetalheModels { get; set; }

        public System.Data.Entity.DbSet<GreenRoad.Web.Models.Comentario.ComentarioBindingModel> ComentarioBindingModels { get; set; }

        public System.Data.Entity.DbSet<GreenRoad.Domain.Produto.ProdutoDataModel> ProdutoDataModels { get; set; }

        public System.Data.Entity.DbSet<GreenRoad.Web.Models.Produto.ProdutoDetalheModel> ProdutoDetalheModels { get; set; }

        public System.Data.Entity.DbSet<GreenRoad.Web.Models.Produto.ProdutoBindingModel> ProdutoBindingModels { get; set; }
    }
}